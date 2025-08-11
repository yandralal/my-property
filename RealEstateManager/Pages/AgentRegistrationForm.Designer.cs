namespace RealEstateManager.Pages
{
    partial class AgentRegistrationForm
    {
        private System.Windows.Forms.GroupBox groupBoxAgent;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label labelContact;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label labelAgency;
        private System.Windows.Forms.TextBox txtAgency;
        private System.Windows.Forms.Button btnRegister;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentRegistrationForm));
            groupBoxAgent = new GroupBox();
            labelName = new Label();
            btnRegister = new Button();
            txtName = new TextBox();
            labelContact = new Label();
            txtContact = new TextBox();
            labelAgency = new Label();
            txtAgency = new TextBox();
            groupBoxAgent.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxAgent
            // 
            groupBoxAgent.Controls.Add(labelName);
            groupBoxAgent.Controls.Add(btnRegister);
            groupBoxAgent.Controls.Add(txtName);
            groupBoxAgent.Controls.Add(labelContact);
            groupBoxAgent.Controls.Add(txtContact);
            groupBoxAgent.Controls.Add(labelAgency);
            groupBoxAgent.Controls.Add(txtAgency);
            groupBoxAgent.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxAgent.ForeColor = Color.MidnightBlue;
            groupBoxAgent.Location = new Point(19, 18);
            groupBoxAgent.Name = "groupBoxAgent";
            groupBoxAgent.Size = new Size(565, 253);
            groupBoxAgent.TabIndex = 0;
            groupBoxAgent.TabStop = false;
            groupBoxAgent.Text = "Agent Details";
            // 
            // labelName
            // 
            labelName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelName.ForeColor = Color.DarkSlateGray;
            labelName.Location = new Point(56, 51);
            labelName.Name = "labelName";
            labelName.Size = new Size(100, 23);
            labelName.TabIndex = 0;
            labelName.Text = "Name:";
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.Green;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(166, 191);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(300, 35);
            btnRegister.TabIndex = 1;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += BtnRegister_Click;
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 10F);
            txtName.Location = new Point(166, 51);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 30);
            txtName.TabIndex = 1;
            // 
            // labelContact
            // 
            labelContact.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelContact.ForeColor = Color.DarkSlateGray;
            labelContact.Location = new Point(56, 98);
            labelContact.Name = "labelContact";
            labelContact.Size = new Size(100, 23);
            labelContact.TabIndex = 2;
            labelContact.Text = "Contact:";
            // 
            // txtContact
            // 
            txtContact.BorderStyle = BorderStyle.FixedSingle;
            txtContact.Font = new Font("Segoe UI", 10F);
            txtContact.Location = new Point(166, 98);
            txtContact.MaxLength = 10;
            txtContact.Name = "txtContact";
            txtContact.Size = new Size(300, 30);
            txtContact.TabIndex = 3;
            // 
            // labelAgency
            // 
            labelAgency.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAgency.ForeColor = Color.DarkSlateGray;
            labelAgency.Location = new Point(56, 145);
            labelAgency.Name = "labelAgency";
            labelAgency.Size = new Size(100, 23);
            labelAgency.TabIndex = 4;
            labelAgency.Text = "Agency:";
            // 
            // txtAgency
            // 
            txtAgency.BorderStyle = BorderStyle.FixedSingle;
            txtAgency.Font = new Font("Segoe UI", 10F);
            txtAgency.Location = new Point(166, 145);
            txtAgency.Name = "txtAgency";
            txtAgency.Size = new Size(300, 30);
            txtAgency.TabIndex = 5;
            // 
            // AgentRegistrationForm
            // 
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(600, 286);
            Controls.Add(groupBoxAgent);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AgentRegistrationForm";
            Text = "Agent Registration";
            groupBoxAgent.ResumeLayout(false);
            groupBoxAgent.PerformLayout();
            ResumeLayout(false);
        }
    }
}