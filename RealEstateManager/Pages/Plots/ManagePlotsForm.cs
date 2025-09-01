using Microsoft.Data.SqlClient;
using RealEstateManager.Entities;
using System.Configuration;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class ManagePlotsForm : BaseForm
    {
        public ManagePlotsForm()
        {
            InitializeComponent();
            SetPaddingForControls(10, 6);
            LoadProperties();
            SetupPlotGrid();
            buttonAddPlot.Enabled = false;
        }

        // Add this property to manage plots for the selected property
        private List<Plot> CurrentPlots { get; set; } = new List<Plot>();

        private void LoadProperties()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
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
            dataGridViewPlots.Height = 363;

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
                Width = 150,
                ReadOnly = true
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 120,
                ReadOnly = true
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

        private void ButtonAddPlot_Click(object? sender, EventArgs e)
        {
            if (comboBoxProperty.SelectedValue is not int propertyId)
            {
                MessageBox.Show("Please select a property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!numericUpDownPlotCount.Enabled)
            {
                numericUpDownPlotCount.Enabled = true;
                buttonAddPlot.Enabled = false;
                numericUpDownPlotCount.Focus();
                return;
            }

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

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            int savedCount = 0;
            string currentUser = Environment.UserName;
            DateTime now = DateTime.Now;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (DataGridViewRow row in dataGridViewPlots.Rows)
                {
                    if (row.IsNewRow) continue;
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
            this.Close();
        }

        private void ButtonDeletePlot_Click(object sender, EventArgs e)
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
                dataGridViewPlots.Rows.Remove(row);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            bool isSold = false;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT COUNT(1) FROM PlotSale WHERE PlotId = @PlotId", conn))
                {
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    var count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        isSold = true;
                    }
                }
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
            this.Close();
        }

        private void ButtonDeleteSelectedPlots_Click(object? sender, EventArgs e)
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
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            int deletedCount = 0;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var row in selectedRows)
                {
                    if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId) || plotId == 0)
                    {
                        dataGridViewPlots.Rows.Remove(row);
                        deletedCount++;
                        continue;
                    }

                    bool isSold = false;
                    using (var cmd = new SqlCommand("SELECT COUNT(1) FROM PlotSale WHERE PlotId = @PlotId", conn))
                    {
                        cmd.Parameters.AddWithValue("@PlotId", plotId);
                        var count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            isSold = true;
                        }
                    }
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
            if (deletedCount > 0)
                this.Close();
        }

        private void ButtonEditPlot_Click(object? sender, EventArgs e)
        {
            if (comboBoxProperty.SelectedValue is not int propertyId)
            {
                MessageBox.Show("Please select a property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var allRows = dataGridViewPlots.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).ToList();

            if (allRows.Count == 0)
            {
                MessageBox.Show("No plots to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string currentUser = Environment.UserName;
            DateTime now = DateTime.Now;
            int updatedCount = 0;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var row in allRows)
                {
                    if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId) || plotId == 0)
                        continue;

                    string plotNumber = row.Cells["PlotNumber"].Value?.ToString() ?? "";
                    string status = row.Cells["Status"].Value?.ToString() ?? "";
                    string areaText = row.Cells["Area"].Value?.ToString() ?? "0";
                    decimal.TryParse(areaText, out decimal area);
                    if (area < 0) area = 0;

                    string select = @"SELECT PlotNumber, Status, Area FROM Plot WHERE Id=@Id AND PropertyId=@PropertyId";
                    string dbPlotNumber = "", dbStatus = "";
                    decimal dbArea = 0;
                    using (var selectCmd = new SqlCommand(select, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@Id", plotId);
                        selectCmd.Parameters.AddWithValue("@PropertyId", propertyId);
                        using (var reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dbPlotNumber = reader["PlotNumber"].ToString() ?? "";
                                dbStatus = reader["Status"].ToString() ?? "";
                                dbArea = reader["Area"] != DBNull.Value ? Convert.ToDecimal(reader["Area"]) : 0;
                            }
                        }
                    }

                    if (plotNumber != dbPlotNumber || status != dbStatus || area != dbArea)
                    {
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
            }

            MessageBox.Show($"{updatedCount} plot(s) updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadPlotsForProperty(propertyId);
            RefreshLandingFormGrids(propertyId);
            this.Close();
        }

        private void LoadPlotsForProperty(int propertyId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"SELECT Id, PlotNumber, Status, Area, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted 
                     FROM Plot WHERE PropertyId = @PropertyId AND IsDeleted = 0";

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
                        var plot = new Plot
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

        private void ComboBoxProperty_SelectedIndexChanged(object sender, EventArgs e)
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

        private void NumericUpDownPlotCount_ValueChanged(object? sender, EventArgs e)
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
                var plot = new Plot
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

        private static void RefreshLandingFormGrids(int propertyId)
        {
            // Find the open LandingForm instance
            foreach (Form form in Application.OpenForms)
            {
                if (form is LandingForm landingForm)
                {
                    landingForm.LoadActiveProperties(propertyId);
                    landingForm.LoadPlotsForProperty(propertyId);
                    break;
                }
            }
        }
    }
}