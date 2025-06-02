using Microsoft.Data.SqlClient;
using RealEstateManager.Pages;
using System.Data;

namespace RealEstateManager
{
    public partial class LandingForm : BaseForm
    {
        private DataTable? _propertyTable;
        private DataTable? _plotTable;

        public LandingForm()
        {
            InitializeFooter();
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // Make the form full screen  
            SetupPlotGrid(); // Call this first  

            // Attach event handlers for the property grid before loading data  
            dataGridViewProperties.DataBindingComplete += DataGridViewProperties_DataBindingComplete;
            dataGridViewProperties.CellContentClick += DataGridViewProperties_CellContentClick;

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
            string query = "SELECT Id, Title, Type, Status, Price, Owner, Phone, Address, City, State, ZipCode, Description FROM Property WHERE IsDeleted = 0";

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

            dataGridViewProperties.CellMouseClick -= dataGridViewProperties_CellMouseClick;
            dataGridViewProperties.CellMouseClick += dataGridViewProperties_CellMouseClick;
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
            var registerSaleForm = new RegisterSaleForm();
            registerSaleForm.ShowDialog();
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

        private void LoadPlotsForProperty(int propertyId)
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
                LEFT JOIN PropertySale ps ON p.Id = ps.PlotId
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
            if (e.RowIndex >= 0 && dataGridViewPlots.Columns[e.ColumnIndex].Name == "Action")
            {
                int iconWidth = 24, padding = 16; // Match the padding used in CellPainting
                int x = e.X - padding;
                int iconIndex = x / (iconWidth + padding);

                var row = dataGridViewPlots.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId))
                {
                    int propertyId = 0; // Initialize propertyId to avoid CS0165

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
                            // TODO: Confirm and delete plotId
                            break;
                    }
                }
            }
        }

        private void registerTransactionMenuItem_Click(object sender, EventArgs e)
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

            var transactionForm = new Pages.RegisterTransactionForm(plotId, saleAmount, plotNumber);
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
            if (e.RowIndex >= 0 && dataGridViewProperties.Columns[e.ColumnIndex].Name == "Action")
            {
                int iconWidth = 24, padding = 12;
                int x = e.X - padding;
                int iconIndex = x / (iconWidth + padding);

                var row = dataGridViewProperties.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["Id"].Value?.ToString(), out int propertyId))
                {
                    switch (iconIndex)
                    {
                        case 0:
                            //View
                            var viewForm = new PropertyDetailsForm(propertyId);
                            viewForm.ShowDialog();
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
            string query = "UPDATE Property SET IsDeleted = 1 WHERE Id = @Id";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", propertyId);
                conn.Open();
                cmd.ExecuteNonQuery();
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

            // Remove any existing Action column to avoid duplicates and ensure it's always last
            if (dgv.Columns.Contains("Action"))
                dgv.Columns.Remove("Action");

            // Set column headers and widths for property grid
            if (dgv.Columns["Title"] != null)
            {
                dgv.Columns["Title"].HeaderText = "Title";
                dgv.Columns["Title"].Width = 230;
            }
            if (dgv.Columns["Type"] != null)
            {
                dgv.Columns["Type"].HeaderText = "Type";
                dgv.Columns["Type"].Width = 150;
            }
            if (dgv.Columns["Status"] != null)
            {
                dgv.Columns["Status"].HeaderText = "Status";
                dgv.Columns["Status"].Width = 120;
            }
            if (dgv.Columns["Price"] != null)
            {
                dgv.Columns["Price"].HeaderText = "Price";
                dgv.Columns["Price"].Width = 120;
            }
            if (dgv.Columns["Owner"] != null)
            {
                dgv.Columns["Owner"].HeaderText = "Owner";
                dgv.Columns["Owner"].Width = 180;
            }
            if (dgv.Columns["Phone"] != null)
            {
                dgv.Columns["Phone"].HeaderText = "Phone";
                dgv.Columns["Phone"].Width = 160;
            }
            if (dgv.Columns["Address"] != null)
            {
                dgv.Columns["Address"].HeaderText = "Address";
                dgv.Columns["Address"].Width = 240;
            }
            if (dgv.Columns["City"] != null)
            {
                dgv.Columns["City"].HeaderText = "City";
                dgv.Columns["City"].Width = 100;
            }
            if (dgv.Columns["Description"] != null)
            {
                dgv.Columns["Description"].HeaderText = "Description";
                dgv.Columns["Description"].Width = 320;
            }
            if (dgv.Columns["Id"] != null)
            {
                dgv.Columns["Id"].Visible = false;
            }
            if (dgv.Columns["State"] != null)
            {
                dgv.Columns["State"].Visible = false;
            }
            if (dgv.Columns["ZipCode"] != null)
            {
                dgv.Columns["ZipCode"].Visible = false;
            }

            var actionCol = new DataGridViewImageColumn
            {
                Name = "Action",
                HeaderText = "Actions",
                Width = 150,
                ImageLayout = DataGridViewImageCellLayout.Normal
            };

            // Match the style to the rest of the grid
            //actionCol.DefaultCellStyle.BackColor = dataGridViewProperties.DefaultCellStyle.BackColor;
            //actionCol.DefaultCellStyle.ForeColor = dataGridViewProperties.DefaultCellStyle.ForeColor;
            //actionCol.DefaultCellStyle.SelectionBackColor = dataGridViewProperties.DefaultCellStyle.SelectionBackColor;
            //actionCol.DefaultCellStyle.SelectionForeColor = dataGridViewProperties.DefaultCellStyle.SelectionForeColor;
            //actionCol.DefaultCellStyle.Font = dataGridViewProperties.DefaultCellStyle.Font;

            dgv.Columns.Add(actionCol);
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
    }
}