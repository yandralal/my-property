using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace RealEstateManager.Pages
{
    public partial class RegisterPropertyForm : BaseForm
    {
        private readonly int? _propertyId;
        private readonly bool _isEditMode;
        public int? SavedPropertyId { get; private set; }

        // Edit mode constructor
        public RegisterPropertyForm(int propertyId)
        {
            InitializeComponent();
            _propertyId = propertyId;
            _isEditMode = true;
            SetupPhoneNumberValidation();
            SetupNumericTextBoxValidation();
            SetupPriceFormatting();
            SetupAreaFormatting();
            LoadPropertyDetails();
            buttonRegister.Text = "Update Property";
            this.Text = "Edit Property";
        }

        public RegisterPropertyForm()
        {
            InitializeComponent();
            _isEditMode = false;
            SetupPhoneNumberValidation();
            SetupNumericTextBoxValidation();
            SetupPriceFormatting();
            SetupAreaFormatting();
        }

        private void LoadPropertyDetails()
        {
            if (!_propertyId.HasValue) return;

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"SELECT Title, Type, Status, Price, Owner, Phone, Address, City, State, ZipCode, Description, KhasraNo, Area
                             FROM Property WHERE Id = @Id AND IsDeleted = 0";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", _propertyId.Value);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBoxTitle.Text = reader["Title"].ToString();
                        comboBoxType.Text = reader["Type"].ToString();
                        comboBoxStatus.Text = reader["Status"].ToString();
                        textBoxPrice.Text = Convert.ToDecimal(reader["Price"]).ToString("F2");
                        textBoxOwner.Text = reader["Owner"].ToString();
                        textBoxPhone.Text = reader["Phone"].ToString();
                        textBoxAddress.Text = reader["Address"].ToString();
                        textBoxCity.Text = reader["City"].ToString();
                        textBoxState.Text = reader["State"].ToString();
                        textBoxZip.Text = reader["ZipCode"].ToString();
                        textBoxDescription.Text = reader["Description"].ToString();
                        textBoxKhasraNo.Text = reader["KhasraNo"]?.ToString() ?? "";
                        textBoxArea.Text = reader["Area"] != DBNull.Value ? Convert.ToDecimal(reader["Area"]).ToString("F2") : "";
                    }
                }
            }
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

        private void SetupAreaFormatting()
        {
            textBoxArea.Leave += (s, e) =>
            {
                if (decimal.TryParse(textBoxArea.Text, out decimal val))
                {
                    textBoxArea.Text = val.ToString("F2");
                }
            };
        }

        private void SetupNumericTextBoxValidation()
        {
            KeyPressEventHandler handler = (s, e) =>
            {
                TextBox tb = s as TextBox;
                char ch = e.KeyChar;

                // Allow control keys (backspace, delete, etc.)
                if (char.IsControl(ch))
                    return;

                // Allow only one decimal separator, and not as the first character
                if (ch == '.' && (tb.Text.Contains('.') || tb.SelectionStart == 0))
                {
                    e.Handled = true;
                    return;
                }

                // Allow digits only
                if (!char.IsDigit(ch) && ch != '.')
                {
                    e.Handled = true;
                }
            };

            textBoxPrice.KeyPress += handler;
            textBoxArea.KeyPress += handler;
        }

        private void ButtonRegister_Click(object sender, EventArgs e)
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
            string khasraNo = textBoxKhasraNo.Text.Trim();
            string areaText = textBoxArea.Text.Trim();

            // Mandatory field validation
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Property title is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxTitle.Focus();
                return;
            }
            if (title.Length < 3)
            {
                MessageBox.Show("Property title must be at least 3 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxTitle.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("Property type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxType.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(status))
            {
                MessageBox.Show("Property status is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxStatus.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(priceText))
            {
                MessageBox.Show("Price is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPrice.Focus();
                return;
            }
            if (!decimal.TryParse(priceText, out decimal priceValue) || priceValue < 0)
            {
                MessageBox.Show("Please enter a valid non-negative price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPrice.Focus();
                return;
            }
            string price = priceValue.ToString("F2");

            if (string.IsNullOrWhiteSpace(owner))
            {
                MessageBox.Show("Owner name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxOwner.Focus();
                return;
            }
            if (owner.Length < 3 || !owner.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                MessageBox.Show("Owner name must be at least 3 letters and contain only letters and spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxOwner.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Phone number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPhone.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(areaText))
            {
                MessageBox.Show("Area is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxArea.Focus();
                return;
            }
            if (!decimal.TryParse(areaText, out decimal areaValue) || areaValue < 0)
            {
                MessageBox.Show("Please enter a valid non-negative area.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxArea.Focus();
                return;
            }
            string area = areaValue.ToString("F2");

            // Optional: Validate zip code (if provided, must be 6 digits)
            if (!string.IsNullOrWhiteSpace(zip) && !Regex.IsMatch(zip, @"^\d{6}$"))
            {
                MessageBox.Show("Please enter a valid 6-digit zip code.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxZip.Focus();
                return;
            }

            string userIdentifier = (!string.IsNullOrEmpty(LoggedInUserId)) ? LoggedInUserId.ToString() : Environment.UserName;
            string modifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (_isEditMode && _propertyId.HasValue)
                    {
                        // Update existing property
                        string update = @"UPDATE Property SET
                            Title = @Title,
                            Type = @Type,
                            Status = @Status,
                            Price = @Price,
                            Owner = @Owner,
                            KhasraNo = @KhasraNo,
                            Phone = @Phone,
                            Address = @Address,
                            City = @City,
                            State = @State,
                            ZipCode = @ZipCode,
                            Description = @Description,
                            Area = @Area,
                            ModifiedBy = @ModifiedBy,
                            ModifiedDate = @ModifiedDate
                            WHERE Id = @Id AND IsDeleted = 0";
                        using (var cmd = new SqlCommand(update, conn))
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
                            cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                            cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                            cmd.Parameters.AddWithValue("@Id", _propertyId.Value);
                            cmd.Parameters.AddWithValue("@KhasraNo", khasraNo);
                            cmd.Parameters.AddWithValue("@Area", area);
                            cmd.ExecuteNonQuery();
                        }

                        this.SavedPropertyId = _propertyId;
                        MessageBox.Show("Property updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Insert new property
                        string createdDate = modifiedDate;
                        int isDeleted = 0;

                        string insert = @"INSERT INTO Property
                            ([Title], [Type], [Status], [Price], [Owner], [KhasraNo], [Phone], [Address], [City], [State], [ZipCode], [Description],
                             [Area], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted])
                            VALUES
                            (@Title, @Type, @Status, @Price, @Owner, @KhasraNo, @Phone, @Address, @City, @State, @ZipCode, @Description,
                             @Area, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @IsDeleted)";
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
                            cmd.Parameters.AddWithValue("@CreatedBy", userIdentifier);
                            cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
                            cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                            cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                            cmd.Parameters.AddWithValue("@IsDeleted", isDeleted);
                            cmd.Parameters.AddWithValue("@KhasraNo", khasraNo);
                            cmd.Parameters.AddWithValue("@Area", area);
                            cmd.ExecuteNonQuery();
                        }
                        this.SavedPropertyId = _propertyId ?? (int?)new SqlCommand("SELECT SCOPE_IDENTITY()", conn).ExecuteScalar();
                        MessageBox.Show("Property registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}