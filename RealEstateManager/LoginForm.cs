using System.Data.OleDb;

namespace RealEstateManager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data", "Database.xlsx");
            if (!File.Exists(filePath))
            {
                MessageBox.Show("User database file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool isValid = false;
            try
            {
                string connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
                using (var conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    // Assuming the first worksheet is named "Sheet1"
                    string query = "SELECT * FROM [Users$]";
                    using var cmd = new OleDbCommand(query, conn);
                    using var reader = cmd.ExecuteReader();
                    while (reader != null && reader.Read())
                    {
                        string dbUsername = reader["UserName"]?.ToString()?.Trim() ?? "";
                        string dbPassword = reader["PasswordHash"]?.ToString()?.Trim() ?? "";
                        if (username.Equals(dbUsername, StringComparison.OrdinalIgnoreCase) && password == dbPassword)
                        {
                            isValid = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading user database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isValid)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Proceed to next form or logic
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonShowPassword_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = !textBoxPassword.UseSystemPasswordChar;
            buttonShowPassword.Text = textBoxPassword.UseSystemPasswordChar ? "👁‍🗨" : "👁";
        }
    }
}
