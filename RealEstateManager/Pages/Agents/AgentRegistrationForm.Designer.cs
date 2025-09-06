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
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label labelNameStar;
        private System.Windows.Forms.Label labelContactStar;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentRegistrationForm));
            groupBoxAgent = new GroupBox();
            labelName = new Label();
            txtName = new TextBox();
            labelContact = new Label();
            txtContact = new TextBox();
            labelAgency = new Label();
            txtAgency = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            labelNameStar = new Label();
            labelContactStar = new Label();
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
            groupBoxAgent.Controls.Add(btnSave);
            groupBoxAgent.Controls.Add(btnCancel);
            groupBoxAgent.Controls.Add(labelNameStar);
            groupBoxAgent.Controls.Add(labelContactStar);
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
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 10F);
            txtName.Location = new Point(179, 51);
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
            txtContact.Location = new Point(179, 98);
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
            txtAgency.Location = new Point(179, 145);
            txtAgency.Name = "txtAgency";
            txtAgency.Size = new Size(300, 30);
            txtAgency.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Green;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(231, 193);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 35);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Gray;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(331, 193);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 35);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // labelNameStar
            // 
            labelNameStar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelNameStar.ForeColor = Color.Red;
            labelNameStar.Location = new Point(56, 51);
            labelNameStar.Name = "labelNameStar";
            labelNameStar.Size = new Size(20, 23);
            labelNameStar.TabIndex = 8;
            labelNameStar.Text = "*";
            labelNameStar.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelContactStar
            // 
            labelContactStar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelContactStar.ForeColor = Color.Red;
            labelContactStar.Location = new Point(56, 98);
            labelContactStar.Name = "labelContactStar";
            labelContactStar.Size = new Size(20, 23);
            labelContactStar.TabIndex = 9;
            labelContactStar.Text = "*";
            labelContactStar.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AgentRegistrationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(600, 286);
            Controls.Add(groupBoxAgent);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "AgentRegistrationForm";
            Text = "Agent Registration";
            groupBoxAgent.ResumeLayout(false);
            groupBoxAgent.PerformLayout();
            ResumeLayout(false);
        }
    }
}