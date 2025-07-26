using Microsoft.Data.SqlClient;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class PlotTransactionFilterForm : Form
    {
        public PlotTransactionFilterForm()
        {
            InitializeComponent();
            dateTimePickerFrom.Value = DateTime.Today.AddMonths(-1);
            dateTimePickerTo.Value = DateTime.Today;
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateTimePickerFrom.Value.Date;
            DateTime toDate = dateTimePickerTo.Value.Date.AddDays(1).AddTicks(-1); // Include the whole 'to' day

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
                SELECT 
                    pt.TransactionId,
                    pt.TransactionDate,
                    pt.TransactionType,
                    pt.Amount,
                    pt.PaymentMethod,
                    pt.ReferenceNumber,
                    pt.Notes,
                    p.PlotNumber,
                    pr.Title AS PropertyName
                FROM PlotTransaction pt
                INNER JOIN Plot p ON pt.PlotId = p.Id
                INNER JOIN Property pr ON p.PropertyId = pr.Id
                WHERE pt.IsDeleted = 0
                  AND pt.TransactionDate >= @FromDate
                  AND pt.TransactionDate <= @ToDate
                ORDER BY pt.TransactionDate ASC";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                dataGridViewResults.DataSource = dt;

                // Set custom column headers and widths
                if (dataGridViewResults.Columns["TransactionId"] != null)
                {
                    dataGridViewResults.Columns["TransactionId"].HeaderText = "TRN #";
                    dataGridViewResults.Columns["TransactionId"].Width = 80;
                    dataGridViewResults.Columns["TransactionId"].DisplayIndex = 0;
                }
                if (dataGridViewResults.Columns["PropertyName"] != null)
                {
                    dataGridViewResults.Columns["PropertyName"].HeaderText = "Property";
                    dataGridViewResults.Columns["PropertyName"].Width = 180;
                    dataGridViewResults.Columns["PropertyName"].DisplayIndex = 1;
                }
                if (dataGridViewResults.Columns["PlotNumber"] != null)
                {
                    dataGridViewResults.Columns["PlotNumber"].HeaderText = "Plot Number";
                    dataGridViewResults.Columns["PlotNumber"].Width = 120;
                    dataGridViewResults.Columns["PlotNumber"].DisplayIndex = 2;
                }
                if (dataGridViewResults.Columns["TransactionDate"] != null)
                {
                    dataGridViewResults.Columns["TransactionDate"].HeaderText = "TRN Date";
                    dataGridViewResults.Columns["TransactionDate"].Width = 180;
                    dataGridViewResults.Columns["TransactionDate"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
                }
                if (dataGridViewResults.Columns["TransactionType"] != null)
                {
                    dataGridViewResults.Columns["TransactionType"].HeaderText = "TRN Type";
                    dataGridViewResults.Columns["TransactionType"].Width = 120;
                }
                if (dataGridViewResults.Columns["Amount"] != null)
                {
                    dataGridViewResults.Columns["Amount"].HeaderText = "Amount";
                    dataGridViewResults.Columns["Amount"].Width = 130;
                    dataGridViewResults.Columns["Amount"].DefaultCellStyle.Format = "C";
                }
                if (dataGridViewResults.Columns["PaymentMethod"] != null)
                {
                    dataGridViewResults.Columns["PaymentMethod"].HeaderText = "Payment Method";
                    dataGridViewResults.Columns["PaymentMethod"].Width = 170;
                }
                if (dataGridViewResults.Columns["ReferenceNumber"] != null)
                {
                    dataGridViewResults.Columns["ReferenceNumber"].HeaderText = "Reference #";
                    dataGridViewResults.Columns["ReferenceNumber"].Width = 140;
                }
                if (dataGridViewResults.Columns["Notes"] != null)
                {
                    dataGridViewResults.Columns["Notes"].HeaderText = "Notes";
                    dataGridViewResults.Columns["Notes"].Width = 220;
                }
            }
        }
    }
}