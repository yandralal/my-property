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
            dataGridViewTransactions.DataBindingComplete += DataGridViewTransactions_DataBindingComplete;
            dataGridViewTransactions.CellPainting += dataGridViewTransactions_CellPainting;
            dataGridViewTransactions.CellMouseClick += DataGridViewTransactions_CellMouseClick;
        }

        private DataTable GetPlotTransactions(int plotId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
            SELECT 
                TransactionId,
                TransactionDate,
                TransactionType,      -- Add this line
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
                LEFT JOIN PlotSale ps ON p.Id = ps.PlotId
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
                        balance = (reader["SaleAmount"] is decimal sa ? sa : 0) - amountPaid;

                        labelPaidAmount.Text = amountPaid.ToString("N2");
                        labelBalanceAmount.Text = balance.ToString("N2");
                    }
                }
            }

            // Load transactions and bind to grid
            var transactions = GetPlotTransactions(_plotId);
            dataGridViewTransactions.DataSource = transactions;
            dataGridViewTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set label texts with prefix
            labelCustomerName.Text = customerName;
            labelCustomerPhone.Text = customerPhone;
            labelCustomerEmail.Text = customerEmail;
            labelSaleAmount.Text = decimal.TryParse(saleAmountStr, out var saleAmount) ? saleAmount.ToString("N2") : "0.00";
            labelPaidAmount.Text = amountPaid.ToString("N2");
            labelBalanceAmount.Text = balance.ToString("N2");
        }

        private void DataGridViewTransactions_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dataGridViewTransactions;

            // Set to None so custom widths are respected
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // Add action column if not already present
            if (!dgv.Columns.Contains("Action"))
            {
                var actionCol = new DataGridViewImageColumn
                {
                    Name = "Action",
                    HeaderText = "Action",
                    Width = 120,
                    ImageLayout = DataGridViewImageCellLayout.Normal
                };
                dgv.Columns.Add(actionCol);
            }

            if (dgv.Columns["TransactionId"] != null)
            {
                dgv.Columns["TransactionId"].HeaderText = "TRN #";
                dgv.Columns["TransactionId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["TransactionId"].Width = 80;
            }
            if (dgv.Columns["TransactionDate"] != null)
            {
                dgv.Columns["TransactionDate"].HeaderText = "TRN Date";
                dgv.Columns["TransactionDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["TransactionDate"].Width = 180;
                dgv.Columns["TransactionDate"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
            }
            if (dgv.Columns["TransactionType"] != null)
            {
                dgv.Columns["TransactionType"].HeaderText = "TRN Type";
                dgv.Columns["TransactionType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["TransactionType"].Width = 100;
            }
            if (dgv.Columns["Amount"] != null)
            {
                dgv.Columns["Amount"].HeaderText = "Amount";
                dgv.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Amount"].Width = 130;
            }
            if (dgv.Columns["PaymentMethod"] != null)
            {
                dgv.Columns["PaymentMethod"].HeaderText = "Payment Method";
                dgv.Columns["PaymentMethod"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["PaymentMethod"].Width = 170;
            }
            if (dgv.Columns["ReferenceNumber"] != null)
            {
                dgv.Columns["ReferenceNumber"].HeaderText = "Reference #";
                dgv.Columns["ReferenceNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["ReferenceNumber"].Width = 140;
            }
            if (dgv.Columns["Notes"] != null)
            {
                dgv.Columns["Notes"].HeaderText = "Notes";
                dgv.Columns["Notes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Notes"].Width = 280;
            }
            if (dgv.Columns["Action"] != null)
            {
                dgv.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Action"].Width = 120;
            }
        }

        private void dataGridViewTransactions_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewTransactions.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Load your icons from resources
                var viewIcon = Properties.Resources.view;      // Replace with your actual resource names
                var editIcon = Properties.Resources.edit;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 12;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;

                // Draw view icon
                e.Graphics.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw edit icon
                e.Graphics.DrawImage(editIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw delete icon
                e.Graphics.DrawImage(deleteIcon, new Rectangle(x, y, iconWidth, iconHeight));

                e.Handled = true;
            }
        }

        private void DataGridViewTransactions_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewTransactions.Columns[e.ColumnIndex].Name == "Action")
            {
                int iconWidth = 24, padding = 12;
                int x = e.X - padding;
                int iconIndex = x / (iconWidth + padding);

                var row = dataGridViewTransactions.Rows[e.RowIndex];
                var transactionId = row.Cells["TransactionId"].Value?.ToString();

                switch (iconIndex)
                {
                    case 0:
                        ViewTransaction(transactionId);
                        break;
                    case 1:
                        EditTransaction(transactionId);
                        break;
                    case 2:
                        DeleteTransaction(transactionId);
                        break;
                }
            }
        }

        private void ViewTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            var form = new RegisterPlotTransactionForm(transactionId, readOnly: true);
            form.ShowDialog();
        }

        private void EditTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            var form = new RegisterPlotTransactionForm(transactionId, readOnly: false);
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Optionally refresh the grid here
                var transactions = GetPlotTransactions(_plotId);
                dataGridViewTransactions.DataSource = transactions;
            }
        }

        private void DeleteTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            if (MessageBox.Show("Are you sure you want to delete this transaction?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
                string query = "UPDATE PlotTransaction SET IsDeleted = 1 WHERE TransactionId = @TransactionId";
                try
                {
                    using (var conn = new SqlConnection(connectionString))
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Transaction deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the grid in this form
                    var transactions = GetPlotTransactions(_plotId);
                    dataGridViewTransactions.DataSource = transactions;

                    // Refresh plot grid in LandingForm if open
                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm is LandingForm landingForm)
                        {
                            // Find the propertyId for this plot
                            int? propertyId = null;
                            string propertyIdQuery = "SELECT PropertyId FROM Plot WHERE Id = @PlotId";
                            using (var conn = new SqlConnection(connectionString))
                            using (var cmd = new SqlCommand(propertyIdQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@PlotId", _plotId);
                                conn.Open();
                                var result = cmd.ExecuteScalar();
                                if (result != null && result != DBNull.Value)
                                {
                                    propertyId = Convert.ToInt32(result);
                                }
                            }
                            if (propertyId.HasValue)
                            {
                                landingForm.LoadPlotsForProperty(propertyId.Value);
                            }
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting transaction: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}