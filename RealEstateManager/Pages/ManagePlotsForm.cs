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
            LoadWingsAndPlots();
        }

        private void LoadWingsAndPlots()
        {
            // Load wings and plots from the database for the given property
            // Display in DataGridViews or TreeView as per your UI preference
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

        private void buttonAddWing_Click(object sender, EventArgs e) { /* Insert new wing */ }
        private void buttonEditWing_Click(object sender, EventArgs e) { /* Edit selected wing */ }
        private void buttonDeleteWing_Click(object sender, EventArgs e) { /* Delete selected wing */ }
        private void buttonAddPlot_Click(object sender, EventArgs e) { /* Insert new plot(s) */ }
        private void buttonEditPlot_Click(object sender, EventArgs e) { /* Edit selected plot */ }
        private void buttonDeletePlot_Click(object sender, EventArgs e) { /* Delete selected plot */ }
    }
}