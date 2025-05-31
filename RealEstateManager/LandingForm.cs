using Microsoft.Data.SqlClient;

namespace RealEstateManager
{
    public partial class LandingForm : Pages.BaseForm
    {
        public LandingForm()
        {
            InitializeComponent();
        }

        private void PropertyMenuItem_Click(object? sender, EventArgs e)
        {
            LoadActiveProperties();
            dataGridViewProperties.Visible = true;
            buttonAddProperty.Visible = true;
        }

        private void ButtonAddProperty_Click(object sender, EventArgs e)
        {
            var registerForm = new Pages.RegisterPropertyForm();
            registerForm.ShowDialog();
            LoadActiveProperties(); // Refresh grid after adding
        }

        private void ButtonManagePlots_Click(object sender, EventArgs e)
        {
            var managePlotsForm = new Pages.ManagePlotsForm();
            managePlotsForm.ShowDialog();
        }

        private void LoadActiveProperties()
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "SELECT Title, Type, Status, Price, Owner, Phone, Address, City, State, ZipCode, Description FROM Property WHERE IsDeleted = 0";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var dt = new System.Data.DataTable();
                conn.Open();
                adapter.Fill(dt);
                dataGridViewProperties.DataSource = dt;
            }
        }

        private void LandingForm_Load(object sender, EventArgs e)
        {

        }

        private void registerSaleMenuItem_Click(object sender, EventArgs e)
        {
            var registerSaleForm = new Pages.RegisterSaleForm();
            registerSaleForm.ShowDialog();
        }
    }
}