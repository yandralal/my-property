using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing.Drawing2D;

namespace RealEstateManager.Pages
{
    public partial class ManagePlotsForm : Form
    {
        public ManagePlotsForm()
        {
            InitializeComponent();
            LoadProperties();
            SetupPlotGrid();
            buttonAddPlot.Enabled = false;
            this.BackColor = Color.AliceBlue;
            this.BackgroundImageLayout = ImageLayout.Stretch; // Or Tile, Center, Zoom
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(230, 240, 255), // Light blue
                Color.FromArgb(100, 140, 220), // Deeper blue
                LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        // Add this property to manage plots for the selected property
        private List<PlotModel> CurrentPlots { get; set; } = new List<PlotModel>();

        private void LoadProperties()
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "SELECT Id, Title FROM Property WHERE IsDeleted = 0";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);

                comboBoxProperty.DataSource = dt;
                comboBoxProperty.DisplayMember = "Title";
                comboBoxProperty.ValueMember = "Id";
                comboBoxProperty.SelectedIndex = -1; // No selection by default
            }
        }

        private void SetupPlotGrid()
        {
            dataGridViewPlots.Columns.Clear();
            dataGridViewPlots.AutoGenerateColumns = false;
            dataGridViewPlots.Height = 430;

            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                Width = 40,
                ReadOnly = true,
                Visible = false
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlotNumber",
                HeaderText = "Plot Number",
                Width = 150
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
                Width = 120,
                ReadOnly = false
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedBy",
                HeaderText = "Created By",
                Width = 100,
                ReadOnly = true,
                Visible = false
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedDate",
                HeaderText = "Created Date",
                Width = 130,
                ReadOnly = true,
                Visible = false
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ModifiedBy",
                HeaderText = "Modified By",
                Width = 100,
                ReadOnly = true,
                Visible = false
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ModifiedDate",
                HeaderText = "Modified Date",
                Width = 130,
                ReadOnly = true,
                Visible = false
            });
            dataGridViewPlots.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsDeleted",
                HeaderText = "Is Deleted",
                Width = 80,
                ReadOnly = true,
                Visible = false
            });

            dataGridViewPlots.MultiSelect = true;
            dataGridViewPlots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void buttonAddPlot_Click(object? sender, EventArgs e)
        {
            if (comboBoxProperty.SelectedValue is not int propertyId)
            {
                MessageBox.Show("Please select a property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // If numericUpDownPlotCount is disabled, enable it and return (for your earlier logic)
            if (!numericUpDownPlotCount.Enabled)
            {
                numericUpDownPlotCount.Enabled = true;
                buttonAddPlot.Enabled = false;
                numericUpDownPlotCount.Focus();
                return;
            }

            // --- Area validation ---
            foreach (DataGridViewRow row in dataGridViewPlots.Rows)
            {
                if (row.IsNewRow) continue;

                var areaCell = row.Cells["Area"];
                string areaText = areaCell.Value?.ToString() ?? "";
                if (string.IsNullOrWhiteSpace(areaText) ||
                    !decimal.TryParse(areaText, out decimal area))
                {
                    dataGridViewPlots.CurrentCell = areaCell;
                    dataGridViewPlots.BeginEdit(true);
                    MessageBox.Show("Please enter a valid, positive number for Area in all plots.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (area < 0)
                {
                    areaCell.Value = 0m;
                }
            }
            // --- End Area validation ---

            // --- Duplicate PlotNumber validation in grid ---
            var plotNumbersInGrid = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataGridViewRow row in dataGridViewPlots.Rows)
            {
                if (row.IsNewRow) continue;
                string plotNumber = row.Cells["PlotNumber"].Value?.ToString() ?? "";
                if (string.IsNullOrWhiteSpace(plotNumber)) continue;
                if (!plotNumbersInGrid.Add(plotNumber))
                {
                    MessageBox.Show($"Duplicate Plot Number '{plotNumber}' found in the grid. Please ensure all plot numbers are unique.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // --- End Duplicate PlotNumber validation in grid ---

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            int savedCount = 0;
            string currentUser = Environment.UserName;
            DateTime now = DateTime.Now;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (DataGridViewRow row in dataGridViewPlots.Rows)
                {
                    if (row.IsNewRow) continue;

                    // Only add if Id cell is empty (new row, not yet in DB)
                    var idCell = row.Cells["Id"].Value;
                    if (idCell != null && !string.IsNullOrWhiteSpace(idCell.ToString()))
                        continue;

                    string plotNumber = row.Cells["PlotNumber"].Value?.ToString() ?? "";
                    string status = row.Cells["Status"].Value?.ToString() ?? "Available";
                    decimal area = 0;
                    var areaCellValue = row.Cells["Area"].Value;
                    if (areaCellValue != null && decimal.TryParse(areaCellValue.ToString(), out var parsedArea))
                    {
                        area = parsedArea;
                    }

                    if (string.IsNullOrWhiteSpace(plotNumber))
                        continue;

                    // --- Check for duplicate PlotNumber in DB for this property ---
                    using (var checkCmd = new SqlCommand("SELECT COUNT(1) FROM Plot WHERE PropertyId = @PropertyId AND PlotNumber = @PlotNumber AND IsDeleted = 0", conn))
                    {
                        checkCmd.Parameters.AddWithValue("@PropertyId", propertyId);
                        checkCmd.Parameters.AddWithValue("@PlotNumber", plotNumber);
                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show($"Plot Number '{plotNumber}' already exists for this property. Duplicate plots will not be added.", "Duplicate Plot Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }
                    }
                    // --- End DB duplicate check ---

                    string insert = @"INSERT INTO Plot (PropertyId, PlotNumber, Status, Area, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted) 
                        VALUES (@PropertyId, @PlotNumber, @Status, @Area, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @IsDeleted)";
                    using (var cmd = new SqlCommand(insert, conn))
                    {
                        cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                        cmd.Parameters.AddWithValue("@PlotNumber", plotNumber);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Area", area);
                        cmd.Parameters.AddWithValue("@CreatedBy", currentUser);
                        cmd.Parameters.AddWithValue("@CreatedDate", now);
                        cmd.Parameters.AddWithValue("@ModifiedBy", currentUser);
                        cmd.Parameters.AddWithValue("@ModifiedDate", now);
                        cmd.Parameters.AddWithValue("@IsDeleted", false);
                        savedCount += cmd.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show($"{savedCount} plot(s) added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadPlotsForProperty(propertyId);
            RefreshLandingFormGrids(propertyId);
        }

        private void buttonDeletePlot_Click(object sender, EventArgs e)
        {
            if (comboBoxProperty.SelectedValue is not int propertyId)
            {
                MessageBox.Show("Please select a property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dataGridViewPlots.CurrentRow == null)
            {
                MessageBox.Show("Please select a plot to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridViewPlots.CurrentRow;

            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId) || plotId == 0)
            {
                // Just remove from grid if not saved in DB
                dataGridViewPlots.Rows.Remove(row);
                return;
            }

            // Check in PlotSale and PlotTransaction tables before allowing deletion
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            bool isSold = false;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check PlotSale table
                using (var cmd = new SqlCommand("SELECT COUNT(1) FROM PlotSale WHERE PlotId = @PlotId", conn))
                {
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    var count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        isSold = true;
                    }
                }

                // Check PlotTransaction table if not already found in PlotSale
                if (!isSold)
                {
                    using (var cmd = new SqlCommand("SELECT COUNT(1) FROM PlotTransaction WHERE PlotId = @PlotId", conn))
                    {
                        cmd.Parameters.AddWithValue("@PlotId", plotId);
                        var count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            isSold = true;
                        }
                    }
                }
            }

            if (isSold)
            {
                MessageBox.Show("You cannot delete a plot that is already sold.", "Delete Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this plot?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            string delete = "DELETE FROM Plot WHERE Id=@Id AND PropertyId=@PropertyId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(delete, conn))
            {
                cmd.Parameters.AddWithValue("@Id", plotId);
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            dataGridViewPlots.Rows.Remove(row);
            MessageBox.Show("Plot deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshLandingFormGrids(propertyId);
        }

        private void buttonDeleteSelectedPlots_Click(object? sender, EventArgs e)
        {
            if (comboBoxProperty.SelectedValue is not int propertyId)
            {
                MessageBox.Show("Please select a property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRows = dataGridViewPlots.SelectedRows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .ToList();

            if (selectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one plot to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete {selectedRows.Count} selected plot(s)?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            int deletedCount = 0;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var row in selectedRows)
                {
                    if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId) || plotId == 0)
                    {
                        // Not saved in DB, just remove from grid
                        dataGridViewPlots.Rows.Remove(row);
                        deletedCount++;
                        continue;
                    }

                    // Check in PlotSale and PlotTransaction tables before allowing deletion
                    bool isSold = false;

                    // Check PlotSale table
                    using (var cmd = new SqlCommand("SELECT COUNT(1) FROM PlotSale WHERE PlotId = @PlotId", conn))
                    {
                        cmd.Parameters.AddWithValue("@PlotId", plotId);
                        var count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            isSold = true;
                        }
                    }

                    // Check PlotTransaction table if not already found in PlotSale
                    if (!isSold)
                    {
                        using (var cmd = new SqlCommand("SELECT COUNT(1) FROM PlotTransaction WHERE PlotId = @PlotId", conn))
                        {
                            cmd.Parameters.AddWithValue("@PlotId", plotId);
                            var count = (int)cmd.ExecuteScalar();
                            if (count > 0)
                            {
                                isSold = true;
                            }
                        }
                    }

                    if (isSold)
                    {
                        MessageBox.Show($"{row.Cells["PlotNumber"].Value} cannot be deleted because it is already sold.", "Delete Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    // Delete from DB
                    using (var cmd = new SqlCommand("DELETE FROM Plot WHERE Id=@Id AND PropertyId=@PropertyId", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", plotId);
                        cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                        cmd.ExecuteNonQuery();
                    }
                    dataGridViewPlots.Rows.Remove(row);
                    deletedCount++;
                }
            }

            if (deletedCount > 0)
                MessageBox.Show($"{deletedCount} plot(s) deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadPlotsForProperty(propertyId);
            RefreshLandingFormGrids(propertyId);
        }

        private void buttonEditPlot_Click(object? sender, EventArgs e)
        {
            if (comboBoxProperty.SelectedValue is not int propertyId)
            {
                MessageBox.Show("Please select a property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRows = dataGridViewPlots.SelectedRows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .ToList();

            if (selectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one plot to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string currentUser = Environment.UserName;
            DateTime now = DateTime.Now;
            int updatedCount = 0;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var row in selectedRows)
                {
                    if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId) || plotId == 0)
                        continue;

                    string plotNumber = row.Cells["PlotNumber"].Value?.ToString() ?? "";
                    string status = row.Cells["Status"].Value?.ToString() ?? "";
                    string areaText = row.Cells["Area"].Value?.ToString() ?? "0";
                    decimal area = 0;
                    decimal.TryParse(areaText, out area);
                    if (area < 0) area = 0;

                    string update = @"UPDATE Plot 
                        SET PlotNumber=@PlotNumber, Status=@Status, Area=@Area, ModifiedBy=@ModifiedBy, ModifiedDate=@ModifiedDate 
                        WHERE Id=@Id AND PropertyId=@PropertyId";

                    using (var cmd = new SqlCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue("@PlotNumber", plotNumber);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Area", area);
                        cmd.Parameters.AddWithValue("@ModifiedBy", currentUser);
                        cmd.Parameters.AddWithValue("@ModifiedDate", now);
                        cmd.Parameters.AddWithValue("@Id", plotId);
                        cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                        updatedCount += cmd.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show($"{updatedCount} plot(s) updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadPlotsForProperty(propertyId);
            // Refresh property and plot grid in LandingForm
            RefreshLandingFormGrids(propertyId);
        }

        private void LoadPlotsForProperty(int propertyId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"SELECT Id, PlotNumber, Status, Area, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted 
                     FROM Plot WHERE PropertyId = @PropertyId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);

                dataGridViewPlots.Rows.Clear();
                CurrentPlots.Clear();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var plot = new PlotModel
                        {
                            Id = row["Id"] as int?,
                            PlotNumber = row["PlotNumber"].ToString() ?? "",
                            Status = row["Status"].ToString() ?? "",
                            Area = decimal.TryParse(row["Area"].ToString(), out var area) ? area : 0,
                            CreatedBy = row["CreatedBy"].ToString(),
                            CreatedDate = row["CreatedDate"] as DateTime?,
                            ModifiedBy = row["ModifiedBy"].ToString(),
                            ModifiedDate = row["ModifiedDate"] as DateTime?,
                            IsDeleted = row["IsDeleted"] is bool isDel && isDel
                        };
                        CurrentPlots.Add(plot);

                        dataGridViewPlots.Rows.Add(
                            plot.Id?.ToString(),
                            plot.PlotNumber,
                            plot.Status,
                            plot.Area.ToString(),
                            plot.CreatedBy,
                            plot.CreatedDate?.ToString("g") ?? "",
                            plot.ModifiedBy,
                            plot.ModifiedDate?.ToString("g") ?? "",
                            plot.IsDeleted
                        );
                    }

                    numericUpDownPlotCount.Enabled = false; // Disable if plots exist
                    buttonAddPlot.Enabled = true; // Allow edit/delete
                }
                else
                {
                    numericUpDownPlotCount.Enabled = true; // Enable if no plots
                    buttonAddPlot.Enabled = false; // Only enable after count is set
                }
            }
        }

        private void comboBoxProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProperty.SelectedValue is int propertyId)
            {
                LoadPlotsForProperty(propertyId);
            }
            else
            {
                dataGridViewPlots.Rows.Clear();
                numericUpDownPlotCount.Enabled = false;
                buttonAddPlot.Enabled = false;
            }
        }

        private void numericUpDownPlotCount_ValueChanged(object? sender, EventArgs e)
        {
            int addCount = (int)numericUpDownPlotCount.Value;
            if (addCount <= 0) {
                buttonAddPlot.Enabled = false;
                return;
            }

            // Find the highest plot number currently in the grid
            int maxPlotNumber = 0;
            foreach (DataGridViewRow row in dataGridViewPlots.Rows)
            {
                if (row.IsNewRow) continue;
                var plotNumberValue = row.Cells["PlotNumber"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(plotNumberValue) && plotNumberValue.StartsWith("Plot-"))
                {
                    if (int.TryParse(plotNumberValue.Substring(5), out int num))
                    {
                        if (num > maxPlotNumber)
                            maxPlotNumber = num;
                    }
                }
            }

            // Add new plots starting from next plot number
            for (int i = 1; i <= addCount; i++)
            {
                int newPlotNumber = maxPlotNumber + i;
                var plot = new PlotModel
                {
                    PlotNumber = $"Plot-{newPlotNumber}",
                    Status = "Available",
                    Area = 0
                    
                };
                CurrentPlots.Add(plot);
                dataGridViewPlots.Rows.Add(null, plot.PlotNumber, plot.Status, "");
            }

            buttonAddPlot.Enabled = addCount > 0;
        }

        private void RefreshLandingFormGrids(int propertyId)
        {
            // Find the open LandingForm instance
            foreach (Form form in Application.OpenForms)
            {
                if (form is RealEstateManager.LandingForm landingForm)
                {
                    landingForm.LoadActiveProperties();
                    landingForm.LoadPlotsForProperty(propertyId);
                    break;
                }
            }
        }
    }

    // Define a simple model for plot data
    public class PlotModel
    {
        public int? Id { get; set; }
        public string PlotNumber { get; set; } = "";
        public string Status { get; set; } = "Available";
        public decimal Area { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}