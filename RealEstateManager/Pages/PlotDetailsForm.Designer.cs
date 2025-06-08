namespace RealEstateManager.Pages
{
    partial class PlotDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelPlotId;
        private Label labelPlotNumber;
        private Label labelStatus;
        private Label labelArea;
        private Label labelCreatedBy;
        private Label labelCreatedDate;
        private Label labelModifiedBy;
        private Label labelModifiedDate;
        private Label labelIsDeleted;
        private Label labelSaleDate;
        private Label labelSaleAmount;
        private Label labelPaidAmount;
        private Label labelBalanceAmount;
        private Label labelCustomerName;
        private Label labelCustomerPhone;
        private Label labelCustomerEmail;
        private DataGridView dataGridViewTransactions;
        private GroupBox groupBoxPlotDetails;
        private Label labelPlotIdTitle;
        private Label labelPlotNumberTitle;
        private Label labelStatusTitle;
        private Label labelAreaTitle;
        private Label labelCustomerNameTitle;
        private Label labelCustomerPhoneTitle;
        private Label labelCustomerEmailTitle;
        private Label labelSaleAmountTitle;
        private Label labelPaidAmountTitle;
        private Label labelBalanceAmountTitle;
        private GroupBox groupBoxTransactionGrid;

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlotDetailsForm));
            labelPlotId = new Label();
            labelPlotNumber = new Label();
            labelStatus = new Label();
            labelArea = new Label();
            labelCreatedBy = new Label();
            labelCreatedDate = new Label();
            labelModifiedBy = new Label();
            labelModifiedDate = new Label();
            labelIsDeleted = new Label();
            labelSaleDate = new Label();
            labelSaleAmount = new Label();
            labelPaidAmount = new Label();
            labelBalanceAmount = new Label();
            labelCustomerName = new Label();
            labelCustomerPhone = new Label();
            labelCustomerEmail = new Label();
            dataGridViewTransactions = new DataGridView();
            groupBoxPlotDetails = new GroupBox();
            buttonGenerateReport = new Button();
            labelPaidAmountTitle = new Label();
            labelPlotIdTitle = new Label();
            labelBalanceAmountTitle = new Label();
            labelPlotNumberTitle = new Label();
            labelStatusTitle = new Label();
            labelAreaTitle = new Label();
            labelCustomerNameTitle = new Label();
            labelCustomerPhoneTitle = new Label();
            labelCustomerEmailTitle = new Label();
            labelSaleAmountTitle = new Label();
            groupBoxTransactionGrid = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTransactions).BeginInit();
            groupBoxPlotDetails.SuspendLayout();
            groupBoxTransactionGrid.SuspendLayout();
            SuspendLayout();
            // 
            // labelPlotId
            // 
            labelPlotId.Font = new Font("Segoe UI", 10F);
            labelPlotId.ForeColor = Color.Black;
            labelPlotId.Location = new Point(160, 40);
            labelPlotId.Name = "labelPlotId";
            labelPlotId.Size = new Size(233, 30);
            labelPlotId.TabIndex = 0;
            labelPlotId.Text = "Plot ID:";
            // 
            // labelPlotNumber
            // 
            labelPlotNumber.Font = new Font("Segoe UI", 10F);
            labelPlotNumber.ForeColor = Color.Black;
            labelPlotNumber.Location = new Point(160, 75);
            labelPlotNumber.Name = "labelPlotNumber";
            labelPlotNumber.Size = new Size(233, 31);
            labelPlotNumber.TabIndex = 1;
            labelPlotNumber.Text = "Plot Number:";
            // 
            // labelStatus
            // 
            labelStatus.Font = new Font("Segoe UI", 10F);
            labelStatus.ForeColor = Color.Black;
            labelStatus.Location = new Point(160, 110);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(233, 34);
            labelStatus.TabIndex = 2;
            labelStatus.Text = "Status:";
            // 
            // labelArea
            // 
            labelArea.Font = new Font("Segoe UI", 10F);
            labelArea.ForeColor = Color.Black;
            labelArea.Location = new Point(160, 145);
            labelArea.Name = "labelArea";
            labelArea.Size = new Size(233, 35);
            labelArea.TabIndex = 3;
            labelArea.Text = "Area:";
            // 
            // labelCreatedBy
            // 
            labelCreatedBy.Location = new Point(0, 0);
            labelCreatedBy.Name = "labelCreatedBy";
            labelCreatedBy.Size = new Size(100, 23);
            labelCreatedBy.TabIndex = 0;
            // 
            // labelCreatedDate
            // 
            labelCreatedDate.Location = new Point(0, 0);
            labelCreatedDate.Name = "labelCreatedDate";
            labelCreatedDate.Size = new Size(100, 23);
            labelCreatedDate.TabIndex = 0;
            // 
            // labelModifiedBy
            // 
            labelModifiedBy.Location = new Point(0, 0);
            labelModifiedBy.Name = "labelModifiedBy";
            labelModifiedBy.Size = new Size(100, 23);
            labelModifiedBy.TabIndex = 0;
            // 
            // labelModifiedDate
            // 
            labelModifiedDate.Location = new Point(0, 0);
            labelModifiedDate.Name = "labelModifiedDate";
            labelModifiedDate.Size = new Size(100, 23);
            labelModifiedDate.TabIndex = 0;
            // 
            // labelIsDeleted
            // 
            labelIsDeleted.Location = new Point(0, 0);
            labelIsDeleted.Name = "labelIsDeleted";
            labelIsDeleted.Size = new Size(100, 23);
            labelIsDeleted.TabIndex = 0;
            // 
            // labelSaleDate
            // 
            labelSaleDate.Location = new Point(0, 0);
            labelSaleDate.Name = "labelSaleDate";
            labelSaleDate.Size = new Size(100, 23);
            labelSaleDate.TabIndex = 0;
            // 
            // labelSaleAmount
            // 
            labelSaleAmount.Font = new Font("Segoe UI", 10F);
            labelSaleAmount.ForeColor = Color.Black;
            labelSaleAmount.Location = new Point(771, 112);
            labelSaleAmount.Name = "labelSaleAmount";
            labelSaleAmount.Size = new Size(312, 32);
            labelSaleAmount.TabIndex = 10;
            labelSaleAmount.Text = "Sale Amount:";
            // 
            // labelPaidAmount
            // 
            labelPaidAmount.Font = new Font("Segoe UI", 10F);
            labelPaidAmount.ForeColor = Color.Black;
            labelPaidAmount.Location = new Point(771, 144);
            labelPaidAmount.Name = "labelPaidAmount";
            labelPaidAmount.Size = new Size(312, 24);
            labelPaidAmount.TabIndex = 11;
            labelPaidAmount.Text = "Paid Amount:";
            // 
            // labelBalanceAmount
            // 
            labelBalanceAmount.Font = new Font("Segoe UI", 10F);
            labelBalanceAmount.ForeColor = Color.Black;
            labelBalanceAmount.Location = new Point(773, 180);
            labelBalanceAmount.Name = "labelBalanceAmount";
            labelBalanceAmount.Size = new Size(310, 23);
            labelBalanceAmount.TabIndex = 12;
            labelBalanceAmount.Text = "Balance Amount:";
            // 
            // labelCustomerName
            // 
            labelCustomerName.Font = new Font("Segoe UI", 10F);
            labelCustomerName.ForeColor = Color.Black;
            labelCustomerName.Location = new Point(160, 180);
            labelCustomerName.Name = "labelCustomerName";
            labelCustomerName.Size = new Size(233, 39);
            labelCustomerName.TabIndex = 13;
            labelCustomerName.Text = "Customer Name:";
            // 
            // labelCustomerPhone
            // 
            labelCustomerPhone.Font = new Font("Segoe UI", 10F);
            labelCustomerPhone.ForeColor = Color.Black;
            labelCustomerPhone.Location = new Point(771, 40);
            labelCustomerPhone.Name = "labelCustomerPhone";
            labelCustomerPhone.Size = new Size(312, 30);
            labelCustomerPhone.TabIndex = 14;
            labelCustomerPhone.Text = "Customer Phone:";
            // 
            // labelCustomerEmail
            // 
            labelCustomerEmail.Font = new Font("Segoe UI", 10F);
            labelCustomerEmail.ForeColor = Color.Black;
            labelCustomerEmail.Location = new Point(771, 76);
            labelCustomerEmail.Name = "labelCustomerEmail";
            labelCustomerEmail.Size = new Size(312, 30);
            labelCustomerEmail.TabIndex = 15;
            labelCustomerEmail.Text = "Customer Email:";
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
            dataGridViewTransactions.Location = new Point(10, 32);
            dataGridViewTransactions.Name = "dataGridViewTransactions";
            dataGridViewTransactions.ReadOnly = true;
            dataGridViewTransactions.RowHeadersWidth = 51;
            dataGridViewTransactions.Size = new Size(1261, 320);
            dataGridViewTransactions.TabIndex = 1;
            // 
            // groupBoxPlotDetails
            // 
            groupBoxPlotDetails.BackColor = Color.AliceBlue;
            groupBoxPlotDetails.Controls.Add(buttonGenerateReport);
            groupBoxPlotDetails.Controls.Add(labelPaidAmountTitle);
            groupBoxPlotDetails.Controls.Add(labelPlotIdTitle);
            groupBoxPlotDetails.Controls.Add(labelPaidAmount);
            groupBoxPlotDetails.Controls.Add(labelBalanceAmountTitle);
            groupBoxPlotDetails.Controls.Add(labelPlotNumberTitle);
            groupBoxPlotDetails.Controls.Add(labelBalanceAmount);
            groupBoxPlotDetails.Controls.Add(labelStatusTitle);
            groupBoxPlotDetails.Controls.Add(labelAreaTitle);
            groupBoxPlotDetails.Controls.Add(labelCustomerNameTitle);
            groupBoxPlotDetails.Controls.Add(labelCustomerPhoneTitle);
            groupBoxPlotDetails.Controls.Add(labelCustomerEmailTitle);
            groupBoxPlotDetails.Controls.Add(labelSaleAmountTitle);
            groupBoxPlotDetails.Controls.Add(labelPlotId);
            groupBoxPlotDetails.Controls.Add(labelPlotNumber);
            groupBoxPlotDetails.Controls.Add(labelStatus);
            groupBoxPlotDetails.Controls.Add(labelArea);
            groupBoxPlotDetails.Controls.Add(labelCustomerName);
            groupBoxPlotDetails.Controls.Add(labelCustomerPhone);
            groupBoxPlotDetails.Controls.Add(labelCustomerEmail);
            groupBoxPlotDetails.Controls.Add(labelSaleAmount);
            groupBoxPlotDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxPlotDetails.ForeColor = Color.MidnightBlue;
            groupBoxPlotDetails.Location = new Point(20, 20);
            groupBoxPlotDetails.Name = "groupBoxPlotDetails";
            groupBoxPlotDetails.Padding = new Padding(15);
            groupBoxPlotDetails.Size = new Size(1280, 234);
            groupBoxPlotDetails.TabIndex = 0;
            groupBoxPlotDetails.TabStop = false;
            groupBoxPlotDetails.Text = "Plot && Customer Details";
            // 
            // buttonGenerateReport
            // 
            buttonGenerateReport.BackColor = Color.Green;
            buttonGenerateReport.FlatStyle = FlatStyle.Flat;
            buttonGenerateReport.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonGenerateReport.ForeColor = Color.White;
            buttonGenerateReport.Location = new Point(1112, 176);
            buttonGenerateReport.Name = "buttonGenerateReport";
            buttonGenerateReport.Size = new Size(159, 36);
            buttonGenerateReport.TabIndex = 41;
            buttonGenerateReport.Text = "Generate Report";
            buttonGenerateReport.UseVisualStyleBackColor = false;
            buttonGenerateReport.Click += buttonGenerateReport_Click;
            // 
            // labelPaidAmountTitle
            // 
            labelPaidAmountTitle.AutoSize = true;
            labelPaidAmountTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPaidAmountTitle.ForeColor = Color.DarkSlateGray;
            labelPaidAmountTitle.Location = new Point(621, 145);
            labelPaidAmountTitle.Name = "labelPaidAmountTitle";
            labelPaidAmountTitle.Size = new Size(120, 23);
            labelPaidAmountTitle.TabIndex = 8;
            labelPaidAmountTitle.Text = "Paid Amount:";
            // 
            // labelPlotIdTitle
            // 
            labelPlotIdTitle.AutoSize = true;
            labelPlotIdTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPlotIdTitle.ForeColor = Color.DarkSlateGray;
            labelPlotIdTitle.Location = new Point(20, 40);
            labelPlotIdTitle.Name = "labelPlotIdTitle";
            labelPlotIdTitle.Size = new Size(70, 23);
            labelPlotIdTitle.TabIndex = 0;
            labelPlotIdTitle.Text = "Plot ID:";
            // 
            // labelBalanceAmountTitle
            // 
            labelBalanceAmountTitle.AutoSize = true;
            labelBalanceAmountTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelBalanceAmountTitle.ForeColor = Color.DarkSlateGray;
            labelBalanceAmountTitle.Location = new Point(621, 180);
            labelBalanceAmountTitle.Name = "labelBalanceAmountTitle";
            labelBalanceAmountTitle.Size = new Size(146, 23);
            labelBalanceAmountTitle.TabIndex = 9;
            labelBalanceAmountTitle.Text = "Balance Amount:";
            // 
            // labelPlotNumberTitle
            // 
            labelPlotNumberTitle.AutoSize = true;
            labelPlotNumberTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPlotNumberTitle.ForeColor = Color.DarkSlateGray;
            labelPlotNumberTitle.Location = new Point(20, 75);
            labelPlotNumberTitle.Name = "labelPlotNumberTitle";
            labelPlotNumberTitle.Size = new Size(119, 23);
            labelPlotNumberTitle.TabIndex = 1;
            labelPlotNumberTitle.Text = "Plot Number:";
            // 
            // labelStatusTitle
            // 
            labelStatusTitle.AutoSize = true;
            labelStatusTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelStatusTitle.ForeColor = Color.DarkSlateGray;
            labelStatusTitle.Location = new Point(20, 110);
            labelStatusTitle.Name = "labelStatusTitle";
            labelStatusTitle.Size = new Size(65, 23);
            labelStatusTitle.TabIndex = 2;
            labelStatusTitle.Text = "Status:";
            // 
            // labelAreaTitle
            // 
            labelAreaTitle.AutoSize = true;
            labelAreaTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAreaTitle.ForeColor = Color.DarkSlateGray;
            labelAreaTitle.Location = new Point(20, 145);
            labelAreaTitle.Name = "labelAreaTitle";
            labelAreaTitle.Size = new Size(52, 23);
            labelAreaTitle.TabIndex = 3;
            labelAreaTitle.Text = "Area:";
            // 
            // labelCustomerNameTitle
            // 
            labelCustomerNameTitle.AutoSize = true;
            labelCustomerNameTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCustomerNameTitle.ForeColor = Color.DarkSlateGray;
            labelCustomerNameTitle.Location = new Point(20, 180);
            labelCustomerNameTitle.Name = "labelCustomerNameTitle";
            labelCustomerNameTitle.Size = new Size(144, 23);
            labelCustomerNameTitle.TabIndex = 4;
            labelCustomerNameTitle.Text = "Customer Name:";
            // 
            // labelCustomerPhoneTitle
            // 
            labelCustomerPhoneTitle.AutoSize = true;
            labelCustomerPhoneTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCustomerPhoneTitle.ForeColor = Color.DarkSlateGray;
            labelCustomerPhoneTitle.Location = new Point(621, 40);
            labelCustomerPhoneTitle.Name = "labelCustomerPhoneTitle";
            labelCustomerPhoneTitle.Size = new Size(146, 23);
            labelCustomerPhoneTitle.TabIndex = 5;
            labelCustomerPhoneTitle.Text = "Customer Phone:";
            // 
            // labelCustomerEmailTitle
            // 
            labelCustomerEmailTitle.AutoSize = true;
            labelCustomerEmailTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCustomerEmailTitle.ForeColor = Color.DarkSlateGray;
            labelCustomerEmailTitle.Location = new Point(621, 76);
            labelCustomerEmailTitle.Name = "labelCustomerEmailTitle";
            labelCustomerEmailTitle.Size = new Size(141, 23);
            labelCustomerEmailTitle.TabIndex = 6;
            labelCustomerEmailTitle.Text = "Customer Email:";
            // 
            // labelSaleAmountTitle
            // 
            labelSaleAmountTitle.AutoSize = true;
            labelSaleAmountTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelSaleAmountTitle.ForeColor = Color.DarkSlateGray;
            labelSaleAmountTitle.Location = new Point(621, 111);
            labelSaleAmountTitle.Name = "labelSaleAmountTitle";
            labelSaleAmountTitle.Size = new Size(118, 23);
            labelSaleAmountTitle.TabIndex = 7;
            labelSaleAmountTitle.Text = "Sale Amount:";
            // 
            // groupBoxTransactionGrid
            // 
            groupBoxTransactionGrid.BackColor = Color.AliceBlue;
            groupBoxTransactionGrid.Controls.Add(dataGridViewTransactions);
            groupBoxTransactionGrid.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxTransactionGrid.ForeColor = Color.MidnightBlue;
            groupBoxTransactionGrid.Location = new Point(20, 273);
            groupBoxTransactionGrid.Name = "groupBoxTransactionGrid";
            groupBoxTransactionGrid.Padding = new Padding(15);
            groupBoxTransactionGrid.Size = new Size(1280, 370);
            groupBoxTransactionGrid.TabIndex = 1;
            groupBoxTransactionGrid.TabStop = false;
            groupBoxTransactionGrid.Text = "Transaction List";
            // 
            // PlotDetailsForm
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1312, 655);
            Controls.Add(groupBoxPlotDetails);
            Controls.Add(groupBoxTransactionGrid);
            Font = new Font("Segoe UI", 10F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PlotDetailsForm";
            Text = "Plot Details";
            ((System.ComponentModel.ISupportInitialize)dataGridViewTransactions).EndInit();
            groupBoxPlotDetails.ResumeLayout(false);
            groupBoxPlotDetails.PerformLayout();
            groupBoxTransactionGrid.ResumeLayout(false);
            ResumeLayout(false);
        }
        private Button buttonGenerateReport;
    }
}