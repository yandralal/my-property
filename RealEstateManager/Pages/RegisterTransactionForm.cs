using Microsoft.Data.SqlClient;

namespace RealEstateManager.Pages
{
    public partial class RegisterTransactionForm : Form
    {
        private readonly int? _plotId;
        private readonly decimal? _saleAmount;
        private readonly string? _plotNumber;
        private decimal _amountPaidTillDate = 0;
        private readonly string? _transactionId;

        public RegisterTransactionForm(int? plotId = null, decimal? saleAmount = null, string? plotNumber = "")
        {
            InitializeComponent();
            _plotId = plotId;
            _saleAmount = saleAmount;
            _plotNumber = plotNumber;
            if (!string.IsNullOrEmpty(_plotNumber))
            {
                textBoxPlotId.Text = _plotNumber.ToString();
                textBoxPlotId.ReadOnly = true;
            }
            if (_saleAmount.HasValue)
            {
                textBoxSaleAmount.Text = _saleAmount.Value.ToString();
                textBoxSaleAmount.ReadOnly = true;
            }

            // Fetch and display amount paid till date
            if (_plotId.HasValue)
            {
                _amountPaidTillDate = GetAmountPaidTillDate(_plotId.Value);
                textBoxAmountPaidTillDate.Text = _amountPaidTillDate.ToString("N2");
            }

            // Update balance label on load
            UpdateBalanceOnLoad();

            // Attach event handler for dynamic update
            textBoxAmount.TextChanged += UpdateBalanceAmount;

            // Populate Transaction Type dropdown
            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(new[] { "Credit", "Debit" });
            comboBoxTransactionType.SelectedIndex = 0; // Default to first item
        }

        public RegisterTransactionForm(string transactionId, bool readOnly = false)
        {
            InitializeComponent();

            _transactionId = transactionId;

            // Populate Transaction Type dropdown
            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(new[] { "Credit", "Debit" });

            // Load transaction details from DB
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"SELECT PlotId, TransactionDate, Amount, PaymentMethod, ReferenceNumber, Notes, TransactionType
                            FROM PlotTransaction WHERE TransactionId = @TransactionId AND IsDeleted = 0";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _plotId = reader["PlotId"] != DBNull.Value ? Convert.ToInt32(reader["PlotId"]) : null;
                        dateTimePickerTransactionDate.Value = reader["TransactionDate"] != DBNull.Value
                            ? Convert.ToDateTime(reader["TransactionDate"])
                            : DateTime.Now;
                        textBoxAmount.Text = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]).ToString("N2") : "";
                        comboBoxPaymentMethod.Text = reader["PaymentMethod"]?.ToString() ?? "";
                        textBoxReferenceNumber.Text = reader["ReferenceNumber"]?.ToString() ?? "";
                        textBoxNotes.Text = reader["Notes"]?.ToString() ?? "";
                        comboBoxTransactionType.Text = reader["TransactionType"]?.ToString() ?? "Credit";

                        // Optionally, load sale amount and plot number for balance display
                        if (_plotId.HasValue)
                        {
                            _amountPaidTillDate = GetAmountPaidTillDate(_plotId.Value);
                            textBoxAmountPaidTillDate.Text = _amountPaidTillDate.ToString("N2");

                            // You may want to load sale amount and plot number for this plot
                            string plotQuery = "SELECT PlotNumber FROM Plot WHERE Id = @PlotId";
                            using (var plotConn = new SqlConnection(connectionString))
                            using (var plotCmd = new SqlCommand(plotQuery, plotConn))
                            {
                                plotCmd.Parameters.AddWithValue("@PlotId", _plotId.Value);
                                plotConn.Open();
                                var plotNumberObj = plotCmd.ExecuteScalar();
                                if (plotNumberObj != null)
                                {
                                    textBoxPlotId.Text = plotNumberObj.ToString();
                                    textBoxPlotId.ReadOnly = true;
                                }
                            }
                        }
                    }
                }
            }

            // Update balance label on load
            UpdateBalanceOnLoad();

            // Attach event handler for dynamic update
            textBoxAmount.TextChanged += UpdateBalanceAmount;

            // Set fields enabled/disabled based on readOnly
            SetFieldsReadOnly(readOnly);

            // Optionally, hide the Save button in view mode
            buttonSave.Visible = !readOnly;
        }

        private static decimal GetAmountPaidTillDate(int plotId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "SELECT ISNULL(SUM(Amount), 0) FROM PlotTransaction WHERE PlotId = @PlotId AND IsDeleted = 0";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PlotId", plotId);
                conn.Open();
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }

        private void UpdateBalanceOnLoad()
        {
            decimal saleAmount = 0;
            if (decimal.TryParse(textBoxSaleAmount.Text, out saleAmount))
            {
                labelBalanceValue.Text = (saleAmount - _amountPaidTillDate).ToString("N2");
            }
            else
            {
                labelBalanceValue.Text = "0.00";
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(textBoxAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string paymentMethod = comboBoxPaymentMethod.Text;
            string referenceNumber = textBoxReferenceNumber.Text;
            string notes = textBoxNotes.Text;
            string transactionType = comboBoxTransactionType.Text;
            string userName = Environment.UserName;

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";

            if (!string.IsNullOrEmpty(_transactionId))
            {
                // UPDATE existing transaction
                string update = @"
                    UPDATE PlotTransaction SET
                        TransactionDate = @TransactionDate,
                        Amount = @Amount,
                        PaymentMethod = @PaymentMethod,
                        ReferenceNumber = @ReferenceNumber,
                        Notes = @Notes,
                        TransactionType = @TransactionType,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = @ModifiedDate
                    WHERE TransactionId = @TransactionId AND IsDeleted = 0";

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(update, conn))
                {
                    cmd.Parameters.AddWithValue("@TransactionDate", dateTimePickerTransactionDate.Value);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.Parameters.AddWithValue("@TransactionType", transactionType);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@TransactionId", _transactionId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Transaction updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // INSERT new transaction
                string insert = @"
                    INSERT INTO PlotTransaction
                    (PlotId, TransactionDate, Amount, PaymentMethod, ReferenceNumber, Notes, TransactionType, CreatedBy, CreatedDate, IsDeleted)
                    VALUES
                    (@PlotId, @TransactionDate, @Amount, @PaymentMethod, @ReferenceNumber, @Notes, @TransactionType, @CreatedBy, @CreatedDate, 0)";

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(insert, conn))
                {
                    cmd.Parameters.AddWithValue("@PlotId", _plotId);
                    cmd.Parameters.AddWithValue("@TransactionDate", dateTimePickerTransactionDate.Value);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.Parameters.AddWithValue("@TransactionType", transactionType);
                    cmd.Parameters.AddWithValue("@CreatedBy", userName);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Transaction registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Refresh grid in LandingForm if open
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is LandingForm landingForm)
                {
                    landingForm.LoadActiveProperties();
                    break;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UpdateBalanceAmount(object sender, EventArgs e)
        {
            decimal saleAmount = 0;
            decimal newPaid = 0;
            if (decimal.TryParse(textBoxSaleAmount.Text, out saleAmount) &&
                decimal.TryParse(textBoxAmount.Text, out newPaid))
            {
                labelBalanceValue.Text = (saleAmount - (_amountPaidTillDate + newPaid)).ToString("N2");
            }
            else if (decimal.TryParse(textBoxSaleAmount.Text, out saleAmount))
            {
                labelBalanceValue.Text = (saleAmount - _amountPaidTillDate).ToString("N2");
            }
            else
            {
                labelBalanceValue.Text = "0.00";
            }
        }

        private void SetFieldsReadOnly(bool readOnly)
        {
            dateTimePickerTransactionDate.Enabled = !readOnly;
            textBoxAmount.ReadOnly = readOnly;
            comboBoxPaymentMethod.Enabled = !readOnly;
            textBoxReferenceNumber.ReadOnly = readOnly;
            textBoxNotes.ReadOnly = readOnly;
            comboBoxTransactionType.Enabled = !readOnly;
        }
    }
}