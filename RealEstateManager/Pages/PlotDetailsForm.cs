using Microsoft.Data.SqlClient;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class PlotDetailsForm : BaseForm
    {
        private readonly int _plotId;

        public PlotDetailsForm(int plotId)
        {
            InitializeComponent();
            _plotId = plotId;
            LoadPlotDetails();
        }

        private DataTable GetPlotTransactions(int plotId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
            SELECT 
                TransactionId,
                TransactionDate,
                Amount,
                PaymentMethod,
                ReferenceNumber,
                Notes
            FROM PlotTransaction
            WHERE PlotId = @PlotId AND IsDeleted = 0
            ORDER BY TransactionDate ASC";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PlotId", plotId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                return dt;
            }
        }

        private void LoadPlotDetails()
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
                SELECT 
                    p.Id, p.PlotNumber, p.Status, p.Area, p.PropertyId,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.IsDeleted,
                    ps.SaleDate, ps.SaleAmount, 
                    ps.CustomerName, ps.CustomerPhone, ps.CustomerEmail
                FROM Plot p
                LEFT JOIN PropertySale ps ON p.Id = ps.PlotId
                WHERE p.Id = @PlotId";

            // Variables to hold values for use outside the reader scope
            string customerName = "";
            string customerPhone = "";
            string customerEmail = "";
            string saleAmountStr = "";
            decimal amountPaid = 0;
            decimal balance = 0;

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PlotId", _plotId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        labelPlotId.Text = reader["Id"].ToString();
                        labelPlotNumber.Text = reader["PlotNumber"].ToString();
                        labelStatus.Text = reader["Status"].ToString();
                        labelArea.Text = reader["Area"].ToString();
                        labelCreatedBy.Text = reader["CreatedBy"].ToString();
                        labelCreatedDate.Text = reader["CreatedDate"] is DateTime cd ? cd.ToString("g") : "";
                        labelModifiedBy.Text = reader["ModifiedBy"].ToString();
                        labelModifiedDate.Text = reader["ModifiedDate"] is DateTime md ? md.ToString("g") : "";
                        labelIsDeleted.Text = reader["IsDeleted"] is bool isDel ? isDel.ToString() : "";
                        labelSaleDate.Text = reader["SaleDate"] is DateTime sd ? sd.ToShortDateString() : "";

                        customerName = reader["CustomerName"]?.ToString() ?? "";
                        customerPhone = reader["CustomerPhone"]?.ToString() ?? "";
                        customerEmail = reader["CustomerEmail"]?.ToString() ?? "";
                        saleAmountStr = reader["SaleAmount"]?.ToString() ?? "";

                        // Get paid amount and balance from PlotTransaction
                        string transQuery = @"
                            SELECT ISNULL(SUM(Amount), 0) AS PaidAmount
                            FROM PlotTransaction
                            WHERE PlotId = @PlotId AND IsDeleted = 0";
                        using (var transConn = new SqlConnection(connectionString))
                        using (var transCmd = new SqlCommand(transQuery, transConn))
                        {
                            transCmd.Parameters.AddWithValue("@PlotId", _plotId);
                            transConn.Open();
                            var paidObj = transCmd.ExecuteScalar();
                            amountPaid = paidObj != DBNull.Value ? Convert.ToDecimal(paidObj) : 0;
                        }
                        decimal saleAmount = reader["SaleAmount"] is decimal sa ? sa : 0;
                        balance = saleAmount - amountPaid;

                        labelPaidAmount.Text = amountPaid.ToString("N2");
                        labelBalanceAmount.Text = balance.ToString("N2");
                    }
                }
            }

            // Load transactions and bind to grid
            var transactions = GetPlotTransactions(_plotId);
            dataGridViewTransactions.DataSource = transactions;

            if (dataGridViewTransactions.Columns["TransactionId"] != null)
                dataGridViewTransactions.Columns["TransactionId"].HeaderText = "Transaction ID";
            if (dataGridViewTransactions.Columns["TransactionDate"] != null)
                dataGridViewTransactions.Columns["TransactionDate"].HeaderText = "Transaction Date";
            if (dataGridViewTransactions.Columns["Amount"] != null)
                dataGridViewTransactions.Columns["Amount"].HeaderText = "Amount";
            if (dataGridViewTransactions.Columns["PaymentMethod"] != null)
                dataGridViewTransactions.Columns["PaymentMethod"].HeaderText = "Payment Method";
            if (dataGridViewTransactions.Columns["ReferenceNumber"] != null)
                dataGridViewTransactions.Columns["ReferenceNumber"].HeaderText = "Reference Number";
            if (dataGridViewTransactions.Columns["Notes"] != null)
                dataGridViewTransactions.Columns["Notes"].HeaderText = "Notes";

            // Set label texts with prefix
            labelCustomerName.Text = customerName;
            labelCustomerPhone.Text = customerPhone;
            labelCustomerEmail.Text = customerEmail;
            labelSaleAmount.Text = saleAmountStr;
            labelPaidAmount.Text = amountPaid.ToString("N2");
            labelBalanceAmount.Text = balance.ToString("N2");
        }
    }
}