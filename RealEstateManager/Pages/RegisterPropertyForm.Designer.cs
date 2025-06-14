namespace RealEstateManager.Pages
{
    partial class RegisterPropertyForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxPropertyDetails;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelStatus; 
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label labelOwner;
        private System.Windows.Forms.TextBox textBoxOwner;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label labelCity;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.TextBox textBoxState;
        private System.Windows.Forms.Label labelZip;
        private System.Windows.Forms.TextBox textBoxZip;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button buttonRegister;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterPropertyForm));
            groupBoxPropertyDetails = new GroupBox();
            labelTitle = new Label();
            textBoxTitle = new TextBox();
            labelType = new Label();
            comboBoxType = new ComboBox();
            labelStatus = new Label();
            comboBoxStatus = new ComboBox();
            labelPrice = new Label();
            textBoxPrice = new TextBox();
            labelOwner = new Label();
            textBoxOwner = new TextBox();
            labelPhone = new Label();
            textBoxPhone = new TextBox();
            labelAddress = new Label();
            textBoxAddress = new TextBox();
            labelCity = new Label();
            textBoxCity = new TextBox();
            labelState = new Label();
            textBoxState = new TextBox();
            labelZip = new Label();
            textBoxZip = new TextBox();
            labelDescription = new Label();
            textBoxDescription = new TextBox();
            buttonRegister = new Button();
            groupBoxPropertyDetails.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxPropertyDetails
            // 
            groupBoxPropertyDetails.BackColor = Color.AliceBlue;
            groupBoxPropertyDetails.Controls.Add(labelTitle);
            groupBoxPropertyDetails.Controls.Add(textBoxTitle);
            groupBoxPropertyDetails.Controls.Add(labelType);
            groupBoxPropertyDetails.Controls.Add(comboBoxType);
            groupBoxPropertyDetails.Controls.Add(labelStatus);
            groupBoxPropertyDetails.Controls.Add(comboBoxStatus);
            groupBoxPropertyDetails.Controls.Add(labelPrice);
            groupBoxPropertyDetails.Controls.Add(textBoxPrice);
            groupBoxPropertyDetails.Controls.Add(labelOwner);
            groupBoxPropertyDetails.Controls.Add(textBoxOwner);
            groupBoxPropertyDetails.Controls.Add(labelPhone);
            groupBoxPropertyDetails.Controls.Add(textBoxPhone);
            groupBoxPropertyDetails.Controls.Add(labelAddress);
            groupBoxPropertyDetails.Controls.Add(textBoxAddress);
            groupBoxPropertyDetails.Controls.Add(labelCity);
            groupBoxPropertyDetails.Controls.Add(textBoxCity);
            groupBoxPropertyDetails.Controls.Add(labelState);
            groupBoxPropertyDetails.Controls.Add(textBoxState);
            groupBoxPropertyDetails.Controls.Add(labelZip);
            groupBoxPropertyDetails.Controls.Add(textBoxZip);
            groupBoxPropertyDetails.Controls.Add(labelDescription);
            groupBoxPropertyDetails.Controls.Add(textBoxDescription);
            groupBoxPropertyDetails.Controls.Add(buttonRegister);
            groupBoxPropertyDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxPropertyDetails.ForeColor = Color.MidnightBlue;
            groupBoxPropertyDetails.Location = new Point(20, 20);
            groupBoxPropertyDetails.Name = "groupBoxPropertyDetails";
            groupBoxPropertyDetails.Size = new Size(560, 630);
            groupBoxPropertyDetails.TabIndex = 0;
            groupBoxPropertyDetails.TabStop = false;
            groupBoxPropertyDetails.Text = "Property Details";
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTitle.ForeColor = Color.DarkSlateGray;
            labelTitle.Location = new Point(30, 54);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(140, 30);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Property Title:";
            // 
            // textBoxTitle
            // 
            textBoxTitle.BorderStyle = BorderStyle.FixedSingle;
            textBoxTitle.Font = new Font("Segoe UI", 10F);
            textBoxTitle.Location = new Point(190, 51);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(320, 30);
            textBoxTitle.TabIndex = 1;
            // 
            // labelType
            // 
            labelType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelType.ForeColor = Color.DarkSlateGray;
            labelType.Location = new Point(30, 99);
            labelType.Name = "labelType";
            labelType.Size = new Size(140, 30);
            labelType.TabIndex = 2;
            labelType.Text = "Type:";
            // 
            // comboBoxType
            // 
            comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxType.Font = new Font("Segoe UI", 10F);
            comboBoxType.Items.AddRange(new object[] { "Residential", "Commercial", "Plot", "Other" });
            comboBoxType.Location = new Point(190, 96);
            comboBoxType.Name = "comboBoxType";
            comboBoxType.Size = new Size(320, 31);
            comboBoxType.TabIndex = 3;
            // 
            // labelStatus
            // 
            labelStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelStatus.ForeColor = Color.DarkSlateGray;
            labelStatus.Location = new Point(30, 144);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(140, 30);
            labelStatus.TabIndex = 4;
            labelStatus.Text = "Status:";
            // 
            // comboBoxStatus
            // 
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxStatus.Font = new Font("Segoe UI", 10F);
            comboBoxStatus.Items.AddRange(new object[] { "Available", "Sold", "Pending", "Rented" });
            comboBoxStatus.Location = new Point(190, 141);
            comboBoxStatus.Name = "comboBoxStatus";
            comboBoxStatus.Size = new Size(320, 31);
            comboBoxStatus.TabIndex = 5;
            // 
            // labelPrice
            // 
            labelPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPrice.ForeColor = Color.DarkSlateGray;
            labelPrice.Location = new Point(30, 189);
            labelPrice.Name = "labelPrice";
            labelPrice.Size = new Size(140, 30);
            labelPrice.TabIndex = 6;
            labelPrice.Text = "Price:";
            // 
            // textBoxPrice
            // 
            textBoxPrice.BorderStyle = BorderStyle.FixedSingle;
            textBoxPrice.Font = new Font("Segoe UI", 10F);
            textBoxPrice.Location = new Point(190, 186);
            textBoxPrice.Name = "textBoxPrice";
            textBoxPrice.Size = new Size(320, 30);
            textBoxPrice.TabIndex = 7;
            // 
            // labelOwner
            // 
            labelOwner.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelOwner.ForeColor = Color.DarkSlateGray;
            labelOwner.Location = new Point(30, 234);
            labelOwner.Name = "labelOwner";
            labelOwner.Size = new Size(140, 30);
            labelOwner.TabIndex = 8;
            labelOwner.Text = "Owner Name:";
            // 
            // textBoxOwner
            // 
            textBoxOwner.BorderStyle = BorderStyle.FixedSingle;
            textBoxOwner.Font = new Font("Segoe UI", 10F);
            textBoxOwner.Location = new Point(190, 231);
            textBoxOwner.Name = "textBoxOwner";
            textBoxOwner.Size = new Size(320, 30);
            textBoxOwner.TabIndex = 9;
            // 
            // labelPhone
            // 
            labelPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPhone.ForeColor = Color.DarkSlateGray;
            labelPhone.Location = new Point(30, 279);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(140, 30);
            labelPhone.TabIndex = 10;
            labelPhone.Text = "Phone Number:";
            // 
            // textBoxPhone
            // 
            textBoxPhone.BorderStyle = BorderStyle.FixedSingle;
            textBoxPhone.Font = new Font("Segoe UI", 10F);
            textBoxPhone.Location = new Point(190, 276);
            textBoxPhone.Name = "textBoxPhone";
            textBoxPhone.Size = new Size(320, 30);
            textBoxPhone.TabIndex = 11;
            // 
            // labelAddress
            // 
            labelAddress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAddress.ForeColor = Color.DarkSlateGray;
            labelAddress.Location = new Point(30, 324);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(140, 30);
            labelAddress.TabIndex = 12;
            labelAddress.Text = "Address:";
            // 
            // textBoxAddress
            // 
            textBoxAddress.BorderStyle = BorderStyle.FixedSingle;
            textBoxAddress.Font = new Font("Segoe UI", 10F);
            textBoxAddress.Location = new Point(190, 321);
            textBoxAddress.Name = "textBoxAddress";
            textBoxAddress.Size = new Size(320, 30);
            textBoxAddress.TabIndex = 13;
            // 
            // labelCity
            // 
            labelCity.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCity.ForeColor = Color.DarkSlateGray;
            labelCity.Location = new Point(30, 369);
            labelCity.Name = "labelCity";
            labelCity.Size = new Size(140, 30);
            labelCity.TabIndex = 14;
            labelCity.Text = "City:";
            // 
            // textBoxCity
            // 
            textBoxCity.BorderStyle = BorderStyle.FixedSingle;
            textBoxCity.Font = new Font("Segoe UI", 10F);
            textBoxCity.Location = new Point(190, 366);
            textBoxCity.Name = "textBoxCity";
            textBoxCity.Size = new Size(320, 30);
            textBoxCity.TabIndex = 15;
            // 
            // labelState
            // 
            labelState.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelState.ForeColor = Color.DarkSlateGray;
            labelState.Location = new Point(30, 414);
            labelState.Name = "labelState";
            labelState.Size = new Size(140, 30);
            labelState.TabIndex = 16;
            labelState.Text = "State:";
            // 
            // textBoxState
            // 
            textBoxState.BorderStyle = BorderStyle.FixedSingle;
            textBoxState.Font = new Font("Segoe UI", 10F);
            textBoxState.Location = new Point(190, 411);
            textBoxState.Name = "textBoxState";
            textBoxState.Size = new Size(320, 30);
            textBoxState.TabIndex = 17;
            // 
            // labelZip
            // 
            labelZip.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelZip.ForeColor = Color.DarkSlateGray;
            labelZip.Location = new Point(30, 459);
            labelZip.Name = "labelZip";
            labelZip.Size = new Size(140, 30);
            labelZip.TabIndex = 18;
            labelZip.Text = "Zip Code:";
            // 
            // textBoxZip
            // 
            textBoxZip.BorderStyle = BorderStyle.FixedSingle;
            textBoxZip.Font = new Font("Segoe UI", 10F);
            textBoxZip.Location = new Point(190, 456);
            textBoxZip.Name = "textBoxZip";
            textBoxZip.Size = new Size(320, 30);
            textBoxZip.TabIndex = 19;
            // 
            // labelDescription
            // 
            labelDescription.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelDescription.ForeColor = Color.DarkSlateGray;
            labelDescription.Location = new Point(30, 504);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(140, 30);
            labelDescription.TabIndex = 20;
            labelDescription.Text = "Description:";
            // 
            // textBoxDescription
            // 
            textBoxDescription.BorderStyle = BorderStyle.FixedSingle;
            textBoxDescription.Font = new Font("Segoe UI", 10F);
            textBoxDescription.Location = new Point(190, 501);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Size = new Size(320, 60);
            textBoxDescription.TabIndex = 21;
            // 
            // buttonRegister
            // 
            buttonRegister.BackColor = Color.Green;
            buttonRegister.FlatStyle = FlatStyle.Flat;
            buttonRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonRegister.ForeColor = Color.White;
            buttonRegister.Location = new Point(190, 579);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(320, 35);
            buttonRegister.TabIndex = 22;
            buttonRegister.Text = "Register Property";
            buttonRegister.UseVisualStyleBackColor = false;
            buttonRegister.Click += buttonRegister_Click;
            // 
            // RegisterPropertyForm
            // 
            ClientSize = new Size(600, 670);
            Controls.Add(groupBoxPropertyDetails);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "RegisterPropertyForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Register Property";
            groupBoxPropertyDetails.ResumeLayout(false);
            groupBoxPropertyDetails.PerformLayout();
            ResumeLayout(false);
        }
    }
}