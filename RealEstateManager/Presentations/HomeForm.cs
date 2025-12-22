using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RealEstateManager
{
    public partial class HomeForm : Form
    {
        private int? _selectedPropertyId = null;
        private DataTable _plotsTable;

        public HomeForm()
        {
            InitializeComponent();
            labelWelcomeBanner.Text = "Welcome to Jay Maa Durga Housing Agency";
            SetupPropertyGrid();
            SetupPlotGrid();
            LoadActiveProperties();
        }

        private void SetupPropertyGrid()
        {
            // Styling and event wiring for dataGridViewProperties
            dataGridViewProperties.Font = new Font("Segoe UI", 10F);
            dataGridViewProperties.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.MidnightBlue,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dataGridViewProperties.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Black,
                BackColor = Color.White
            };
            dataGridViewProperties.RowTemplate.Height = 28;
            dataGridViewProperties.GridColor = Color.LightGray;
            dataGridViewProperties.EnableHeadersVisualStyles = false;
            dataGridViewProperties.DataBindingComplete += DataGridViewProperties_DataBindingComplete;
            dataGridViewProperties.CellMouseClick += DataGridViewProperties_CellMouseClick;
            dataGridViewProperties.CellFormatting += DataGridViewProperties_CellFormatting;
            dataGridViewProperties.SelectionChanged += DataGridViewProperties_SelectionChanged;
        }

        private void SetupPlotGrid()
        {
            dataGridViewPlots.Font = new Font("Segoe UI", 10F);
            dataGridViewPlots.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.MidnightBlue,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dataGridViewPlots.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Black,
                BackColor = Color.White
            };
            dataGridViewPlots.RowTemplate.Height = 28;
            dataGridViewPlots.GridColor = Color.LightGray;
            dataGridViewPlots.EnableHeadersVisualStyles = false;

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
                Width = 80
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerName",
                HeaderText = "Customer Name"
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerPhone",
                HeaderText = "Phone"
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status"
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Area",
                HeaderText = "Area (sq.ft)"
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SaleDate",
                HeaderText = "Sale Date"
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SalePrice",
                HeaderText = "Sale Price"
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HasSale",
                Visible = false
            });

            var actionColumn = new DataGridViewImageColumn
            {
                Name = "Action",
                HeaderText = "Action",
                ImageLayout = DataGridViewImageCellLayout.Normal,
                Width = 180
            };
            dataGridViewPlots.Columns.Add(actionColumn);
            dataGridViewPlots.Columns["Action"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Set currency formatting for monetary columns
            if (dataGridViewPlots.Columns["SalePrice"] != null)
                dataGridViewPlots.Columns["SalePrice"].DefaultCellStyle.Format = "C";
            if (dataGridViewPlots.Columns["SaleDate"] != null)
                dataGridViewPlots.Columns["SaleDate"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";

            foreach (DataGridViewColumn col in dataGridViewPlots.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.Resizable = DataGridViewTriState.True;
            }

            dataGridViewPlots.CellContentClick -= DataGridViewPlots_CellContentClick;
            dataGridViewPlots.CellContentClick += DataGridViewPlots_CellContentClick;

            dataGridViewPlots.CellPainting -= DataGridViewPlots_CellPainting;
            dataGridViewPlots.CellPainting += DataGridViewPlots_CellPainting;

            dataGridViewPlots.CellMouseClick -= DataGridViewPlots_CellMouseClick;
            dataGridViewPlots.CellMouseClick += DataGridViewPlots_CellMouseClick;

            dataGridViewPlots.SelectionChanged -= DataGridViewPlots_SelectionChanged;
            dataGridViewPlots.SelectionChanged += DataGridViewPlots_SelectionChanged;

            ApplyGridStyle(dataGridViewPlots);
        }

        public void LoadActiveProperties(int? selectedPropertyId = null)
        {
            int? propertyIdToSelect = selectedPropertyId ?? _selectedPropertyId;

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"
                SELECT 
                    p.Id, 
                    p.Title, 
                    p.Type, 
                    p.Status, 
                    p.Price AS [BuyPrice], 
                    p.Owner, 
                    p.Phone,
                    ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0) AS AmountPaid,
                    ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0) AS TotalLoanPrincipal,
                    (p.Price 
                        - ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0)
                        - ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0)
                    ) AS AmountBalance,
                    p.KhasraNo,
                    p.Area
                FROM Property p
                WHERE p.IsDeleted = 0";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);

                if (leftPanel.Controls.Find("groupBoxProperties", true).FirstOrDefault() is GroupBox gbProps)
                    gbProps.Text = $"Properties ({dt.Rows.Count})";

                BindActiveProperties(dt);
            }

            // Restore selection if possible
            if (propertyIdToSelect.HasValue)
            {
                foreach (DataGridViewRow row in dataGridViewProperties.Rows)
                {
                    if (row.Cells["Id"].Value != null && Convert.ToInt32(row.Cells["Id"].Value) == propertyIdToSelect.Value)
                    {
                        row.Selected = true;
                        // Find the first visible column to set as CurrentCell
                        var firstVisibleCol = dataGridViewProperties.Columns
                            .Cast<DataGridViewColumn>()
                            .FirstOrDefault(c => c.Visible && row.Cells[c.Index] != null);
                        if (firstVisibleCol != null)
                        {
                            dataGridViewProperties.CurrentCell = row.Cells[firstVisibleCol.Index];
                        }
                        break;
                    }
                }
            }

            dataGridViewProperties.CellPainting -= DataGridViewProperties_CellPainting;
            dataGridViewProperties.CellPainting += DataGridViewProperties_CellPainting;
            ApplyGridStyle(dataGridViewProperties);
        }

        public void UpdatePlotCount(int count)
        {
            if (leftPanel.Controls.Find("groupBoxPlots", true).FirstOrDefault() is GroupBox gbPlots)
                gbPlots.Text = $"Plots ({count})";
        }

        public void BindActiveProperties(DataTable dt)
        {
            dataGridViewProperties.DataSource = null;
            dataGridViewProperties.Columns.Clear();
            dataGridViewProperties.AutoGenerateColumns = true;
            dataGridViewProperties.DataSource = dt.DefaultView;
            // Hide Phone column if exists
            if (dataGridViewProperties.Columns["Phone"] != null)
                dataGridViewProperties.Columns["Phone"].Visible = false;
            // Format Area column
            if (dataGridViewProperties.Columns["Area"] != null)
            {
                dataGridViewProperties.Columns["Area"].HeaderText = "Area (sq.ft)";
                dataGridViewProperties.Columns["Area"].Width = 120;
                dataGridViewProperties.Columns["Area"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridViewProperties.Columns["Area"].DefaultCellStyle.Format = "N2";
            }
            // Set selected property details in right panel
            if (dt.Rows.Count > 0)
            {
                SetSelectedPropertyDetails(dt.Rows[0]);
            }
        }

        private void SetSelectedPropertyDetails(DataRow row)
        {
            var lblAmountPaid = rightPanel.Controls.Find("lblAmountPaid", true).FirstOrDefault() as Label;
            var lblTotalLoanPrincipal = rightPanel.Controls.Find("lblTotalLoanPrincipal", true).FirstOrDefault() as Label;
            var lblAmountBalance = rightPanel.Controls.Find("lblAmountBalance", true).FirstOrDefault() as Label;
            var lblProfitLoss = rightPanel.Controls.Find("lblProfitLoss", true).FirstOrDefault() as Label;
            if (lblAmountPaid != null && row.Table.Columns.Contains("AmountPaid"))
                lblAmountPaid.Text = row["AmountPaid"].ToString();
            if (lblTotalLoanPrincipal != null && row.Table.Columns.Contains("TotalLoanPrincipal"))
                lblTotalLoanPrincipal.Text = row["TotalLoanPrincipal"].ToString();
            if (lblAmountBalance != null && row.Table.Columns.Contains("AmountBalance"))
                lblAmountBalance.Text = row["AmountBalance"].ToString();
            // Calculate and show profit/loss
            if (lblProfitLoss != null)
            {
                decimal buyPrice = row.Table.Columns.Contains("BuyPrice") && row["BuyPrice"] != DBNull.Value ? Convert.ToDecimal(row["BuyPrice"]) : 0;
                decimal amountPaid = row.Table.Columns.Contains("AmountPaid") && row["AmountPaid"] != DBNull.Value ? Convert.ToDecimal(row["AmountPaid"]) : 0;
                decimal totalLoan = row.Table.Columns.Contains("TotalLoanPrincipal") && row["TotalLoanPrincipal"] != DBNull.Value ? Convert.ToDecimal(row["TotalLoanPrincipal"]) : 0;
                decimal profitLoss = amountPaid - (buyPrice + totalLoan);
                lblProfitLoss.Text = profitLoss.ToString("C");
                lblProfitLoss.ForeColor = profitLoss >= 0 ? Color.DarkGreen : Color.DarkRed;
            }
        }

        public void LoadPlotsForProperty(int propertyId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
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
                    CASE WHEN ps.PlotId IS NOT NULL THEN 1 ELSE 0 END AS HasSale
                FROM Plot p
                LEFT JOIN PlotSale ps ON p.Id = ps.PlotId
                WHERE p.PropertyId = @PropertyId AND p.IsDeleted = 0";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(plotQuery, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                _plotsTable = dt;
                dataGridViewPlots.DataSource = null;
                dataGridViewPlots.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    int plotId = 0;
                    if (row.Table.Columns.Contains("Id") && row["Id"] != DBNull.Value && int.TryParse(row["Id"].ToString(), out int parsedPlotId))
                        plotId = parsedPlotId;
                    decimal saleAmount = 0;
                    if (row.Table.Columns.Contains("SaleAmount") && row["SaleAmount"] != DBNull.Value && decimal.TryParse(row["SaleAmount"].ToString(), out decimal parsedSaleAmount))
                        saleAmount = parsedSaleAmount;
                    decimal amountPaid = 0;
                    using (var transCmd = new SqlCommand("SELECT ISNULL(SUM(Amount), 0) FROM PlotTransaction WHERE PlotId = @PlotId AND IsDeleted = 0", conn))
                    {
                        transCmd.Parameters.AddWithValue("@PlotId", plotId);
                        var result = transCmd.ExecuteScalar();
                        if (result != null && decimal.TryParse(result.ToString(), out decimal parsedAmountPaid))
                            amountPaid = parsedAmountPaid;
                    }
                    decimal amountBalance = saleAmount - amountPaid;
                    dataGridViewPlots.Rows.Add(
                        row["Id"].ToString(),
                        row["PlotNumber"].ToString(),
                        row["CustomerName"] == DBNull.Value ? "" : row["CustomerName"].ToString(),
                        row["CustomerPhone"] == DBNull.Value ? "" : row["CustomerPhone"].ToString(),
                        row["Status"].ToString(),
                        row["Area"].ToString(),
                        row["SaleDate"],
                        saleAmount == 0 ? DBNull.Value : saleAmount,
                        row["HasSale"]
                    );
                }
                // Update total plots label in property details groupbox
                var lblTotalPlots = rightPanel.Controls.Find("lblTotalPlots", true).FirstOrDefault() as Label;
                if (lblTotalPlots != null)
                    lblTotalPlots.Text = dt.Rows.Count.ToString();
            }
        }

        private void DataGridViewProperties_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dataGridViewProperties;
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
            dgv.Columns["Action"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            int displayIndex = 0;
            void SetCol(string name, string header, int width, string? format = null)
            {
                if (dgv.Columns[name] != null)
                {
                    dgv.Columns[name].HeaderText = header;
                    dgv.Columns[name].Width = width;
                    dgv.Columns[name].DisplayIndex = displayIndex++;
                    dgv.Columns[name].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                    if (format != null)
                        dgv.Columns[name].DefaultCellStyle.Format = format;
                    dgv.Columns[name].MinimumWidth = width;
                }
            }
            SetCol("Title", "Property Name", 150);
            SetCol("Owner", "Owner Name", 160);
            SetCol("KhasraNo", "Khasra No", 100);
            SetCol("Area", "Area (sq.ft)", 120);
            SetCol("BuyPrice", "Buy Price", 155, "C");
            if (dgv.Columns["Id"] != null)
                dgv.Columns["Id"].Visible = false;
            if (dgv.Columns["Type"] != null)
                dgv.Columns.Remove("Type");
            if (dgv.Columns["Status"] != null)
                dgv.Columns.Remove("Status");
            if (dgv.Columns["Description"] != null)
                dgv.Columns.Remove("Description");
            if (dgv.Columns["AmountPaid"] != null)
                dgv.Columns.Remove("AmountPaid");
            if (dgv.Columns["TotalLoanPrincipal"] != null)
                dgv.Columns.Remove("TotalLoanPrincipal");
            if (dgv.Columns["AmountBalance"] != null)
                dgv.Columns.Remove("AmountBalance");
            if (dgv.Columns["Action"] != null)
            {
                dgv.Columns["Action"].DisplayIndex = dgv.Columns.Count - 1;
                dgv.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Action"].Width = 150;
                dgv.Columns["Action"].MinimumWidth = 150;
            }
        }

        private void DataGridViewProperties_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            // Add your logic here or leave empty for now
        }

        private void DataGridViewProperties_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = dataGridViewProperties;
            var colName = grid.Columns[e.ColumnIndex].Name;
            if (colName == "AmountBalance" || colName == "BuyPrice" || colName == "AmountPaid" || colName == "TotalLoanPrincipal" || colName == "Area")
            {
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.Value = "NILL";
                    e.FormattingApplied = true;
                }
                else if (decimal.TryParse(e.Value.ToString(), out decimal val))
                {
                    e.Value = val % 1 == 0 ? val.ToString("N0") : val.ToString("N2");
                    e.FormattingApplied = true;
                }
            }
        }

        private void DataGridViewProperties_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridViewProperties.CurrentRow != null)
            {
                var idCell = dataGridViewProperties.CurrentRow.Cells["Id"];
                if (idCell?.Value != null && int.TryParse(idCell.Value.ToString(), out int propertyId))
                {
                    _selectedPropertyId = propertyId; // Track selection
                    LoadPlotsForProperty(propertyId);
                }
                else
                {
                    dataGridViewPlots.Rows.Clear();
                    var lblTotalPlots = rightPanel.Controls.Find("lblTotalPlots", true).FirstOrDefault() as Label;
                    if (lblTotalPlots != null)
                        lblTotalPlots.Text = "0";
                }
            }
            else
            {
                dataGridViewPlots.Rows.Clear();
                var lblTotalPlots = rightPanel.Controls.Find("lblTotalPlots", true).FirstOrDefault() as Label;
                if (lblTotalPlots != null)
                    lblTotalPlots.Text = "0";
            }
        }

        private void DataGridViewProperties_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewProperties.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                var whatsappIcon = Properties.Resources.whatsapp;
                var viewIcon = Properties.Resources.view;
                var editIcon = Properties.Resources.edit;
                var deleteIcon = Properties.Resources.delete1;
                int iconWidth = 24, iconHeight = 24, padding = 12;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;
                e?.Graphics?.DrawImage(editIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;
                e?.Graphics?.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;
                e?.Graphics?.DrawImage(deleteIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;
                e?.Graphics?.DrawImage(whatsappIcon, new Rectangle(x, y, iconWidth, iconHeight));
                e.Handled = true;
            }
        }

        private void DataGridViewPlots_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Implement your logic for cell content click if needed
        }

        private void DataGridViewPlots_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewPlots.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                var whatsappIcon = Properties.Resources.whatsapp;
                var viewIcon = Properties.Resources.view;
                var editIcon = Properties.Resources.edit;
                var deleteIcon = Properties.Resources.delete1;
                int iconWidth = 24, iconHeight = 24, padding = 12;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;
                e?.Graphics?.DrawImage(editIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;
                e?.Graphics?.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;
                e?.Graphics?.DrawImage(deleteIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;
                e?.Graphics?.DrawImage(whatsappIcon, new Rectangle(x, y, iconWidth, iconHeight));
                e.Handled = true;
            }
        }

        private void DataGridViewPlots_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            // Implement your logic for cell mouse click if needed
        }

        private void DataGridViewPlots_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridViewPlots.CurrentRow != null)
            {
                var row = dataGridViewPlots.CurrentRow;
                var lblPlotEmail = rightPanel.Controls.Find("lblPlotEmail", true).FirstOrDefault() as Label;
                var lblPlotAmountPaid = rightPanel.Controls.Find("lblPlotAmountPaid", true).FirstOrDefault() as Label;
                var lblPlotBalance = rightPanel.Controls.Find("lblPlotBalance", true).FirstOrDefault() as Label;
                var plotIdCell = row.Cells["Id"];
                int plotId = 0;
                if (plotIdCell?.Value != null && int.TryParse(plotIdCell.Value.ToString(), out int parsedPlotId))
                    plotId = parsedPlotId;
                decimal amountPaid = 0;
                decimal balance = 0;
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString))
                {
                    conn.Open();
                    using (var transCmd = new SqlCommand("SELECT ISNULL(SUM(Amount), 0) FROM PlotTransaction WHERE PlotId = @PlotId AND IsDeleted = 0", conn))
                    {
                        transCmd.Parameters.AddWithValue("@PlotId", plotId);
                        var result = transCmd.ExecuteScalar();
                        if (result != null && decimal.TryParse(result.ToString(), out decimal parsedAmountPaid))
                            amountPaid = parsedAmountPaid;
                    }
                    using (var saleCmd = new SqlCommand("SELECT ISNULL(SaleAmount, 0) FROM PlotSale WHERE PlotId = @PlotId", conn))
                    {
                        saleCmd.Parameters.AddWithValue("@PlotId", plotId);
                        var result = saleCmd.ExecuteScalar();
                        decimal saleAmount = 0;
                        if (result != null && decimal.TryParse(result.ToString(), out decimal parsedSaleAmount))
                            saleAmount = parsedSaleAmount;
                        balance = saleAmount - amountPaid;
                    }
                }
                if (lblPlotAmountPaid != null)
                    lblPlotAmountPaid.Text = amountPaid.ToString("C");
                if (lblPlotBalance != null)
                    lblPlotBalance.Text = balance.ToString("C");
            }
            else
            {
                var lblPlotEmail = rightPanel.Controls.Find("lblPlotEmail", true).FirstOrDefault() as Label;
                var lblPlotAmountPaid = rightPanel.Controls.Find("lblPlotAmountPaid", true).FirstOrDefault() as Label;
                var lblPlotBalance = rightPanel.Controls.Find("lblPlotBalance", true).FirstOrDefault() as Label;
                if (lblPlotEmail != null) lblPlotEmail.Text = "";
                if (lblPlotAmountPaid != null) lblPlotAmountPaid.Text = "";
                if (lblPlotBalance != null) lblPlotBalance.Text = "";
            }
        }

        private void DataGridViewPlots_SizeChanged(object? sender, EventArgs e)
        {
            AdjustPlotGridColumnWidths();
        }

        private void AdjustPlotGridColumnWidths()
        {
            // Get total grid width
            int gridWidth = dataGridViewPlots.ClientSize.Width;
            // Set desired minimum widths for each column
            int[] minWidths = { 80, 160, 110, 80, 80, 120, 90, 60 }; // PlotNumber, CustomerName, CustomerPhone, Status, Area, SaleDate, SalePrice, Action
            string[] colNames = { "PlotNumber", "CustomerName", "CustomerPhone", "Status", "Area", "SaleDate", "SalePrice", "Action" };
            int totalMinWidth = 0;
            for (int i = 0; i < minWidths.Length; i++) totalMinWidth += minWidths[i];
            int extra = gridWidth - totalMinWidth;
            int customerNameExtra = extra > 0 ? extra : 0;
            for (int i = 0; i < colNames.Length; i++)
            {
                var col = dataGridViewPlots.Columns[colNames[i]];
                if (col != null)
                {
                    if (colNames[i] == "CustomerName")
                    {
                        col.Width = minWidths[i] + customerNameExtra;
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    }
                    else
                    {
                        col.Width = minWidths[i];
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    }
                }
            }
        }

        private static void ApplyGridStyle(DataGridView grid)
        {
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.ColumnHeadersHeight = 32;
            grid.Dock = DockStyle.Top;
            grid.ReadOnly = true;
            grid.RowHeadersWidth = 51;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.GridColor = Color.LightGray;
            grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(220, 237, 255),
                SelectionForeColor = Color.Black,
                BackColor = Color.White
            };
            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.MidnightBlue,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                SelectionBackColor = Color.MidnightBlue,
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.False
            };
            grid.EnableHeadersVisualStyles = false;
            grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 248, 255),
                ForeColor = Color.Black
            };
            grid.RowTemplate.Height = 28;
        }
    }
}
