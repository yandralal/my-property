using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;

namespace RealEstateManager.Pages
{
    public partial class MiscTransactionDetailForm : BaseForm
    {
        public MiscTransactionDetailForm()
        {
            InitializeComponent();

            // Wire up grid events
            dataGridViewMiscTransactions.DataBindingComplete += DataGridViewMiscTransactions_DataBindingComplete;
            dataGridViewMiscTransactions.CellPainting += DataGridViewMiscTransactions_CellPainting;
            dataGridViewMiscTransactions.CellMouseClick += DataGridViewMiscTransactions_CellMouseClick;

            dataGridViewMiscTransactions.ReadOnly = true;
            dataGridViewMiscTransactions.DefaultCellStyle.ForeColor = Color.Gray; 

            LoadMiscTransactions();
        }

        private void LoadMiscTransactions()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"
                SELECT 
                    TransactionId,
                    CreatedDate,
                    TransactionType,
                    Amount,
                    Recipient,
                    PaymentMethod,
                    ReferenceNumber,
                    Notes
                FROM MiscTransaction
                WHERE IsDeleted = 0
                ORDER BY CreatedDate DESC";

            var dt = new DataTable();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                conn.Open();
                adapter.Fill(dt);
            }

            dataGridViewMiscTransactions.DataSource = dt;
        }

        private void DataGridViewMiscTransactions_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewMiscTransactions.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                var viewIcon = Properties.Resources.view;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 12;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;

                e.Graphics?.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;
                e.Graphics?.DrawImage(deleteIcon, new Rectangle(x, y, iconWidth, iconHeight));

                e.Handled = true;
            }
        }

        private void DataGridViewMiscTransactions_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridViewMiscTransactions.Columns[e.ColumnIndex].Name != "Action") return;

            int iconWidth = 24, padding = 12;
            int x = e.X - padding;
            int iconIndex = x / (iconWidth + padding);

            var row = dataGridViewMiscTransactions.Rows[e.RowIndex];
            var transactionId = row.Cells["TransactionId"].Value?.ToString();

            switch (iconIndex)
            {
                case 0:
                    ViewTransaction(transactionId);
                    break;
                case 1:
                    DeleteTransaction(transactionId);
                    break;
            }
        }

        private void ViewTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;

            var viewForm = new RegisterMiscTransactionForm(transactionId, true);
            viewForm.ShowDialog();
        }

        private void DeleteTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            if (MessageBox.Show("Are you sure you want to delete this transaction?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
                string query = "UPDATE MiscTransaction SET IsDeleted = 1 WHERE TransactionId = @TransactionId";
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Transaction deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMiscTransactions();
            }
        }

        // You can add any post-binding logic here, e.g. formatting, selection, etc.
        private void DataGridViewMiscTransactions_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dataGridViewMiscTransactions;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (!dgv.Columns.Contains("Action"))
            {
                var actionCol = new DataGridViewImageColumn
                {
                    Name = "Action",
                    HeaderText = "Action",
                    Width = 90,
                    ImageLayout = DataGridViewImageCellLayout.Normal
                };
                dgv.Columns.Add(actionCol);
            }

            if (dgv.Columns["TransactionId"] != null)
            {
                dgv.Columns["TransactionId"].HeaderText = "TRN #";
                dgv.Columns["TransactionId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["TransactionId"].Width = 100;
            }
            if (dgv.Columns["CreatedDate"] != null)
            {
                dgv.Columns["CreatedDate"].HeaderText = "TRN Date";
                dgv.Columns["CreatedDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["CreatedDate"].Width = 180;
                dgv.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
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
                dgv.Columns["Amount"].Width = 150;
                dgv.Columns["Amount"].DefaultCellStyle.Format = "C";
            }
            if (dgv.Columns["PaymentMethod"] != null)
            {
                dgv.Columns["PaymentMethod"].HeaderText = "Payment Method";
                dgv.Columns["PaymentMethod"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["PaymentMethod"].Width = 160;
            }
            if (dgv.Columns["ReferenceNumber"] != null)
            {
                dgv.Columns["ReferenceNumber"].HeaderText = "Reference #";
                dgv.Columns["ReferenceNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["ReferenceNumber"].Width = 120;
            }
            if (dgv.Columns["Recipient"] != null)
            {
                dgv.Columns["Recipient"].HeaderText = "Recipient";
                dgv.Columns["Recipient"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Recipient"].Width = 160;
            }
            if (dgv.Columns["Notes"] != null)
            {
                dgv.Columns["Notes"].HeaderText = "Notes";
                dgv.Columns["Notes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Notes"].Width = 180;
            }
            if (dgv.Columns["Action"] != null)
            {
                dgv.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Action"].Width = 90;
                dgv.Columns["Action"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
}