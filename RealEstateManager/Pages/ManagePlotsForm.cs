using Microsoft.Data.SqlClient;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class ManagePlotsForm : Form
    {
        public ManagePlotsForm()
        {
            InitializeComponent();
            LoadProperties();
            SetupPlotGrid();
        }

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
            dataGridViewPlots.Height = 500;

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
        }

        private void buttonAddPlot_Click(object? sender, EventArgs e)
        {
            if (comboBoxProperty.SelectedValue is not int propertyId)
            {
                MessageBox.Show("Please select a property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
        }

        private void buttonEditPlot_Click(object sender, EventArgs e)
        {
            if (comboBoxProperty.SelectedValue is not int propertyId)
            {
                MessageBox.Show("Please select a property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dataGridViewPlots.CurrentRow == null)
            {
                MessageBox.Show("Please select a plot to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridViewPlots.CurrentRow;
            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId) || plotId == 0)
            {
                MessageBox.Show("Cannot edit unsaved plot. Please save it first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string plotNumber = row.Cells["PlotNumber"].Value?.ToString() ?? "";
            string status = row.Cells["Status"].Value?.ToString() ?? "";
            string area = row.Cells["Area"].Value?.ToString() ?? "";

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string update = "UPDATE Plot SET PlotNumber=@PlotNumber, Status=@Status, Area=@Area WHERE Id=@Id AND PropertyId=@PropertyId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(update, conn))
            {
                cmd.Parameters.AddWithValue("@PlotNumber", plotNumber);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Area", area);
                cmd.Parameters.AddWithValue("@Id", plotId);
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Plot updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadPlotsForProperty(propertyId);
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

            var confirm = MessageBox.Show("Are you sure you want to delete this plot?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
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

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        dataGridViewPlots.Rows.Add(
                            row["Id"].ToString(),
                            row["PlotNumber"].ToString(),
                            row["Status"].ToString(),
                            row["Area"].ToString(),
                            row["CreatedBy"].ToString(),
                            row["CreatedDate"] is DateTime cd ? cd.ToString("g") : "",
                            row["ModifiedBy"].ToString(),
                            row["ModifiedDate"] is DateTime md ? md.ToString("g") : "",
                            row["IsDeleted"] is bool isDel ? isDel : false
                        );
                    }
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
            }
        }

        private void numericUpDownPlotCount_ValueChanged(object? sender, EventArgs e)
        {
            int count = (int)numericUpDownPlotCount.Value;
            dataGridViewPlots.Rows.Clear();

            for (int i = 0; i < count; i++)
            {
                dataGridViewPlots.Rows.Add(Convert.ToString(i + 1), $"Plot-{i + 1}", "Available", "");
            }
        }
    }
}