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
            // labelTitle
            labelTitle.Text = "Property Title:";
            labelTitle.Location = new System.Drawing.Point(30, 30);
            labelTitle.Size = new System.Drawing.Size(120, 25);
            // textBoxTitle
            textBoxTitle.Location = new System.Drawing.Point(160, 30);
            textBoxTitle.Size = new System.Drawing.Size(250, 27);
            // labelType
            labelType.Text = "Type:";
            labelType.Location = new System.Drawing.Point(30, 70);
            labelType.Size = new System.Drawing.Size(120, 25);
            // comboBoxType
            comboBoxType.Location = new System.Drawing.Point(160, 70);
            comboBoxType.Size = new System.Drawing.Size(250, 27);
            comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxType.Items.AddRange(new object[] { "Residential", "Commercial", "Plot", "Other" });
            // labelStatus
            labelStatus.Text = "Status:";
            labelStatus.Location = new System.Drawing.Point(30, 110);
            labelStatus.Size = new System.Drawing.Size(120, 25);
            // comboBoxStatus
            comboBoxStatus.Location = new System.Drawing.Point(160, 110);
            comboBoxStatus.Size = new System.Drawing.Size(250, 27);
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxStatus.Items.AddRange(new object[] { "Available", "Sold", "Pending", "Rented" });
            // labelPrice
            labelPrice.Text = "Price:";
            labelPrice.Location = new System.Drawing.Point(30, 150);
            labelPrice.Size = new System.Drawing.Size(120, 25);
            // textBoxPrice
            textBoxPrice.Location = new System.Drawing.Point(160, 150);
            textBoxPrice.Size = new System.Drawing.Size(250, 27);
            // labelOwner
            labelOwner.Text = "Owner Name:";
            labelOwner.Location = new System.Drawing.Point(30, 190);
            labelOwner.Size = new System.Drawing.Size(120, 25);
            // textBoxOwner
            textBoxOwner.Location = new System.Drawing.Point(160, 190);
            textBoxOwner.Size = new System.Drawing.Size(250, 27);
            // labelPhone
            labelPhone.Text = "Phone Number:";
            labelPhone.Location = new System.Drawing.Point(30, 230);
            labelPhone.Size = new System.Drawing.Size(120, 25);
            // textBoxPhone
            textBoxPhone.Location = new System.Drawing.Point(160, 230);
            textBoxPhone.Size = new System.Drawing.Size(250, 27);
            // labelAddress
            labelAddress.Text = "Address:";
            labelAddress.Location = new System.Drawing.Point(30, 270);
            labelAddress.Size = new System.Drawing.Size(120, 25);
            // textBoxAddress
            textBoxAddress.Location = new System.Drawing.Point(160, 270);
            textBoxAddress.Size = new System.Drawing.Size(250, 27);
            // labelCity
            labelCity.Text = "City:";
            labelCity.Location = new System.Drawing.Point(30, 310);
            labelCity.Size = new System.Drawing.Size(120, 25);
            // textBoxCity
            textBoxCity.Location = new System.Drawing.Point(160, 310);
            textBoxCity.Size = new System.Drawing.Size(250, 27);
            // labelState
            labelState.Text = "State:";
            labelState.Location = new System.Drawing.Point(30, 350);
            labelState.Size = new System.Drawing.Size(120, 25);
            // textBoxState
            textBoxState.Location = new System.Drawing.Point(160, 350);
            textBoxState.Size = new System.Drawing.Size(250, 27);
            // labelZip
            labelZip.Text = "Zip Code:";
            labelZip.Location = new System.Drawing.Point(30, 390);
            labelZip.Size = new System.Drawing.Size(120, 25);
            // textBoxZip
            textBoxZip.Location = new System.Drawing.Point(160, 390);
            textBoxZip.Size = new System.Drawing.Size(250, 27);
            // labelDescription
            labelDescription.Text = "Description:";
            labelDescription.Location = new System.Drawing.Point(30, 430);
            labelDescription.Size = new System.Drawing.Size(120, 25);
            // textBoxDescription
            textBoxDescription.Location = new System.Drawing.Point(160, 430);
            textBoxDescription.Size = new System.Drawing.Size(250, 60);
            textBoxDescription.Multiline = true;
            // buttonRegister
            buttonRegister.Text = "Register Property";
            buttonRegister.Location = new System.Drawing.Point(160, 510);
            buttonRegister.Size = new System.Drawing.Size(180, 35);
            buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);

            // Add controls
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
            ClientSize = new System.Drawing.Size(460, 570);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}