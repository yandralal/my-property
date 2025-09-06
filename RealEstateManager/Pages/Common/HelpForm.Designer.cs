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
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTitle.Location = new Point(30, 30);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(300, 40);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Help & Support";
            // 
            // labelPhone
            // 
            labelPhone.Font = new Font("Segoe UI", 12F);
            labelPhone.Location = new Point(30, 80);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(300, 30);
            labelPhone.TabIndex = 1;
            labelPhone.Text = "Phone: +91-9637151024";
            // 
            // labelEmail
            // 
            labelEmail.Font = new Font("Segoe UI", 12F);
            labelEmail.Location = new Point(30, 120);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(300, 30);
            labelEmail.TabIndex = 2;
            labelEmail.Text = "Email: yandralal@live.com";
            // 
            // HelpForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 200);
            Controls.Add(labelTitle);
            Controls.Add(labelPhone);
            Controls.Add(labelEmail);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HelpForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Help";
            ResumeLayout(false);
        }
    }
}