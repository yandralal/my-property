using Microsoft.Data.SqlClient;
using RealEstateManager.Entities;
using System.Configuration;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class AgentDetailsForm : BaseForm
    {
        int _agentId;
        public AgentDetailsForm(Agent agent)
        {
            this._agentId = agent.Id;
            InitializeComponent();
            DisplayAgent(agent);
            LoadAgentTransactions(agent.Id);
            DisplayAgentFinancials(agent.Id);
            LoadPlotsSoldByAgent(agent.Id); 

            // Add event handlers for custom painting and clicks
            dataGridViewTransactions.CellPainting += DataGridViewTransactions_CellPainting;
            dataGridViewTransactions.CellMouseClick += DataGridViewTransactions_CellMouseClick;
            dataGridViewPlotsSold.CellClick += DataGridViewPlotsSold_CellClick;

            RegisterAgentTransactionForm.AgentTransactionChanged += OnAgentTransactionChanged;
        }
        public AgentDetailsForm()
        {
            InitializeComponent();
            RegisterAgentTransactionForm.AgentTransactionChanged += OnAgentTransactionChanged;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            RegisterAgentTransactionForm.AgentTransactionChanged -= OnAgentTransactionChanged;
            base.OnFormClosed(e);
        }

        private void OnAgentTransactionChanged()
        {
            LoadPlotsSoldByAgent(_agentId);
        }

        private void DisplayAgent(Agent agent)  
        {
            labelNameValue.Text = agent.Name;
            labelContactValue.Text = agent.Contact;
            labelAgencyValue.Text = agent.Agency;
            labelIdValue.Text = agent.Id.ToString();
        }

        private static DataTable GetAgentTransactions(int agentId, int? plotId = null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"
                SELECT 
                    at.TransactionDate,
                    at.TransactionType,
                    at.Amount,
                    at.PaymentMethod,
                    at.ReferenceNumber,
                    at.Notes,
                    at.TransactionId
                FROM AgentTransaction at
                WHERE at.AgentId = @AgentId AND at.IsDeleted = 0
                {0}
                ORDER BY at.TransactionDate ASC";

            string plotFilter = plotId.HasValue ? "AND at.PlotId = @PlotId" : "";
            query = string.Format(query, plotFilter);

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@AgentId", agentId);
                if (plotId.HasValue)
                    cmd.Parameters.AddWithValue("@PlotId", plotId.Value);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                return dt;
            }
        }

        private void LoadAgentTransactions(int agentId, int? plotId = null)
        {
            var transactions = GetAgentTransactions(agentId, plotId);

            dataGridViewTransactions.DataSource = null;
            dataGridViewTransactions.Columns.Clear();

            dataGridViewTransactions.AutoGenerateColumns = false;
            dataGridViewTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TransactionId",
                HeaderText = "TRN #",
                Width = 100,
                Name = "TransactionId"
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TransactionDate",
                HeaderText = "TRN Date",
                Width = 200,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy hh:mm tt" }
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TransactionType",
                HeaderText = "TRN Type",
                Width = 120,
                ReadOnly = true
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount",
                Width = 150,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "Payment Method",
                Width = 180,
                ReadOnly = true
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReferenceNumber",
                HeaderText = "Ref #",
                Width = 150,
                ReadOnly = true
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Notes",
                HeaderText = "Notes",
                Width = 260,
                ReadOnly = true
            });

            // Action column (last)
            var actionCol = new DataGridViewImageColumn
            {
                Name = "Action",
                HeaderText = "Action",
                Width = 90,
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

                var viewIcon = Properties.Resources.view;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 12;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;

                if (e.Graphics != null)
                {
                    // Draw view icon (first)
                    e.Graphics.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                    x += iconWidth + padding;

                    // Draw delete icon (second)
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
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = "UPDATE AgentTransaction SET IsDeleted = 1 WHERE TransactionId = @TransactionId";
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TransactionId", transactionId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        private void DisplayAgentFinancials(int agentId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
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

        private void LoadPlotsSoldByAgent(int agentId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"
                SELECT 
                    p.Id AS PlotId,
                    pr.Title AS PropertyName,
                    p.PlotNumber,
                    ps.SaleDate,
                    ps.BrokerageAmount,
                    (SELECT ISNULL(SUM(Amount), 0) FROM AgentTransaction WHERE PlotId = p.Id AND AgentId = @AgentId AND IsDeleted = 0) AS Paid,
                    (ps.BrokerageAmount - (SELECT ISNULL(SUM(Amount), 0) FROM AgentTransaction WHERE PlotId = p.Id AND AgentId = @AgentId AND IsDeleted = 0)) AS Balance
                FROM PlotSale ps
                INNER JOIN Plot p ON ps.PlotId = p.Id
                INNER JOIN Property pr ON p.PropertyId = pr.Id
                WHERE ps.AgentId = @AgentId AND ps.IsDeleted = 0
                ORDER BY TRY_CAST(p.PlotNumber AS INT) ASC, p.PlotNumber ASC";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@AgentId", agentId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);

                dataGridViewPlotsSold.DataSource = null;
                dataGridViewPlotsSold.Columns.Clear();
                dataGridViewPlotsSold.AutoGenerateColumns = false;

                // Do NOT add PlotId as a visible column, but keep it in the DataTable for logic
                dataGridViewPlotsSold.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PlotId",
                    HeaderText = "PlotId",
                    Visible = false
                });
                dataGridViewPlotsSold.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PropertyName",
                    HeaderText = "Property",
                    Width = 180
                });
                dataGridViewPlotsSold.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PlotNumber",
                    HeaderText = "Plot",
                    Width = 100
                });
                dataGridViewPlotsSold.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SaleDate",
                    HeaderText = "Sale Date",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy hh:mm tt" }
                });
                dataGridViewPlotsSold.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "BrokerageAmount",
                    HeaderText = "Brokerage Amount",
                    Width = 110,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
                });
                dataGridViewPlotsSold.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Paid",
                    HeaderText = "Amount Paid",
                    Width = 110,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
                });
                dataGridViewPlotsSold.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Balance",
                    HeaderText = "Amount Balance",
                    Width = 110,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
                });

                dataGridViewPlotsSold.DataSource = dt;

                // Select the first row and show its transactions
                if (dataGridViewPlotsSold.Rows.Count > 0)
                {
                    dataGridViewPlotsSold.ClearSelection();
                    dataGridViewPlotsSold.Rows[0].Selected = true;

                    var firstRow = dataGridViewPlotsSold.Rows[0];
                    if (firstRow.DataBoundItem is DataRowView drv && drv.Row.Table.Columns.Contains("PlotId"))
                    {
                        int plotId = Convert.ToInt32(drv["PlotId"]);
                        LoadAgentTransactions(_agentId, plotId);
                    }
                }
            }
        }

        private void DataGridViewPlotsSold_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewPlotsSold.Rows[e.RowIndex];
                if (row.DataBoundItem is DataRowView drv && drv.Row.Table.Columns.Contains("PlotId"))
                {
                    int plotId = Convert.ToInt32(drv["PlotId"]);
                    LoadAgentTransactions(_agentId, plotId);
                }
            }
        }
    }
}