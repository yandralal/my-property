using RealEstateManager.Repositories;

namespace RealEstateManager.Pages
{
    public partial class ViewAllAgentsForm : BaseForm
    {
        public ViewAllAgentsForm()
        {
            InitializeComponent();
            
            LoadAgents();
            AgentRepository.AgentsChanged += RefreshGrid;
        }

        private void RefreshGrid()
        {
            // Reload agents and bind to grid with all transaction details
            var agents = AgentRepository.GetAllAgents()
                .Select(agent => new
                {
                    agent.Id,
                    agent.Name,
                    agent.Contact,
                    agent.Agency,
                    TotalBrokerage = AgentRepository.GetTotalBrokerage(agent.Id),
                    Paid = AgentRepository.GetTotalPaid(agent.Id),
                    Balance = AgentRepository.GetTotalBrokerage(agent.Id) - AgentRepository.GetTotalPaid(agent.Id)
                })
                .ToList();

            dataGridViewAgents.DataSource = agents;
            labelAgents.Text = $"Agents ({agents.Count})";
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            AgentRepository.AgentsChanged -= RefreshGrid;
            base.OnFormClosed(e);
        }

        private void LoadAgents()
        {
            var agents = AgentRepository.GetAllAgents()
                .Select(agent => new
                {
                    agent.Id,
                    agent.Name,
                    agent.Contact,
                    agent.Agency,
                    TotalBrokerage = AgentRepository.GetTotalBrokerage(agent.Id),
                    Paid = AgentRepository.GetTotalPaid(agent.Id),
                    Balance = AgentRepository.GetTotalBrokerage(agent.Id) - AgentRepository.GetTotalPaid(agent.Id)
                })
                .ToList();

            dataGridViewAgents.DataSource = null;
            dataGridViewAgents.Rows.Clear();
            dataGridViewAgents.Columns.Clear();

            dataGridViewAgents.AutoGenerateColumns = false;
            dataGridViewAgents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridViewAgents.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Visible = false // Make sure this is true if you want to see the column
            });
            dataGridViewAgents.Columns.Add(new DataGridViewTextBoxColumn {
                DataPropertyName = "Name",
                HeaderText = "Name",
                Width = 200,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridViewAgents.Columns.Add(new DataGridViewTextBoxColumn {
                DataPropertyName = "Contact",
                HeaderText = "Contact",
                Width = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridViewAgents.Columns.Add(new DataGridViewTextBoxColumn {
                DataPropertyName = "Agency",
                HeaderText = "Agency",
                Width = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridViewAgents.Columns.Add(new DataGridViewTextBoxColumn {
                DataPropertyName = "TotalBrokerage",
                HeaderText = "Total Brokerage",
                Width = 160,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dataGridViewAgents.Columns.Add(new DataGridViewTextBoxColumn {
                DataPropertyName = "Paid",
                HeaderText = "Amount Paid",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dataGridViewAgents.Columns.Add(new DataGridViewTextBoxColumn {
                DataPropertyName = "Balance",
                HeaderText = "Balance",
                Width = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            // Action column with icons
            var actionCol = new DataGridViewImageColumn
            {
                Name = "Action",
                HeaderText = "Action",
                Width = 120,
                ImageLayout = DataGridViewImageCellLayout.Normal,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dataGridViewAgents.Columns.Add(actionCol);

            dataGridViewAgents.DataSource = agents;

            labelAgents.Text = $"Agents ({agents.Count})";
        }

        private void DataGridViewAgents_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewAgents.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                var viewIcon = Properties.Resources.view;
                var editIcon = Properties.Resources.edit;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 8;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;

                // Draw edit icon (first)
                e.Graphics.DrawImage(editIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw view icon (second)
                e.Graphics.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw delete icon (third)
                e.Graphics.DrawImage(deleteIcon, new Rectangle(x, y, iconWidth, iconHeight));

                e.Handled = true;
            }
        }

        private void DataGridViewAgents_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewAgents.Columns[e.ColumnIndex].Name == "Action")
            {
                int iconWidth = 24, padding = 8;
                int x = e.X - padding;
                int iconIndex = x / (iconWidth + padding);

                int agentId = Convert.ToInt32(dataGridViewAgents.Rows[e.RowIndex].Cells["Id"].Value);
                var agent = AgentRepository.GetAllAgents().Find(a => a.Id == agentId);

                if (agent == null)
                {
                    MessageBox.Show("Agent not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                switch (iconIndex)
                {
                    case 0: // Edit
                        var form = new AgentRegistrationForm(agent);
                        if (form.ShowDialog() == DialogResult.OK)
                            LoadAgents();
                        break;
                    case 1: // View
                        var detailsForm = new AgentDetailsForm(agent);
                        detailsForm.ShowDialog();
                        break;
                    case 2: // Delete
                        if (MessageBox.Show("Are you sure you want to delete this agent?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            try
                            {
                                AgentRepository.DeleteAgent(agentId);
                                LoadAgents();
                            }
                            catch (InvalidOperationException ex)
                            {
                                MessageBox.Show(ex.Message, "Cannot Delete Agent", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        break;
                }
            }
        }

        private void ButtonRegisterAgent_Click(object sender, EventArgs e)
        {
            var form = new AgentRegistrationForm();
            if (form.ShowDialog() == DialogResult.OK)
                LoadAgents();
        }
    }
}