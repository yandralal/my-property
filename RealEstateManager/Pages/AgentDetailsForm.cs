using RealEstateManager.Entities;
using System;
using System.Windows.Forms;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;

namespace RealEstateManager.Pages
{
    public partial class AgentDetailsForm : BaseForm
    {
        public AgentDetailsForm(Agent agent)
        {
            InitializeComponent();
            InitializeAgentDetailsGroup();
            InitializeTransactionGridGroup();
            DisplayAgent(agent);
            LoadAgentTransactions(agent.Id);

            // Add event handlers for custom painting and clicks
            dataGridViewTransactions.CellPainting += DataGridViewTransactions_CellPainting;
            dataGridViewTransactions.CellMouseClick += DataGridViewTransactions_CellMouseClick;
        }

        private void InitializeAgentDetailsGroup()
        {
            groupBoxAgentDetails = new GroupBox
            {
                Text = "Agent Details",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.MidnightBlue,
                BackColor = Color.AliceBlue,
                Location = new Point(20, 20),
                Size = new Size(1280, 120),
                Padding = new Padding(15)
            };

            labelIdTitle = new Label
            {
                Text = "Agent ID:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                Location = new Point(20, 40),
                AutoSize = true
            };
            labelIdValue = new Label
            {
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Location = new Point(120, 40),
                AutoSize = true
            };

            labelNameTitle = new Label
            {
                Text = "Name:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                Location = new Point(300, 40),
                AutoSize = true
            };
            labelNameValue = new Label
            {
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Location = new Point(370, 40),
                AutoSize = true
            };

            labelContactTitle = new Label
            {
                Text = "Contact:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                Location = new Point(600, 40),
                AutoSize = true
            };
            labelContactValue = new Label
            {
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Location = new Point(690, 40),
                AutoSize = true
            };

            labelAgencyTitle = new Label
            {
                Text = "Agency:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                Location = new Point(900, 40),
                AutoSize = true
            };
            labelAgencyValue = new Label
            {
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Location = new Point(980, 40),
                AutoSize = true
            };

            groupBoxAgentDetails.Controls.Add(labelIdTitle);
            groupBoxAgentDetails.Controls.Add(labelIdValue);
            groupBoxAgentDetails.Controls.Add(labelNameTitle);
            groupBoxAgentDetails.Controls.Add(labelNameValue);
            groupBoxAgentDetails.Controls.Add(labelContactTitle);
            groupBoxAgentDetails.Controls.Add(labelContactValue);
            groupBoxAgentDetails.Controls.Add(labelAgencyTitle);
            groupBoxAgentDetails.Controls.Add(labelAgencyValue);

            Controls.Add(groupBoxAgentDetails);
        }

        private void InitializeTransactionGridGroup()
        {
            groupBoxTransactionGrid = new GroupBox
            {
                Text = "Transaction List",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.MidnightBlue,
                BackColor = Color.AliceBlue,
                Location = new Point(20, 160),
                Size = new Size(1280, 370),
                Padding = new Padding(15)
            };

            dataGridViewTransactions = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.AliceBlue },
                EnableHeadersVisualStyles = false,
                GridColor = Color.LightSteelBlue
            };

            // Add columns
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TransactionId",
                HeaderText = "TRN #",
                Width = 80
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TransactionDate",
                HeaderText = "TRN Date",
                Width = 180,
                DefaultCellStyle = { Format = "dd/MM/yyyy hh:mm tt" }
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TransactionType",
                HeaderText = "TRN Type",
                Width = 100
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount",
                Width = 130,
                DefaultCellStyle = { Format = "C" }
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "Payment Method",
                Width = 170
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReferenceNumber",
                HeaderText = "Reference #",
                Width = 140
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Notes",
                HeaderText = "Notes",
                Width = 280
            });
            dataGridViewTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PlotNumber",
                HeaderText = "Plot Number",
                Width = 120
            });

            groupBoxTransactionGrid.Controls.Add(dataGridViewTransactions);
            Controls.Add(groupBoxTransactionGrid);
        }

        private void DisplayAgent(Agent agent)
        {
            labelNameValue.Text = agent.Name;
            labelContactValue.Text = agent.Contact;
            labelAgencyValue.Text = agent.Agency;
            labelIdValue.Text = agent.Id.ToString();
        }

        private DataTable GetAgentTransactions(int agentId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
                SELECT 
                    at.TransactionId,
                    at.AgentId,
                    at.TransactionDate,
                    at.Amount,
                    at.PaymentMethod,
                    at.ReferenceNumber,
                    at.TransactionType,
                    at.Notes,
                    at.CreatedBy,
                    at.CreatedDate,
                    at.ModifiedBy,
                    at.ModifiedDate,
                    at.IsDeleted,
                    at.PlotId,
                    p.PlotNumber
                FROM AgentTransaction at
                LEFT JOIN Plot p ON at.PlotId = p.Id
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

            // Add the action column dynamically if not already present
            if (dataGridViewTransactions.Columns["Action"] == null)
            {
                var actionCol = new DataGridViewImageColumn
                {
                    Name = "Action",
                    HeaderText = "Action",
                    Width = 120,
                    ImageLayout = DataGridViewImageCellLayout.Normal
                };
                dataGridViewTransactions.Columns.Add(actionCol);
            }

            dataGridViewTransactions.DataSource = transactions;
        }

        // Custom painting for the action column to show view, edit, delete icons
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

        private void ViewTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            // Implement your view logic here
            MessageBox.Show($"View Transaction: {transactionId}", "View", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EditTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            // Implement your edit logic here
            MessageBox.Show($"Edit Transaction: {transactionId}", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeleteTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            if (MessageBox.Show("Are you sure you want to delete this transaction?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // Implement your delete logic here
                MessageBox.Show($"Deleted Transaction: {transactionId}", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Optionally reload the grid after deletion
            }
        }
    }
}