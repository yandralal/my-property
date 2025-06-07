using Microsoft.Data.SqlClient;
using RealEstateManager.Pages;
using System.Data;

namespace RealEstateManager
{
    public partial class LandingForm : BaseForm
    {
        private DataTable? _propertyTable;
        private DataTable? _plotTable;
        public DataGridView DataGridViewProperties => dataGridViewProperties;

        public LandingForm()
        {
            InitializeFooter();
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            SetupPlotGrid();
            dataGridViewProperties.DataBindingComplete += DataGridViewProperties_DataBindingComplete;
            dataGridViewProperties.CellMouseClick += dataGridViewProperties_CellMouseClick;
            LoadActiveProperties();
        }

        private void InitializeFooter()
        {
            footerLabel = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 32,
                Text = "© 2025 RealEstateManager. All rights reserved.",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                BackColor = Color.FromArgb(30, 60, 114),
                ForeColor = Color.White
            };
            Controls.Add(footerLabel);
        }

        private void ButtonAddProperty_Click(object sender, EventArgs e)
        {
            var registerForm = new Pages.RegisterPropertyForm();
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                LoadActiveProperties(); // Refresh grid after adding
            }
        }

        private void ButtonManagePlots_Click(object sender, EventArgs e)
        {
            var managePlotsForm = new ManagePlotsForm();
            managePlotsForm.ShowDialog();
        }

        public void LoadActiveProperties()
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
                SELECT 
                    p.Id, 
                    p.Title, 
                    p.Type, 
                    p.Status, 
                    p.Price AS [BuyPrice], 
                    p.Owner, 
                    p.Description,
                    ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0) AS AmountPaid,
                    (p.Price - ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0)) AS AmountBalance
                FROM Property p
                WHERE p.IsDeleted = 0";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                _propertyTable = dt; // Store for filtering

                // Always clear columns and set AutoGenerateColumns before rebinding
                dataGridViewProperties.DataSource = null;
                dataGridViewProperties.Columns.Clear();
                dataGridViewProperties.AutoGenerateColumns = true;
                dataGridViewProperties.DataSource = dt.DefaultView;

                // Defensive: force UI to update columns
                dataGridViewProperties.Refresh();

                // Update property count label
                labelProperties.Text = $"Properties ({dt.Rows.Count})";
            }

            AdjustGridAndGroupBoxHeight(dataGridViewProperties, groupBoxProperties, 8, 120, 400, 100);

            dataGridViewProperties.CellPainting -= dataGridViewProperties_CellPainting;
            dataGridViewProperties.CellPainting += dataGridViewProperties_CellPainting;
        }

        private void SetupPlotGrid()
        {
            dataGridViewPlots.Columns.Clear();
            dataGridViewPlots.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                Width = 10,
                ReadOnly = true,
                Visible = false
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlotNumber",
                HeaderText = "Plot #",
                Width = 100
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerName",
                HeaderText = "Customer Name",
                Width = 245
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerPhone",
                HeaderText = "Phone",
                Width = 150
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerEmail",
                HeaderText = "Email",
                Width = 230
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 120
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Area",
                HeaderText = "Area",
                Width = 130
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SaleDate",
                HeaderText = "Sale Date",
                Width = 140
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SalePrice",
                HeaderText = "Sale Price",
                Width = 150
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AmountPaid",
                HeaderText = "Amount Paid",
                Width = 150
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AmountBalance",
                HeaderText = "Amount Balance",
                Width = 150
            });

            var actionColumn = new DataGridViewImageColumn
            {
                Name = "Action",
                HeaderText = "Action",
                Width = 150,
                ImageLayout = DataGridViewImageCellLayout.Normal
            };
            dataGridViewPlots.Columns.Add(actionColumn);

            dataGridViewPlots.CellContentClick -= dataGridViewPlots_CellContentClick;
            dataGridViewPlots.CellContentClick += dataGridViewPlots_CellContentClick;

            dataGridViewPlots.CellPainting -= dataGridViewPlots_CellPainting;
            dataGridViewPlots.CellPainting += dataGridViewPlots_CellPainting;

            dataGridViewPlots.CellMouseClick -= dataGridViewPlots_CellMouseClick;
            dataGridViewPlots.CellMouseClick += dataGridViewPlots_CellMouseClick;
        }

        private void LandingForm_Load(object sender, EventArgs e)
        {

        }

        private void registerSaleMenuItem_Click(object sender, EventArgs e)
        {
            // Ensure a property and a plot are selected
            if (dataGridViewProperties.CurrentRow == null)
            {
                MessageBox.Show("Please select a property.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridViewPlots.CurrentRow == null)
            {
                MessageBox.Show("Please select a plot.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get propertyId
            var propertyIdCell = dataGridViewProperties.CurrentRow.Cells["Id"];
            if (propertyIdCell == null || !int.TryParse(propertyIdCell.Value?.ToString(), out int propertyId))
            {
                MessageBox.Show("Invalid property selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get plot details
            var row = dataGridViewPlots.CurrentRow;
            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId))
            {
                MessageBox.Show("Invalid plot selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string plotNumber = row.Cells["PlotNumber"].Value?.ToString() ?? "";
            string customerName = row.Cells["CustomerName"].Value?.ToString() ?? "";
            string customerPhone = row.Cells["CustomerPhone"].Value?.ToString() ?? "";
            string customerEmail = row.Cells["CustomerEmail"].Value?.ToString() ?? "";
            string status = row.Cells["Status"].Value?.ToString() ?? "";
            string area = row.Cells["Area"].Value?.ToString() ?? "";
            string saleDate = row.Cells["SaleDate"].Value?.ToString() ?? "";
            string salePrice = row.Cells["SalePrice"].Value?.ToString() ?? "";

            var editForm = new RegisterSaleForm(
                propertyId,
                plotId,
                plotNumber,
                customerName,
                customerPhone,
                customerEmail,
                status,
                area,
                saleDate,
                salePrice
            );
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadPlotsForProperty(propertyId);
                LoadActiveProperties();
            }
        }

        private void DataGridViewProperties_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridViewProperties.CurrentRow != null)
            {
                var idCell = dataGridViewProperties.CurrentRow.Cells["Id"] ?? dataGridViewProperties.CurrentRow.Cells["Id"];
                if (idCell?.Value != null && int.TryParse(idCell.Value.ToString(), out int propertyId))
                {
                    LoadPlotsForProperty(propertyId);
                }
                else
                {
                    dataGridViewPlots.Rows.Clear();
                }
            }
            else
            {
                dataGridViewPlots.Rows.Clear();
            }
        }

        public void LoadPlotsForProperty(int propertyId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string plotQuery = @"
                SELECT 
                    p.Id, 
                    p.PlotNumber, 
                    p.Status, 
                    p.Area,
                    ps.SaleDate,
                    ps.SaleAmount,
                    ps.CustomerName AS CustomerName,
                    ps.CustomerPhone AS CustomerPhone,
                    ps.CustomerEmail AS CustomerEmail
                FROM Plot p
                LEFT JOIN PlotSale ps ON p.Id = ps.PlotId
                WHERE p.PropertyId = @PropertyId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(plotQuery, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                _plotTable = dt; // Store for filtering

                dataGridViewPlots.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int plotId = Convert.ToInt32(row["Id"]);
                    decimal saleAmount = row["SaleAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SaleAmount"]);

                    // Fetch transactions for this plot
                    decimal amountPaid = 0;
                    using (var transCmd = new SqlCommand(
                        "SELECT ISNULL(SUM(Amount), 0) FROM PlotTransaction WHERE PlotId = @PlotId AND IsDeleted = 0", conn))
                    {
                        transCmd.Parameters.AddWithValue("@PlotId", plotId);
                        var result = transCmd.ExecuteScalar();
                        amountPaid = result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                    }
                    decimal amountBalance = saleAmount - amountPaid;

                    dataGridViewPlots.Rows.Add(
                        row["Id"].ToString(),
                        row["PlotNumber"].ToString(),
                        row["CustomerName"] == DBNull.Value ? "" : row["CustomerName"].ToString(),
                        row["CustomerPhone"] == DBNull.Value ? "" : row["CustomerPhone"].ToString(),
                        row["CustomerEmail"] == DBNull.Value ? "" : row["CustomerEmail"].ToString(),
                        row["Status"].ToString(),
                        row["Area"].ToString(),
                        row["SaleDate"] == DBNull.Value ? "" : Convert.ToDateTime(row["SaleDate"]).ToShortDateString(),
                        saleAmount == 0 ? "" : saleAmount.ToString("N2"),
                        amountPaid.ToString("N2"),
                        amountBalance.ToString("N2")
                    );
                }

                labelPlots.Text = $"Plots ({dt.Rows.Count})";
            }

            AdjustGridAndGroupBoxHeight(dataGridViewPlots, groupBoxPlots, 10, 120, 500, 80);
        }

        private void dataGridViewPlots_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewPlots.Columns[e.ColumnIndex].Name == "ViewDetails")
            {
                var row = dataGridViewPlots.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId))
                {
                    var detailsForm = new Pages.PlotDetailsForm(plotId);
                    detailsForm.ShowDialog();
                }
            }
        }

        private void dataGridViewPlots_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewPlots.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Load your icons from resources
                var viewIcon = Properties.Resources.view;
                var editIcon = Properties.Resources.edit;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 16; // Increased padding
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

        private void dataGridViewPlots_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewPlots.Columns[e.ColumnIndex].Name == "Action")
            {
                int iconWidth = 24, padding = 16;
                int x = e.X - padding;
                int iconIndex = x / (iconWidth + padding);

                var row = dataGridViewPlots.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId))
                {
                    int propertyId = 0; 

                    var idCell = dataGridViewProperties.CurrentRow?.Cells["Id"];
                    if (idCell != null && int.TryParse(idCell.Value?.ToString(), out int parsedPropertyId))
                    {
                        propertyId = parsedPropertyId;
                    }

                    switch (iconIndex)
                    {
                        case 0:
                            // View
                            var detailsForm = new PlotDetailsForm(plotId);
                            detailsForm.ShowDialog();
                            break;
                        case 1:
                            string plotNumber = row.Cells["PlotNumber"].Value?.ToString() ?? "";
                            string customerName = row.Cells["CustomerName"].Value?.ToString() ?? "";
                            string customerPhone = row.Cells["CustomerPhone"].Value?.ToString() ?? "";
                            string customerEmail = row.Cells["CustomerEmail"].Value?.ToString() ?? "";
                            string status = row.Cells["Status"].Value?.ToString() ?? "";
                            string area = row.Cells["Area"].Value?.ToString() ?? "";
                            string saleDate = row.Cells["SaleDate"].Value?.ToString() ?? "";
                            string salePrice = row.Cells["SalePrice"].Value?.ToString() ?? "";

                            var editForm = new RegisterSaleForm(
                                propertyId,
                                plotId,
                                plotNumber,
                                customerName,
                                customerPhone,
                                customerEmail,
                                status,
                                area,
                                saleDate,
                                salePrice
                            );
                            if (editForm.ShowDialog() == DialogResult.OK)
                            {
                                LoadPlotsForProperty(propertyId);
                            }
                            break;
                        case 2:
                            // Delete
                            if (MessageBox.Show("Are you sure you want to delete this plot?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                DeletePlot(plotId);
                                LoadPlotsForProperty(propertyId);
                            }
                            break;
                    }
                }
            }
        }

        private void PlotTransactionMenuItem_Click(object sender, EventArgs e)
        {
            // Optionally, pass the selected plot ID, sale amount, and plot number if a plot is selected in the grid
            int? plotId = null;
            decimal? saleAmount = null;
            string? plotNumber = null;

            if (dataGridViewPlots.CurrentRow != null)
            {
                var idCell = dataGridViewPlots.CurrentRow.Cells["Id"];
                var saleAmountCell = dataGridViewPlots.CurrentRow.Cells["SalePrice"];
                var plotNumberCell = dataGridViewPlots.CurrentRow.Cells["PlotNumber"];

                if (idCell != null && int.TryParse(idCell.Value?.ToString(), out int selectedPlotId))
                {
                    plotId = selectedPlotId;
                }

                if (saleAmountCell != null && decimal.TryParse(saleAmountCell.Value?.ToString(), out decimal parsedSaleAmount))
                {
                    saleAmount = parsedSaleAmount;
                }

                if (plotNumberCell != null)
                {
                    plotNumber = plotNumberCell.Value?.ToString();
                }
            }

            var transactionForm = new Pages.RegisterPlotTransactionForm(plotId, saleAmount, plotNumber);
            transactionForm.ShowDialog();
        }

        private void AdjustGridAndGroupBoxHeight(DataGridView grid, GroupBox groupBox, int maxVisibleRows = 10, int minGridHeight = 100, int maxGridHeight = 500, int groupBoxExtra = 100)
        {
            int rowCount = grid.Rows.Count;
            int rowHeight = grid.RowTemplate.Height;
            int headerHeight = grid.ColumnHeadersHeight;
            int border = 2; // for grid border/padding

            // Show up to maxVisibleRows, but not more than maxGridHeight
            int visibleRows = Math.Min(rowCount, maxVisibleRows);
            int newGridHeight = headerHeight + (rowHeight * visibleRows) + border;

            // Clamp grid height
            newGridHeight = Math.Max(minGridHeight, Math.Min(newGridHeight, maxGridHeight));
            grid.Height = newGridHeight;

            int minGroupBoxHeight = newGridHeight + 200;
            groupBox.Height = Math.Max(minGroupBoxHeight, groupBox.MinimumSize.Height);
        }

        private void dataGridViewProperties_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewProperties.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Load your icons from resources
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

        private void dataGridViewProperties_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewProperties.Columns[e.ColumnIndex].Name == "Action")
            {
                // Existing logic
                int iconWidth = 24, padding = 12;
                int x = e.X - padding;
                int iconIndex = x / (iconWidth + padding);

                var row = dataGridViewProperties.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["Id"].Value?.ToString(), out int propertyId))
                {
                    switch (iconIndex)
                    {
                        case 0:
                            // View
                            var viewForm = new PropertyDetailsForm(propertyId);
                            if (viewForm.ShowDialog() == DialogResult.OK)
                            {
                                LoadActiveProperties();
                            }
                            break;
                        case 1:
                            // Edit
                            var editForm = new RegisterPropertyForm(propertyId);
                            if (editForm.ShowDialog() == DialogResult.OK)
                            {
                                LoadActiveProperties();
                            }
                            break;
                        case 2:
                            // Delete
                            if (MessageBox.Show("Are you sure you want to delete this property?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                DeleteProperty(propertyId);
                                LoadActiveProperties();
                            }
                            break;
                    }
                }
            }
        }

        private void DeleteProperty(int propertyId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        // 1. Get all plot IDs under this property
                        List<int> plotIds = new List<int>();
                        using (var cmd = new SqlCommand("SELECT Id FROM Plot WHERE PropertyId = @PropertyId AND IsDeleted = 0", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    plotIds.Add(reader.GetInt32(0));
                                }
                            }
                        }

                        if (plotIds.Count > 0)
                        {
                            // 2. Check if any plot is referenced in PlotSale
                            string plotIdList = string.Join(",", plotIds);
                            if (!string.IsNullOrEmpty(plotIdList))
                            {
                                using (var cmd = new SqlCommand($"SELECT COUNT(1) FROM PlotSale WHERE PlotId IN ({plotIdList})", conn, tran))
                                {
                                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                                    if (count > 0)
                                    {
                                        MessageBox.Show("Cannot delete this property because one or more plots have already been sold.", "Delete Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        tran.Rollback();
                                        return;
                                    }
                                }

                                // 3. Check if any plot has a 'Sale' transaction in PlotTransaction
                                using (var cmd = new SqlCommand($"SELECT COUNT(1) FROM PlotTransaction WHERE PlotId IN ({plotIdList}) AND TransactionType = 'Sale' AND IsDeleted = 0", conn, tran))
                                {
                                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                                    if (count > 0)
                                    {
                                        MessageBox.Show("Cannot delete this property because one or more plots have already been sold.", "Delete Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        tran.Rollback();
                                        return;
                                    }
                                }
                            }
                        }

                        // 4. Soft delete the property
                        using (var cmd = new SqlCommand("UPDATE Property SET IsDeleted = 1 WHERE Id = @Id", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Id", propertyId);
                            cmd.ExecuteNonQuery();
                        }

                        // 5. Soft delete all plots under this property
                        using (var cmd = new SqlCommand("UPDATE Plot SET IsDeleted = 1 WHERE PropertyId = @PropertyId", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the property: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeletePlot(int plotId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        decimal salePrice = 0;
                        decimal amountPaid = 0;

                        // 1. Get SalePrice from PlotSale
                        using (var cmd = new SqlCommand("SELECT ISNULL(SaleAmount, 0) FROM PlotSale WHERE PlotId = @PlotId", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@PlotId", plotId);
                            var result = cmd.ExecuteScalar();
                            salePrice = result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                        }

                        // 2. Get AmountPaid from PlotTransaction
                        using (var cmd = new SqlCommand("SELECT ISNULL(SUM(Amount), 0) FROM PlotTransaction WHERE PlotId = @PlotId AND IsDeleted = 0", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@PlotId", plotId);
                            var result = cmd.ExecuteScalar();
                            amountPaid = result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                        }

                        decimal amountBalance = salePrice - amountPaid;

                        // 3. Only allow delete if both are zero
                        if (salePrice != 0 || amountBalance != 0)
                        {
                            MessageBox.Show("Cannot delete this plot because it has a sale record or outstanding balance.", "Delete Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tran.Rollback();
                            return;
                        }

                        // 4. Soft delete the plot
                        using (var cmd = new SqlCommand("UPDATE Plot SET IsDeleted = 1 WHERE Id = @Id", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Id", plotId);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the plot: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxPropertyFilter_TextChanged(object sender, EventArgs e)
        {
            if (_propertyTable == null) return;
            string filter = textBoxPropertyFilter.Text.Replace("'", "''");
            if (string.IsNullOrWhiteSpace(filter))
            {
                ((DataView)dataGridViewProperties.DataSource).RowFilter = "";
            }
            else
            {
                // Filter on multiple columns (adjust as needed)
                ((DataView)dataGridViewProperties.DataSource).RowFilter =
                    $"Title LIKE '%{filter}%' OR Type LIKE '%{filter}%' OR Status LIKE '%{filter}%' OR Owner LIKE '%{filter}%'";
            }
            AdjustGridAndGroupBoxHeight(dataGridViewProperties, groupBoxProperties, 8, 120, 400, 100);
        }

        private void textBoxPlotFilter_TextChanged(object sender, EventArgs e)
        {
            if (_plotTable == null) return;
            string filter = textBoxPlotFilter.Text.Replace("'", "''");
            if (string.IsNullOrWhiteSpace(filter))
            {
                ((DataView)dataGridViewPlots.DataSource).RowFilter = "";
            }
            else
            {
                // Filter on multiple columns (adjust as needed)
                ((DataView)dataGridViewPlots.DataSource).RowFilter =
                    $"PlotNumber LIKE '%{filter}%' OR CustomerName LIKE '%{filter}%' OR Status LIKE '%{filter}%'";
            }
            AdjustGridAndGroupBoxHeight(dataGridViewPlots, groupBoxPlots, 10, 120, 500, 100);
        }

        private void DataGridViewProperties_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dataGridViewProperties;

            // Remove and re-add Action column to ensure it's always last
            if (dgv.Columns.Contains("Action"))
                dgv.Columns.Remove("Action");

            var actionCol = new DataGridViewImageColumn
            {
                Name = "Action",
                HeaderText = "Actions",
                Width = 150,
                ImageLayout = DataGridViewImageCellLayout.Normal
            };
            dgv.Columns.Add(actionCol);

            // Set headers, widths, order, and AutoSizeMode
            int displayIndex = 0;
            void SetCol(string name, string header, int width, string? format = null)
            {
                if (dgv.Columns[name] != null)
                {
                    dgv.Columns[name].HeaderText = header;
                    dgv.Columns[name].Width = width;
                    dgv.Columns[name].DisplayIndex = displayIndex++;
                    dgv.Columns[name].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    if (format != null)
                        dgv.Columns[name].DefaultCellStyle.Format = format;
                }
            }

            SetCol("Title", "Title", 230);
            SetCol("Type", "Type", 150);
            SetCol("Status", "Status", 120);
            SetCol("Owner", "Owner", 190);
            SetCol("BuyPrice", "Buy Price", 180, "N2");
            SetCol("AmountPaid", "Amount Paid", 180, "N2");
            SetCol("AmountBalance", "Amount Balance", 180, "N2");
            SetCol("Description", "Description", 350);

            if (dgv.Columns["Id"] != null)
                dgv.Columns["Id"].Visible = false;

            if (dgv.Columns["Action"] != null)
            {
                dgv.Columns["Action"].DisplayIndex = dgv.Columns.Count - 1;
                dgv.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Action"].Width = 150; // Set a fixed width for the Action column
            }
        }

        private void DataGridViewProperties_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridViewProperties.Columns[e.ColumnIndex].Name != "Action")
                return;

            var row = dataGridViewProperties.Rows[e.RowIndex];
            var propertyId = row.Cells["Id"].Value?.ToString();

            // Show a context menu for actions
            var menu = new ContextMenuStrip();
            menu.Items.Add("View", null, (s, ea) => ViewProperty(propertyId));
            menu.Items.Add("Edit", null, (s, ea) => EditProperty(propertyId));
            menu.Items.Add("Delete", null, (s, ea) => DeletePropertyWithConfirm(propertyId));
            var cellDisplayRectangle = dataGridViewProperties.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            var location = dataGridViewProperties.PointToScreen(new Point(cellDisplayRectangle.Left, cellDisplayRectangle.Bottom));
        }

        // Helper methods for property actions
        private void ViewProperty(string? propertyId)
        {
            if (int.TryParse(propertyId, out int id))
            {
                var viewForm = new PropertyDetailsForm(id);
                viewForm.ShowDialog();
            }
        }

        private void EditProperty(string? propertyId)
        {
            if (int.TryParse(propertyId, out int id))
            {
                var editForm = new RegisterPropertyForm(id);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadActiveProperties();
                }
            }
        }

        private void DeletePropertyWithConfirm(string? propertyId)
        {
            if (int.TryParse(propertyId, out int id))
            {
                if (MessageBox.Show("Are you sure you want to delete this property?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DeleteProperty(id);
                    LoadActiveProperties();
                }
            }
        }

        private void PropertyTransactionsMenuItem_Click(object sender, EventArgs e)
        {
            // Optionally, pass the selected property ID, sale amount, and property number if a property is selected in the grid
            int? propertyId = null;
            decimal? saleAmount = null;
            string? propertyNumber = null;

            if (dataGridViewProperties.CurrentRow != null)
            {
                var idCell = dataGridViewProperties.CurrentRow.Cells["Id"];
                var saleAmountCell = dataGridViewProperties.CurrentRow.Cells["BuyPrice"];
                var propertyNumberCell = dataGridViewProperties.CurrentRow.Cells["Title"];

                if (idCell != null && int.TryParse(idCell.Value?.ToString(), out int selectedPropertyId))
                {
                    propertyId = selectedPropertyId;
                }

                if (saleAmountCell != null && decimal.TryParse(saleAmountCell.Value?.ToString(), out decimal parsedSaleAmount))
                {
                    saleAmount = parsedSaleAmount;
                }

                if (propertyNumberCell != null)
                {
                    propertyNumber = propertyNumberCell.Value?.ToString();
                }
            }

            var transactionForm = new Pages.RegisterPropertyTransactionForm(propertyId, saleAmount, propertyNumber);
            transactionForm.ShowDialog();
        }

        private void registerPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var registerForm = new Pages.RegisterPropertyForm();
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                LoadActiveProperties(); // Refresh grid after adding
            }
        }
    }
}