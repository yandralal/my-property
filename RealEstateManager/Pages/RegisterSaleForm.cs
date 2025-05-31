using Microsoft.Data.SqlClient;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class RegisterSaleForm : Form
    {
        public RegisterSaleForm()
        {
            InitializeComponent();
            LoadProperties();
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
            comboBoxPlot.DataSource = null;
        }

        private void ComboBoxProperty_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBoxProperty.SelectedValue is int propertyId)
            {
                LoadPlots(propertyId);
            }
            else
            {
                comboBoxPlot.DataSource = null;
            }
        }

        private void LoadPlots(int propertyId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "SELECT Id, PlotNumber FROM Plot WHERE PropertyId = @PropertyId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);

                comboBoxPlot.DataSource = dt;
                comboBoxPlot.DisplayMember = "PlotNumber";
                comboBoxPlot.ValueMember = "Id";
                comboBoxPlot.SelectedIndex = -1;
            }
        }

        private void ButtonRegisterSale_Click(object sender, EventArgs e)
        {
            int propertyId = Convert.ToInt32(comboBoxProperty.SelectedValue);
            int? plotId = comboBoxPlot.SelectedValue as int?;
            string customerName = textBoxCustomerName.Text.Trim();
            string customerPhone = textBoxCustomerPhone.Text.Trim();
            string customerEmail = textBoxCustomerEmail.Text.Trim();
            decimal saleAmount = decimal.TryParse(textBoxSaleAmount.Text, out var amt) ? amt : 0;
            DateTime saleDate = dateTimePickerSaleDate.Value;
            string createdBy = Environment.UserName;
            DateTime createdDate = DateTime.Now;

            if (propertyId == 0 || string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(customerPhone) || saleAmount <= 0)
            {
                MessageBox.Show("Please fill all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string insert = @"INSERT INTO PropertySale
                (PropertyId, PlotId, CustomerName, CustomerPhone, CustomerEmail, SaleAmount, SaleDate, CreatedBy, CreatedDate)
                VALUES (@PropertyId, @PlotId, @CustomerName, @CustomerPhone, @CustomerEmail, @SaleAmount, @SaleDate, @CreatedBy, @CreatedDate,
                @ModifiedBy, @ModifiedDate)";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                cmd.Parameters.AddWithValue("@PlotId", (object?)plotId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CustomerName", customerName);
                cmd.Parameters.AddWithValue("@CustomerPhone", customerPhone);
                cmd.Parameters.AddWithValue("@CustomerEmail", (object?)customerEmail ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SaleAmount", saleAmount);
                cmd.Parameters.AddWithValue("@SaleDate", saleDate);
                cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
                cmd.Parameters.AddWithValue("@ModifiedBy", createdBy);
                cmd.Parameters.AddWithValue("@ModifiedDate", createdDate);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Sale registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void comboBoxPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPlot.SelectedValue is int plotId)
            {
                string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
                string query = "SELECT Status FROM Plot WHERE Id = @PlotId";
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    conn.Open();
                    var status = cmd.ExecuteScalar() as string;
                    labelPlotStatus.Text = "Status: " + (status ?? "-");
                }
            }
            else
            {
                labelPlotStatus.Text = "Status: -";
            }
        }
    }
}