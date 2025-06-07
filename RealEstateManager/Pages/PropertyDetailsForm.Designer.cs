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
        private Label labelPropertyBuyPriceTitle;
        private Label labelPropertyBuyPrice;
        private Label labelPropertyAmountPaidTitle;
        private Label labelPropertyAmountPaid;
        private Label labelPropertyBalanceTitle;
        private Label labelPropertyBalance;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            groupBoxTransactionGrid = new GroupBox();
            dataGridViewTransactions = new DataGridView();
            groupBoxPropertyDetails = new GroupBox();
            labelTitle = new Label();
            labelTitleValue = new Label();
            labelType = new Label();
            labelTypeValue = new Label();
            labelStatus = new Label();
            labelStatusValue = new Label();
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
            labelPropertyBuyPriceTitle = new Label();
            labelPropertyBuyPrice = new Label();
            labelPropertyAmountPaidTitle = new Label();
            labelPropertyAmountPaid = new Label();
            labelPropertyBalanceTitle = new Label();
            labelPropertyBalance = new Label();
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
            groupBoxTransactionGrid.Location = new Point(22, 332);
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
            dataGridViewCellStyle1.BackColor = Color.AliceBlue;
            dataGridViewTransactions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTransactions.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewTransactions.ColumnHeadersHeight = 29;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle3;
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
            groupBoxPropertyDetails.Controls.Add(labelPropertyBuyPriceTitle);
            groupBoxPropertyDetails.Controls.Add(labelPropertyBuyPrice);
            groupBoxPropertyDetails.Controls.Add(labelPropertyAmountPaidTitle);
            groupBoxPropertyDetails.Controls.Add(labelPropertyAmountPaid);
            groupBoxPropertyDetails.Controls.Add(labelPropertyBalanceTitle);
            groupBoxPropertyDetails.Controls.Add(labelPropertyBalance);
            groupBoxPropertyDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxPropertyDetails.ForeColor = Color.MidnightBlue;
            groupBoxPropertyDetails.Location = new Point(22, 22);
            groupBoxPropertyDetails.Name = "groupBoxPropertyDetails";
            groupBoxPropertyDetails.Size = new Size(1290, 304);
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
            // labelOwner
            // 
            labelOwner.AutoSize = true;
            labelOwner.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelOwner.ForeColor = Color.DarkSlateGray;
            labelOwner.Location = new Point(26, 153);
            labelOwner.Name = "labelOwner";
            labelOwner.Size = new Size(69, 23);
            labelOwner.TabIndex = 8;
            labelOwner.Text = "Owner:";
            // 
            // labelOwnerValue
            // 
            labelOwnerValue.Font = new Font("Segoe UI", 10F);
            labelOwnerValue.ForeColor = Color.Black;
            labelOwnerValue.Location = new Point(186, 153);
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
            labelPhone.Location = new Point(26, 190);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(64, 23);
            labelPhone.TabIndex = 10;
            labelPhone.Text = "Phone:";
            // 
            // labelPhoneValue
            // 
            labelPhoneValue.Font = new Font("Segoe UI", 10F);
            labelPhoneValue.ForeColor = Color.Black;
            labelPhoneValue.Location = new Point(186, 190);
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
            labelAddress.Location = new Point(26, 224);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(79, 23);
            labelAddress.TabIndex = 12;
            labelAddress.Text = "Address:";
            // 
            // labelAddressValue
            // 
            labelAddressValue.Font = new Font("Segoe UI", 10F);
            labelAddressValue.ForeColor = Color.Black;
            labelAddressValue.Location = new Point(186, 224);
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
            labelCity.Location = new Point(582, 37);
            labelCity.Name = "labelCity";
            labelCity.Size = new Size(47, 23);
            labelCity.TabIndex = 14;
            labelCity.Text = "City:";
            // 
            // labelCityValue
            // 
            labelCityValue.Font = new Font("Segoe UI", 10F);
            labelCityValue.ForeColor = Color.Black;
            labelCityValue.Location = new Point(742, 37);
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
            labelState.Location = new Point(582, 72);
            labelState.Name = "labelState";
            labelState.Size = new Size(57, 23);
            labelState.TabIndex = 16;
            labelState.Text = "State:";
            // 
            // labelStateValue
            // 
            labelStateValue.Font = new Font("Segoe UI", 10F);
            labelStateValue.ForeColor = Color.Black;
            labelStateValue.Location = new Point(742, 72);
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
            labelZip.Location = new Point(582, 107);
            labelZip.Name = "labelZip";
            labelZip.Size = new Size(41, 23);
            labelZip.TabIndex = 18;
            labelZip.Text = "Zip:";
            // 
            // labelZipValue
            // 
            labelZipValue.Font = new Font("Segoe UI", 10F);
            labelZipValue.ForeColor = Color.Black;
            labelZipValue.Location = new Point(742, 107);
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
            labelDescription.Location = new Point(582, 142);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(107, 23);
            labelDescription.TabIndex = 20;
            labelDescription.Text = "Description:";
            // 
            // labelDescriptionValue
            // 
            labelDescriptionValue.Font = new Font("Segoe UI", 10F);
            labelDescriptionValue.ForeColor = Color.Black;
            labelDescriptionValue.Location = new Point(742, 142);
            labelDescriptionValue.Name = "labelDescriptionValue";
            labelDescriptionValue.Size = new Size(233, 30);
            labelDescriptionValue.TabIndex = 21;
            labelDescriptionValue.Text = "Description: ";
            // 
            // labelPropertyBuyPriceTitle
            // 
            labelPropertyBuyPriceTitle.AutoSize = true;
            labelPropertyBuyPriceTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPropertyBuyPriceTitle.ForeColor = Color.DarkSlateGray;
            labelPropertyBuyPriceTitle.Location = new Point(582, 177);
            labelPropertyBuyPriceTitle.Name = "labelPropertyBuyPriceTitle";
            labelPropertyBuyPriceTitle.Size = new Size(89, 23);
            labelPropertyBuyPriceTitle.TabIndex = 22;
            labelPropertyBuyPriceTitle.Text = "Buy Price:";
            // 
            // labelPropertyBuyPrice
            // 
            labelPropertyBuyPrice.Font = new Font("Segoe UI", 10F);
            labelPropertyBuyPrice.ForeColor = Color.Black;
            labelPropertyBuyPrice.Location = new Point(742, 177);
            labelPropertyBuyPrice.Name = "labelPropertyBuyPrice";
            labelPropertyBuyPrice.Size = new Size(233, 30);
            labelPropertyBuyPrice.TabIndex = 23;
            labelPropertyBuyPrice.Text = "0.00";
            // 
            // labelPropertyAmountPaidTitle
            // 
            labelPropertyAmountPaidTitle.AutoSize = true;
            labelPropertyAmountPaidTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPropertyAmountPaidTitle.ForeColor = Color.DarkSlateGray;
            labelPropertyAmountPaidTitle.Location = new Point(582, 212);
            labelPropertyAmountPaidTitle.Name = "labelPropertyAmountPaidTitle";
            labelPropertyAmountPaidTitle.Size = new Size(120, 23);
            labelPropertyAmountPaidTitle.TabIndex = 24;
            labelPropertyAmountPaidTitle.Text = "Amount Paid:";
            // 
            // labelPropertyAmountPaid
            // 
            labelPropertyAmountPaid.Font = new Font("Segoe UI", 10F);
            labelPropertyAmountPaid.ForeColor = Color.Black;
            labelPropertyAmountPaid.Location = new Point(742, 212);
            labelPropertyAmountPaid.Name = "labelPropertyAmountPaid";
            labelPropertyAmountPaid.Size = new Size(233, 30);
            labelPropertyAmountPaid.TabIndex = 25;
            labelPropertyAmountPaid.Text = "0.00";
            // 
            // labelPropertyBalanceTitle
            // 
            labelPropertyBalanceTitle.AutoSize = true;
            labelPropertyBalanceTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPropertyBalanceTitle.ForeColor = Color.DarkSlateGray;
            labelPropertyBalanceTitle.Location = new Point(582, 247);
            labelPropertyBalanceTitle.Name = "labelPropertyBalanceTitle";
            labelPropertyBalanceTitle.Size = new Size(146, 23);
            labelPropertyBalanceTitle.TabIndex = 26;
            labelPropertyBalanceTitle.Text = "Amount Balance:";
            // 
            // labelPropertyBalance
            // 
            labelPropertyBalance.Font = new Font("Segoe UI", 10F);
            labelPropertyBalance.ForeColor = Color.Black;
            labelPropertyBalance.Location = new Point(742, 247);
            labelPropertyBalance.Name = "labelPropertyBalance";
            labelPropertyBalance.Size = new Size(233, 30);
            labelPropertyBalance.TabIndex = 27;
            labelPropertyBalance.Text = "0.00";
            // 
            // PropertyDetailsForm
            // 
            ClientSize = new Size(1319, 709);
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