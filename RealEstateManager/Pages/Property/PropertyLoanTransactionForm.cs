using Microsoft.Data.SqlClient;
using System.Configuration;

namespace RealEstateManager.Pages
{
    public partial class PropertyLoanTransactionForm : BaseForm
    {
        private int _propertyId;
        // Change the declaration of _propertyTitle to be nullable
        private string? _propertyTitle;
        private readonly int? _editId = null;
        private readonly bool _isReadOnly;

        // Add this field to store mapping from lender name to PropertyLoanId
        private readonly Dictionary<string, int> _lenderNameToLoanId = new();

        private const int Y_AFTER_PAYING_FOR = 240;
        private const int Y_STEP = 45;

        // Constructor for new transaction (edit mode)
        public PropertyLoanTransactionForm(int propertyId, string propertyTitle)
            : this(propertyId, propertyTitle, null, false)
        {
        }

        // Constructor for edit/view by transactionId
        public PropertyLoanTransactionForm(int transactionId, bool isReadOnly = false)
        {
            InitializeComponent();
            _isReadOnly = isReadOnly;
            _editId = transactionId;

            // Load transaction details from DB
            LoadTransactionForEditOrView(transactionId);

            // Set controls to read-only if needed
            SetReadOnlyMode(_isReadOnly);
        }

        // Unified constructor for internal use
        private PropertyLoanTransactionForm(int propertyId, string propertyTitle, int? editId, bool isReadOnly)
        {
            InitializeComponent();
            _propertyId = propertyId;
            _propertyTitle = propertyTitle;
            _editId = editId;
            _isReadOnly = isReadOnly;

            textBoxPropertyId.Text = _propertyTitle; // Pre-populate property name

            dateTimePickerTransactionDate.Value = DateTime.Now;
            LoadLenders();

            if (comboBoxTransactionType.Items.Count > 0)
                comboBoxTransactionType.SelectedIndex = 0;
            if (comboBoxPaymentMethod.Items.Count > 0)
                comboBoxPaymentMethod.SelectedIndex = 0;

            comboBoxPayingFor.SelectedIndex = 0; // Default to Interest
            ComboBoxPayingFor_SelectedIndexChanged(comboBoxPayingFor, EventArgs.Empty);

            comboBoxLenderName.SelectedIndexChanged += ComboBoxLenderName_SelectedIndexChanged;
            textBoxAmount.TextChanged += TextBoxAmount_TextChanged;
            textBoxAmount.Leave += TextBoxAmount_Leave;

            // Populate Transaction Type dropdown
            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(new[] { "Credit", "Debit" });
            comboBoxTransactionType.SelectedIndex = 1; // Default to "Debit"

            // If editing, load data
            if (_editId.HasValue)
            {
                LoadTransactionForEditOrView(_editId.Value);
            }

            SetReadOnlyMode(_isReadOnly);
        }

        private void LoadTransactionForEditOrView(int transactionId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            int? propertyLoanId = null;
            string payingFor = "Principle"; // default

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT t.*, p.Title AS PropertyTitle
                FROM PropertyLoanTransaction t
                INNER JOIN Property p ON t.PropertyId = p.Id
                WHERE t.Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", transactionId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _propertyId = reader.GetInt32(reader.GetOrdinal("PropertyId"));
                        _propertyTitle = reader["PropertyTitle"].ToString() ?? "";

                        LoadLenders();

                        textBoxPropertyId.Text = _propertyTitle;
                        string lenderName = reader["LenderName"].ToString() ?? "";
                        SetComboBoxSelectedItem(comboBoxLenderName, lenderName);

                        string transactionType = reader["TransactionType"].ToString() ?? "";
                        EnsureTransactionTypeItems();
                        SetComboBoxSelectedItem(comboBoxTransactionType, transactionType);

                        string paymentMethod = reader["PaymentMethod"].ToString() ?? "";
                        SetComboBoxSelectedItem(comboBoxPaymentMethod, paymentMethod);

                        textBoxReferenceNumber.Text = reader["ReferenceNumber"]?.ToString() ?? "";
                        textBoxNotes.Text = reader["Notes"]?.ToString() ?? "";

                        if (reader["TransactionDate"] != DBNull.Value)
                            dateTimePickerTransactionDate.Value = Convert.ToDateTime(reader["TransactionDate"]);

                        decimal Principle = reader["PrincipleAmount"] != DBNull.Value ? Convert.ToDecimal(reader["PrincipleAmount"]) : 0;
                        decimal interest = reader["InterestAmount"] != DBNull.Value ? Convert.ToDecimal(reader["InterestAmount"]) : 0;

                        // Set PayingFor and Amount
                        if (interest > 0)
                        {
                            payingFor = "Interest";
                            textBoxAmount.Text = interest.ToString("N2");
                        }
                        else
                        {
                            payingFor = "Principle";
                            textBoxAmount.Text = Principle.ToString("N2");
                        }

                        // Ensure .00 is always appended and balance is updated
                        UpdateBalanceWithAmountToPay();

                        if (reader["PropertyLoanId"] != DBNull.Value)
                            propertyLoanId = Convert.ToInt32(reader["PropertyLoanId"]);
                    }
                }
            }

            // Ensure comboBoxPayingFor has items before setting
            if (comboBoxPayingFor.Items.Count == 0)
            {
                comboBoxPayingFor.Items.AddRange(new[] { "Interest", "Principle" });
            }
            SetComboBoxSelectedItem(comboBoxPayingFor, payingFor);

            // Fetch summary fields from PropertyLoan table
            if (propertyLoanId.HasValue)
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(@"
                    SELECT LoanAmount, TotalInterest, TotalRepayment
                    FROM PropertyLoan
                    WHERE Id = @LoanId", conn))
                {
                    cmd.Parameters.AddWithValue("@LoanId", propertyLoanId.Value);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBoxLoanAmount.Text = reader["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(reader["LoanAmount"]).ToString("0.00") : "";
                            textBoxTotalAmount.Text = reader["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(reader["LoanAmount"]).ToString("0.00") : "";
                        }
                    }
                }
            }

            // Now update summary fields (including Total Interest Paid)
            UpdateLoanSummaryFields();
        }

        private void SetReadOnlyMode(bool isReadOnly)
        {
            // Disable all input controls in view mode
            textBoxAmount.ReadOnly = isReadOnly;
            textBoxReferenceNumber.ReadOnly = isReadOnly;
            textBoxNotes.ReadOnly = isReadOnly;
            comboBoxLenderName.Enabled = !isReadOnly;
            comboBoxTransactionType.Enabled = !isReadOnly;
            comboBoxPaymentMethod.Enabled = !isReadOnly;
            comboBoxPayingFor.Enabled = !isReadOnly;
            dateTimePickerTransactionDate.Enabled = !isReadOnly;
            buttonSave.Visible = !isReadOnly;
        }

        private void LoadLenders()
        {
            comboBoxLenderName.Items.Clear();
            _lenderNameToLoanId.Clear();
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT Id, LenderName FROM PropertyLoan WHERE PropertyId = @PropertyId AND IsDeleted = 0", conn))
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
            // 1. Property (should be set)
            if (string.IsNullOrWhiteSpace(textBoxPropertyId.Text))
            {
                MessageBox.Show("Please select a property.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPropertyId.Focus();
                return;
            }

            // 2. Lender Name
            if (comboBoxLenderName.SelectedIndex < 0 || string.IsNullOrWhiteSpace(comboBoxLenderName.Text))
            {
                MessageBox.Show("Please select a lender name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxLenderName.Focus();
                return;
            }

            // 3. Loan Amount
            if (string.IsNullOrWhiteSpace(textBoxLoanAmount.Text) || !decimal.TryParse(textBoxLoanAmount.Text, out decimal loanAmount) || loanAmount <= 0)
            {
                MessageBox.Show("Please enter a valid loan amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxLoanAmount.Focus();
                return;
            }

            // 4. Paying For
            if (comboBoxPayingFor.SelectedIndex < 0 || string.IsNullOrWhiteSpace(comboBoxPayingFor.Text))
            {
                MessageBox.Show("Please select what you are paying for (Interest/Principle).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxPayingFor.Focus();
                return;
            }

            // 5. Transaction Type
            if (comboBoxTransactionType.SelectedIndex < 0 || string.IsNullOrWhiteSpace(comboBoxTransactionType.Text))
            {
                MessageBox.Show("Please select a transaction type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxTransactionType.Focus();
                return;
            }

            // 6. Amount To Pay
            if (string.IsNullOrWhiteSpace(textBoxAmount.Text) || !decimal.TryParse(textBoxAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount to pay.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxAmount.Focus();
                return;
            }

            // 7. Payment Method
            if (comboBoxPaymentMethod.SelectedIndex < 0 || string.IsNullOrWhiteSpace(comboBoxPaymentMethod.Text))
            {
                MessageBox.Show("Please select a payment method.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxPaymentMethod.Focus();
                return;
            }

            // 8. Date
            if (dateTimePickerTransactionDate.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Transaction date cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerTransactionDate.Focus();
                return;
            }

            DateTime transactionDate = dateTimePickerTransactionDate.Value;
            string transactionType = comboBoxTransactionType.SelectedItem?.ToString() ?? "";
            string paymentMethod = comboBoxPaymentMethod.SelectedItem?.ToString() ?? "";
            string referenceNumber = textBoxReferenceNumber.Text.Trim();
            string notes = textBoxNotes.Text.Trim();
            string lenderName = comboBoxLenderName.SelectedItem?.ToString() ?? "";
            int? propertyLoanId = _lenderNameToLoanId.TryGetValue(lenderName, out int id) ? id : (int?)null;

            bool isInterest = comboBoxPayingFor.SelectedItem?.ToString() == "Interest";
            decimal PrincipleAmount = isInterest ? 0 : amount;
            decimal interestAmount = isInterest ? amount : 0;

            string userIdentifier = !string.IsNullOrEmpty(LoggedInUserId) ? LoggedInUserId : Environment.UserName;

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
                            PrincipleAmount = @PrincipleAmount,
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
                            (PropertyId, PropertyLoanId, LenderName, TransactionType, TransactionDate, PrincipleAmount, InterestAmount, PaymentMethod, ReferenceNumber, Notes, CreatedDate, CreatedBy, IsDeleted)
                            VALUES
                            (@PropertyId, @PropertyLoanId, @LenderName, @TransactionType, @TransactionDate, @PrincipleAmount, @InterestAmount, @PaymentMethod, @ReferenceNumber, @Notes, @CreatedDate, @CreatedBy, 0)";
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", userIdentifier);
                    }

                    cmd.Parameters.AddWithValue("@PropertyId", _propertyId);
                    cmd.Parameters.AddWithValue("@PropertyLoanId", (object?)propertyLoanId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LenderName", lenderName);
                    cmd.Parameters.AddWithValue("@TransactionType", transactionType);
                    cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);
                    cmd.Parameters.AddWithValue("@PrincipleAmount", PrincipleAmount);
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
            labelTotalAmount.Text = isInterest ? "Total Interest:" : "Total Principle:";
            labelTotalPaid.Text = isInterest ? "Total Interest Paid:" : "Total Principle Paid:";
            UpdateLoanSummaryFields();
        }

        private void UpdateLoanSummaryFields()
        {
            string lenderName = comboBoxLenderName.SelectedItem?.ToString() ?? "";
            if (!_lenderNameToLoanId.TryGetValue(lenderName, out int propertyLoanId))
            {
                textBoxTotalAmount.Text = "";
                textBoxTotalPaid.Text = "";
                textBoxLoanAmount.Text = "";
                labelBalanceValue.Text = "0.00";
                return;
            }

            bool isInterest = comboBoxPayingFor.SelectedItem?.ToString() == "Interest";
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;

            decimal Principle = 0;
            decimal totalAmount = 0;
            decimal totalPaid = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Get Principle and total amount (interest or Principle)
                string totalQuery = "SELECT LoanAmount, TotalInterest FROM PropertyLoan WHERE Id = @LoanId";
                using (var cmd = new SqlCommand(totalQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@LoanId", propertyLoanId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Principle = reader["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(reader["LoanAmount"]) : 0;
                            totalAmount = isInterest
                                ? (reader["TotalInterest"] != DBNull.Value ? Convert.ToDecimal(reader["TotalInterest"]) : 0)
                                : Principle;
                        }
                    }
                }
                // Get total paid (interest or Principle)
                string paidQuery = isInterest
                    ? "SELECT ISNULL(SUM(InterestAmount),0) FROM PropertyLoanTransaction WHERE PropertyLoanId = @LoanId AND IsDeleted = 0"
                    : "SELECT ISNULL(SUM(PrincipleAmount),0) FROM PropertyLoanTransaction WHERE PropertyLoanId = @LoanId AND IsDeleted = 0";
                if (_editId.HasValue)
                    paidQuery += " AND Id <> @CurrentId";
                using (var cmd = new SqlCommand(paidQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@LoanId", propertyLoanId);
                    if (_editId.HasValue)
                        cmd.Parameters.AddWithValue("@CurrentId", _editId.Value);
                    var paidResult = cmd.ExecuteScalar();
                    totalPaid = paidResult != null && paidResult != DBNull.Value ? Convert.ToDecimal(paidResult) : 0;
                }
            }
            textBoxLoanAmount.Text = Principle.ToString("N2");
            textBoxTotalAmount.Text = totalAmount.ToString("N2");
            textBoxTotalPaid.Text = totalPaid.ToString("N2");
            decimal amountToPay = 0;
            decimal.TryParse(textBoxAmount.Text, out amountToPay);
            // Calculate balance: totalAmount - totalPaid - amountToPay
            decimal balance = totalAmount - totalPaid - amountToPay;
            labelBalanceValue.Text = balance.ToString("N2");
        }

        private void TextBoxAmount_TextChanged(object? sender, EventArgs e)
        {
            UpdateBalanceWithAmountToPay();
        }

        private void TextBoxAmount_Leave(object? sender, EventArgs e)
        {
            if (decimal.TryParse(textBoxAmount.Text, out decimal value))
            {
                textBoxAmount.Text = value.ToString("N2");
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

            decimal.TryParse(textBoxTotalAmount.Text, out total);
            decimal.TryParse(textBoxTotalPaid.Text, out paid);

            decimal newBalance = total - (paid + amountToPay);
            labelBalanceValue.Text = newBalance.ToString("N2");
        }

        private void SetComboBoxSelectedItem(ComboBox comboBox, string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (string.Equals(comboBox?.Items[i]?.ToString(), value, StringComparison.OrdinalIgnoreCase))
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
            comboBox.Text = value; // fallback
        }

        private void EnsureTransactionTypeItems()
        {
            if (comboBoxTransactionType.Items.Count == 0)
            {
                comboBoxTransactionType.Items.AddRange(new[] { "Credit", "Debit" });
            }
        }

        private decimal GetCurrentAmount()
        {
            decimal amount = 0;
            decimal.TryParse(textBoxAmount.Text, out amount);
            return amount;
        }
    }
}