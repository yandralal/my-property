using Microsoft.Data.SqlClient;
using System.Data;

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
            dataGridViewTransactions.CellPainting += dataGridViewTransactions_CellPainting;
            dataGridViewTransactions.CellMouseClick += DataGridViewTransactions_CellMouseClick;
            LoadPropertyDetails();
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
                dgv.Columns["TransactionId"].HeaderText = "TRN ID";
                dgv.Columns["TransactionId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["TransactionId"].Width = 80;
            }
            if (dgv.Columns["Date"] != null)
            {
                dgv.Columns["Date"].HeaderText = "TRN Date";
                dgv.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Date"].Width = 180;
                dgv.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
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
            var form = new RegisterPropertyTransactionForm(transactionId, readOnly: true);
            form.ShowDialog();
        }

        private void EditTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            var form = new RegisterPropertyTransactionForm(transactionId, readOnly: false);
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Optionally refresh the grid here
                var transactions = GetPropertyTransactions(_propertyId);
                dataGridViewTransactions.DataSource = transactions;
            }
        }

        private void DeleteTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            if (MessageBox.Show("Are you sure you want to delete this transaction?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
                string query = "UPDATE PropertyTransaction SET IsDeleted = 1 WHERE TransactionId = @TransactionId";
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
                    var transactions = GetPropertyTransactions(_propertyId);
                    dataGridViewTransactions.DataSource = transactions;

                    // Optionally refresh property grid in LandingForm if open
                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm is LandingForm landingForm)
                        {
                            landingForm.LoadActiveProperties();
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

        private void LoadPropertyDetails()
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string propertyQuery = @"
                SELECT 
                    Title, Type, Status, Price, Owner, Phone, Address, City, State, ZipCode, Description,
                    ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0) AS AmountPaid,
                    (Price - ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0)) AS AmountBalance
                FROM Property p
                WHERE Id = @Id AND IsDeleted = 0";
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
                        labelTitleValue.Text        = reader["Title"].ToString();
                        labelTypeValue.Text         = reader["Type"].ToString();
                        labelStatusValue.Text       = reader["Status"].ToString();
                        labelOwnerValue.Text        = reader["Owner"].ToString();
                        labelPhoneValue.Text        = reader["Phone"].ToString();
                        labelAddressValue.Text      = reader["Address"].ToString();
                        labelCityValue.Text         = reader["City"].ToString();
                        labelStateValue.Text        = reader["State"].ToString();
                        labelZipValue.Text          = reader["ZipCode"].ToString();
                        labelDescriptionValue.Text  = reader["Description"].ToString();

                        // Add these lines for Amount Paid and Balance
                        decimal amountPaid = reader["AmountPaid"] is decimal ap ? ap : 0;
                        decimal amountBalance = reader["AmountBalance"] is decimal ab ? ab : 0;
                        labelPropertyBuyPrice.Text = string.Format("{0:C}", reader["Price"]);
                        labelPropertyAmountPaid.Text = amountPaid.ToString("N2");
                        labelPropertyBalance.Text = amountBalance.ToString("N2");
                    }
                }

                // Load property transactions
                using (var adapter = new SqlDataAdapter(transactionCmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewTransactions.DataSource = dt;
                }
            }
        }

        private DataTable GetPropertyTransactions(int propertyId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
                SELECT 
                    TransactionId,  
                    TransactionDate AS [Date], 
                    Amount, 
                    TransactionType,    -- Debit or Credit
                    PaymentMethod, 
                    ReferenceNumber, 
                    Notes
                FROM PropertyTransaction
                WHERE PropertyId = @PropertyId AND IsDeleted = 0
                ORDER BY TransactionDate DESC";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                return dt;
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
    }
}