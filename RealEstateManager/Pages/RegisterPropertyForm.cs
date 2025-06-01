using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace RealEstateManager.Pages
{
    public partial class RegisterPropertyForm : Form
    {
        public RegisterPropertyForm()
        {
            InitializeComponent();
            SetupPhoneNumberValidation();
            SetupPriceFormatting();
        }

        private void SetupPhoneNumberValidation()
        {
            // Restrict phone number input to digits only and max 10 chars
            textBoxPhone.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                // Prevent more than 10 digits
                if (!char.IsControl(e.KeyChar) && textBoxPhone.Text.Length >= 10)
                {
                    e.Handled = true;
                }
            };
            textBoxPhone.MaxLength = 10;
        }

        private void SetupPriceFormatting()
        {
            textBoxPrice.Leave += (s, e) =>
            {
                if (decimal.TryParse(textBoxPrice.Text, out decimal val))
                {
                    textBoxPrice.Text = val.ToString("F2");
                }
            };
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            // Collect data
            string title = textBoxTitle.Text.Trim();
            string type = comboBoxType.Text.Trim();
            string status = comboBoxStatus.Text.Trim();
            string priceText = textBoxPrice.Text.Trim();
            string owner = textBoxOwner.Text.Trim();
            string phone = textBoxPhone.Text.Trim();
            string address = textBoxAddress.Text.Trim();
            string city = textBoxCity.Text.Trim();
            string state = textBoxState.Text.Trim();
            string zip = textBoxZip.Text.Trim();
            string description = textBoxDescription.Text.Trim();

            // Validation
            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(type) ||
                string.IsNullOrWhiteSpace(status) ||
                string.IsNullOrWhiteSpace(priceText) ||
                string.IsNullOrWhiteSpace(owner) ||
                string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Price validation and formatting
            if (!decimal.TryParse(priceText, out decimal priceValue) || priceValue < 0)
            {
                MessageBox.Show("Please enter a valid non-negative price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string price = priceValue.ToString("F2"); // Format as .00

            // Phone number validation: exactly 10 digits
            if (!Regex.IsMatch(phone, @"^\d{10}$"))
            {
                MessageBox.Show("Please enter a valid 10-digit phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Audit fields
            string createdBy = Environment.UserName;
            string createdDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string modifiedBy = createdBy;
            string modifiedDate = createdDate;
            int isDeleted = 0;

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string insert = @"INSERT INTO Property
                        ([Title], [Type], [Status], [Price], [Owner], [Phone], [Address], [City], [State], [ZipCode], [Description],
                         [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted])
                        VALUES
                        (@Title, @Type, @Status, @Price, @Owner, @Phone, @Address, @City, @State, @ZipCode, @Description,
                         @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @IsDeleted)";
                    using (var cmd = new SqlCommand(insert, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Owner", owner);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@City", city);
                        cmd.Parameters.AddWithValue("@State", state);
                        cmd.Parameters.AddWithValue("@ZipCode", zip);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                        cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
                        cmd.Parameters.AddWithValue("@ModifiedBy", modifiedBy);
                        cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                        cmd.Parameters.AddWithValue("@IsDeleted", isDeleted);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Property registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving property: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}