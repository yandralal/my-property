using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace RealEstateManager.Pages
{
    public partial class RegisterMiscTransactionForm : BaseForm
    {
        private readonly string? _transactionId;

        public RegisterMiscTransactionForm()
        {
            InitializeComponent();

            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(new object[] { "Credit", "Debit" });
            comboBoxTransactionType.SelectedIndex = 0;

            comboBoxPaymentMethod.Items.Clear();
            comboBoxPaymentMethod.Items.AddRange(new object[] { "Cash", "Cheque", "Bank Transfer", "Other" });
        }

        public RegisterMiscTransactionForm(string transactionId, bool readOnly = false)
        {
            InitializeComponent();
            _transactionId = transactionId;

            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(new object[] { "Credit", "Debit" });

            comboBoxPaymentMethod.Items.Clear();
            comboBoxPaymentMethod.Items.AddRange(new object[] { "Cash", "Cheque", "Bank Transfer", "Other" });

            // Load transaction details from DB
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
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
                        textBoxRecipient.Text = reader["Recipient"]?.ToString() ?? ""; // <-- Add this line
                        textBoxNotes.Text = reader["Notes"]?.ToString() ?? "";
                        comboBoxTransactionType.Text = reader["TransactionType"]?.ToString() ?? "Credit";
                    }
                }
            }

            SetFieldsReadOnly(readOnly);
            buttonSave.Visible = !readOnly;
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
            string recipient = textBoxRecipient.Text; // <-- Add this line
            string userName = Environment.UserName;

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";

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
                    cmd.Parameters.AddWithValue("@Recipient", recipient); // <-- Add this line
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
                    cmd.Parameters.AddWithValue("@Recipient", recipient); // <-- Add this line
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.Parameters.AddWithValue("@TransactionType", transactionType);
                    cmd.Parameters.AddWithValue("@CreatedBy", userName);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Transaction registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
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
    }
}