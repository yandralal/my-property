using System.Windows.Forms;
using System.Drawing;

namespace RealEstateManager.Pages
{
    partial class HelpForm
    {
        private Label labelTitle;
        private Label labelPhone;
        private Label labelEmail;

        private void InitializeComponent()
        {
            labelTitle = new Label();
            labelPhone = new Label();
            labelEmail = new Label();

            SuspendLayout();

            // labelTitle
            labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTitle.Text = "Help & Support";
            labelTitle.Location = new Point(30, 30);
            labelTitle.Size = new Size(300, 40);

            // labelPhone
            labelPhone.Font = new Font("Segoe UI", 12F);
            labelPhone.Text = "Phone: +91-9637151024";
            labelPhone.Location = new Point(30, 80);
            labelPhone.Size = new Size(300, 30);

            // labelEmail
            labelEmail.Font = new Font("Segoe UI", 12F);
            labelEmail.Text = "Email: yandralal@live.com";
            labelEmail.Location = new Point(30, 120);
            labelEmail.Size = new Size(300, 30);

            // HelpForm
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 200);
            Controls.Add(labelTitle);
            Controls.Add(labelPhone);
            Controls.Add(labelEmail);
            Text = "Help";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            ResumeLayout(false);
        }
    }
}