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
        private Label labelTotalPlotsTitle;
        private Label labelTotalPlotsValue;
        private Label labelTotalSaleAmountTitle;
        private Label labelTotalSaleAmountValue;
        private Label labelTotalPaidTitle;
        private Label labelTotalPaidValue;
        private Label labelTotalBalanceTitle;
        private Label labelTotalBalanceValue;
        private Label labelTotalProfitLossTitle;
        private Label labelTotalProfitLossValue;
        private Button buttonGenerateReport;
        private Label labelKhasraNoTitle;
        private Label labelKhasraNoValue;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyDetailsForm));
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
            labelKhasraNoTitle = new Label();
            labelKhasraNoValue = new Label();
            labelTotalBalanceTitle = new Label();
            labelTotalBalanceValue = new Label();
            labelTotalPlotsTitle = new Label();
            labelTotalPlotsValue = new Label();
            labelTotalSaleAmountTitle = new Label();
            labelTotalSaleAmountValue = new Label();
            labelTotalPaidTitle = new Label();
            labelTotalPaidValue = new Label();
            labelTotalProfitLossTitle = new Label();
            labelTotalProfitLossValue = new Label();
            groupBoxSummary = new GroupBox();
            buttonGenerateReport = new Button();
            label33 = new Label();
            label34 = new Label();
            groupBoxTransactionGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTransactions).BeginInit();
            groupBoxPropertyDetails.SuspendLayout();
            groupBoxSummary.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxTransactionGrid
            // 
            groupBoxTransactionGrid.BackColor = Color.AliceBlue;
            groupBoxTransactionGrid.Controls.Add(dataGridViewTransactions);
            groupBoxTransactionGrid.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxTransactionGrid.ForeColor = Color.MidnightBlue;
            groupBoxTransactionGrid.Location = new Point(22, 431);
            groupBoxTransactionGrid.Name = "groupBoxTransactionGrid";
            groupBoxTransactionGrid.Padding = new Padding(15);
            groupBoxTransactionGrid.Size = new Size(1290, 341);
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
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewTransactions.ColumnHeadersHeight = 29;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewTransactions.EnableHeadersVisualStyles = false;
            dataGridViewTransactions.GridColor = Color.LightSteelBlue;
            dataGridViewTransactions.Location = new Point(18, 46);
            dataGridViewTransactions.Name = "dataGridViewTransactions";
            dataGridViewTransactions.ReadOnly = true;
            dataGridViewTransactions.RowHeadersWidth = 51;
            dataGridViewTransactions.Size = new Size(1261, 280);
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
            groupBoxPropertyDetails.Controls.Add(labelKhasraNoTitle);
            groupBoxPropertyDetails.Controls.Add(labelKhasraNoValue);
            groupBoxPropertyDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxPropertyDetails.ForeColor = Color.MidnightBlue;
            groupBoxPropertyDetails.Location = new Point(22, 25);
            groupBoxPropertyDetails.Name = "groupBoxPropertyDetails";
            groupBoxPropertyDetails.Size = new Size(1290, 230);
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
            labelPhone.Location = new Point(26, 187);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(64, 23);
            labelPhone.TabIndex = 10;
            labelPhone.Text = "Phone:";
            // 
            // labelPhoneValue
            // 
            labelPhoneValue.Font = new Font("Segoe UI", 10F);
            labelPhoneValue.ForeColor = Color.Black;
            labelPhoneValue.Location = new Point(186, 187);
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
            labelAddress.Location = new Point(420, 47);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(79, 23);
            labelAddress.TabIndex = 12;
            labelAddress.Text = "Address:";
            // 
            // labelAddressValue
            // 
            labelAddressValue.Font = new Font("Segoe UI", 10F);
            labelAddressValue.ForeColor = Color.Black;
            labelAddressValue.Location = new Point(580, 47);
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
            labelCity.Location = new Point(420, 83);
            labelCity.Name = "labelCity";
            labelCity.Size = new Size(47, 23);
            labelCity.TabIndex = 14;
            labelCity.Text = "City:";
            // 
            // labelCityValue
            // 
            labelCityValue.Font = new Font("Segoe UI", 10F);
            labelCityValue.ForeColor = Color.Black;
            labelCityValue.Location = new Point(580, 83);
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
            labelState.Location = new Point(420, 118);
            labelState.Name = "labelState";
            labelState.Size = new Size(57, 23);
            labelState.TabIndex = 16;
            labelState.Text = "State:";
            // 
            // labelStateValue
            // 
            labelStateValue.Font = new Font("Segoe UI", 10F);
            labelStateValue.ForeColor = Color.Black;
            labelStateValue.Location = new Point(580, 118);
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
            labelZip.Location = new Point(420, 153);
            labelZip.Name = "labelZip";
            labelZip.Size = new Size(41, 23);
            labelZip.TabIndex = 18;
            labelZip.Text = "Zip:";
            // 
            // labelZipValue
            // 
            labelZipValue.Font = new Font("Segoe UI", 10F);
            labelZipValue.ForeColor = Color.Black;
            labelZipValue.Location = new Point(580, 153);
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
            labelDescription.Location = new Point(420, 187);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(107, 23);
            labelDescription.TabIndex = 20;
            labelDescription.Text = "Description:";
            // 
            // labelDescriptionValue
            // 
            labelDescriptionValue.Font = new Font("Segoe UI", 10F);
            labelDescriptionValue.ForeColor = Color.Black;
            labelDescriptionValue.Location = new Point(580, 187);
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
            labelPropertyBuyPriceTitle.Location = new Point(843, 75);
            labelPropertyBuyPriceTitle.Name = "labelPropertyBuyPriceTitle";
            labelPropertyBuyPriceTitle.Size = new Size(89, 23);
            labelPropertyBuyPriceTitle.TabIndex = 22;
            labelPropertyBuyPriceTitle.Text = "Buy Price:";
            // 
            // labelPropertyBuyPrice
            // 
            labelPropertyBuyPrice.Font = new Font("Segoe UI", 10F);
            labelPropertyBuyPrice.ForeColor = Color.Black;
            labelPropertyBuyPrice.Location = new Point(1003, 75);
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
            labelPropertyAmountPaidTitle.Location = new Point(843, 109);
            labelPropertyAmountPaidTitle.Name = "labelPropertyAmountPaidTitle";
            labelPropertyAmountPaidTitle.Size = new Size(120, 23);
            labelPropertyAmountPaidTitle.TabIndex = 24;
            labelPropertyAmountPaidTitle.Text = "Amount Paid:";
            // 
            // labelPropertyAmountPaid
            // 
            labelPropertyAmountPaid.Font = new Font("Segoe UI", 10F);
            labelPropertyAmountPaid.ForeColor = Color.Black;
            labelPropertyAmountPaid.Location = new Point(1003, 109);
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
            labelPropertyBalanceTitle.Location = new Point(843, 144);
            labelPropertyBalanceTitle.Name = "labelPropertyBalanceTitle";
            labelPropertyBalanceTitle.Size = new Size(146, 23);
            labelPropertyBalanceTitle.TabIndex = 26;
            labelPropertyBalanceTitle.Text = "Amount Balance:";
            // 
            // labelPropertyBalance
            // 
            labelPropertyBalance.Font = new Font("Segoe UI", 10F);
            labelPropertyBalance.ForeColor = Color.Black;
            labelPropertyBalance.Location = new Point(1003, 144);
            labelPropertyBalance.Name = "labelPropertyBalance";
            labelPropertyBalance.Size = new Size(233, 30);
            labelPropertyBalance.TabIndex = 27;
            labelPropertyBalance.Text = "0.00";
            // 
            // labelKhasraNoTitle
            // 
            labelKhasraNoTitle.AutoSize = true;
            labelKhasraNoTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelKhasraNoTitle.ForeColor = Color.DarkSlateGray;
            labelKhasraNoTitle.Location = new Point(843, 40);
            labelKhasraNoTitle.Name = "labelKhasraNoTitle";
            labelKhasraNoTitle.Size = new Size(96, 23);
            labelKhasraNoTitle.TabIndex = 38;
            labelKhasraNoTitle.Text = "Khasra No:";
            // 
            // labelKhasraNoValue
            // 
            labelKhasraNoValue.Font = new Font("Segoe UI", 10F);
            labelKhasraNoValue.ForeColor = Color.Black;
            labelKhasraNoValue.Location = new Point(1003, 40);
            labelKhasraNoValue.Name = "labelKhasraNoValue";
            labelKhasraNoValue.Size = new Size(233, 30);
            labelKhasraNoValue.TabIndex = 39;
            // 
            // labelTotalBalanceTitle
            // 
            labelTotalBalanceTitle.AutoSize = true;
            labelTotalBalanceTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalBalanceTitle.ForeColor = Color.DarkSlateGray;
            labelTotalBalanceTitle.Location = new Point(420, 41);
            labelTotalBalanceTitle.Name = "labelTotalBalanceTitle";
            labelTotalBalanceTitle.Size = new Size(120, 23);
            labelTotalBalanceTitle.TabIndex = 34;
            labelTotalBalanceTitle.Text = "Total Balance:";
            // 
            // labelTotalBalanceValue
            // 
            labelTotalBalanceValue.Font = new Font("Segoe UI", 10F);
            labelTotalBalanceValue.ForeColor = Color.Black;
            labelTotalBalanceValue.Location = new Point(580, 41);
            labelTotalBalanceValue.Name = "labelTotalBalanceValue";
            labelTotalBalanceValue.Size = new Size(233, 30);
            labelTotalBalanceValue.TabIndex = 35;
            labelTotalBalanceValue.Text = "0.00";
            // 
            // labelTotalPlotsTitle
            // 
            labelTotalPlotsTitle.AutoSize = true;
            labelTotalPlotsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalPlotsTitle.ForeColor = Color.DarkSlateGray;
            labelTotalPlotsTitle.Location = new Point(26, 41);
            labelTotalPlotsTitle.Name = "labelTotalPlotsTitle";
            labelTotalPlotsTitle.Size = new Size(98, 23);
            labelTotalPlotsTitle.TabIndex = 28;
            labelTotalPlotsTitle.Text = "Total Plots:";
            // 
            // labelTotalPlotsValue
            // 
            labelTotalPlotsValue.Font = new Font("Segoe UI", 10F);
            labelTotalPlotsValue.ForeColor = Color.Black;
            labelTotalPlotsValue.Location = new Point(186, 41);
            labelTotalPlotsValue.Name = "labelTotalPlotsValue";
            labelTotalPlotsValue.Size = new Size(233, 30);
            labelTotalPlotsValue.TabIndex = 29;
            labelTotalPlotsValue.Text = "0";
            // 
            // labelTotalSaleAmountTitle
            // 
            labelTotalSaleAmountTitle.AutoSize = true;
            labelTotalSaleAmountTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalSaleAmountTitle.ForeColor = Color.DarkSlateGray;
            labelTotalSaleAmountTitle.Location = new Point(26, 76);
            labelTotalSaleAmountTitle.Name = "labelTotalSaleAmountTitle";
            labelTotalSaleAmountTitle.Size = new Size(162, 23);
            labelTotalSaleAmountTitle.TabIndex = 30;
            labelTotalSaleAmountTitle.Text = "Total Sale Amount:";
            // 
            // labelTotalSaleAmountValue
            // 
            labelTotalSaleAmountValue.Font = new Font("Segoe UI", 10F);
            labelTotalSaleAmountValue.ForeColor = Color.Black;
            labelTotalSaleAmountValue.Location = new Point(186, 76);
            labelTotalSaleAmountValue.Name = "labelTotalSaleAmountValue";
            labelTotalSaleAmountValue.Size = new Size(233, 30);
            labelTotalSaleAmountValue.TabIndex = 31;
            labelTotalSaleAmountValue.Text = "0.00";
            // 
            // labelTotalPaidTitle
            // 
            labelTotalPaidTitle.AutoSize = true;
            labelTotalPaidTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalPaidTitle.ForeColor = Color.DarkSlateGray;
            labelTotalPaidTitle.Location = new Point(26, 111);
            labelTotalPaidTitle.Name = "labelTotalPaidTitle";
            labelTotalPaidTitle.Size = new Size(94, 23);
            labelTotalPaidTitle.TabIndex = 32;
            labelTotalPaidTitle.Text = "Total Paid:";
            // 
            // labelTotalPaidValue
            // 
            labelTotalPaidValue.Font = new Font("Segoe UI", 10F);
            labelTotalPaidValue.ForeColor = Color.Black;
            labelTotalPaidValue.Location = new Point(186, 111);
            labelTotalPaidValue.Name = "labelTotalPaidValue";
            labelTotalPaidValue.Size = new Size(233, 30);
            labelTotalPaidValue.TabIndex = 33;
            labelTotalPaidValue.Text = "0.00";
            // 
            // labelTotalProfitLossTitle
            // 
            labelTotalProfitLossTitle.AutoSize = true;
            labelTotalProfitLossTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalProfitLossTitle.ForeColor = Color.DarkSlateGray;
            labelTotalProfitLossTitle.Location = new Point(420, 76);
            labelTotalProfitLossTitle.Name = "labelTotalProfitLossTitle";
            labelTotalProfitLossTitle.Size = new Size(146, 23);
            labelTotalProfitLossTitle.TabIndex = 36;
            labelTotalProfitLossTitle.Text = "Total Profit/Loss:";
            // 
            // labelTotalProfitLossValue
            // 
            labelTotalProfitLossValue.Font = new Font("Segoe UI", 10F);
            labelTotalProfitLossValue.ForeColor = Color.Black;
            labelTotalProfitLossValue.Location = new Point(580, 76);
            labelTotalProfitLossValue.Name = "labelTotalProfitLossValue";
            labelTotalProfitLossValue.Size = new Size(233, 30);
            labelTotalProfitLossValue.TabIndex = 37;
            labelTotalProfitLossValue.Text = "0.00";
            // 
            // groupBoxSummary
            // 
            groupBoxSummary.BackColor = Color.AliceBlue;
            groupBoxSummary.Controls.Add(labelTotalBalanceTitle);
            groupBoxSummary.Controls.Add(labelTotalBalanceValue);
            groupBoxSummary.Controls.Add(labelTotalProfitLossTitle);
            groupBoxSummary.Controls.Add(buttonGenerateReport);
            groupBoxSummary.Controls.Add(labelTotalProfitLossValue);
            groupBoxSummary.Controls.Add(labelTotalSaleAmountTitle);
            groupBoxSummary.Controls.Add(labelTotalPaidValue);
            groupBoxSummary.Controls.Add(labelTotalPaidTitle);
            groupBoxSummary.Controls.Add(labelTotalSaleAmountValue);
            groupBoxSummary.Controls.Add(labelTotalPlotsValue);
            groupBoxSummary.Controls.Add(labelTotalPlotsTitle);
            groupBoxSummary.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxSummary.ForeColor = Color.MidnightBlue;
            groupBoxSummary.Location = new Point(22, 264);
            groupBoxSummary.Name = "groupBoxSummary";
            groupBoxSummary.Size = new Size(1290, 149);
            groupBoxSummary.TabIndex = 36;
            groupBoxSummary.TabStop = false;
            groupBoxSummary.Text = "Summary";
            // 
            // buttonGenerateReport
            // 
            buttonGenerateReport.BackColor = Color.Green;
            buttonGenerateReport.FlatStyle = FlatStyle.Flat;
            buttonGenerateReport.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonGenerateReport.ForeColor = Color.White;
            buttonGenerateReport.Location = new Point(1110, 94);
            buttonGenerateReport.Name = "buttonGenerateReport";
            buttonGenerateReport.Size = new Size(159, 36);
            buttonGenerateReport.TabIndex = 40;
            buttonGenerateReport.Text = "Generate Report";
            buttonGenerateReport.UseVisualStyleBackColor = false;
            buttonGenerateReport.Click += buttonGenerateReport_Click;
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label33.ForeColor = Color.DarkSlateGray;
            label33.Location = new Point(843, 257);
            label33.Name = "label33";
            label33.Size = new Size(120, 23);
            label33.TabIndex = 34;
            label33.Text = "Total Balance:";
            // 
            // label34
            // 
            label34.Font = new Font("Segoe UI", 10F);
            label34.ForeColor = Color.Black;
            label34.Location = new Point(1003, 257);
            label34.Name = "label34";
            label34.Size = new Size(233, 30);
            label34.TabIndex = 35;
            label34.Text = "0.00";
            // 
            // PropertyDetailsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1319, 779);
            Controls.Add(groupBoxSummary);
            Controls.Add(groupBoxTransactionGrid);
            Controls.Add(groupBoxPropertyDetails);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PropertyDetailsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Property Details";
            groupBoxTransactionGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewTransactions).EndInit();
            groupBoxPropertyDetails.ResumeLayout(false);
            groupBoxPropertyDetails.PerformLayout();
            groupBoxSummary.ResumeLayout(false);
            groupBoxSummary.PerformLayout();
            ResumeLayout(false);
        }
        private GroupBox groupBoxSummary;
        private Label label33;
        private Label label34;
    }
}