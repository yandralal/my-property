using Microsoft.Data.SqlClient;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class RegisterPlotSaleForm : BaseForm
    {
        private readonly bool _isEditMode;
        private readonly int? _editPlotId;
        private readonly int? _editPropertyId;

        // Edit mode constructor
        public RegisterPlotSaleForm(
            int propertyId,
            int plotId,
            string plotNumber,
            string customerName,
            string customerPhone,
            string customerEmail,
            string status,
            string area,
            string saleDate,
            string salePrice
        )
        {
            InitializeComponent();
            ApplyDesignerAppearance(); // <-- Ensure designer look
            SetupPhoneNumberValidation();

            _isEditMode = true;
            _editPlotId = plotId;
            _editPropertyId = propertyId;

            // Load properties and set selected property
            LoadProperties();
            comboBoxProperty.SelectedValue = propertyId;

            // Load plots for the selected property and set selected plot
            LoadPlots(propertyId);
            comboBoxPlot.SelectedValue = plotId;

            // Fill customer and sale info
            textBoxCustomerName.Text = customerName;
            textBoxCustomerPhone.Text = customerPhone;
            textBoxCustomerEmail.Text = customerEmail;
            textBoxSaleAmount.Text = salePrice;
            if (DateTime.TryParse(saleDate, out var dt))
                dateTimePickerSaleDate.Value = dt;
            else
                dateTimePickerSaleDate.Value = DateTime.Now;

            comboBoxProperty.Enabled = false;
            comboBoxPlot.Enabled = false;

            // Set button text for edit mode
            if (string.IsNullOrEmpty(customerName) && string.IsNullOrEmpty(salePrice))
            {
                _isEditMode = false;
                buttonRegisterSale.Text = "Register Sale";
            }
            else
            {
                buttonRegisterSale.Text = "Edit Sale";
            }

            LoadAgents();

            if (_isEditMode && _editPlotId.HasValue)
            {
                string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
                string query = "SELECT AgentId, BrokerageAmount FROM PlotSale WHERE PlotId = @PlotId AND IsDeleted = 0";
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlotId", _editPlotId.Value);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                comboBoxAgent.SelectedValue = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                textBoxBrokerage.Text = reader.GetDecimal(1).ToString("0.##");
                        }
                    }
                }
            }
        }

        // Add mode constructor
        public RegisterPlotSaleForm()
        {
            InitializeComponent();
            ApplyDesignerAppearance(); // <-- Ensure designer look
            SetupPhoneNumberValidation();
            LoadProperties();
            LoadAgents();

            // Set button text for add mode
            buttonRegisterSale.Text = "Register Sale";
        }

        private void ApplyDesignerAppearance()
        {
            // These match the designer settings
            this.BackColor = Color.FromArgb(245, 248, 255);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            // Icon and other properties are set by designer, no need to repeat unless overridden elsewhere
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

        private void LoadAgents()
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "SELECT Id, Name FROM Agent WHERE IsDeleted = 0";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);

                comboBoxAgent.DataSource = dt;
                comboBoxAgent.DisplayMember = "Name";
                comboBoxAgent.ValueMember = "Id";
                comboBoxAgent.SelectedIndex = -1;
            }
        }

        private void ButtonRegisterSale_Click(object sender, EventArgs e)
        {
            int propertyId = 0;
            int? plotId = null;

            if (_isEditMode && _editPlotId.HasValue)
            {
                // In edit mode, use the plotId passed in
                plotId = _editPlotId;
            }
            else
            {
                propertyId = Convert.ToInt32(comboBoxProperty.SelectedValue);
                plotId = comboBoxPlot.SelectedValue as int?;
            }

            string customerName = textBoxCustomerName.Text.Trim();
            string customerPhone = textBoxCustomerPhone.Text.Trim();
            string customerEmail = textBoxCustomerEmail.Text.Trim();
            decimal saleAmount = decimal.TryParse(textBoxSaleAmount.Text, out var amt) ? amt : 0;
            DateTime saleDate = dateTimePickerSaleDate.Value;
            string createdBy = Environment.UserName;
            DateTime createdDate = DateTime.Now;

            if ((!_isEditMode && propertyId == 0) || string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(customerPhone) || saleAmount <= 0)
            {
                MessageBox.Show("Please fill all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";

            if (_isEditMode)
            {
                // Update existing sale
                string update = @"UPDATE PlotSale
                    SET AgentId = @AgentId, BrokerageAmount = @BrokerageAmount, CustomerName = @CustomerName, CustomerPhone = @CustomerPhone, CustomerEmail = @CustomerEmail,
                        SaleAmount = @SaleAmount, SaleDate = @SaleDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate
                    WHERE PlotId = @PlotId AND IsDeleted = 0";

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(update, conn))
                {
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    cmd.Parameters.AddWithValue("@AgentId", comboBoxAgent.SelectedValue);
                    cmd.Parameters.AddWithValue("@BrokerageAmount", string.IsNullOrEmpty(textBoxBrokerage.Text) ? 0 : Convert.ToDecimal(textBoxBrokerage.Text));
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@CustomerPhone", customerPhone);
                    cmd.Parameters.AddWithValue("@CustomerEmail", (object?)customerEmail ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SaleAmount", saleAmount);
                    cmd.Parameters.AddWithValue("@SaleDate", saleDate);
                    cmd.Parameters.AddWithValue("@ModifiedBy", createdBy);
                    cmd.Parameters.AddWithValue("@ModifiedDate", createdDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Set plot status to Booked
                    SetPlotStatusToBooked(conn, plotId);
                }

                MessageBox.Show("Sale updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Insert new sale
                string insert = @"INSERT INTO PlotSale
                    (PropertyId, PlotId, AgentId, BrokerageAmount, CustomerName, CustomerPhone, CustomerEmail, SaleAmount, SaleDate, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
                    VALUES (@PropertyId, @PlotId, @AgentId, @BrokerageAmount, @CustomerName, @CustomerPhone, @CustomerEmail, @SaleAmount, @SaleDate, @CreatedBy, @CreatedDate,
                    @ModifiedBy, @ModifiedDate)";

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(insert, conn))
                {
                    cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                    cmd.Parameters.AddWithValue("@BrokerageAmount", string.IsNullOrEmpty(textBoxBrokerage.Text) ? 0 : Convert.ToDecimal(textBoxBrokerage.Text));
                    cmd.Parameters.AddWithValue("@PlotId", (object?)plotId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentId", comboBoxAgent.SelectedValue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@CustomerPhone", customerPhone);
                    cmd.Parameters.AddWithValue("@CustomerEmail", (object?)customerEmail ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SaleAmount", saleAmount);
                    cmd.Parameters.AddWithValue("@SaleDate", saleDate);
                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                    cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
                    cmd.Parameters.AddWithValue("@ModifiedBy", createdBy);
                    cmd.Parameters.AddWithValue("@ModifiedDate", createdDate);
                    cmd.Parameters.AddWithValue("@IsDeleted", false);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Set plot status to Booked
                    SetPlotStatusToBooked(conn, plotId);
                }

                MessageBox.Show("Sale registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ComboBoxPlot_SelectedIndexChanged(object sender, EventArgs e)
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

        private void SetupPhoneNumberValidation()
        {
            // Restrict phone number input to digits only and max 10 chars
            textBoxCustomerPhone.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                // Prevent more than 10 digits
                if (!char.IsControl(e.KeyChar) && textBoxCustomerPhone.Text.Length >= 10)
                {
                    e.Handled = true;
                }
            };
            textBoxCustomerPhone.MaxLength = 10;
        }

        // Helper method to set plot status to Booked
        private void SetPlotStatusToBooked(SqlConnection conn, int? plotId)
        {
            if (plotId.HasValue)
            {
                string updatePlot = "UPDATE Plot SET Status = @Status WHERE Id = @PlotId";
                using (var cmd = new SqlCommand(updatePlot, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", "Booked");
                    cmd.Parameters.AddWithValue("@PlotId", plotId.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}