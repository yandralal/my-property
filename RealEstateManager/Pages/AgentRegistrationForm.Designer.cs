namespace RealEstateManager.Pages
{
    partial class AgentRegistrationForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtName;
        private TextBox txtContact;
        private TextBox txtAgency;
        private Button btnRegister;
        private Label lblName;
        private Label lblContact;
        private Label lblAgency;

        private void InitializeComponent()
        {
            txtName = new TextBox();
            txtContact = new TextBox();
            txtAgency = new TextBox();
            btnRegister = new Button();
            lblName = new Label();
            lblContact = new Label();
            lblAgency = new Label();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(205, 27);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 27);
            txtName.TabIndex = 1;
            // 
            // txtContact
            // 
            txtContact.Location = new Point(205, 67);
            txtContact.Name = "txtContact";
            txtContact.Size = new Size(200, 27);
            txtContact.TabIndex = 3;
            // 
            // txtAgency
            // 
            txtAgency.Location = new Point(205, 107);
            txtAgency.Name = "txtAgency";
            txtAgency.Size = new Size(200, 27);
            txtAgency.TabIndex = 5;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.Green;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(205, 157);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(200, 31);
            btnRegister.TabIndex = 6;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            // 
            // lblName
            // 
            lblName.Location = new Point(47, 31);
            lblName.Name = "lblName";
            lblName.Size = new Size(100, 23);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            // 
            // lblContact
            // 
            lblContact.Location = new Point(47, 71);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(100, 23);
            lblContact.TabIndex = 2;
            lblContact.Text = "Contact:";
            // 
            // lblAgency
            // 
            lblAgency.Location = new Point(47, 111);
            lblAgency.Name = "lblAgency";
            lblAgency.Size = new Size(100, 23);
            lblAgency.TabIndex = 4;
            lblAgency.Text = "Agency:";
            // 
            // AgentRegistrationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            ClientSize = new Size(574, 245);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblContact);
            Controls.Add(txtContact);
            Controls.Add(lblAgency);
            Controls.Add(txtAgency);
            Controls.Add(btnRegister);
            Name = "AgentRegistrationForm";
            Text = "Agent/Broker Registration";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}