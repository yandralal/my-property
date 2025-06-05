namespace RealEstateManager.Pages
{
    partial class PropertyDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBoxTransactionGrid;
        private DataGridView dataGridViewTransactions;
        private GroupBox groupBoxPropertyDetails;
        private Label labelTitle;
        private Label labelTitleValue;
        private Label labelType;
        private Label labelTypeValue;
        private Label labelStatus;
        private Label labelStatusValue;
        private Label labelPrice;
        private Label labelPriceValue;
        private Label labelOwner;
        private Label labelOwnerValue;
        private Label labelPhone;
        private Label labelPhoneValue;
        private Label labelAddress;
        private Label labelAddressValue;
        private Label labelCity;
        private Label labelCityValue;
        private Label labelState;
        private Label labelStateValue;
        private Label labelZip;
        private Label labelZipValue;
        private Label labelDescription;
        private Label labelDescriptionValue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            groupBoxTransactionGrid = new GroupBox();
            dataGridViewTransactions = new DataGridView();
            groupBoxPropertyDetails = new GroupBox();
            labelTitle = new Label();
            labelTitleValue = new Label();
            labelType = new Label();
            labelTypeValue = new Label();
            labelStatus = new Label();
            labelStatusValue = new Label();
            labelPrice = new Label();
            labelPriceValue = new Label();
            labelOwner = new Label();
            labelOwnerValue = new Label();
            labelPhone = new Label();
            labelPhoneValue = new Label();
            labelAddress = new Label();
            labelAddressValue = new Label();
            labelCity = new Label();
            labelCityValue = new Label();
            labelState = new Label();
            labelStateValue = new Label();
            labelZip = new Label();
            labelZipValue = new Label();
            labelDescription = new Label();
            labelDescriptionValue = new Label();
            groupBoxTransactionGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTransactions).BeginInit();
            groupBoxPropertyDetails.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxTransactionGrid
            // 
            groupBoxTransactionGrid.BackColor = Color.AliceBlue;
            groupBoxTransactionGrid.Controls.Add(dataGridViewTransactions);
            groupBoxTransactionGrid.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxTransactionGrid.ForeColor = Color.MidnightBlue;
            groupBoxTransactionGrid.Location = new Point(22, 311);
            groupBoxTransactionGrid.Name = "groupBoxTransactionGrid";
            groupBoxTransactionGrid.Padding = new Padding(15);
            groupBoxTransactionGrid.Size = new Size(1290, 363);
            groupBoxTransactionGrid.TabIndex = 3;
            groupBoxTransactionGrid.TabStop = false;
            groupBoxTransactionGrid.Text = "Transaction List";
            // 
            // dataGridViewTransactions
            // 
            dataGridViewTransactions.AllowUserToAddRows = false;
            dataGridViewTransactions.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = Color.AliceBlue;
            dataGridViewTransactions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTransactions.BackgroundColor = Color.White;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = Color.White;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewTransactions.ColumnHeadersHeight = 29;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.White;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle9.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle9.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle9.SelectionForeColor = Color.Black;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            dataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewTransactions.EnableHeadersVisualStyles = false;
            dataGridViewTransactions.GridColor = Color.LightSteelBlue;
            dataGridViewTransactions.Location = new Point(18, 40);
            dataGridViewTransactions.Name = "dataGridViewTransactions";
            dataGridViewTransactions.ReadOnly = true;
            dataGridViewTransactions.RowHeadersWidth = 51;
            dataGridViewTransactions.Size = new Size(1261, 320);
            dataGridViewTransactions.TabIndex = 1;
            // 
            // groupBoxPropertyDetails
            // 
            groupBoxPropertyDetails.BackColor = Color.AliceBlue;
            groupBoxPropertyDetails.Controls.Add(labelTitle);
            groupBoxPropertyDetails.Controls.Add(labelTitleValue);
            groupBoxPropertyDetails.Controls.Add(labelType);
            groupBoxPropertyDetails.Controls.Add(labelTypeValue);
            groupBoxPropertyDetails.Controls.Add(labelStatus);
            groupBoxPropertyDetails.Controls.Add(labelStatusValue);
            groupBoxPropertyDetails.Controls.Add(labelPrice);
            groupBoxPropertyDetails.Controls.Add(labelPriceValue);
            groupBoxPropertyDetails.Controls.Add(labelOwner);
            groupBoxPropertyDetails.Controls.Add(labelOwnerValue);
            groupBoxPropertyDetails.Controls.Add(labelPhone);
            groupBoxPropertyDetails.Controls.Add(labelPhoneValue);
            groupBoxPropertyDetails.Controls.Add(labelAddress);
            groupBoxPropertyDetails.Controls.Add(labelAddressValue);
            groupBoxPropertyDetails.Controls.Add(labelCity);
            groupBoxPropertyDetails.Controls.Add(labelCityValue);
            groupBoxPropertyDetails.Controls.Add(labelState);
            groupBoxPropertyDetails.Controls.Add(labelStateValue);
            groupBoxPropertyDetails.Controls.Add(labelZip);
            groupBoxPropertyDetails.Controls.Add(labelZipValue);
            groupBoxPropertyDetails.Controls.Add(labelDescription);
            groupBoxPropertyDetails.Controls.Add(labelDescriptionValue);
            groupBoxPropertyDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxPropertyDetails.ForeColor = Color.MidnightBlue;
            groupBoxPropertyDetails.Location = new Point(22, 22);
            groupBoxPropertyDetails.Name = "groupBoxPropertyDetails";
            groupBoxPropertyDetails.Size = new Size(1290, 267);
            groupBoxPropertyDetails.TabIndex = 4;
            groupBoxPropertyDetails.TabStop = false;
            groupBoxPropertyDetails.Text = "Property Details";
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTitle.ForeColor = Color.DarkSlateGray;
            labelTitle.Location = new Point(26, 47);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(51, 23);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Title:";
            // 
            // labelTitleValue
            // 
            labelTitleValue.Font = new Font("Segoe UI", 10F);
            labelTitleValue.ForeColor = Color.Black;
            labelTitleValue.Location = new Point(186, 47);
            labelTitleValue.Name = "labelTitleValue";
            labelTitleValue.Size = new Size(233, 30);
            labelTitleValue.TabIndex = 1;
            labelTitleValue.Text = "Title: ";
            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelType.ForeColor = Color.DarkSlateGray;
            labelType.Location = new Point(26, 82);
            labelType.Name = "labelType";
            labelType.Size = new Size(53, 23);
            labelType.TabIndex = 2;
            labelType.Text = "Type:";
            // 
            // labelTypeValue
            // 
            labelTypeValue.Font = new Font("Segoe UI", 10F);
            labelTypeValue.ForeColor = Color.Black;
            labelTypeValue.Location = new Point(186, 82);
            labelTypeValue.Name = "labelTypeValue";
            labelTypeValue.Size = new Size(233, 30);
            labelTypeValue.TabIndex = 3;
            labelTypeValue.Text = "Type: ";
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelStatus.ForeColor = Color.DarkSlateGray;
            labelStatus.Location = new Point(26, 117);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(65, 23);
            labelStatus.TabIndex = 4;
            labelStatus.Text = "Status:";
            // 
            // labelStatusValue
            // 
            labelStatusValue.Font = new Font("Segoe UI", 10F);
            labelStatusValue.ForeColor = Color.Black;
            labelStatusValue.Location = new Point(186, 117);
            labelStatusValue.Name = "labelStatusValue";
            labelStatusValue.Size = new Size(233, 30);
            labelStatusValue.TabIndex = 5;
            labelStatusValue.Text = "Status: ";
            // 
            // labelPrice
            // 
            labelPrice.AutoSize = true;
            labelPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPrice.ForeColor = Color.DarkSlateGray;
            labelPrice.Location = new Point(26, 152);
            labelPrice.Name = "labelPrice";
            labelPrice.Size = new Size(54, 23);
            labelPrice.TabIndex = 6;
            labelPrice.Text = "Price:";
            // 
            // labelPriceValue
            // 
            labelPriceValue.Font = new Font("Segoe UI", 10F);
            labelPriceValue.ForeColor = Color.Black;
            labelPriceValue.Location = new Point(186, 152);
            labelPriceValue.Name = "labelPriceValue";
            labelPriceValue.Size = new Size(233, 30);
            labelPriceValue.TabIndex = 7;
            labelPriceValue.Text = "Price: ";
            // 
            // labelOwner
            // 
            labelOwner.AutoSize = true;
            labelOwner.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelOwner.ForeColor = Color.DarkSlateGray;
            labelOwner.Location = new Point(26, 187);
            labelOwner.Name = "labelOwner";
            labelOwner.Size = new Size(69, 23);
            labelOwner.TabIndex = 8;
            labelOwner.Text = "Owner:";
            // 
            // labelOwnerValue
            // 
            labelOwnerValue.Font = new Font("Segoe UI", 10F);
            labelOwnerValue.ForeColor = Color.Black;
            labelOwnerValue.Location = new Point(186, 187);
            labelOwnerValue.Name = "labelOwnerValue";
            labelOwnerValue.Size = new Size(233, 30);
            labelOwnerValue.TabIndex = 9;
            labelOwnerValue.Text = "Owner: ";
            // 
            // labelPhone
            // 
            labelPhone.AutoSize = true;
            labelPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPhone.ForeColor = Color.DarkSlateGray;
            labelPhone.Location = new Point(26, 224);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(64, 23);
            labelPhone.TabIndex = 10;
            labelPhone.Text = "Phone:";
            // 
            // labelPhoneValue
            // 
            labelPhoneValue.Font = new Font("Segoe UI", 10F);
            labelPhoneValue.ForeColor = Color.Black;
            labelPhoneValue.Location = new Point(186, 224);
            labelPhoneValue.Name = "labelPhoneValue";
            labelPhoneValue.Size = new Size(233, 30);
            labelPhoneValue.TabIndex = 11;
            labelPhoneValue.Text = "Phone: ";
            // 
            // labelAddress
            // 
            labelAddress.AutoSize = true;
            labelAddress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAddress.ForeColor = Color.DarkSlateGray;
            labelAddress.Location = new Point(582, 36);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(79, 23);
            labelAddress.TabIndex = 12;
            labelAddress.Text = "Address:";
            // 
            // labelAddressValue
            // 
            labelAddressValue.Font = new Font("Segoe UI", 10F);
            labelAddressValue.ForeColor = Color.Black;
            labelAddressValue.Location = new Point(742, 36);
            labelAddressValue.Name = "labelAddressValue";
            labelAddressValue.Size = new Size(233, 30);
            labelAddressValue.TabIndex = 13;
            labelAddressValue.Text = "Address: ";
            // 
            // labelCity
            // 
            labelCity.AutoSize = true;
            labelCity.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCity.ForeColor = Color.DarkSlateGray;
            labelCity.Location = new Point(582, 71);
            labelCity.Name = "labelCity";
            labelCity.Size = new Size(47, 23);
            labelCity.TabIndex = 14;
            labelCity.Text = "City:";
            // 
            // labelCityValue
            // 
            labelCityValue.Font = new Font("Segoe UI", 10F);
            labelCityValue.ForeColor = Color.Black;
            labelCityValue.Location = new Point(742, 71);
            labelCityValue.Name = "labelCityValue";
            labelCityValue.Size = new Size(233, 30);
            labelCityValue.TabIndex = 15;
            labelCityValue.Text = "City: ";
            // 
            // labelState
            // 
            labelState.AutoSize = true;
            labelState.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelState.ForeColor = Color.DarkSlateGray;
            labelState.Location = new Point(582, 106);
            labelState.Name = "labelState";
            labelState.Size = new Size(57, 23);
            labelState.TabIndex = 16;
            labelState.Text = "State:";
            // 
            // labelStateValue
            // 
            labelStateValue.Font = new Font("Segoe UI", 10F);
            labelStateValue.ForeColor = Color.Black;
            labelStateValue.Location = new Point(742, 106);
            labelStateValue.Name = "labelStateValue";
            labelStateValue.Size = new Size(233, 30);
            labelStateValue.TabIndex = 17;
            labelStateValue.Text = "State: ";
            // 
            // labelZip
            // 
            labelZip.AutoSize = true;
            labelZip.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelZip.ForeColor = Color.DarkSlateGray;
            labelZip.Location = new Point(582, 141);
            labelZip.Name = "labelZip";
            labelZip.Size = new Size(41, 23);
            labelZip.TabIndex = 18;
            labelZip.Text = "Zip:";
            // 
            // labelZipValue
            // 
            labelZipValue.Font = new Font("Segoe UI", 10F);
            labelZipValue.ForeColor = Color.Black;
            labelZipValue.Location = new Point(742, 141);
            labelZipValue.Name = "labelZipValue";
            labelZipValue.Size = new Size(233, 30);
            labelZipValue.TabIndex = 19;
            labelZipValue.Text = "Zip: ";
            // 
            // labelDescription
            // 
            labelDescription.AutoSize = true;
            labelDescription.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelDescription.ForeColor = Color.DarkSlateGray;
            labelDescription.Location = new Point(582, 176);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(107, 23);
            labelDescription.TabIndex = 20;
            labelDescription.Text = "Description:";
            // 
            // labelDescriptionValue
            // 
            labelDescriptionValue.Font = new Font("Segoe UI", 10F);
            labelDescriptionValue.ForeColor = Color.Black;
            labelDescriptionValue.Location = new Point(742, 176);
            labelDescriptionValue.Name = "labelDescriptionValue";
            labelDescriptionValue.Size = new Size(233, 30);
            labelDescriptionValue.TabIndex = 21;
            labelDescriptionValue.Text = "Description: ";
            // 
            // PropertyDetailsForm
            // 
            ClientSize = new Size(1319, 689);
            Controls.Add(groupBoxTransactionGrid);
            Controls.Add(groupBoxPropertyDetails);
            Name = "PropertyDetailsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Property Details";
            groupBoxTransactionGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewTransactions).EndInit();
            groupBoxPropertyDetails.ResumeLayout(false);
            groupBoxPropertyDetails.PerformLayout();
            ResumeLayout(false);
        }
    }
}