using Microsoft.Data.SqlClient;
using RealEstateManager.Entities;
using System.Configuration;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class PropertyLoanForm : BaseForm
    {
        private int? _editId = null;

        public PropertyLoanForm()
        {
            InitializeComponent();
            buttonSave.Text = "Save";      // Changed from "Add"
            buttonCancel.Text = "Cancel";  // Ensure this is set
            LoadProperties();

            // Calculation triggers
            textBoxLoanAmount.TextChanged += (s, e) => UpdateTotalRepayable();
            textBoxInterestRate.TextChanged += (s, e) => UpdateTotalRepayable();
            textBoxTenure.TextChanged += (s, e) => UpdateTotalRepayable();

            // Formatting triggers
            textBoxLoanAmount.Leave += AmountTextBox_Leave;
            textBoxInterestRate.Leave += AmountTextBox_Leave;
            textBoxTenure.Leave += AmountTextBox_Leave;
            textBoxTotalInterest.Leave += AmountTextBox_Leave;
            textBoxTotalRepayable.Leave += AmountTextBox_Leave;

            // Prevent non-numeric input
            textBoxLoanAmount.KeyPress += TextBoxDecimal_KeyPress;
            textBoxInterestRate.KeyPress += TextBoxDecimal_KeyPress;
            textBoxTotalInterest.KeyPress += TextBoxDecimal_KeyPress;
            textBoxTotalRepayable.KeyPress += TextBoxDecimal_KeyPress;
            textBoxTenure.KeyPress += TextBoxInteger_KeyPress;
        }

        public PropertyLoanForm(LoanTransaction loan)
        {
            InitializeComponent();
            _editId = loan.Id;
            buttonSave.Text = "Save";
            buttonCancel.Text = "Cancel";
            LoadProperties();
            if (loan.PropertyId.HasValue)
            {
                comboBoxProperty.SelectedValue = loan.PropertyId.Value;
            }
            textBoxLoanAmount.Text = loan.LoanAmount.ToString("0.00");
            textBoxLenderName.Text = loan.LenderName;
            textBoxInterestRate.Text = loan.InterestRate.ToString("0.00");
            dateTimePickerLoanDate.Value = loan.LoanDate;
            textBoxRemarks.Text = loan.Remarks ?? "";
            textBoxTenure.Text = loan.Tenure?.ToString() ?? "";
            textBoxTotalInterest.Text = loan.TotalInterest.ToString("0.00");     
            textBoxTotalRepayable.Text = loan.TotalRepayable.ToString("0.00");   

            // Calculation triggers
            textBoxLoanAmount.TextChanged += (s, e) => UpdateTotalRepayable();
            textBoxInterestRate.TextChanged += (s, e) => UpdateTotalRepayable();
            textBoxTenure.TextChanged += (s, e) => UpdateTotalRepayable();

            // Formatting triggers
            textBoxLoanAmount.Leave += AmountTextBox_Leave;
            textBoxInterestRate.Leave += AmountTextBox_Leave;
            textBoxTenure.Leave += AmountTextBox_Leave;
            textBoxTotalInterest.Leave += AmountTextBox_Leave;
            textBoxTotalRepayable.Leave += AmountTextBox_Leave;
        }

        private void LoadProperties()
        {
            comboBoxProperty.DisplayMember = "Title";
            comboBoxProperty.ValueMember = "Id";
            var dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT Id, Title FROM Property WHERE IsDeleted = 0", conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                conn.Open();
                adapter.Fill(dt);
            }
            comboBoxProperty.DataSource = dt;
            comboBoxProperty.SelectedIndex = -1;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out int? propertyId, out decimal loanAmount, out string lenderName, out decimal interestRate, out DateTime loanDate, out string remarks, out int tenure))
                return;

            if (_editId.HasValue)
            {
                UpdatePropertyLoan(_editId.Value, propertyId, loanAmount, lenderName, interestRate, loanDate, remarks, tenure);
            }
            else
            {
                InsertPropertyLoan(propertyId, loanAmount, lenderName, interestRate, loanDate, remarks, tenure);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInputs(out int? propertyId, out decimal loanAmount, out string lenderName, out decimal interestRate, out DateTime loanDate, out string remarks, out int tenure)
        {
            propertyId = null;
            loanAmount = 0;
            lenderName = "";
            interestRate = 0;
            loanDate = dateTimePickerLoanDate.Value;
            remarks = textBoxRemarks.Text.Trim();
            tenure = 0;

            if (comboBoxProperty.SelectedValue is int pid)
                propertyId = pid;

            if (propertyId == null)
            {
                MessageBox.Show("Property is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxProperty.Focus();
                return false;
            }
            if (!decimal.TryParse(textBoxLoanAmount.Text, out loanAmount) || loanAmount <= 0)
            {
                MessageBox.Show("Enter a valid loan amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxLoanAmount.Focus();
                return false;
            }
            lenderName = textBoxLenderName.Text.Trim();
            if (string.IsNullOrWhiteSpace(lenderName))
            {
                MessageBox.Show("Lender name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxLenderName.Focus();
                return false;
            }
            if (!decimal.TryParse(textBoxInterestRate.Text, out interestRate) || interestRate < 0)
            {
                MessageBox.Show("Enter a valid interest rate.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxInterestRate.Focus();
                return false;
            }
            if (!int.TryParse(textBoxTenure.Text, out tenure) || tenure <= 0)
            {
                MessageBox.Show("Enter a valid tenure (months).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxTenure.Focus();
                return false;
            }
            return true;
        }

        private void InsertPropertyLoan(int? propertyId, decimal loanAmount, string lenderName, decimal interestRate, DateTime loanDate, string remarks, int tenure)
        {
            decimal.TryParse(textBoxTotalInterest.Text, out decimal totalInterest);
            decimal.TryParse(textBoxTotalRepayable.Text, out decimal totalRepayable);

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string insert = @"INSERT INTO PropertyLoan
                (PropertyId, LoanAmount, LenderName, InterestRate, LoanDate, Remarks, CreatedDate, CreatedBy, TotalInterest, TotalRepayable, Tenure)
                VALUES (@PropertyId, @LoanAmount, @LenderName, @InterestRate, @LoanDate, @Remarks, @CreatedDate, @CreatedBy, @TotalInterest, @TotalRepayable, @Tenure)";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@PropertyId", (object?)propertyId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoanAmount", loanAmount);
                cmd.Parameters.AddWithValue("@LenderName", lenderName);
                cmd.Parameters.AddWithValue("@InterestRate", interestRate);
                cmd.Parameters.AddWithValue("@LoanDate", loanDate);
                cmd.Parameters.AddWithValue("@Remarks", (object?)remarks ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreatedBy", BaseForm.LoggedInUserId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalInterest", totalInterest);
                cmd.Parameters.AddWithValue("@TotalRepayable", totalRepayable);
                cmd.Parameters.AddWithValue("@Tenure", tenure);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Loan added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UpdatePropertyLoan(int id, int? propertyId, decimal loanAmount, string lenderName, decimal interestRate, DateTime loanDate, string remarks, int tenure)
        {
            decimal.TryParse(textBoxTotalInterest.Text, out decimal totalInterest);
            decimal.TryParse(textBoxTotalRepayable.Text, out decimal totalRepayable);

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string update = @"UPDATE PropertyLoan SET
                PropertyId = @PropertyId,
                LoanAmount = @LoanAmount,
                LenderName = @LenderName,
                InterestRate = @InterestRate,
                LoanDate = @LoanDate,
                Remarks = @Remarks,
                ModifiedDate = @ModifiedDate,
                ModifiedBy = @ModifiedBy,
                TotalInterest = @TotalInterest,
                TotalRepayable = @TotalRepayable,
                Tenure = @Tenure
                WHERE Id = @Id";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(update, conn))
            {
                cmd.Parameters.AddWithValue("@PropertyId", (object?)propertyId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoanAmount", loanAmount);
                cmd.Parameters.AddWithValue("@LenderName", lenderName);
                cmd.Parameters.AddWithValue("@InterestRate", interestRate);
                cmd.Parameters.AddWithValue("@LoanDate", loanDate);
                cmd.Parameters.AddWithValue("@Remarks", (object?)remarks ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ModifiedBy", BaseForm.LoggedInUserId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalInterest", totalInterest);
                cmd.Parameters.AddWithValue("@TotalRepayable", totalRepayable);
                cmd.Parameters.AddWithValue("@Tenure", tenure);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Loan updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public static void DeleteLoanTransaction(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string delete = "DELETE FROM LoanTransaction WHERE Id = @Id";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(delete, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateTotalRepayable()
        {
            bool hasPrincipal = decimal.TryParse(textBoxLoanAmount.Text, out decimal principal);
            bool hasRate = decimal.TryParse(textBoxInterestRate.Text, out decimal rate);
            bool hasMonths = int.TryParse(textBoxTenure.Text, out int months);

            if (hasPrincipal && hasRate && hasMonths)
            {
                decimal interestPerPeriod = principal * rate / 100m;
                decimal totalInterest = interestPerPeriod * months;
                decimal total = principal + totalInterest;
                textBoxTotalInterest.Text = totalInterest.ToString("0.00");
                textBoxTotalRepayable.Text = total.ToString("0.00");
            }
            else if (string.IsNullOrWhiteSpace(textBoxLoanAmount.Text) &&
                     string.IsNullOrWhiteSpace(textBoxInterestRate.Text) &&
                     string.IsNullOrWhiteSpace(textBoxTenure.Text))
            {
                textBoxTotalInterest.Text = "";
                textBoxTotalRepayable.Text = "";
            }
        }

        private void AmountTextBox_Leave(object? sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb != textBoxTenure && decimal.TryParse(tb.Text, out decimal value))
                    tb.Text = value.ToString("0.00");
            }
            UpdateTotalRepayable();
        }

        private void PropertyLoanForm_Load(object sender, EventArgs e)
        {
            textBoxLoanAmount.Leave += AmountTextBox_Leave;
            textBoxInterestRate.Leave += AmountTextBox_Leave;
            textBoxTenure.Leave += AmountTextBox_Leave;
            textBoxTotalInterest.Leave += AmountTextBox_Leave;
            textBoxTotalRepayable.Leave += AmountTextBox_Leave;
            textBoxTenure.KeyPress += TextBoxTenure_KeyPress;
            textBoxLoanAmount.KeyPress += TextBoxLoanAmount_KeyPress;
        }

        private void TextBoxLoanAmount_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Allow control keys, digits, and one decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // Only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox)?.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBoxTenure_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxDecimal_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Allow control keys, digits, and one decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // Only allow one decimal point
            if ((e.KeyChar == '.') && (sender is TextBox tb) && tb.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void TextBoxInteger_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}