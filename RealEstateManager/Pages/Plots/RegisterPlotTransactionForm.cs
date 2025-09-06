using System.Configuration;
using Microsoft.Data.SqlClient;

namespace RealEstateManager.Pages
{
    public partial class RegisterPlotTransactionForm : BaseForm
    {
        private readonly int? _plotId;
        private readonly decimal? _saleAmount;
        private readonly string? _plotNumber;
        private readonly decimal _amountPaidTillDate = 0;

        public RegisterPlotTransactionForm(int? plotId = null, decimal? saleAmount = null, string? plotNumber = "")
        {
            InitializeComponent();
            SetupNumericTextBoxValidation();

            // Add red stars for mandatory fields
            AddMandatoryFieldStars();

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
                textBoxSaleAmount.Text = _saleAmount.Value.ToString("N2");
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

            // Sale amount formatting on leave
            textBoxSaleAmount.Leave += (s, e) =>
            {
                if (decimal.TryParse(textBoxSaleAmount.Text, out var value))
                    textBoxSaleAmount.Text = value.ToString("N2");
                else
                    textBoxSaleAmount.Text = "0.00";
            };

            // Set default payment mode to Cash
            comboBoxPaymentMethod.SelectedItem = "Cash";
        }

        public RegisterPlotTransactionForm(string transactionId)
        {
            InitializeComponent();
            SetupNumericTextBoxValidation();

            // Add red stars for mandatory fields
            AddMandatoryFieldStars();

            // Bind transaction type combobox before setting its value
            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(new[] { "Credit", "Debit" });

            // Load transaction details from DB
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"
            SELECT 
                pt.PlotId, 
                pt.TransactionDate, 
                pt.Amount, 
                pt.PaymentMethod, 
                pt.ReferenceNumber, 
                pt.Notes, 
                pt.TransactionType,
                p.PlotNumber,
                ps.SaleAmount
            FROM PlotTransaction pt
            INNER JOIN Plot p ON pt.PlotId = p.Id
            LEFT JOIN PlotSale ps ON pt.PlotId = ps.PlotId
            WHERE pt.TransactionId = @TransactionId AND pt.IsDeleted = 0";

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

                        // Set PlotNumber and SaleAmount from joined tables
                        var plotNumberObj = reader["PlotNumber"];
                        if (plotNumberObj != null)
                        {
                            textBoxPlotId.Text = plotNumberObj.ToString();
                            textBoxPlotId.ReadOnly = true;
                        }

                        var saleAmountObj = reader["SaleAmount"];
                        if (saleAmountObj != DBNull.Value)
                        {
                            textBoxSaleAmount.Text = Convert.ToDecimal(saleAmountObj).ToString("N2");
                            textBoxSaleAmount.ReadOnly = true;
                        }

                        // Fetch and display amount paid till date
                        if (_plotId.HasValue)
                        {
                            _amountPaidTillDate = GetAmountPaidTillDate(_plotId.Value);
                            textBoxAmountPaidTillDate.Text = _amountPaidTillDate.ToString("N2");
                        }
                    }
                }
            }

            // Update balance label on load
            UpdateBalanceOnLoad();

            // Disable all input controls for view-only mode
            SetFieldsReadOnly(true);
            buttonSave.Visible = false; // Hide Save button
        }

        private static decimal GetAmountPaidTillDate(int plotId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
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
            // Validate Transaction Date (should not be in the future)
            if (dateTimePickerTransactionDate.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Transaction date cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerTransactionDate.Focus();
                return;
            }

            // Validate Amount
            if (!decimal.TryParse(textBoxAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid, positive amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxAmount.Focus();
                return;
            }

            // Validate Transaction Type
            if (string.IsNullOrWhiteSpace(comboBoxTransactionType.Text))
            {
                MessageBox.Show("Please select a transaction type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxTransactionType.Focus();
                return;
            }

            // Validate Payment Method
            if (string.IsNullOrWhiteSpace(comboBoxPaymentMethod.Text))
            {
                MessageBox.Show("Please select a payment method.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxPaymentMethod.Focus();
                return;
            }

            // Validate Reference Number (optional, but if provided, at least 3 chars)
            if (!string.IsNullOrWhiteSpace(textBoxReferenceNumber.Text) && textBoxReferenceNumber.Text.Trim().Length < 3)
            {
                MessageBox.Show("Reference number must be at least 3 characters if provided.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxReferenceNumber.Focus();
                return;
            }

            // Validate PlotId
            if (!_plotId.HasValue)
            {
                MessageBox.Show("Plot information is missing. Please select a plot.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Sale Amount (should be positive)
            if (!decimal.TryParse(textBoxSaleAmount.Text, out decimal saleAmount) || saleAmount <= 0)
            {
                MessageBox.Show("Sale amount must be a valid, positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxSaleAmount.Focus();
                return;
            }

            // Validate Amount Paid Till Date (should not exceed sale amount)
            if (_amountPaidTillDate + amount > saleAmount)
            {
                MessageBox.Show("Total paid amount cannot exceed sale amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAmount.Focus();
                return;
            }

            string paymentMethod = comboBoxPaymentMethod.Text;
            string referenceNumber = textBoxReferenceNumber.Text;
            string notes = textBoxNotes.Text;
            string transactionType = comboBoxTransactionType.Text;
            string userIdentifier = (!string.IsNullOrEmpty(LoggedInUserId)) ? LoggedInUserId.ToString() : Environment.UserName;
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;

            // INSERT new transaction only
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
                cmd.Parameters.AddWithValue("@CreatedBy", userIdentifier);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Transaction registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Refresh grid in LandingForm if open and select the property
            int? propertyId = null;
            if (_plotId.HasValue)
            {
                string query = "SELECT PropertyId FROM Plot WHERE Id = @PlotId";
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlotId", _plotId.Value);
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        propertyId = Convert.ToInt32(result);
                }
            }

            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is LandingForm landingForm)
                {
                    landingForm.LoadActiveProperties(propertyId);
                    break;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UpdateBalanceAmount(object? sender, EventArgs e)
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

        private void TextBoxAmount_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBoxAmount.Text, out decimal value))
            {
                textBoxAmount.Text = value.ToString("N2");
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Restricts numeric textboxes to digits, one decimal point, and control keys.
        /// </summary>
        private void SetupNumericTextBoxValidation()
        {
            KeyPressEventHandler handler = (s, e) =>
            {
                TextBox tb = s as TextBox;
                char ch = e.KeyChar;

                // Allow control keys (backspace, delete, etc.)
                if (char.IsControl(ch))
                    return;

                // Allow only one decimal separator, and not as the first character
                if (ch == '.' && (tb.Text.Contains('.') || tb.SelectionStart == 0))
                {
                    e.Handled = true;
                    return;
                }

                // Allow digits only
                if (!char.IsDigit(ch) && ch != '.')
                {
                    e.Handled = true;
                }
            };

            textBoxAmount.KeyPress += handler;
            textBoxSaleAmount.KeyPress += handler;
            textBoxAmountPaidTillDate.KeyPress += handler;

            textBoxSaleAmount.Leave += (s, e) =>
            {
                if (decimal.TryParse(textBoxSaleAmount.Text, out var value))
                    textBoxSaleAmount.Text = value.ToString("N2");
                else
                    textBoxSaleAmount.Text = "0.00";
            };
        }

        private void AddMandatoryFieldStars()
        {
            // Plot Number
            var plotIdStar = new Label
            {
                Text = "*",
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(textBoxPlotId.Right + 5, textBoxPlotId.Top)
            };
            groupBoxTransactionEntry.Controls.Add(plotIdStar);

            // Sale Amount
            var saleAmountStar = new Label
            {
                Text = "*",
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(textBoxSaleAmount.Right + 5, textBoxSaleAmount.Top)
            };
            groupBoxTransactionEntry.Controls.Add(saleAmountStar);

            // Amount To Pay
            var amountStar = new Label
            {
                Text = "*",
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(textBoxAmount.Right + 5, textBoxAmount.Top)
            };
            groupBoxTransactionEntry.Controls.Add(amountStar);

            // Transaction Type
            var transactionTypeStar = new Label
            {
                Text = "*",
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(comboBoxTransactionType.Right + 5, comboBoxTransactionType.Top)
            };
            groupBoxTransactionEntry.Controls.Add(transactionTypeStar);

            // Payment Method
            var paymentMethodStar = new Label
            {
                Text = "*",
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(comboBoxPaymentMethod.Right + 5, comboBoxPaymentMethod.Top)
            };
            groupBoxTransactionEntry.Controls.Add(paymentMethodStar);

            // Transaction Date
            var transactionDateStar = new Label
            {
                Text = "*",
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(dateTimePickerTransactionDate.Right + 5, dateTimePickerTransactionDate.Top)
            };
            groupBoxTransactionEntry.Controls.Add(transactionDateStar);
        }
    }
}