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
            labelTitle = new Label();
            labelCompany = new Label();
            labelContact = new Label();
            labelPhone = new Label();
            labelEmail = new Label();

            SuspendLayout();

            // labelTitle
            labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTitle.Text = "About This Application";
            labelTitle.Location = new Point(30, 20);
            labelTitle.Size = new Size(350, 40);

            // labelCompany
            labelCompany.Font = new Font("Segoe UI", 11F);
            labelCompany.Text = "Jay Maa Durga Housing Agency is powered by VVT Softwares Pvt. Ltd.\n\nVVT Softwares is a leading provider of innovative IT solutions, specializing in real estate management, business automation, and custom software development. Our mission is to deliver reliable, user-friendly, and scalable solutions to help businesses grow.";
            labelCompany.Location = new Point(30, 70);
            labelCompany.Size = new Size(350, 80);

            // labelContact
            labelContact.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelContact.Text = "Contact Information:";
            labelContact.Location = new Point(30, 160);
            labelContact.Size = new Size(350, 25);

            // labelPhone
            labelPhone.Font = new Font("Segoe UI", 11F);
            labelPhone.Text = "Phone: +91-9637151024";
            labelPhone.Location = new Point(30, 190);
            labelPhone.Size = new Size(350, 25);

            // labelEmail
            labelEmail.Font = new Font("Segoe UI", 11F);
            labelEmail.Text = "Email: yandralal@live.com";
            labelEmail.Location = new Point(30, 220);
            labelEmail.Size = new Size(350, 25);

            // AboutForm
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 270);
            Controls.Add(labelTitle);
            Controls.Add(labelCompany);
            Controls.Add(labelContact);
            Controls.Add(labelPhone);
            Controls.Add(labelEmail);
            Text = "About";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            ResumeLayout(false);
        }
    }
}