using Microsoft.Data.SqlClient;
using RealEstateManager.Entities;
using RealEstateManager.Pages;
using System.Configuration; 
using System.Data;

namespace RealEstateManager
{
    public partial class LandingForm : BaseForm
    {
        public DataGridView DataGridViewProperties => dataGridViewProperties;

        private void OnPropertyLoansChanged() => LoadActiveProperties();
        private int? _selectedPropertyId = null;
        public LandingForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            SetupPlotGrid();
            dataGridViewProperties.DataBindingComplete += DataGridViewProperties_DataBindingComplete;
            dataGridViewProperties.CellMouseClick += DataGridViewProperties_CellMouseClick;
            dataGridViewProperties.CellFormatting += DataGridViewProperties_CellFormatting;
            dataGridViewPlots.CellFormatting += DataGridViewPlots_CellFormatting;
            LoadActiveProperties();

            Pages.ManagePropertyLoansForm.PropertyLoansChanged += OnPropertyLoansChanged;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Pages.ManagePropertyLoansForm.PropertyLoansChanged -= OnPropertyLoansChanged;
            base.OnFormClosed(e);
        }

        private void ButtonAddProperty_Click(object sender, EventArgs e)
        {
            var registerForm = new Pages.RegisterPropertyForm();
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                int? selectedId = registerForm.SavedPropertyId;
                LoadActiveProperties(selectedId);
            }
        }

        private void ButtonManagePlots_Click(object sender, EventArgs e)
        {
            var managePlotsForm = new ManagePlotsForm();
            managePlotsForm.ShowDialog();
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
                    p.Description,
                    p.Phone,
                    ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0) AS AmountPaid,
                    -- Calculate total principal loan for the property
                    ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0) AS TotalLoanPrincipal,
                    -- AmountBalance = BuyPrice - AmountPaid - TotalLoanPrincipal
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

                // Add a hidden Phone column if it doesn't exist
                if (!dt.Columns.Contains("Phone"))
                {
                    dt.Columns.Add("Phone", typeof(string));
                }

                // Always clear columns and set AutoGenerateColumns before rebinding
                dataGridViewProperties.DataSource = null;
                dataGridViewProperties.Columns.Clear();
                dataGridViewProperties.AutoGenerateColumns = true;
                dataGridViewProperties.DataSource = dt.DefaultView;

                // Defensive: force UI to update columns
                dataGridViewProperties.Refresh();

                // Hide the Phone column in the grid if it exists
                if (dataGridViewProperties.Columns["Phone"] != null)
                    dataGridViewProperties.Columns["Phone"].Visible = false;

                // Update property count label
                labelProperties.Text = $"Properties ({dt.Rows.Count})";
            }

            // Set Area column header, width, and format
            if (dataGridViewProperties.Columns["Area"] != null)
            {
                dataGridViewProperties.Columns["Area"].HeaderText = "Area (sq.ft)";
                dataGridViewProperties.Columns["Area"].Width = 120;
                dataGridViewProperties.Columns["Area"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridViewProperties.Columns["Area"].DefaultCellStyle.Format = "N2";
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
                Width = 110
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerName",
                HeaderText = "Customer Name",
                Width = 215
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerPhone",
                HeaderText = "Phone",
                Width = 120
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerEmail",
                HeaderText = "Email",
                Width = 225
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
                HeaderText = "Area (sq.ft)",
                Width = 120
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SaleDate",
                HeaderText = "Sale Date",
                Width = 190
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SalePrice",
                HeaderText = "Sale Price",
                Width = 155
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AmountPaid",
                HeaderText = "Amount Paid",
                Width = 155
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AmountBalance",
                HeaderText = "Amount Balance",
                Width = 155
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
                Width = 170,
                ImageLayout = DataGridViewImageCellLayout.Normal
            };
            dataGridViewPlots.Columns.Add(actionColumn);

            // Center align the Action column header
            dataGridViewPlots.Columns["Action"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Set currency formatting for monetary columns
            if (dataGridViewPlots.Columns["SalePrice"] != null)
                dataGridViewPlots.Columns["SalePrice"].DefaultCellStyle.Format = "C";
            if (dataGridViewPlots.Columns["AmountPaid"] != null)
                dataGridViewPlots.Columns["AmountPaid"].DefaultCellStyle.Format = "C";
            if (dataGridViewPlots.Columns["AmountBalance"] != null)
                dataGridViewPlots.Columns["AmountBalance"].DefaultCellStyle.Format = "C";
            if (dataGridViewPlots.Columns["SaleDate"] != null)
                dataGridViewPlots.Columns["SaleDate"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";

            dataGridViewPlots.CellContentClick -= DataGridViewPlots_CellContentClick;
            dataGridViewPlots.CellContentClick += DataGridViewPlots_CellContentClick;

            dataGridViewPlots.CellPainting -= DataGridViewPlots_CellPainting;
            dataGridViewPlots.CellPainting += DataGridViewPlots_CellPainting;

            dataGridViewPlots.CellMouseClick -= DataGridViewPlots_CellMouseClick;
            dataGridViewPlots.CellMouseClick += DataGridViewPlots_CellMouseClick;

            // After all columns are added to dataGridViewPlots.Columns
            foreach (DataGridViewColumn col in dataGridViewPlots.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                col.Resizable = DataGridViewTriState.False;
            }
        }

        private void LandingForm_Load(object sender, EventArgs e)
        {

        }

        private void RegisterSaleMenuItem_Click(object sender, EventArgs e)
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

            int? selectedPropertyId = null;
            if (dataGridViewProperties.CurrentRow != null)
                selectedPropertyId = Convert.ToInt32(dataGridViewProperties.CurrentRow.Cells["Id"].Value);

            var editForm = new RegisterPlotSaleForm(
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
                LoadActiveProperties(selectedPropertyId);
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
                }
            }
            else
            {
                dataGridViewPlots.Rows.Clear();
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
                    ps.CustomerEmail AS CustomerEmail,
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

                dataGridViewPlots.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int plotId = Convert.ToInt32(row["Id"]);
                    decimal saleAmount = row["SaleAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SaleAmount"]);

                    decimal amountPaid = 0;
                    using (var transCmd = new SqlCommand("SELECT ISNULL(SUM(Amount), 0) FROM PlotTransaction WHERE PlotId = @PlotId AND IsDeleted = 0", conn))
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
                        row["SaleDate"],
                        saleAmount == 0 ? DBNull.Value : saleAmount,
                        amountPaid == 0 ? DBNull.Value : amountPaid,
                        amountBalance == 0 ? DBNull.Value : amountBalance,
                        row["HasSale"]
                    );
                }

                labelPlots.Text = $"Plots ({dt.Rows.Count})";
            }
        }

        private void DataGridViewPlots_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewPlots.Columns[e.ColumnIndex].Name == "ViewDetails")
            {
                var row = dataGridViewPlots.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId))
                {
                    var detailsForm = new PlotDetailsForm(plotId);
                    detailsForm.ShowDialog();
                }
            }
        }

        private void DataGridViewPlots_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewPlots.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Load your icons from resources
                var whatsappIcon = Properties.Resources.whatsapp;
                var viewIcon = Properties.Resources.view;
                var editIcon = Properties.Resources.edit;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 12;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;

                // Draw edit icon (first)
                e?.Graphics?.DrawImage(editIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw view icon (second)
                e?.Graphics?.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw delete icon (third)
                e?.Graphics?.DrawImage(deleteIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw WhatsApp icon (last)
                e?.Graphics?.DrawImage(whatsappIcon, new Rectangle(x, y, iconWidth, iconHeight));

                e.Handled = true;
            }
        }

        private void DataGridViewPlots_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewPlots.Columns[e.ColumnIndex].Name == "Action")
            {
                int iconWidth = 24, padding = 12;
                int x = e.X - padding;
                int iconIndex = x / (iconWidth + padding);

                var row = dataGridViewPlots.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId))
                {
                    int propertyId = 0;

                    if (dataGridViewProperties.CurrentRow != null && dataGridViewProperties.CurrentRow.Cells["Id"].Value != null)
                    {
                        var idCell = dataGridViewProperties.CurrentRow.Cells["Id"];
                        if (int.TryParse(idCell.Value?.ToString(), out propertyId) && propertyId > 0)
                        {
                            // propertyId is valid, proceed
                        }
                        else
                        {
                            MessageBox.Show("Please select a valid property.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a property.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    switch (iconIndex)
                    {
                        case 0:
                            // Edit
                            var hasSaleCell = row.Cells["HasSale"];
                            bool hasSale = hasSaleCell.Value != null && hasSaleCell.Value.ToString() == "1";
                            if (!hasSale)
                            {
                                MessageBox.Show("Edit is disabled because there is no sale entry for this plot.", "Edit Disabled", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                            var editForm = new RegisterPlotSaleForm(
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
                        case 1:
                            // View
                            var detailsForm = new PlotDetailsForm(plotId);
                            detailsForm.ShowDialog();
                            break;
                        case 2:
                            // Delete
                            var result = MessageBox.Show("Are you sure you want to delete this plot?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {
                                DeletePlot(plotId);
                                LoadPlotsForProperty(propertyId);
                            }
                            break;
                        case 3:
                            // WhatsApp
                            string? phone = row.Cells["CustomerPhone"]?.Value?.ToString();
                            if (string.IsNullOrWhiteSpace(phone))
                            {
                                MessageBox.Show("No phone number found for the selected plot.", "No Phone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            using (var msgForm = new SendWhatsAppMessageForm())
                            {
                                if (msgForm.ShowDialog() == DialogResult.OK)
                                {
                                    string message = msgForm.MessageText;
                                    if (string.IsNullOrWhiteSpace(message))
                                    {
                                        MessageBox.Show("Please enter a message.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    SendWhatsAppMessage(phone, message);
                                    MessageBox.Show("WhatsApp message window opened for the selected customer.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
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

            var transactionForm = new RegisterPlotTransactionForm(plotId, saleAmount, plotNumber);
            transactionForm.ShowDialog();
        }

        private void DataGridViewProperties_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewProperties.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Load your icons from resources
                var whatsappIcon = Properties.Resources.whatsapp;
                var viewIcon = Properties.Resources.view;
                var editIcon = Properties.Resources.edit;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 12;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;

                // Draw edit icon (first)
                e?.Graphics?.DrawImage(editIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw view icon (second)
                e?.Graphics?.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw delete icon (third)
                e?.Graphics?.DrawImage(deleteIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;
                
                // Draw WhatsApp icon
                e?.Graphics?.DrawImage(whatsappIcon, new Rectangle(x, y, iconWidth, iconHeight));

                e.Handled = true;
            }
        }

        private void DataGridViewProperties_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewProperties.Columns[e.ColumnIndex].Name == "Action")
            {
                int iconWidth = 24, padding = 12;
                int x = e.X - padding;
                int iconIndex = x / (iconWidth + padding);

                var row = dataGridViewProperties.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["Id"].Value?.ToString(), out int propertyId))
                {
                    // iconIndex: 0=Edit, 1=View, 2=Delete, 3=WhatsApp
                    switch (iconIndex)
                    {
                        case 0:
                            // Edit
                            var editForm = new RegisterPropertyForm(propertyId);
                            if (editForm.ShowDialog() == DialogResult.OK)
                            {
                                int? selectedId = editForm.SavedPropertyId ?? propertyId;
                                LoadActiveProperties(selectedId);
                            }
                            break;
                        case 1:
                            // View
                            var viewForm = new PropertyDetailsForm(propertyId);
                            if (viewForm.ShowDialog() == DialogResult.OK)
                            {
                                LoadActiveProperties();
                            }
                            break;
                        case 2:
                            // Delete
                            var result = MessageBox.Show("Are you sure you want to delete this property?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {
                                DeleteProperty(propertyId);
                                LoadActiveProperties();
                            }
                            break;
                        case 3:
                            // WhatsApp icon clicked
                            string? phone = row.Cells["Phone"]?.Value?.ToString();
                            if (string.IsNullOrWhiteSpace(phone))
                            {
                                MessageBox.Show("No phone number found for the selected property.", "No Phone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            using (var msgForm = new SendWhatsAppMessageForm())
                            {
                                if (msgForm.ShowDialog() == DialogResult.OK)
                                {
                                    string message = msgForm.MessageText;
                                    if (string.IsNullOrWhiteSpace(message))
                                    {
                                        MessageBox.Show("Please enter a message.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    SendWhatsAppMessage(phone, message);
                                    MessageBox.Show("WhatsApp message window opened for the selected customer.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            break;
                    }
                }
            }
        }

        private static void DeleteProperty(int propertyId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
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
                       
                        string userIdentifier = (!string.IsNullOrEmpty(LoggedInUserId)) ? LoggedInUserId.ToString() : Environment.UserName;

                        // 4. Soft delete the property
                        using (var cmd = new SqlCommand("UPDATE Property SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE Id = @Id", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Id", propertyId);
                            cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                            cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        // 5. Soft delete all plots under this property
                        using (var cmd = new SqlCommand("UPDATE Plot SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE PropertyId = @PropertyId", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                            cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                            cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
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

        private static void DeletePlot(int plotId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
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

                        // 4. Soft delete the plot and update ModifiedBy/ModifiedDate
                        string userIdentifier = (!string.IsNullOrEmpty(LoggedInUserId)) ? LoggedInUserId.ToString() : Environment.UserName;
                        using (var cmd = new SqlCommand("UPDATE Plot SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE Id = @Id", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Id", plotId);
                            cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                            cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
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
            dataGridViewProperties.Columns["Action"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Set headers, widths, order, and AutoSizeMode
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

            SetCol("Title", "Property Name", 180);
            SetCol("Type", "Type", 100); 
            SetCol("Status", "Status", 90);
            SetCol("Owner", "Owner Name", 160);
            SetCol("KhasraNo", "Khasra No", 100);
            SetCol("Area", "Area (sq.ft)", 120);
            SetCol("BuyPrice", "Buy Price", 155, "C");
            SetCol("AmountPaid", "Amount Paid", 155, "C");
            SetCol("TotalLoanPrincipal", "Total Loan", 160, "C"); // <-- Add this line
            SetCol("AmountBalance", "Amount Balance", 155, "C");
            SetCol("Description", "Description", 200);

            if (dgv.Columns["Id"] != null)
                dgv.Columns["Id"].Visible = false;

            if (dgv.Columns["Action"] != null)
            {
                dgv.Columns["Action"].DisplayIndex = dgv.Columns.Count - 1;
                dgv.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Action"].Width = 150;
                dgv.Columns["Action"].MinimumWidth = 150;
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
        private static void ViewProperty(string? propertyId)
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
                    // Use the SavedPropertyId to restore selection
                    int? selectedId = editForm.SavedPropertyId ?? id;
                    LoadActiveProperties(selectedId);
                }
            }
        }

        private void DeletePropertyWithConfirm(string? propertyId)
        {
            if (int.TryParse(propertyId, out int id))
            {
                var result = MessageBox.Show("Are you sure you want to delete this property?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
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

            var transactionForm = new RegisterPropertyTransactionForm(propertyId, saleAmount, propertyNumber);
            transactionForm.ShowDialog();
        }

        private void RegisterPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterPropertyForm();
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                LoadActiveProperties(); // Refresh grid after adding
            }
        }

        private void ViewAllAgentsMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ViewAllAgentsForm();
            form.ShowDialog();
        }

        private void AgentTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? selectedPropertyId = null;
            if (dataGridViewProperties.CurrentRow != null)
            {
                var idCell = dataGridViewProperties.CurrentRow.Cells["Id"];
                if (idCell != null && int.TryParse(idCell.Value?.ToString(), out int propertyId))
                {
                    selectedPropertyId = propertyId;
                }
            }

            var agentTransactionForm = new RegisterAgentTransactionForm(agentId: null, propertyId: selectedPropertyId);
            agentTransactionForm.ShowDialog();
        }

        private void MiscTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new RegisterMiscTransactionForm();
            form.ShowDialog();
        }

        private void ViewReportsMenuItem_Click(object sender, EventArgs e)
        {
            var filterForm = new AllTransactionsFilterForm();
            filterForm.ShowDialog();
        }

        private void SendMessageToAllMenuItem_Click(object sender, EventArgs e)
        {
            // Show dialog to get the message
            using var msgForm = new SendWhatsAppMessageForm();
            if (msgForm.ShowDialog() != DialogResult.OK)
                return;

            string message = msgForm.MessageText;
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please enter a message.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get the selected row
            var selectedRow = dataGridViewProperties.CurrentRow;
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a property row to send the message.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string? phone = selectedRow.Cells["Phone"]?.Value?.ToString();
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("No phone number found for the selected property.", "No Phone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SendWhatsAppMessage(phone, message);

            MessageBox.Show("WhatsApp message window opened for the selected customer.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ChangeBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string userIdentifier = (!string.IsNullOrEmpty(LoggedInUserId)) ? LoggedInUserId.ToString() : Environment.UserName;
            using var customizeForm = new CustomizeBackgroundForm(userIdentifier);
            customizeForm.ShowDialog();
        }

        private void DataGridViewPlots_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewPlots.Columns[e.ColumnIndex].Name == "AmountBalance")
            {
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.Value = "NILL";
                    e.FormattingApplied = true;
                }
            }
        }

        private void DataGridViewProperties_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewProperties.Columns[e.ColumnIndex].Name == "AmountBalance")
            {
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.Value = "NILL";
                    e.FormattingApplied = true;
                }
            }
        }

        private void helpMenuItem_Click(object sender, EventArgs e)
        {
            using (var helpForm = new HelpForm())
            {
                helpForm.ShowDialog(this);
            }
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutForm = new Pages.AboutForm())
            {
                aboutForm.ShowDialog(this);
            }
        }

        private void DownloadUserGuideMenuItem_Click(object sender, EventArgs e)
        {
            // Update the path to point to the assets folder
            string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "User Guide.pdf");

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveDialog.FileName = "User Guide.pdf";
                saveDialog.Title = "Save User Guide";

                if (File.Exists(pdfPath) && saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(pdfPath, saveDialog.FileName, true);
                        MessageBox.Show("User Guide downloaded successfully.", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to download the User Guide.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!File.Exists(pdfPath))
                {
                    MessageBox.Show("User Guide PDF not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PropertyLoanMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewProperties.CurrentRow == null)
            {
                MessageBox.Show("Please select a property.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var idCell = dataGridViewProperties.CurrentRow.Cells["Id"];
            var titleCell = dataGridViewProperties.CurrentRow.Cells["Title"];
            if (idCell == null || !int.TryParse(idCell.Value?.ToString(), out int propertyId))
            {
                MessageBox.Show("Invalid property selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string propertyTitle = titleCell?.Value?.ToString() ?? "";

            // Fetch existing loan for this property (if any)
            LoanTransaction? loan = null;
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT TOP 1 * FROM LoanTransaction WHERE PropertyId = @PropertyId", conn))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        loan = new LoanTransaction
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PropertyId = reader["PropertyId"] as int?,
                            LoanAmount = reader.GetDecimal(reader.GetOrdinal("LoanAmount")),
                            LenderName = reader["LenderName"].ToString() ?? "",
                            InterestRate = reader.GetDecimal(reader.GetOrdinal("InterestRate")),
                            LoanDate = reader.GetDateTime(reader.GetOrdinal("LoanDate")),
                            Remarks = reader["Remarks"] as string,
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                        };
                    }
                }
            }

            // Open the form with loan details if exists, else just prepopulate property
            if (loan != null)
            {
                var form = new PropertyLoanForm(loan);
                //form..Text = propertyTitle; // Show property name
                //form.textBoxPropertyId.ReadOnly = true;
                form.ShowDialog();
            }
            else
            {
                var form = new PropertyLoanForm();
                //form.textBoxPropertyId.Text = propertyTitle; // Show property name
                //form.textBoxPropertyId.ReadOnly = true;
                form.ShowDialog();
            }
        }

        private void PropertyLoanTransactionMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewProperties.CurrentRow == null)
            {
                MessageBox.Show("Please select a property.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var idCell = dataGridViewProperties.CurrentRow.Cells["Id"];
            var titleCell = dataGridViewProperties.CurrentRow.Cells["Title"];
            if (idCell == null || !int.TryParse(idCell.Value?.ToString(), out int propertyId))
            {
                MessageBox.Show("Invalid property selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string propertyTitle = titleCell?.Value?.ToString() ?? "";

            var form = new PropertyLoanTransactionForm(propertyId, propertyTitle);
            form.ShowDialog();
        }

        private void ManagePropertyLoansMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ManagePropertyLoansForm();
            form.ShowDialog();
        }
    }
}