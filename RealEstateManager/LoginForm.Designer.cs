namespace RealEstateManager
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.CheckBox checkBoxRemember;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button buttonShowPassword;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            panelLogin = new Panel();
            buttonShowPassword = new Button();
            pictureBoxLogo = new PictureBox();
            labelTitle = new Label();
            textBoxUsername = new TextBox();
            textBoxPassword = new TextBox();
            buttonLogin = new Button();
            checkBoxRemember = new CheckBox();
            panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // panelLogin
            // 
            panelLogin.BackColor = Color.WhiteSmoke;
            panelLogin.BorderStyle = BorderStyle.FixedSingle;
            panelLogin.Controls.Add(buttonShowPassword);
            panelLogin.Controls.Add(pictureBoxLogo);
            panelLogin.Controls.Add(labelTitle);
            panelLogin.Controls.Add(textBoxUsername);
            panelLogin.Controls.Add(textBoxPassword);
            panelLogin.Controls.Add(buttonLogin);
            panelLogin.Controls.Add(checkBoxRemember);
            panelLogin.Location = new Point(200, 75);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(400, 363);
            // 
            // buttonShowPassword
            // 
            buttonShowPassword.FlatStyle = FlatStyle.Flat;
            buttonShowPassword.Location = new Point(315, 200);
            buttonShowPassword.Name = "buttonShowPassword";
            buttonShowPassword.Size = new Size(35, 34);
            buttonShowPassword.Text = "👁";
            buttonShowPassword.Click += ButtonShowPassword_Click;
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Image = Properties.Resources.logo;
            pictureBoxLogo.Location = new Point(160, 10);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(70, 63);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabStop = false;
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            labelTitle.ForeColor = Color.DodgerBlue;
            labelTitle.Location = new Point(0, 100);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(400, 40);
            labelTitle.TabIndex = 5;
            labelTitle.Text = "Sign In";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBoxUsername
            // 
            textBoxUsername.Font = new Font("Segoe UI", 12F);
            textBoxUsername.Location = new Point(50, 150);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.PlaceholderText = "Username";
            textBoxUsername.Size = new Size(300, 34);
            textBoxUsername.TabIndex = 0;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Font = new Font("Segoe UI", 12F);
            textBoxPassword.Location = new Point(50, 200);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PlaceholderText = "Password";
            textBoxPassword.Size = new Size(267, 34);
            textBoxPassword.TabIndex = 1;
            textBoxPassword.UseSystemPasswordChar = true;
            // 
            // buttonLogin
            // 
            buttonLogin.BackColor = Color.ForestGreen;
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonLogin.ForeColor = Color.White;
            buttonLogin.Location = new Point(50, 290);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(300, 40);
            buttonLogin.TabIndex = 2;
            buttonLogin.Text = "Login";
            buttonLogin.UseVisualStyleBackColor = false;
            buttonLogin.Click += ButtonLogin_Click;
            // 
            // checkBoxRemember
            // 
            checkBoxRemember.Font = new Font("Segoe UI", 10F);
            checkBoxRemember.Location = new Point(50, 250);
            checkBoxRemember.Name = "checkBoxRemember";
            checkBoxRemember.Size = new Size(150, 24);
            checkBoxRemember.TabIndex = 3;
            checkBoxRemember.Text = "Remember Me";
            // 
            // LoginForm
            // 
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(813, 513);
            Controls.Add(panelLogin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
