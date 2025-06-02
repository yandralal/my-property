using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RealEstateManager.Pages
{
    public partial class PropertyDetailsForm : BaseForm
    {
        private readonly int _propertyId;

        public PropertyDetailsForm(int propertyId)
        {
            InitializeComponent();
            _propertyId = propertyId;
            dataGridViewTransactions.DataBindingComplete += DataGridViewTransactions_DataBindingComplete;
            dataGridViewTransactions.CellContentClick += DataGridViewTransactions_CellContentClick;
            LoadPropertyDetails();
        }

        private void DataGridViewTransactions_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dataGridViewTransactions;

            // Add action column if not already present
            if (!dgv.Columns.Contains("Action"))
            {
                var actionCol = new DataGridViewButtonColumn
                {
                    Name = "Action",
                    HeaderText = "Actions",
                    Text = "View/Edit/Delete",
                    UseColumnTextForButtonValue = false,
                    Width = 180
                };
                dgv.Columns.Add(actionCol);
            }

            // Set column headers and widths as before
            if (dgv.Columns["TransactionId"] != null)
            {
                dgv.Columns["TransactionId"].HeaderText = "TRN ID";
                dgv.Columns["TransactionId"].Width = 70;
            }
            if (dgv.Columns["Date"] != null)
            {
                dgv.Columns["Date"].HeaderText = "Date";
                dgv.Columns["Date"].Width = 120;
            }
            if (dgv.Columns["Amount"] != null)
            {
                dgv.Columns["Amount"].HeaderText = "Amount";
                dgv.Columns["Amount"].Width = 130;
            }
            if (dgv.Columns["TransactionType"] != null)
            {
                dgv.Columns["TransactionType"].HeaderText = "TRN Type";
                dgv.Columns["TransactionType"].Width = 120;
            }
            if (dgv.Columns["PaymentMethod"] != null)
            {
                dgv.Columns["PaymentMethod"].HeaderText = "Payment Method";
                dgv.Columns["PaymentMethod"].Width = 160;
            }
            if (dgv.Columns["ReferenceNumber"] != null)
            {
                dgv.Columns["ReferenceNumber"].HeaderText = "Reference Number";
                dgv.Columns["ReferenceNumber"].Width = 160;
            }
            if (dgv.Columns["Notes"] != null)
            {
                dgv.Columns["Notes"].HeaderText = "Notes";
                dgv.Columns["Notes"].Width = 280;
            }
        }

        private void DataGridViewTransactions_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridViewTransactions.Columns[e.ColumnIndex].Name != "Action")
                return;

            var row = dataGridViewTransactions.Rows[e.RowIndex];
            var transactionId = row.Cells["TransactionId"].Value?.ToString();

            // Show a context menu for actions
            var menu = new ContextMenuStrip();
            menu.Items.Add("View", null, (s, ea) => ViewTransaction(transactionId));
            menu.Items.Add("Edit", null, (s, ea) => EditTransaction(transactionId));
            menu.Items.Add("Delete", null, (s, ea) => DeleteTransaction(transactionId));
            var cellDisplayRectangle = dataGridViewTransactions.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            var location = dataGridViewTransactions.PointToScreen(new Point(cellDisplayRectangle.Left, cellDisplayRectangle.Bottom));
            menu.Show(location);
        }

        private void ViewTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            MessageBox.Show($"View Transaction: {transactionId}", "View", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // TODO: Implement view logic
        }

        private void EditTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            MessageBox.Show($"Edit Transaction: {transactionId}", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // TODO: Implement edit logic
        }

        private void DeleteTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            if (MessageBox.Show($"Are you sure you want to delete transaction {transactionId}?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // TODO: Implement delete logic
                MessageBox.Show($"Deleted Transaction: {transactionId}", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadPropertyDetails()
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string propertyQuery = @"SELECT Title, Type, Status, Price, Owner, Phone, Address, City, State, ZipCode, Description
                                     FROM Property WHERE Id = @Id AND IsDeleted = 0";
            string transactionQuery = @"
                SELECT 
                    TransactionId,  
                    TransactionDate AS [Date], 
                    Amount, 
                    TransactionType,    -- Debit or Credit
                    PaymentMethod, 
                    ReferenceNumber, 
                    Notes
                FROM PropertyTransaction
                WHERE PropertyId = @Id AND IsDeleted = 0
                ORDER BY TransactionDate DESC";

            using (var conn = new SqlConnection(connectionString))
            using (var propertyCmd = new SqlCommand(propertyQuery, conn))
            using (var transactionCmd = new SqlCommand(transactionQuery, conn))
            {
                propertyCmd.Parameters.AddWithValue("@Id", _propertyId);
                transactionCmd.Parameters.AddWithValue("@Id", _propertyId);

                conn.Open();

                // Load property details
                using (var reader = propertyCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        labelTitleValue.Text        = $"Title: {reader["Title"]}";
                        labelTypeValue.Text         = $"Type: {reader["Type"]}";
                        labelStatusValue.Text       = $"Status: {reader["Status"]}";
                        labelPriceValue.Text        = $"Price: {reader["Price"]:C}";
                        labelOwnerValue.Text        = $"Owner: {reader["Owner"]}";
                        labelPhoneValue.Text        = $"Phone: {reader["Phone"]}";
                        labelAddressValue.Text      = $"Address: {reader["Address"]}";
                        labelCityValue.Text         = $"City: {reader["City"]}";
                        labelStateValue.Text        = $"State: {reader["State"]}";
                        labelZipValue.Text          = $"Zip: {reader["ZipCode"]}";
                        labelDescriptionValue.Text  = $"Description: {reader["Description"]}";
                    }
                }

                // Load property transactions
                using (var adapter = new SqlDataAdapter(transactionCmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewTransactions.DataSource = dt;

                    // Consistent look and feel
                    dataGridViewTransactions.EnableHeadersVisualStyles = false;
                    dataGridViewTransactions.ColumnHeadersDefaultCellStyle.BackColor = Color.MidnightBlue;
                    dataGridViewTransactions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dataGridViewTransactions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    dataGridViewTransactions.DefaultCellStyle.BackColor = Color.White;
                    dataGridViewTransactions.DefaultCellStyle.ForeColor = Color.MidnightBlue;
                    dataGridViewTransactions.DefaultCellStyle.SelectionBackColor = Color.LightCyan;
                    dataGridViewTransactions.DefaultCellStyle.SelectionForeColor = Color.Black;
                    dataGridViewTransactions.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                    dataGridViewTransactions.GridColor = Color.LightSteelBlue;
                    dataGridViewTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                }
            }
        }
    }
}