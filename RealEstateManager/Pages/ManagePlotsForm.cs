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
                ReadOnly = true
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlotNumber",
                HeaderText = "Plot Number",
                Width = 120
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 100
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Area",
                HeaderText = "Area",
                Width = 100
            });
        }

        private void buttonAddWing_Click(object sender, EventArgs e) { /* Insert new wing */ }
        private void buttonEditWing_Click(object sender, EventArgs e) { /* Edit selected wing */ }
        private void buttonDeleteWing_Click(object sender, EventArgs e) { /* Delete selected wing */ }
        private void buttonAddPlot_Click(object sender, EventArgs e) { /* Insert new plot(s) */ }
        private void buttonEditPlot_Click(object sender, EventArgs e) { /* Edit selected plot */ }
        private void buttonDeletePlot_Click(object sender, EventArgs e) { /* Delete selected plot */ }

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

        private void LoadPlotsForProperty(int propertyId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "SELECT Id, PlotNumber, Status, Area FROM Plot WHERE PropertyId = @PropertyId";

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
                            row["Area"].ToString()
                        );
                    }
                }
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