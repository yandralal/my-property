using System.Configuration;
using Microsoft.Data.SqlClient;

namespace RealEstateManager.Pages
{
    public partial class RegisterPropertyTransactionForm : BaseForm
    {
        private readonly int? _propertyId;
        private readonly decimal? _saleAmount;
        private readonly string? _propertyNumber;
        private readonly decimal _amountPaidTillDate = 0;
        private readonly string? _transactionId;

        // In your class, add a field to store total loan
        private decimal _totalLoan = 0;

        public RegisterPropertyTransactionForm(int? propertyId = null, decimal? saleAmount = null, string? propertyNumber = "")
        {
            InitializeComponent();
            SetupNumericTextBoxValidation(); 
            _propertyId = propertyId;
            _saleAmount = saleAmount;
            _propertyNumber = propertyNumber;
            if (!string.IsNullOrEmpty(_propertyNumber))
            {
                textBoxPropertyId.Text = _propertyNumber.ToString();
                textBoxPropertyId.ReadOnly = true;
            }
            if (_saleAmount.HasValue)
            {
                textBoxBuyAmount.Text = _saleAmount.Value.ToString("N2");
                textBoxBuyAmount.ReadOnly = true;
            }

            // Fetch and display amount paid till date
            if (_propertyId.HasValue)
            {
                _amountPaidTillDate = GetAmountPaidTillDate(_propertyId.Value);
                textBoxAmountPaidTillDate.Text = _amountPaidTillDate.ToString("N2");

                _totalLoan = GetTotalLoan(_propertyId.Value);
                textBoxTotalLoan.Text = _totalLoan.ToString("N2");
            }

            // Update balance label on load
            UpdateBalanceOnLoad();

            // Attach event handler for dynamic update
            textBoxAmount.TextChanged += UpdateBalanceAmount;

            // Populate Transaction Type dropdown
            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(["Credit", "Debit"]);
            comboBoxTransactionType.SelectedItem = "Debit"; // Set "Debit" as default

            // Set default payment method
            comboBoxPaymentMethod.SelectedItem = "Cash";
        }

        public RegisterPropertyTransactionForm(string transactionId, bool readOnly = false)
        {
            InitializeComponent();
            SetupNumericTextBoxValidation();

            _transactionId = transactionId;

            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(new[] { "Credit", "Debit" });

            decimal currentTransactionAmount = 0;
            decimal buyPrice = 0;

            // Load transaction details from DB
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"
                SELECT 
                    pt.PropertyId, 
                    pt.TransactionDate, 
                    pt.TransactionType,
                    pt.Amount, 
                    pt.PaymentMethod, 
                    pt.ReferenceNumber, 
                    p.Title,
                    p.Price AS BuyPrice,
                    pt.Notes
                FROM PropertyTransaction pt
                INNER JOIN Property p ON pt.PropertyId = p.Id
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
                        _propertyId = reader["PropertyId"] != DBNull.Value ? Convert.ToInt32(reader["PropertyId"]) : null;
                        dateTimePickerTransactionDate.Value = reader["TransactionDate"] != DBNull.Value
                            ? Convert.ToDateTime(reader["TransactionDate"])
                            : DateTime.Now;
                        currentTransactionAmount = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]) : 0;
                        textBoxAmount.Text = currentTransactionAmount.ToString("N2");
                        comboBoxPaymentMethod.Text = reader["PaymentMethod"]?.ToString() ?? "";
                        textBoxReferenceNumber.Text = reader["ReferenceNumber"]?.ToString() ?? "";
                        textBoxNotes.Text = reader["Notes"]?.ToString() ?? "";
                        comboBoxTransactionType.Text = reader["TransactionType"]?.ToString() ?? "Debit";

                        var propertyNumberObj = reader["Title"];
                        if (propertyNumberObj != null)
                        {
                            textBoxPropertyId.Text = propertyNumberObj.ToString();
                            textBoxPropertyId.ReadOnly = true;
                        }

                        buyPrice = reader["BuyPrice"] != DBNull.Value ? Convert.ToDecimal(reader["BuyPrice"]) : 0;
                        textBoxBuyAmount.Text = buyPrice.ToString("N2");
                        textBoxBuyAmount.ReadOnly = true;

                        if (_propertyId.HasValue)
                        {
                            _totalLoan = GetTotalLoan(_propertyId.Value);
                            textBoxTotalLoan.Text = _totalLoan.ToString("N2");
                        }

                        // Calculate balance using only the current transaction
                        labelBalanceValue.Text = (buyPrice - currentTransactionAmount - _totalLoan).ToString("N2");
                        textBoxAmountPaidTillDate.Text = currentTransactionAmount.ToString("N2");
                    }
                }
            }

            // Disable all input controls for view-only mode
            SetFieldsReadOnly(true);
            buttonSave.Visible = false;

            if (string.IsNullOrWhiteSpace(comboBoxPaymentMethod.Text))
                comboBoxPaymentMethod.SelectedItem = "Cash";
        }

        private static decimal GetAmountPaidTillDate(int propertyId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = "SELECT ISNULL(SUM(Amount), 0) FROM PropertyTransaction WHERE PropertyId = @PropertyId AND IsDeleted = 0";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                conn.Open();
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }

        // Add this method to fetch total loan for the property
        private static decimal GetTotalLoan(int propertyId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = "SELECT ISNULL(SUM(LoanAmount), 0) FROM PropertyLoan WHERE PropertyId = @PropertyId AND IsDeleted = 0";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                conn.Open();
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }

        private void UpdateBalanceOnLoad()
        {
            if (decimal.TryParse(textBoxBuyAmount.Text, out decimal saleAmount))
            {
                labelBalanceValue.Text = (saleAmount - _amountPaidTillDate - _totalLoan).ToString("N2");
            }
            else
            {
                labelBalanceValue.Text = "0.00";
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Validate property selection
            if (!_propertyId.HasValue)
            {
                MessageBox.Show("Property information is missing. Please select a property.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate transaction date (not in future)
            if (dateTimePickerTransactionDate.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Transaction date cannot be in the future.", "Validation Error" , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerTransactionDate.Focus();
                return;
            }

            // Validate amount
            if (!decimal.TryParse(textBoxAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid, positive amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxAmount.Focus();
                return;
            }

            // Validate transaction type
            if (string.IsNullOrWhiteSpace(comboBoxTransactionType.Text))
            {
                MessageBox.Show("Please select a transaction type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxTransactionType.Focus();
                return;
            }

            // Validate payment method
            if (string.IsNullOrWhiteSpace(comboBoxPaymentMethod.Text))
            {
                MessageBox.Show("Please select a payment method.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxPaymentMethod.Focus();
                return;
            }

            // Validate reference number (optional, but if provided, at least 3 chars)
            if (!string.IsNullOrWhiteSpace(textBoxReferenceNumber.Text) && textBoxReferenceNumber.Text.Trim().Length < 3)
            {
                MessageBox.Show("Reference number must be at least 3 characters if provided.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxReferenceNumber.Focus();
                return;
            }

            // Validate sale amount (should be positive)
            if (!decimal.TryParse(textBoxBuyAmount.Text, out decimal saleAmount) || saleAmount <= 0)
            {
                MessageBox.Show("Sale amount must be a valid, positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxBuyAmount.Focus();
                return;
            }

            // Validate amount paid till date (should not exceed sale amount)
            if (_amountPaidTillDate + amount > saleAmount)
            {
                MessageBox.Show("Total paid amount cannot exceed sale amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxAmount.Focus();
                return;
            }

            string paymentMethod = comboBoxPaymentMethod.Text;
            string referenceNumber = textBoxReferenceNumber.Text;
            string notes = textBoxNotes.Text;
            string transactionType = comboBoxTransactionType.Text;
            string userIdentifier = (!string.IsNullOrEmpty(LoggedInUserId)) ? LoggedInUserId.ToString() : Environment.UserName;
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;

            if (!string.IsNullOrEmpty(_transactionId))
            {
                string update = @"
                    UPDATE PropertyTransaction SET
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
                    cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@TransactionId", _transactionId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Transaction updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string insert = @"
                    INSERT INTO PropertyTransaction
                    (PropertyId, TransactionDate, Amount, PaymentMethod, ReferenceNumber, Notes, TransactionType, CreatedBy, CreatedDate, IsDeleted)
                    VALUES
                    (@PropertyId, @TransactionDate, @Amount, @PaymentMethod, @ReferenceNumber, @Notes, @TransactionType, @CreatedBy, @CreatedDate, 0)";

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(insert, conn))
                {
                    cmd.Parameters.AddWithValue("@PropertyId", _propertyId);
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
            }

            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is LandingFormResponsive landingForm)
                {
                    landingForm.LoadActiveProperties();
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
            if (decimal.TryParse(textBoxBuyAmount.Text, out saleAmount) &&
                decimal.TryParse(textBoxAmount.Text, out newPaid))
            {
                labelBalanceValue.Text = (saleAmount - (_amountPaidTillDate + newPaid) - _totalLoan).ToString("N2");
            }
            else if (decimal.TryParse(textBoxBuyAmount.Text, out saleAmount))
            {
                labelBalanceValue.Text = (saleAmount - _amountPaidTillDate - _totalLoan).ToString("N2");
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

        private void textBoxAmount_Leave(object sender, EventArgs e)
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
            textBoxBuyAmount.KeyPress += handler;
            textBoxAmountPaidTillDate.KeyPress += handler;
        }
    }
}