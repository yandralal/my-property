using Microsoft.Data.SqlClient;
using System.Configuration;
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
            AddMandatoryFieldStars();
            SetupPhoneNumberValidation();
            textBoxSaleAmount.Leave += FormatDecimalTextBoxOnLeave;
            textBoxBrokerage.Leave += FormatDecimalTextBoxOnLeave;

            _isEditMode = true;
            _editPlotId = plotId;
            _editPropertyId = propertyId;

            LoadProperties();
            comboBoxProperty.SelectedValue = propertyId;

            // Load plots for the selected property and set selected plot
            LoadPlots(propertyId);
            comboBoxPlot.SelectedValue = plotId;

            // Fill customer and sale info
            textBoxCustomerName.Text = customerName;
            textBoxCustomerPhone.Text = customerPhone;
            textBoxCustomerEmail.Text = customerEmail;
            textBoxSaleAmount.Text = decimal.TryParse(salePrice, out var saleAmt)
                ? saleAmt.ToString("N2")
                : "0.00";
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
                string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
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
                                textBoxBrokerage.Text = reader.GetDecimal(1).ToString("N2");
                            else
                                textBoxBrokerage.Text = "0.00";
                        }
                    }
                }
            }
        }

        // Add mode constructor
        public RegisterPlotSaleForm()
        {
            InitializeComponent();
            AddMandatoryFieldStars();
            SetupPhoneNumberValidation();
            SetupNumericTextBoxValidation();
            textBoxSaleAmount.Leave += FormatDecimalTextBoxOnLeave;
            textBoxBrokerage.Leave += FormatDecimalTextBoxOnLeave;
            LoadProperties();
            LoadAgents();
            buttonRegisterSale.Text = "Register Sale";
        }

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

                comboBoxProperty.DisplayMember = "Title";
                comboBoxProperty.ValueMember = "Id";
                comboBoxProperty.DataSource = dt;
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
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = "SELECT Id, PlotNumber FROM Plot WHERE PropertyId = @PropertyId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);

                comboBoxPlot.DisplayMember = "PlotNumber";
                comboBoxPlot.ValueMember = "Id";
                comboBoxPlot.DataSource = dt;
                comboBoxPlot.SelectedIndex = -1;
            }
        }

        private void LoadAgents()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = "SELECT Id, Name FROM Agent WHERE IsDeleted = 0";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);

                // Insert "Please select" option at the top
                var newRow = dt.NewRow();
                newRow["Id"] = 0;
                newRow["Name"] = "Please select";
                dt.Rows.InsertAt(newRow, 0);

                comboBoxAgent.DataSource = dt;
                comboBoxAgent.DisplayMember = "Name";
                comboBoxAgent.ValueMember = "Id";
                comboBoxAgent.SelectedIndex = 0;
            }
        }

        private void ButtonRegisterSale_Click(object sender, EventArgs e)
        {
            // Parse sale amount and brokerage amount once for reuse
            decimal saleAmount = decimal.TryParse(textBoxSaleAmount.Text, out var parsedSaleAmt) ? parsedSaleAmt : 0;
            decimal brokerageAmount = decimal.TryParse(textBoxBrokerage.Text, out var parsedBrokerage) ? parsedBrokerage : 0;

            // Check for deal cancellation (all fields cleared or zero)
            bool isDealCancelled =
                string.IsNullOrWhiteSpace(textBoxCustomerName.Text)
                && string.IsNullOrWhiteSpace(textBoxCustomerPhone.Text)
                && string.IsNullOrWhiteSpace(textBoxCustomerEmail.Text)
                && saleAmount == 0
                && (comboBoxAgent.SelectedValue == null || comboBoxAgent.SelectedValue.ToString() == "0")
                && brokerageAmount == 0;
            bool flowControl = HandleDealCancellation(isDealCancelled);
            if (!flowControl)
            {
                return;
            }

            // Validate property selection
            int propertyId = 0;
            int? plotId = null;
            if (_isEditMode && _editPlotId.HasValue)
            {
                plotId = _editPlotId;
                propertyId = _editPropertyId ?? 0;
            }
            else
            {
                if (comboBoxProperty.SelectedValue == null || !int.TryParse(comboBoxProperty.SelectedValue.ToString(), out propertyId) || propertyId == 0)
                {
                    MessageBox.Show("Please select a property.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxProperty.Focus();
                    return;
                }
                plotId = comboBoxPlot.SelectedValue as int?;
                if (plotId == null || (int)plotId == 0)
                {
                    MessageBox.Show("Please select a plot.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxPlot.Focus();
                    return;
                }
            }

            // Validate customer name
            string customerName = textBoxCustomerName.Text.Trim();
            if (string.IsNullOrWhiteSpace(customerName) || customerName.Length < 3 || !customerName.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                MessageBox.Show("Please enter a valid customer name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxCustomerName.Focus();
                return;
            }

            // Validate customer phone
            string customerPhone = textBoxCustomerPhone.Text.Trim();
            if (customerPhone.Length != 10 || !customerPhone.All(char.IsDigit) || !"6789".Contains(customerPhone[0]))
            {
                MessageBox.Show("Please enter a valid 10-digit mobile number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxCustomerPhone.Focus();
                return;
            }

            // Validate sale amount (allow 0 and above)
            if (saleAmount < 0)
            {
                MessageBox.Show("Please enter a valid sale amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxSaleAmount.Focus();
                return;
            }
            // Ensure textbox is formatted
            textBoxSaleAmount.Text = saleAmount.ToString("N2");

            // Validate sale date (not in future)
            DateTime saleDate = dateTimePickerSaleDate.Value;
            if (saleDate.Date > DateTime.Today)
            {
                MessageBox.Show("Sale date cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerSaleDate.Focus();
                return;
            }

            int agentId = 0;
            bool agentSelected = comboBoxAgent.SelectedValue != null && int.TryParse(comboBoxAgent.SelectedValue.ToString(), out agentId) && agentId > 0;

            if (agentSelected)
            {
                if (brokerageAmount <= 0)
                {
                    MessageBox.Show("Please enter a valid brokerage amount greater than zero when an agent is selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxBrokerage.Focus();
                    return;
                }
                textBoxBrokerage.Text = brokerageAmount.ToString("N2");
            }
            else
            {
                brokerageAmount = 0;
                textBoxBrokerage.Text = "0.00";
            }

            // Validate customer email (optional, but if provided, must be valid format)
            string customerEmail = textBoxCustomerEmail.Text.Trim();
            if (!string.IsNullOrWhiteSpace(customerEmail) && !System.Text.RegularExpressions.Regex.IsMatch(customerEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxCustomerEmail.Focus();
                return;
            }

            string createdBy = (!string.IsNullOrEmpty(LoggedInUserId)) ? LoggedInUserId.ToString() : Environment.UserName;
            DateTime createdDate = DateTime.Now;

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;

            if (_isEditMode)
            {
                string update = @"UPDATE PlotSale
                    SET AgentId = @AgentId, BrokerageAmount = @BrokerageAmount, CustomerName = @CustomerName, CustomerPhone = @CustomerPhone, CustomerEmail = @CustomerEmail,
                        SaleAmount = @SaleAmount, SaleDate = @SaleDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate
                    WHERE PlotId = @PlotId AND IsDeleted = 0";

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(update, conn))
                {
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    cmd.Parameters.AddWithValue("@AgentId", agentId);
                    cmd.Parameters.AddWithValue("@BrokerageAmount", brokerageAmount);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@CustomerPhone", customerPhone);
                    cmd.Parameters.AddWithValue("@CustomerEmail", (object?)customerEmail ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SaleAmount", saleAmount);
                    cmd.Parameters.AddWithValue("@SaleDate", saleDate);
                    cmd.Parameters.AddWithValue("@ModifiedBy", createdBy);
                    cmd.Parameters.AddWithValue("@ModifiedDate", createdDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    SetPlotStatusToBooked(conn, plotId);
                }

                MessageBox.Show("Sale updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string insert = @"INSERT INTO PlotSale
                    (PropertyId, PlotId, AgentId, BrokerageAmount, CustomerName, CustomerPhone, CustomerEmail, SaleAmount, SaleDate, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
                    VALUES (@PropertyId, @PlotId, @AgentId, @BrokerageAmount, @CustomerName, @CustomerPhone, @CustomerEmail, @SaleAmount, @SaleDate, @CreatedBy, @CreatedDate,
                    @ModifiedBy, @ModifiedDate)";

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(insert, conn))
                {
                    cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                    cmd.Parameters.AddWithValue("@BrokerageAmount", brokerageAmount);
                    cmd.Parameters.AddWithValue("@PlotId", (object?)plotId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentId", agentId);
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
                    SetPlotStatusToBooked(conn, plotId);
                }

                MessageBox.Show("Sale registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool HandleDealCancellation(bool isDealCancelled)
        {
            if (_isEditMode && _editPlotId.HasValue && isDealCancelled)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            using (var cmd = new SqlCommand("DELETE FROM PlotSale WHERE PlotId = @PlotId", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@PlotId", _editPlotId.Value);
                                cmd.ExecuteNonQuery();
                            }

                            // Set plot status back to Available
                            using (var cmd = new SqlCommand(
                                "UPDATE Plot SET Status = @Status, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE Id = @PlotId", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@Status", "Available");
                                cmd.Parameters.AddWithValue("@PlotId", _editPlotId.Value);
                                cmd.Parameters.AddWithValue("@ModifiedBy", (!string.IsNullOrEmpty(LoggedInUserId)) ? LoggedInUserId.ToString() : Environment.UserName);
                                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                            tran.Commit();
                            MessageBox.Show("Plot sale cancelled. The plot is now available for sale.", "Deal Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            MessageBox.Show("Failed to cancel plot sale.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                return false;
            }

            return true;
        }

        private void ComboBoxPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPlot.SelectedValue is int plotId)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
                string query = "SELECT Status FROM Plot WHERE Id = @PlotId";
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    conn.Open();
                    var status = cmd.ExecuteScalar() as string;
                    labelPlotStatusValue.Text = status == "Available" ? "For Sale" : "Booked"; 
                }
            }
            else
            {
                labelPlotStatusValue.Text = ""; 
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

        private void SetupNumericTextBoxValidation()
        {
            // Allow only digits, one decimal separator, and control keys
            KeyPressEventHandler handler = (s, e) =>
            {
                TextBox tb = s as TextBox;
                char ch = e.KeyChar;

                // Allow control keys (backspace, delete, etc.)
                if (char.IsControl(ch))
                    return;

                // Allow only one decimal separator
                if (ch == '.' && (tb.Text.Contains('.') || tb.SelectionStart == 0))
                {
                    e.Handled = true;
                    return;
                }

                // Allow digits
                if (!char.IsDigit(ch) && ch != '.')
                {
                    e.Handled = true;
                }
            };

            textBoxSaleAmount.KeyPress += handler;
            textBoxBrokerage.KeyPress += handler;
        }

        // Add this method to handle formatting for both fields
        private void FormatDecimalTextBoxOnLeave(object? sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (decimal.TryParse(tb.Text, out var value))
                    tb.Text = value.ToString("N2");
                else
                    tb.Text = "0.00";
            }
        }

        // Helper method to set plot status to Booked
        private static void SetPlotStatusToBooked(SqlConnection conn, int? plotId)
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

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // In both constructors, after InitializeComponent(), add red stars for mandatory fields (right of controls)
        private void AddMandatoryFieldStars()
        {
            void AddStar(Control target)
            {
                var star = new Label
                {
                    Text = "*",
                    ForeColor = Color.Red,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(target.Right + 5, target.Top + 2)
                };
                target.Parent.Controls.Add(star);
                star.BringToFront();
            }

            AddStar(comboBoxProperty);
            AddStar(comboBoxPlot);
            AddStar(textBoxCustomerName);
            AddStar(textBoxCustomerPhone);
            AddStar(textBoxSaleAmount);
            AddStar(dateTimePickerSaleDate);
            AddStar(textBoxBrokerage); // Always show, or show conditionally if agent is selected
        }
    }
}