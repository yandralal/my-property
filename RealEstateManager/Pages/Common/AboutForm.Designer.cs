using System.Windows.Forms;
using System.Drawing;

namespace RealEstateManager.Pages
{
    partial class AboutForm
    {
        private Label labelTitle;
        private Label labelCompany;
        private Label labelContact;
        private Label labelPhone;
        private Label labelEmail;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            labelTitle = new Label();
            labelCompany = new Label();
            labelContact = new Label();
            labelPhone = new Label();
            labelEmail = new Label();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTitle.Location = new Point(30, 20);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(350, 40);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "About This Application";
            // 
            // labelCompany
            // 
            labelCompany.Font = new Font("Segoe UI", 11F);
            labelCompany.Location = new Point(30, 70);
            labelCompany.Name = "labelCompany";
            labelCompany.Size = new Size(350, 80);
            labelCompany.TabIndex = 1;
            labelCompany.Text = resources.GetString("labelCompany.Text");
            // 
            // labelContact
            // 
            labelContact.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelContact.Location = new Point(30, 160);
            labelContact.Name = "labelContact";
            labelContact.Size = new Size(350, 25);
            labelContact.TabIndex = 2;
            labelContact.Text = "Contact Information:";
            // 
            // labelPhone
            // 
            labelPhone.Font = new Font("Segoe UI", 11F);
            labelPhone.Location = new Point(30, 190);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(350, 25);
            labelPhone.TabIndex = 3;
            labelPhone.Text = "Phone: +91-9637151024";
            // 
            // labelEmail
            // 
            labelEmail.Font = new Font("Segoe UI", 11F);
            labelEmail.Location = new Point(30, 220);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(350, 25);
            labelEmail.TabIndex = 4;
            labelEmail.Text = "Email: yandralal@live.com";
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 270);
            Controls.Add(labelTitle);
            Controls.Add(labelCompany);
            Controls.Add(labelContact);
            Controls.Add(labelPhone);
            Controls.Add(labelEmail);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            ResumeLayout(false);
        }
    }
}