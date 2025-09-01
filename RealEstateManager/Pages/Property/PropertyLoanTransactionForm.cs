using Microsoft.Data.SqlClient;
using System.Configuration;

namespace RealEstateManager.Pages
{
    public partial class PropertyLoanTransactionForm : BaseForm
    {
        private readonly int _propertyId;
        private readonly string _propertyTitle;
        private readonly int? _editId = null; 

        // Add this field to store mapping from lender name to PropertyLoanId
        private readonly Dictionary<string, int> _lenderNameToLoanId = new();

        private const int Y_AFTER_PAYING_FOR = 240;
        private const int Y_STEP = 45;

        public PropertyLoanTransactionForm(int propertyId, string propertyTitle)
        {
            InitializeComponent();
            _propertyId = propertyId;
            _propertyTitle = propertyTitle;

            textBoxPropertyId.Text = _propertyTitle; // Pre-populate property name

            dateTimePickerTransactionDate.Value = DateTime.Now;
            LoadLenders();

            if (comboBoxTransactionType.Items.Count > 0)
                comboBoxTransactionType.SelectedIndex = 0;
            if (comboBoxPaymentMethod.Items.Count > 0)
                comboBoxPaymentMethod.SelectedIndex = 0;

            comboBoxPayingFor.SelectedIndex = 0; // Default to Interest
            ComboBoxPayingFor_SelectedIndexChanged(comboBoxPayingFor, EventArgs.Empty);
            ArrangePayingForControls();

            comboBoxLenderName.SelectedIndexChanged += ComboBoxLenderName_SelectedIndexChanged;
            textBoxAmount.TextChanged += TextBoxAmount_TextChanged;
            textBoxAmount.Leave += TextBoxAmount_Leave;

            // Populate Transaction Type dropdown
            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(new[] { "Credit", "Debit" });
            comboBoxTransactionType.SelectedIndex = 1; // Default to "Debit"
        }

        private void LoadLenders()
        {
            comboBoxLenderName.Items.Clear();
            _lenderNameToLoanId.Clear();
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT Id, LenderName FROM PropertyLoan WHERE PropertyId = @PropertyId", conn))
            {
                cmd.Parameters.AddWithValue("@PropertyId", _propertyId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int loanId = reader.GetInt32(0);
                        string lenderName = reader.GetString(1);
                        comboBoxLenderName.Items.Add(lenderName);
                        _lenderNameToLoanId[lenderName] = loanId;
                    }
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Validate input
            if (!decimal.TryParse(textBoxAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxAmount.Focus();
                return;
            }
            if (comboBoxTransactionType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a transaction type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxTransactionType.Focus();
                return;
            }
            if (comboBoxPaymentMethod.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a payment method.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxPaymentMethod.Focus();
                return;
            }

            DateTime transactionDate = dateTimePickerTransactionDate.Value;
            string transactionType = comboBoxTransactionType.SelectedItem.ToString() ?? "";
            string paymentMethod = comboBoxPaymentMethod.SelectedItem.ToString() ?? "";
            string referenceNumber = textBoxReferenceNumber.Text.Trim();
            string notes = textBoxNotes.Text.Trim();
            string lenderName = comboBoxLenderName.SelectedItem?.ToString() ?? "";
            int? propertyLoanId = _lenderNameToLoanId.TryGetValue(lenderName, out int id) ? id : (int?)null;
            decimal loanAmount = 0;
            decimal.TryParse(textBoxLoanAmount.Text, out loanAmount);

            // Determine principal/interest split
            bool isInterest = comboBoxPayingFor.SelectedItem?.ToString() == "Interest";
            decimal principalAmount = isInterest ? 0 : amount;
            decimal interestAmount = isInterest ? amount : 0;

            string userIdentifier = !string.IsNullOrEmpty(BaseForm.LoggedInUserId)
                ? BaseForm.LoggedInUserId
                : Environment.UserName;

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    if (_editId.HasValue)
                    {
                        // UPDATE
                        cmd.CommandText = @"UPDATE PropertyLoanTransaction SET
                            PropertyId = @PropertyId,
                            PropertyLoanId = @PropertyLoanId,
                            LenderName = @LenderName,
                            TransactionType = @TransactionType,
                            TransactionDate = @TransactionDate,
                            PrincipalAmount = @PrincipalAmount,
                            InterestAmount = @InterestAmount,
                            PaymentMethod = @PaymentMethod,
                            ReferenceNumber = @ReferenceNumber,
                            Notes = @Notes,
                            ModifiedDate = @ModifiedDate,
                            ModifiedBy = @ModifiedBy
                            WHERE Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", _editId.Value);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                    }
                    else
                    {
                        // INSERT
                        cmd.CommandText = @"INSERT INTO PropertyLoanTransaction
                            (PropertyId, PropertyLoanId, LenderName, TransactionType, TransactionDate, PrincipalAmount, InterestAmount, PaymentMethod, ReferenceNumber, Notes, CreatedDate, CreatedBy, IsDeleted)
                            VALUES
                            (@PropertyId, @PropertyLoanId, @LenderName, @TransactionType, @TransactionDate, @PrincipalAmount, @InterestAmount, @PaymentMethod, @ReferenceNumber, @Notes, @CreatedDate, @CreatedBy, 0)";
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", userIdentifier);
                    }

                    cmd.Parameters.AddWithValue("@PropertyId", _propertyId);
                    cmd.Parameters.AddWithValue("@PropertyLoanId", (object?)propertyLoanId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LenderName", lenderName);
                    cmd.Parameters.AddWithValue("@TransactionType", transactionType);
                    cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);
                    cmd.Parameters.AddWithValue("@PrincipalAmount", principalAmount);
                    cmd.Parameters.AddWithValue("@InterestAmount", interestAmount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@ReferenceNumber", (object)referenceNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Loan transaction saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving transaction:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ComboBoxLenderName_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateLoanSummaryFields();
        }

        private void ComboBoxPayingFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isInterest = comboBoxPayingFor.SelectedItem?.ToString() == "Interest";

            // Show/hide interest/principle fields
            labelTotalInterest.Visible = textBoxTotalInterest.Visible =
            labelTotalInterestPaid.Visible = textBoxTotalInterestPaid.Visible = isInterest;

            labelTotalPrinciplePaid.Visible = textBoxTotalPrinciplePaid.Visible = !isInterest;

            UpdateLoanSummaryFields();
            ArrangePayingForControls();
        }

        private void UpdateLoanSummaryFields()
        {
            string lenderName = comboBoxLenderName.SelectedItem?.ToString() ?? "";
            if (!_lenderNameToLoanId.TryGetValue(lenderName, out int propertyLoanId))
            {
                // Clear all fields if no lender selected
                textBoxTotalInterest.Text = "";
                textBoxTotalInterestPaid.Text = "";
                textBoxTotalPrinciplePaid.Text = "";
                labelBalanceValue.Text = "0.00";
                return;
            }

            bool isInterest = comboBoxPayingFor.SelectedItem?.ToString() == "Interest";
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Get loan details
                decimal totalInterest = 0, totalRepayable = 0, loanAmount = 0;
                using (var cmd = new SqlCommand("SELECT LoanAmount, TotalInterest, TotalRepayable FROM PropertyLoan WHERE Id = @LoanId", conn))
                {
                    cmd.Parameters.AddWithValue("@LoanId", propertyLoanId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            loanAmount = reader.GetDecimal(0);
                            totalInterest = reader.GetDecimal(1);
                            totalRepayable = reader.GetDecimal(2);
                        }
                    }
                }
                textBoxLoanAmount.Text = loanAmount.ToString("0.00");
                textBoxTotalPrinciple.Text = loanAmount.ToString("0.00");

                if (isInterest)
                {
                    // Total interest paid till date
                    decimal totalInterestPaid = 0;
                    using (var cmd = new SqlCommand(@"
                        SELECT ISNULL(SUM(InterestAmount),0) FROM PropertyLoanTransaction
                        WHERE PropertyLoanId = @LoanId AND IsDeleted = 0", conn))
                    {
                        cmd.Parameters.AddWithValue("@LoanId", propertyLoanId);
                        totalInterestPaid = (decimal)cmd.ExecuteScalar();
                    }

                    // Amount paid till date (interest)
                    textBoxTotalInterest.Text = totalInterest.ToString("0.00");
                    textBoxTotalInterestPaid.Text = totalInterestPaid.ToString("0.00");
                    labelBalanceValue.Text = (totalInterest - totalInterestPaid).ToString("0.00");

                    // Hide principle fields
                    textBoxTotalPrinciplePaid.Text = "";
                }
                else // Principle
                {
                    // Total principle paid till date
                    decimal totalPrinciplePaid = 0;
                    using (var cmd = new SqlCommand(@"
                        SELECT ISNULL(SUM(PrincipalAmount),0) FROM PropertyLoanTransaction
                        WHERE PropertyLoanId = @LoanId AND IsDeleted = 0", conn))
                    {
                        cmd.Parameters.AddWithValue("@LoanId", propertyLoanId);
                        totalPrinciplePaid = (decimal)cmd.ExecuteScalar();
                    }

                    // Amount paid till date (principle)
                    textBoxTotalPrinciplePaid.Text = totalPrinciplePaid.ToString("0.00");
                    labelBalanceValue.Text = (loanAmount - totalPrinciplePaid).ToString("0.00");

                    // Hide interest fields
                    textBoxTotalInterest.Text = "";
                    textBoxTotalInterestPaid.Text = "";
                }
            }

            UpdateBalanceWithAmountToPay();
        }

        private void TextBoxAmount_TextChanged(object? sender, EventArgs e)
        {
            UpdateBalanceWithAmountToPay();
        }

        private void TextBoxAmount_Leave(object? sender, EventArgs e)
        {
            if (decimal.TryParse(textBoxAmount.Text, out decimal value))
            {
                textBoxAmount.Text = value.ToString("0.00");
            }
            else if (!string.IsNullOrWhiteSpace(textBoxAmount.Text))
            {
                MessageBox.Show("Please enter a valid numeric amount.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxAmount.Focus();
            }
        }

        private void UpdateBalanceWithAmountToPay()
        {
            decimal amountToPay = 0;
            decimal.TryParse(textBoxAmount.Text, out amountToPay);

            bool isInterest = comboBoxPayingFor.SelectedItem?.ToString() == "Interest";
            decimal total = 0;
            decimal paid = 0;

            if (isInterest)
            {
                decimal.TryParse(textBoxTotalInterest.Text, out total);
                decimal.TryParse(textBoxTotalInterestPaid.Text, out paid);
            }
            else
            {
                decimal.TryParse(textBoxTotalPrinciple.Text, out total);
                decimal.TryParse(textBoxTotalPrinciplePaid.Text, out paid);
            }

            decimal newBalance = total - (paid + amountToPay);
            labelBalanceValue.Text = newBalance.ToString("0.00");
        }

        private void ArrangePayingForControls()
        {
            // Base Y position after "Paying For"
            int y = Y_AFTER_PAYING_FOR;
            bool isInterest = comboBoxPayingFor.SelectedItem?.ToString() == "Interest";

            // Hide all first
            labelTotalInterest.Visible = textBoxTotalInterest.Visible =
            labelTotalInterestPaid.Visible = textBoxTotalInterestPaid.Visible = isInterest;

            labelTotalPrinciplePaid.Visible = textBoxTotalPrinciplePaid.Visible = !isInterest;

            // --- Dynamic arrangement ---
            if (isInterest)
            {
                // Interest fields
                labelTotalInterest.Visible = textBoxTotalInterest.Visible = true;
                labelTotalInterest.Location = new Point(27, y);
                textBoxTotalInterest.Location = new Point(229, y - 3);
                y += Y_STEP;

                labelTotalInterestPaid.Visible = textBoxTotalInterestPaid.Visible = true;
                labelTotalInterestPaid.Location = new Point(27, y);
                textBoxTotalInterestPaid.Location = new Point(229, y - 3);
                y += Y_STEP;

                labelTransactionType.Location = new Point(27, y);
                comboBoxTransactionType.Location = new Point(229, y - 3);
                y += Y_STEP;

                // Amount To Pay (after interest fields)
                labelAmount.Location = new Point(27, y);
                textBoxAmount.Location = new Point(229, y - 3);
                y += Y_STEP;

                // The rest of the controls follow
                labelBalanceAmount.Location = new Point(27, y);
                labelBalanceValue.Location = new Point(229, y - 3);
                y += Y_STEP;

                labelPaymentMethod.Location = new Point(27, y);
                comboBoxPaymentMethod.Location = new Point(229, y - 3);
                y += Y_STEP;

                labelReferenceNumber.Location = new Point(27, y);
                textBoxReferenceNumber.Location = new Point(229, y - 3);
                y += Y_STEP;

                labelNotes.Location = new Point(27, y);
                textBoxNotes.Location = new Point(229, y - 3);
                y += Y_STEP + 30; // Extra space for multiline

                labelTransactionDate.Location = new Point(27, y);
                dateTimePickerTransactionDate.Location = new Point(229, y - 3);
                y += Y_STEP;

                buttonSave.Location = new Point(263, y + 20);
                buttonCancel.Location = new Point(398, y + 20);
            }
            else
            {
                // Principle fields
                // Hide all first
                labelTotalPrinciple.Visible = textBoxTotalPrinciple.Visible = false;
                labelTotalPrinciplePaid.Visible = textBoxTotalPrinciplePaid.Visible =

                // Total Principle
                labelTotalPrinciple.Visible = textBoxTotalPrinciple.Visible = true;
                labelTotalPrinciple.Location = new Point(27, y);
                textBoxTotalPrinciple.Location = new Point(229, y - 3);
                y += Y_STEP;

                // Principle Paid
                labelTotalPrinciplePaid.Visible = textBoxTotalPrinciplePaid.Visible = true;
                labelTotalPrinciplePaid.Location = new Point(27, y);
                textBoxTotalPrinciplePaid.Location = new Point(229, y - 3);
                y += Y_STEP;

                labelTransactionType.Location = new Point(27, y);
                comboBoxTransactionType.Location = new Point(229, y - 3);
                y += Y_STEP;

                // Amount To Pay (before principle fields)
                labelAmount.Location = new Point(27, y);
                textBoxAmount.Location = new Point(229, y - 3);
                y += Y_STEP;

                // The rest of the controls follow
                labelBalanceAmount.Location = new Point(27, y);
                labelBalanceValue.Location = new Point(229, y - 3);
                y += Y_STEP;

                labelPaymentMethod.Location = new Point(27, y);
                comboBoxPaymentMethod.Location = new Point(229, y - 3);
                y += Y_STEP;

                labelReferenceNumber.Location = new Point(27, y);
                textBoxReferenceNumber.Location = new Point(229, y - 3);
                y += Y_STEP;

                labelNotes.Location = new Point(27, y);
                textBoxNotes.Location = new Point(229, y - 3);
                y += Y_STEP + 30; // Extra space for multiline

                labelTransactionDate.Location = new Point(27, y);
                dateTimePickerTransactionDate.Location = new Point(229, y - 3);
                y += Y_STEP;

                buttonSave.Location = new Point(263, y + 20);
                buttonCancel.Location = new Point(398, y + 20);
            }

            int minHeight = 400; // your preferred minimum height
            int paddingBottom = 40; // space below buttons

            // Calculate the bottom of the lowest control (Save/Cancel buttons)
            int bottomY = Math.Max(buttonSave.Bottom, buttonCancel.Bottom);

            // Set new height for group box and form
            int newGroupBoxHeight = bottomY - groupBoxTransactionEntry.Top + paddingBottom;
            int newFormHeight = groupBoxTransactionEntry.Top + newGroupBoxHeight + 40; // 40 for form padding

            // Optionally, set a max width if you want to reduce width as well
            int newWidth = 605; // or a smaller value if you want to reduce width

            groupBoxTransactionEntry.Size = new Size(newWidth, Math.Max(minHeight, newGroupBoxHeight));
            this.ClientSize = new Size(newWidth + 32, Math.Max(minHeight + 40, newFormHeight)); // 32 for form border/padding
        }
    }
}