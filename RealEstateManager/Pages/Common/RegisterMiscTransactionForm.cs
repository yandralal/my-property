using System.Configuration;
using Microsoft.Data.SqlClient;

namespace RealEstateManager.Pages
{
    public partial class RegisterMiscTransactionForm : BaseForm
    {
        private readonly string? _transactionId;

        public RegisterMiscTransactionForm()
        {
            InitializeComponent();
            
            AddMandatoryFieldStars();
            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(["Credit", "Debit"]);
            comboBoxTransactionType.SelectedIndex = 1;

            comboBoxPaymentMethod.Items.Clear();
            comboBoxPaymentMethod.Items.AddRange(["Cash", "Cheque", "Bank Transfer", "Other"]);
            comboBoxPaymentMethod.SelectedIndex = 0;

            SetupAmountFormatting();
            SetupNumericTextBoxValidation();
        }

        public RegisterMiscTransactionForm(string transactionId, bool readOnly = false)
        {
            InitializeComponent();
            AddMandatoryFieldStars();
            _transactionId = transactionId;

            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(["Credit", "Debit"]);

            comboBoxPaymentMethod.Items.Clear();
            comboBoxPaymentMethod.Items.AddRange(["Cash", "Cheque", "Bank Transfer", "Other"]);

            // Load transaction details from DB
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"
                SELECT 
                    TransactionDate,
                    Amount,
                    PaymentMethod,
                    ReferenceNumber,
                    Recipient,
                    Notes,
                    TransactionType
                FROM MiscTransaction
                WHERE TransactionId = @TransactionId AND IsDeleted = 0";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dateTimePickerTransactionDate.Value = reader["TransactionDate"] != DBNull.Value ? Convert.ToDateTime(reader["TransactionDate"]) : DateTime.Now;
                        textBoxAmount.Text = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]).ToString("N2") : "";
                        comboBoxPaymentMethod.Text = reader["PaymentMethod"]?.ToString() ?? "";
                        textBoxReferenceNumber.Text = reader["ReferenceNumber"]?.ToString() ?? "";
                        textBoxRecipient.Text = reader["Recipient"]?.ToString() ?? "";
                        textBoxNotes.Text = reader["Notes"]?.ToString() ?? "";
                        comboBoxTransactionType.Text = reader["TransactionType"]?.ToString() ?? "Credit";
                    }
                }
            }

            SetupAmountFormatting();
            SetupNumericTextBoxValidation();

            SetFieldsReadOnly(readOnly);
            buttonSave.Visible = !readOnly;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // 1. Recipient (TabIndex 0)
            if (string.IsNullOrWhiteSpace(textBoxRecipient.Text) || textBoxRecipient.Text.Trim().Length < 3)
            {
                MessageBox.Show("Please enter a valid recipient name (at least 3 characters).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxRecipient.Focus();
                return;
            }

            // 2. Amount (TabIndex 1)
            if (!decimal.TryParse(textBoxAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid, positive amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxAmount.Focus();
                return;
            }

            // 3. Transaction Type (TabIndex 2)
            if (string.IsNullOrWhiteSpace(comboBoxTransactionType.Text))
            {
                MessageBox.Show("Please select a transaction type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxTransactionType.Focus();
                return;
            }

            // 4. Payment Method (TabIndex 3)
            if (string.IsNullOrWhiteSpace(comboBoxPaymentMethod.Text))
            {
                MessageBox.Show("Please select a payment method.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxPaymentMethod.Focus();
                return;
            }

            // 5. Reference Number (TabIndex 4, optional, but if provided, at least 3 chars)
            if (!string.IsNullOrWhiteSpace(textBoxReferenceNumber.Text) && textBoxReferenceNumber.Text.Trim().Length < 3)
            {
                MessageBox.Show("Reference number must be at least 3 characters if provided.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxReferenceNumber.Focus();
                return;
            }

            // 6. Notes (TabIndex 5) - usually optional, skip unless you want to enforce a rule

            // 7. Transaction Date (TabIndex 6)
            if (dateTimePickerTransactionDate.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Transaction date cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerTransactionDate.Focus();
                return;
            }

            string paymentMethod = comboBoxPaymentMethod.Text;
            string referenceNumber = textBoxReferenceNumber.Text;
            string notes = textBoxNotes.Text;
            string transactionType = comboBoxTransactionType.Text;
            string recipient = textBoxRecipient.Text;
            string userIdentifier = (!string.IsNullOrEmpty(LoggedInUserId)) ? LoggedInUserId.ToString() : Environment.UserName;
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;

            if (!string.IsNullOrEmpty(_transactionId))
            {
                // UPDATE existing transaction
                string update = @"
                    UPDATE MiscTransaction SET
                        TransactionDate = @TransactionDate,
                        Amount = @Amount,
                        PaymentMethod = @PaymentMethod,
                        ReferenceNumber = @ReferenceNumber,
                        Recipient = @Recipient,
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
                    cmd.Parameters.AddWithValue("@Recipient", recipient);
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
                // INSERT new transaction
                string insert = @"
                    INSERT INTO MiscTransaction
                    (TransactionDate, Amount, PaymentMethod, ReferenceNumber, Recipient, Notes, TransactionType, CreatedBy, CreatedDate, IsDeleted)
                    VALUES
                    (@TransactionDate, @Amount, @PaymentMethod, @ReferenceNumber, @Recipient, @Notes, @TransactionType, @CreatedBy, @CreatedDate, 0)";

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(insert, conn))
                {
                    cmd.Parameters.AddWithValue("@TransactionDate", dateTimePickerTransactionDate.Value);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                    cmd.Parameters.AddWithValue("@Recipient", recipient);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.Parameters.AddWithValue("@TransactionType", transactionType);
                    cmd.Parameters.AddWithValue("@CreatedBy", userIdentifier);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Transaction registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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

        // Add this method to your class:
        private void SetupAmountFormatting()
        {
            textBoxAmount.Leave += (s, e) =>
            {
                if (decimal.TryParse(textBoxAmount.Text, out decimal val))
                {
                    textBoxAmount.Text = val.ToString("N2");
                }
                else
                {
                    textBoxAmount.Text = "0.00";
                }
            };
        }

        /// <summary>
        /// Restricts textBoxAmount to digits, one decimal point, and control keys.
        /// </summary>
        private void SetupNumericTextBoxValidation()
        {
            textBoxAmount.KeyPress += (s, e) =>
            {
                char ch = e.KeyChar;
                TextBox tb = s as TextBox;

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
        }

        private void AddMandatoryFieldStars()
        {
            // Helper to add a star to the group box
            void AddStar(Control target)
            {
                var star = new Label
                {
                    Text = "*",
                    ForeColor = Color.Red,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(target.Right + 5, target.Top + 2)
                };
                // Add to the same parent as the target control
                target.Parent.Controls.Add(star);
                star.BringToFront();
            }

            AddStar(textBoxRecipient);
            AddStar(textBoxAmount);
            AddStar(comboBoxTransactionType);
            AddStar(comboBoxPaymentMethod);
            AddStar(dateTimePickerTransactionDate);
        }
    }
}