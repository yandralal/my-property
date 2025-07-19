namespace RealEstateManager.Pages
{
    partial class AgentRegistrationForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxAgent;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label labelContact;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label labelAgency;
        private System.Windows.Forms.TextBox txtAgency;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnDelete;

        private void InitializeComponent()
        {
            groupBoxAgent = new GroupBox();
            labelName = new Label();
            txtName = new TextBox();
            labelContact = new Label();
            txtContact = new TextBox();
            labelAgency = new Label();
            txtAgency = new TextBox();
            btnRegister = new Button();
            btnDelete = new Button();
            groupBoxAgent.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxAgent
            // 
            groupBoxAgent.Controls.Add(labelName);
            groupBoxAgent.Controls.Add(txtName);
            groupBoxAgent.Controls.Add(labelContact);
            groupBoxAgent.Controls.Add(txtContact);
            groupBoxAgent.Controls.Add(labelAgency);
            groupBoxAgent.Controls.Add(txtAgency);
            groupBoxAgent.Location = new Point(20, 20);
            groupBoxAgent.Name = "groupBoxAgent";
            groupBoxAgent.Size = new Size(350, 150);
            groupBoxAgent.TabIndex = 0;
            groupBoxAgent.TabStop = false;
            groupBoxAgent.Text = "Agent Details";
            // 
            // labelName
            // 
            labelName.Location = new Point(10, 20);
            labelName.Name = "labelName";
            labelName.Size = new Size(100, 23);
            labelName.TabIndex = 0;
            labelName.Text = "Name:";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(120, 20);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 27);
            txtName.TabIndex = 1;
            // 
            // labelContact
            // 
            labelContact.Location = new Point(10, 60);
            labelContact.Name = "labelContact";
            labelContact.Size = new Size(100, 23);
            labelContact.TabIndex = 2;
            labelContact.Text = "Contact:";
            // 
            // txtContact
            // 
            txtContact.BorderStyle = BorderStyle.FixedSingle;
            txtContact.Location = new Point(120, 60);
            txtContact.Name = "txtContact";
            txtContact.Size = new Size(300, 27);
            txtContact.TabIndex = 3;
            // 
            // labelAgency
            // 
            labelAgency.Location = new Point(10, 100);
            labelAgency.Name = "labelAgency";
            labelAgency.Size = new Size(100, 23);
            labelAgency.TabIndex = 4;
            labelAgency.Text = "Agency:";
            // 
            // txtAgency
            // 
            txtAgency.BorderStyle = BorderStyle.FixedSingle;
            txtAgency.Location = new Point(120, 100);
            txtAgency.Name = "txtAgency";
            txtAgency.Size = new Size(300, 27);
            txtAgency.TabIndex = 5;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(60, 190);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(100, 30);
            btnRegister.TabIndex = 1;
            btnRegister.Text = "Register";
            btnRegister.Click += btnRegister_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(180, 190);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 30);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            // 
            // AgentRegistrationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            ClientSize = new Size(574, 245);
            Controls.Add(groupBoxAgent);
            Controls.Add(btnRegister);
            Controls.Add(btnDelete);
            Name = "AgentRegistrationForm";
            Text = "Agent Registration";
            groupBoxAgent.ResumeLayout(false);
            groupBoxAgent.PerformLayout();
            ResumeLayout(false);
        }
    }
}