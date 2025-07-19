namespace RealEstateManager.Pages
{
    partial class AgentDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxAgentDetails;
        private System.Windows.Forms.GroupBox groupBoxTransactionGrid;
        private System.Windows.Forms.DataGridView dataGridViewTransactions;
        private System.Windows.Forms.Label labelNameTitle, labelContactTitle, labelAgencyTitle, labelIdTitle;
        private System.Windows.Forms.Label labelNameValue, labelContactValue, labelAgencyValue, labelIdValue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            groupBoxAgentDetails = new GroupBox();
            labelIdTitle = new Label();
            labelIdValue = new Label();
            labelNameTitle = new Label();
            labelNameValue = new Label();
            labelContactTitle = new Label();
            labelContactValue = new Label();
            labelAgencyTitle = new Label();
            labelAgencyValue = new Label();
            groupBoxTransactionGrid = new GroupBox();
            dataGridViewTransactions = new DataGridView();
            actionCol = new DataGridViewImageColumn();
            groupBoxAgentDetails.SuspendLayout();
            groupBoxTransactionGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTransactions).BeginInit();
            SuspendLayout();
            // 
            // groupBoxAgentDetails
            // 
            groupBoxAgentDetails.BackColor = Color.AliceBlue;
            groupBoxAgentDetails.Controls.Add(labelIdTitle);
            groupBoxAgentDetails.Controls.Add(labelIdValue);
            groupBoxAgentDetails.Controls.Add(labelNameTitle);
            groupBoxAgentDetails.Controls.Add(labelNameValue);
            groupBoxAgentDetails.Controls.Add(labelContactTitle);
            groupBoxAgentDetails.Controls.Add(labelContactValue);
            groupBoxAgentDetails.Controls.Add(labelAgencyTitle);
            groupBoxAgentDetails.Controls.Add(labelAgencyValue);
            groupBoxAgentDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxAgentDetails.ForeColor = Color.MidnightBlue;
            groupBoxAgentDetails.Location = new Point(20, 20);
            groupBoxAgentDetails.Name = "groupBoxAgentDetails";
            groupBoxAgentDetails.Padding = new Padding(15);
            groupBoxAgentDetails.Size = new Size(1280, 120);
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
            // groupBoxTransactionGrid
            // 
            groupBoxTransactionGrid.BackColor = Color.AliceBlue;
            groupBoxTransactionGrid.Controls.Add(dataGridViewTransactions);
            groupBoxTransactionGrid.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxTransactionGrid.ForeColor = Color.MidnightBlue;
            groupBoxTransactionGrid.Location = new Point(20, 160);
            groupBoxTransactionGrid.Name = "groupBoxTransactionGrid";
            groupBoxTransactionGrid.Padding = new Padding(15);
            groupBoxTransactionGrid.Size = new Size(1280, 434);
            groupBoxTransactionGrid.TabIndex = 1;
            groupBoxTransactionGrid.TabStop = false;
            groupBoxTransactionGrid.Text = "Transaction List";
            // 
            // dataGridViewTransactions
            // 
            dataGridViewTransactions.AllowUserToAddRows = false;
            dataGridViewTransactions.AllowUserToDeleteRows = false;
            dataGridViewTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTransactions.BackgroundColor = Color.White;
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewTransactions.ColumnHeadersHeight = 29;
            dataGridViewTransactions.Columns.AddRange(new DataGridViewColumn[] { actionCol });
            dataGridViewTransactions.Dock = DockStyle.Fill;
            dataGridViewTransactions.EnableHeadersVisualStyles = false;
            dataGridViewTransactions.GridColor = Color.LightSteelBlue;
            dataGridViewTransactions.Location = new Point(15, 42);
            dataGridViewTransactions.Name = "dataGridViewTransactions";
            dataGridViewTransactions.ReadOnly = true;
            dataGridViewTransactions.RowHeadersWidth = 51;
            dataGridViewTransactions.Size = new Size(1250, 377);
            dataGridViewTransactions.TabIndex = 0;
            // 
            // actionCol
            // 
            actionCol.MinimumWidth = 6;
            actionCol.Name = "actionCol";
            actionCol.ReadOnly = true;
            // 
            // AgentDetailsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1320, 600);
            Controls.Add(groupBoxAgentDetails);
            Controls.Add(groupBoxTransactionGrid);
            Name = "AgentDetailsForm";
            Text = "Agent Details";
            groupBoxAgentDetails.ResumeLayout(false);
            groupBoxAgentDetails.PerformLayout();
            groupBoxTransactionGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewTransactions).EndInit();
            ResumeLayout(false);
        }
        private DataGridViewImageColumn actionCol;
    }
}