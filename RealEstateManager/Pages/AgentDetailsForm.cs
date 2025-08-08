using Microsoft.Data.SqlClient;
using RealEstateManager.Entities;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class AgentDetailsForm : BaseForm
    {
        public AgentDetailsForm(Agent agent)
        {
            InitializeComponent();
            DisplayAgent(agent);
            LoadAgentTransactions(agent.Id);
            DisplayAgentFinancials(agent.Id);

            // Add event handlers for custom painting and clicks
            dataGridViewTransactions.CellPainting += DataGridViewTransactions_CellPainting;
            dataGridViewTransactions.CellMouseClick += DataGridViewTransactions_CellMouseClick;
        }
        private void DisplayAgent(Agent agent)
        {
            labelNameValue.Text = agent.Name;
            labelContactValue.Text = agent.Contact;
            labelAgencyValue.Text = agent.Agency;
            labelIdValue.Text = agent.Id.ToString();
        }

        private static DataTable GetAgentTransactions(int agentId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
                SELECT 
                    pr.Title AS PropertyName,
                    pl.PlotNumber AS PlotNumber,
                    at.TransactionDate,
                    at.TransactionType,
                    at.Amount,
                    at.PaymentMethod,
                    at.ReferenceNumber,
                    at.Notes,
                    at.TransactionId
                FROM AgentTransaction at
                LEFT JOIN Plot pl ON at.PlotId = pl.Id
                LEFT JOIN Property pr ON pl.PropertyId = pr.Id
                WHERE at.AgentId = @AgentId AND at.IsDeleted = 0
                ORDER BY at.TransactionDate ASC";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@AgentId", agentId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                return dt;
            }
        }

        private void LoadAgentTransactions(int agentId)
        {
            var transactions = GetAgentTransactions(agentId);

            dataGridViewTransactions.DataSource = null;
            dataGridViewTransactions.Columns.Clear();

            dataGridViewTransactions.AutoGenerateColumns = false;
            dataGridViewTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TransactionId",
                HeaderText = "TRN #",
                Width = 70,
                Name = "TransactionId"
            });

            var col = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PropertyName",
                HeaderText = "Property",
                Width = 130,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None // This line is important
            };
            dataGridViewTransactions.Columns.Add(col);

            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PlotNumber",
                HeaderText = "Plot #",
                Width = 100,
                ReadOnly = true
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TransactionDate",
                HeaderText = "TRN Date",
                Width = 180,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy hh:mm tt" }
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TransactionType",
                HeaderText = "TRN Type",
                Width = 100,
                ReadOnly = true
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "Payment Method",
                Width = 160,
                ReadOnly = true
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReferenceNumber",
                HeaderText = "Ref #",
                Width = 120,
                ReadOnly = true
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Notes",
                HeaderText = "Notes",
                Width = 160,
                ReadOnly = true
            });

            // Action column (last)
            var actionCol = new DataGridViewImageColumn
            {
                Name = "Action",
                HeaderText = "Action",
                Width = 120,
                ImageLayout = DataGridViewImageCellLayout.Normal
            };
            dataGridViewTransactions.Columns.Add(actionCol);

            dataGridViewTransactions.DataSource = transactions;
        }

        private void DataGridViewTransactions_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewTransactions.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Replace with your actual resource icons
                var viewIcon = Properties.Resources.view;
                var editIcon = Properties.Resources.edit;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 12;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;

                if (e.Graphics != null)
                {
                    // Draw view icon
                    e.Graphics.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                    x += iconWidth + padding;

                    // Draw edit icon
                    e.Graphics.DrawImage(editIcon, new Rectangle(x, y, iconWidth, iconHeight));
                    x += iconWidth + padding;

                    // Draw delete icon
                    e.Graphics.DrawImage(deleteIcon, new Rectangle(x, y, iconWidth, iconHeight));
                }

                e.Handled = true;
            }
        }

        // Handle clicks on the action column
        private void DataGridViewTransactions_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewTransactions.Columns[e.ColumnIndex].Name == "Action")
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

        private static void ViewTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            // Open RegisterAgentTransactionForm in view mode
            using var form = new RegisterAgentTransactionForm(transactionId, true);
            form.ShowDialog();
        }

        private void EditTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            // Open RegisterAgentTransactionForm in edit mode
            using var form = new RegisterAgentTransactionForm(transactionId, false);
            if (form.ShowDialog() == DialogResult.OK)
            {
                int agentId = int.Parse(labelIdValue.Text);
                LoadAgentTransactions(agentId);
                DisplayAgentFinancials(agentId);
            }
        }

        private void DeleteTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            if (MessageBox.Show("Are you sure you want to delete this transaction?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // Implement your delete logic here
                DeleteTransactionFromDatabase(transactionId); // <-- implement this method to actually delete from DB

                MessageBox.Show($"Deleted Transaction: {transactionId}", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the grid and agent financials
                int agentId = int.Parse(labelIdValue.Text); // or keep agentId as a field
                LoadAgentTransactions(agentId);
                DisplayAgentFinancials(agentId);
            }
        }

        // Example implementation for deleting from DB
        private static void DeleteTransactionFromDatabase(string transactionId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "UPDATE AgentTransaction SET IsDeleted = 1 WHERE TransactionId = @TransactionId";
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TransactionId", transactionId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        private void DisplayAgentFinancials(int agentId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            decimal totalBrokerage = 0, amountPaid = 0;

            // Total Brokerage
            string brokerageQuery = @"
                SELECT ISNULL(SUM(BrokerageAmount), 0)
                FROM PlotSale
                WHERE AgentId = @AgentId AND IsDeleted = 0";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(brokerageQuery, conn))
            {
                cmd.Parameters.AddWithValue("@AgentId", agentId);
                conn.Open();
                var result = cmd.ExecuteScalar();
                totalBrokerage = result != null ? Convert.ToDecimal(result) : 0;
            }

            // Amount Paid
            string paidQuery = @"
                SELECT ISNULL(SUM(Amount), 0)
                FROM AgentTransaction
                WHERE AgentId = @AgentId AND IsDeleted = 0";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(paidQuery, conn))
            {
                cmd.Parameters.AddWithValue("@AgentId", agentId);
                conn.Open();
                var result = cmd.ExecuteScalar();
                amountPaid = result != null ? Convert.ToDecimal(result) : 0;
            }

            decimal balance = totalBrokerage - amountPaid;

            labelTotalBrokerageValue.Text = totalBrokerage.ToString("N2");
            labelAmountPaidValue.Text = amountPaid.ToString("N2");
            labelBalanceValue.Text = balance.ToString("N2");
        }
    }
}