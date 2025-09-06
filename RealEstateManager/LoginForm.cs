using Microsoft.Data.SqlClient;
using RealEstateManager.Pages;
using System.Configuration; // <-- Add this

namespace RealEstateManager
{
    public partial class LoginForm : BaseForm
    {
        public LoginForm()
        {
            InitializeComponent();
            
            textBoxPassword.KeyDown += TextBoxPassword_KeyDown;
        }

        private void ButtonLogin_Click(object? sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString; 
            bool isValid = false;
            string userName = string.Empty;

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT UserId, UserName, PasswordHash FROM [Login] WHERE UserName = @username";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userName = reader["UserName"]?.ToString()?.Trim() ?? "";
                                string dbPassword = reader["PasswordHash"]?.ToString()?.Trim() ?? "";
                                if (username.Equals(userName, StringComparison.OrdinalIgnoreCase) && password == dbPassword)
                                {
                                    isValid = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading user database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isValid && !string.IsNullOrEmpty(userName))
            {
                LoggedInUserId = userName;
                string colorHex = "#F5F7FA"; // Default
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand("SELECT BackgroundColor FROM Login WHERE UserName = @UserId", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userName);
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null && !string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        string? colorHexResult = result as string;
                        if (!string.IsNullOrWhiteSpace(colorHexResult))
                        {
                            colorHex = colorHexResult;
                        }
                    }
                }
                GlobalBackgroundColor = ColorTranslator.FromHtml(colorHex);
                var landingForm = new LandingForm();
                landingForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonShowPassword_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = !textBoxPassword.UseSystemPasswordChar;
            buttonShowPassword.Text = textBoxPassword.UseSystemPasswordChar ? "👁‍🗨" : "👁";
        }

        private void TextBoxPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ButtonLogin_Click(sender, e);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            textBoxUsername.Focus();
        }
    }
}
