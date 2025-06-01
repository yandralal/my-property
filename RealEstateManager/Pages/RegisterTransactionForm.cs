using Microsoft.Data.SqlClient;

namespace RealEstateManager.Pages
{
    public partial class RegisterTransactionForm : Form
    {
        private readonly int? _plotId;
        private readonly decimal? _saleAmount;
        private readonly string? _plotNumber;
        private decimal _amountPaidTillDate = 0;

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
        }

        private decimal GetAmountPaidTillDate(int plotId)
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(textBoxAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string paymentMethod = comboBoxPaymentMethod.Text;
            string referenceNumber = textBoxReferenceNumber.Text;
            string notes = textBoxNotes.Text;
            string createdBy = Environment.UserName;

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string insert = @"
                INSERT INTO PlotTransaction
                (PlotId, TransactionDate, Amount, PaymentMethod, ReferenceNumber, Notes, CreatedBy, CreatedDate, IsDeleted)
                VALUES
                (@PlotId, @TransactionDate, @Amount, @PaymentMethod, @ReferenceNumber, @Notes, @CreatedBy, @CreatedDate, 0)";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@PlotId", _plotId);
                cmd.Parameters.AddWithValue("@TransactionDate", dateTimePickerTransactionDate.Value);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                cmd.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                cmd.Parameters.AddWithValue("@Notes", notes);
                cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Transaction registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
    }
}