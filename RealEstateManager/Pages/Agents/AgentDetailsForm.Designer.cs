using System.Windows.Forms;

namespace RealEstateManager.Pages
{
    partial class AgentDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxAgentDetails;
        private System.Windows.Forms.GroupBox groupBoxTransactionGrid;
        private System.Windows.Forms.DataGridView dataGridViewTransactions;
        private System.Windows.Forms.Label labelIdTitle, labelIdValue;
        private System.Windows.Forms.Label labelNameTitle, labelNameValue;
        private System.Windows.Forms.Label labelContactTitle, labelContactValue;
        private System.Windows.Forms.Label labelAgencyTitle, labelAgencyValue;
        private System.Windows.Forms.Label labelTotalBrokerageTitle, labelTotalBrokerageValue;
        private System.Windows.Forms.Label labelAmountPaidTitle, labelAmountPaidValue;
        private System.Windows.Forms.Label labelBalanceTitle, labelBalanceValue;
        private System.Windows.Forms.GroupBox groupBoxPlotsSold;
        private System.Windows.Forms.DataGridView dataGridViewPlotsSold;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentDetailsForm));
            groupBoxAgentDetails = new GroupBox();
            labelIdTitle = new Label();
            labelIdValue = new Label();
            labelNameTitle = new Label();
            labelNameValue = new Label();
            labelContactTitle = new Label();
            labelContactValue = new Label();
            labelAgencyTitle = new Label();
            labelAgencyValue = new Label();
            labelTotalBrokerageTitle = new Label();
            labelTotalBrokerageValue = new Label();
            labelAmountPaidTitle = new Label();
            labelAmountPaidValue = new Label();
            labelBalanceTitle = new Label();
            labelBalanceValue = new Label();
            groupBoxTransactionGrid = new GroupBox();
            dataGridViewTransactions = new DataGridView();
            actionCol = new DataGridViewImageColumn();
            groupBoxPlotsSold = new GroupBox();
            dataGridViewPlotsSold = new DataGridView();
            groupBoxAgentDetails.SuspendLayout();
            groupBoxTransactionGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTransactions).BeginInit();
            groupBoxPlotsSold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlotsSold).BeginInit();
            SuspendLayout();
            // 
            // groupBoxAgentDetails
            // 
            groupBoxAgentDetails.BackColor = SystemColors.Control;
            groupBoxAgentDetails.Controls.Add(labelIdTitle);
            groupBoxAgentDetails.Controls.Add(labelIdValue);
            groupBoxAgentDetails.Controls.Add(labelNameTitle);
            groupBoxAgentDetails.Controls.Add(labelNameValue);
            groupBoxAgentDetails.Controls.Add(labelContactTitle);
            groupBoxAgentDetails.Controls.Add(labelContactValue);
            groupBoxAgentDetails.Controls.Add(labelAgencyTitle);
            groupBoxAgentDetails.Controls.Add(labelAgencyValue);
            groupBoxAgentDetails.Controls.Add(labelTotalBrokerageTitle);
            groupBoxAgentDetails.Controls.Add(labelTotalBrokerageValue);
            groupBoxAgentDetails.Controls.Add(labelAmountPaidTitle);
            groupBoxAgentDetails.Controls.Add(labelAmountPaidValue);
            groupBoxAgentDetails.Controls.Add(labelBalanceTitle);
            groupBoxAgentDetails.Controls.Add(labelBalanceValue);
            groupBoxAgentDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxAgentDetails.ForeColor = Color.MidnightBlue;
            groupBoxAgentDetails.Location = new Point(20, 20);
            groupBoxAgentDetails.Name = "groupBoxAgentDetails";
            groupBoxAgentDetails.Padding = new Padding(15);
            groupBoxAgentDetails.Size = new Size(1347, 159);
            groupBoxAgentDetails.TabIndex = 0;
            groupBoxAgentDetails.TabStop = false;
            groupBoxAgentDetails.Text = "Agent Details";
            // 
            // labelIdTitle
            // 
            labelIdTitle.AutoSize = true;
            labelIdTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelIdTitle.ForeColor = Color.DarkSlateGray;
            labelIdTitle.Location = new Point(20, 40);
            labelIdTitle.Name = "labelIdTitle";
            labelIdTitle.Size = new Size(87, 23);
            labelIdTitle.TabIndex = 0;
            labelIdTitle.Text = "Agent ID:";
            // 
            // labelIdValue
            // 
            labelIdValue.AutoSize = true;
            labelIdValue.Font = new Font("Segoe UI", 10F);
            labelIdValue.ForeColor = Color.Black;
            labelIdValue.Location = new Point(120, 40);
            labelIdValue.Name = "labelIdValue";
            labelIdValue.Size = new Size(0, 23);
            labelIdValue.TabIndex = 1;
            // 
            // labelNameTitle
            // 
            labelNameTitle.AutoSize = true;
            labelNameTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelNameTitle.ForeColor = Color.DarkSlateGray;
            labelNameTitle.Location = new Point(300, 40);
            labelNameTitle.Name = "labelNameTitle";
            labelNameTitle.Size = new Size(62, 23);
            labelNameTitle.TabIndex = 2;
            labelNameTitle.Text = "Name:";
            // 
            // labelNameValue
            // 
            labelNameValue.AutoSize = true;
            labelNameValue.Font = new Font("Segoe UI", 10F);
            labelNameValue.ForeColor = Color.Black;
            labelNameValue.Location = new Point(370, 40);
            labelNameValue.Name = "labelNameValue";
            labelNameValue.Size = new Size(0, 23);
            labelNameValue.TabIndex = 3;
            // 
            // labelContactTitle
            // 
            labelContactTitle.AutoSize = true;
            labelContactTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelContactTitle.ForeColor = Color.DarkSlateGray;
            labelContactTitle.Location = new Point(600, 40);
            labelContactTitle.Name = "labelContactTitle";
            labelContactTitle.Size = new Size(77, 23);
            labelContactTitle.TabIndex = 4;
            labelContactTitle.Text = "Contact:";
            // 
            // labelContactValue
            // 
            labelContactValue.AutoSize = true;
            labelContactValue.Font = new Font("Segoe UI", 10F);
            labelContactValue.ForeColor = Color.Black;
            labelContactValue.Location = new Point(690, 40);
            labelContactValue.Name = "labelContactValue";
            labelContactValue.Size = new Size(0, 23);
            labelContactValue.TabIndex = 5;
            // 
            // labelAgencyTitle
            // 
            labelAgencyTitle.AutoSize = true;
            labelAgencyTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAgencyTitle.ForeColor = Color.DarkSlateGray;
            labelAgencyTitle.Location = new Point(900, 40);
            labelAgencyTitle.Name = "labelAgencyTitle";
            labelAgencyTitle.Size = new Size(74, 23);
            labelAgencyTitle.TabIndex = 6;
            labelAgencyTitle.Text = "Agency:";
            // 
            // labelAgencyValue
            // 
            labelAgencyValue.AutoSize = true;
            labelAgencyValue.Font = new Font("Segoe UI", 10F);
            labelAgencyValue.ForeColor = Color.Black;
            labelAgencyValue.Location = new Point(980, 40);
            labelAgencyValue.Name = "labelAgencyValue";
            labelAgencyValue.Size = new Size(0, 23);
            labelAgencyValue.TabIndex = 7;
            // 
            // labelTotalBrokerageTitle
            // 
            labelTotalBrokerageTitle.AutoSize = true;
            labelTotalBrokerageTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalBrokerageTitle.ForeColor = Color.DarkSlateGray;
            labelTotalBrokerageTitle.Location = new Point(20, 90);
            labelTotalBrokerageTitle.Name = "labelTotalBrokerageTitle";
            labelTotalBrokerageTitle.Size = new Size(142, 23);
            labelTotalBrokerageTitle.TabIndex = 8;
            labelTotalBrokerageTitle.Text = "Total Brokerage:";
            // 
            // labelTotalBrokerageValue
            // 
            labelTotalBrokerageValue.AutoSize = true;
            labelTotalBrokerageValue.Font = new Font("Segoe UI", 10F);
            labelTotalBrokerageValue.ForeColor = Color.Black;
            labelTotalBrokerageValue.Location = new Point(160, 90);
            labelTotalBrokerageValue.Name = "labelTotalBrokerageValue";
            labelTotalBrokerageValue.Size = new Size(0, 23);
            labelTotalBrokerageValue.TabIndex = 9;
            // 
            // labelAmountPaidTitle
            // 
            labelAmountPaidTitle.AutoSize = true;
            labelAmountPaidTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAmountPaidTitle.ForeColor = Color.DarkSlateGray;
            labelAmountPaidTitle.Location = new Point(300, 90);
            labelAmountPaidTitle.Name = "labelAmountPaidTitle";
            labelAmountPaidTitle.Size = new Size(120, 23);
            labelAmountPaidTitle.TabIndex = 10;
            labelAmountPaidTitle.Text = "Amount Paid:";
            // 
            // labelAmountPaidValue
            // 
            labelAmountPaidValue.AutoSize = true;
            labelAmountPaidValue.Font = new Font("Segoe UI", 10F);
            labelAmountPaidValue.ForeColor = Color.Black;
            labelAmountPaidValue.Location = new Point(420, 90);
            labelAmountPaidValue.Name = "labelAmountPaidValue";
            labelAmountPaidValue.Size = new Size(0, 23);
            labelAmountPaidValue.TabIndex = 11;
            // 
            // labelBalanceTitle
            // 
            labelBalanceTitle.AutoSize = true;
            labelBalanceTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelBalanceTitle.ForeColor = Color.DarkSlateGray;
            labelBalanceTitle.Location = new Point(600, 90);
            labelBalanceTitle.Name = "labelBalanceTitle";
            labelBalanceTitle.Size = new Size(76, 23);
            labelBalanceTitle.TabIndex = 12;
            labelBalanceTitle.Text = "Balance:";
            // 
            // labelBalanceValue
            // 
            labelBalanceValue.AutoSize = true;
            labelBalanceValue.Font = new Font("Segoe UI", 10F);
            labelBalanceValue.ForeColor = Color.Black;
            labelBalanceValue.Location = new Point(690, 90);
            labelBalanceValue.Name = "labelBalanceValue";
            labelBalanceValue.Size = new Size(0, 23);
            labelBalanceValue.TabIndex = 13;
            // 
            // groupBoxTransactionGrid
            // 
            groupBoxTransactionGrid.BackColor = SystemColors.Control;
            groupBoxTransactionGrid.Controls.Add(dataGridViewTransactions);
            groupBoxTransactionGrid.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxTransactionGrid.ForeColor = Color.MidnightBlue;
            groupBoxTransactionGrid.Location = new Point(20, 514);
            groupBoxTransactionGrid.Name = "groupBoxTransactionGrid";
            groupBoxTransactionGrid.Padding = new Padding(15);
            groupBoxTransactionGrid.Size = new Size(1347, 315);
            groupBoxTransactionGrid.TabIndex = 1;
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
            dataGridViewTransactions.Location = new Point(10, 32);
            dataGridViewTransactions.Name = "dataGridViewTransactions";
            dataGridViewTransactions.ReadOnly = true;
            dataGridViewTransactions.RowHeadersWidth = 51;
            dataGridViewTransactions.Size = new Size(1326, 263);
            dataGridViewTransactions.TabIndex = 1;
            // 
            // actionCol
            // 
            actionCol.MinimumWidth = 6;
            actionCol.Name = "actionCol";
            actionCol.ReadOnly = true;
            actionCol.Width = 125;
            // 
            // groupBoxPlotsSold
            // 
            groupBoxPlotsSold.BackColor = SystemColors.Control;
            groupBoxPlotsSold.Controls.Add(dataGridViewPlotsSold);
            groupBoxPlotsSold.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxPlotsSold.ForeColor = Color.MidnightBlue;
            groupBoxPlotsSold.Location = new Point(20, 204);
            groupBoxPlotsSold.Name = "groupBoxPlotsSold";
            groupBoxPlotsSold.Padding = new Padding(15);
            groupBoxPlotsSold.Size = new Size(1347, 283);
            groupBoxPlotsSold.TabIndex = 2;
            groupBoxPlotsSold.TabStop = false;
            groupBoxPlotsSold.Text = "Plots Sold";
            // 
            // dataGridViewPlotsSold
            // 
            dataGridViewPlotsSold.AllowUserToAddRows = false;
            dataGridViewPlotsSold.AllowUserToDeleteRows = false;
            dataGridViewPlotsSold.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewPlotsSold.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPlotsSold.BackgroundColor = Color.White;
            dataGridViewPlotsSold.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewPlotsSold.ColumnHeadersHeight = 29;
            dataGridViewPlotsSold.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewPlotsSold.Location = new Point(10, 32);
            dataGridViewPlotsSold.Name = "dataGridViewPlotsSold";
            dataGridViewPlotsSold.ReadOnly = true;
            dataGridViewPlotsSold.RowHeadersWidth = 51;
            dataGridViewPlotsSold.Size = new Size(1326, 225);
            dataGridViewPlotsSold.TabIndex = 2;
            // 
            // AgentDetailsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            BackColor = Color.WhiteSmoke;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1384, 843);
            Controls.Add(groupBoxAgentDetails);
            Controls.Add(groupBoxTransactionGrid);
            Controls.Add(groupBoxPlotsSold);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "AgentDetailsForm";
            Text = "Agent Details";
            groupBoxAgentDetails.ResumeLayout(false);
            groupBoxAgentDetails.PerformLayout();
            groupBoxTransactionGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewTransactions).EndInit();
            groupBoxPlotsSold.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlotsSold).EndInit();
            ResumeLayout(false);
        }
        private DataGridViewImageColumn actionCol;
    }
}