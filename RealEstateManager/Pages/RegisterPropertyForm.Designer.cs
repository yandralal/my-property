namespace RealEstateManager.Pages
{
    partial class RegisterPropertyForm
    {
        private System.ComponentModel.IContainer components = null;
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
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.Location = new Point(30, 30);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(120, 25);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Property Title:";
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new Point(160, 30);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(250, 27);
            textBoxTitle.TabIndex = 1;
            // 
            // labelType
            // 
            labelType.Location = new Point(30, 70);
            labelType.Name = "labelType";
            labelType.Size = new Size(120, 25);
            labelType.TabIndex = 2;
            labelType.Text = "Type:";
            // 
            // comboBoxType
            // 
            comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxType.Items.AddRange(new object[] { "Residential", "Commercial", "Plot", "Other" });
            comboBoxType.Location = new Point(160, 70);
            comboBoxType.Name = "comboBoxType";
            comboBoxType.Size = new Size(250, 28);
            comboBoxType.TabIndex = 3;
            // 
            // labelStatus
            // 
            labelStatus.Location = new Point(30, 110);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(120, 25);
            labelStatus.TabIndex = 4;
            labelStatus.Text = "Status:";
            // 
            // comboBoxStatus
            // 
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxStatus.Items.AddRange(new object[] { "Available", "Sold", "Pending", "Rented" });
            comboBoxStatus.Location = new Point(160, 110);
            comboBoxStatus.Name = "comboBoxStatus";
            comboBoxStatus.Size = new Size(250, 28);
            comboBoxStatus.TabIndex = 5;
            // 
            // labelPrice
            // 
            labelPrice.Location = new Point(30, 150);
            labelPrice.Name = "labelPrice";
            labelPrice.Size = new Size(120, 25);
            labelPrice.TabIndex = 6;
            labelPrice.Text = "Price:";
            // 
            // textBoxPrice
            // 
            textBoxPrice.Location = new Point(160, 150);
            textBoxPrice.Name = "textBoxPrice";
            textBoxPrice.Size = new Size(250, 27);
            textBoxPrice.TabIndex = 7;
            // 
            // labelOwner
            // 
            labelOwner.Location = new Point(30, 190);
            labelOwner.Name = "labelOwner";
            labelOwner.Size = new Size(120, 25);
            labelOwner.TabIndex = 8;
            labelOwner.Text = "Owner Name:";
            // 
            // textBoxOwner
            // 
            textBoxOwner.Location = new Point(160, 190);
            textBoxOwner.Name = "textBoxOwner";
            textBoxOwner.Size = new Size(250, 27);
            textBoxOwner.TabIndex = 9;
            // 
            // labelPhone
            // 
            labelPhone.Location = new Point(30, 230);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(120, 25);
            labelPhone.TabIndex = 10;
            labelPhone.Text = "Phone Number:";
            // 
            // textBoxPhone
            // 
            textBoxPhone.Location = new Point(160, 230);
            textBoxPhone.Name = "textBoxPhone";
            textBoxPhone.Size = new Size(250, 27);
            textBoxPhone.TabIndex = 11;
            // 
            // labelAddress
            // 
            labelAddress.Location = new Point(30, 270);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(120, 25);
            labelAddress.TabIndex = 12;
            labelAddress.Text = "Address:";
            // 
            // textBoxAddress
            // 
            textBoxAddress.Location = new Point(160, 270);
            textBoxAddress.Name = "textBoxAddress";
            textBoxAddress.Size = new Size(250, 27);
            textBoxAddress.TabIndex = 13;
            // 
            // labelCity
            // 
            labelCity.Location = new Point(30, 310);
            labelCity.Name = "labelCity";
            labelCity.Size = new Size(120, 25);
            labelCity.TabIndex = 14;
            labelCity.Text = "City:";
            // 
            // textBoxCity
            // 
            textBoxCity.Location = new Point(160, 310);
            textBoxCity.Name = "textBoxCity";
            textBoxCity.Size = new Size(250, 27);
            textBoxCity.TabIndex = 15;
            // 
            // labelState
            // 
            labelState.Location = new Point(30, 350);
            labelState.Name = "labelState";
            labelState.Size = new Size(120, 25);
            labelState.TabIndex = 16;
            labelState.Text = "State:";
            // 
            // textBoxState
            // 
            textBoxState.Location = new Point(160, 350);
            textBoxState.Name = "textBoxState";
            textBoxState.Size = new Size(250, 27);
            textBoxState.TabIndex = 17;
            // 
            // labelZip
            // 
            labelZip.Location = new Point(30, 390);
            labelZip.Name = "labelZip";
            labelZip.Size = new Size(120, 25);
            labelZip.TabIndex = 18;
            labelZip.Text = "Zip Code:";
            // 
            // textBoxZip
            // 
            textBoxZip.Location = new Point(160, 390);
            textBoxZip.Name = "textBoxZip";
            textBoxZip.Size = new Size(250, 27);
            textBoxZip.TabIndex = 19;
            // 
            // labelDescription
            // 
            labelDescription.Location = new Point(30, 430);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(120, 25);
            labelDescription.TabIndex = 20;
            labelDescription.Text = "Description:";
            // 
            // textBoxDescription
            // 
            textBoxDescription.Location = new Point(160, 430);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Size = new Size(250, 60);
            textBoxDescription.TabIndex = 21;
            // 
            // buttonRegister
            // 
            buttonRegister.BackColor = Color.Green;
            buttonRegister.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonRegister.ForeColor = Color.White;
            buttonRegister.Location = new Point(160, 510);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(250, 40);
            buttonRegister.TabIndex = 22;
            buttonRegister.Text = "Register Property";
            buttonRegister.UseVisualStyleBackColor = false;
            buttonRegister.Click += buttonRegister_Click;
            // 
            // RegisterPropertyForm
            // 
            ClientSize = new Size(460, 570);
            Controls.Add(labelTitle);
            Controls.Add(textBoxTitle);
            Controls.Add(labelType);
            Controls.Add(comboBoxType);
            Controls.Add(labelStatus);
            Controls.Add(comboBoxStatus);
            Controls.Add(labelPrice);
            Controls.Add(textBoxPrice);
            Controls.Add(labelOwner);
            Controls.Add(textBoxOwner);
            Controls.Add(labelPhone);
            Controls.Add(textBoxPhone);
            Controls.Add(labelAddress);
            Controls.Add(textBoxAddress);
            Controls.Add(labelCity);
            Controls.Add(textBoxCity);
            Controls.Add(labelState);
            Controls.Add(textBoxState);
            Controls.Add(labelZip);
            Controls.Add(textBoxZip);
            Controls.Add(labelDescription);
            Controls.Add(textBoxDescription);
            Controls.Add(buttonRegister);
            Name = "RegisterPropertyForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Register Property";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}