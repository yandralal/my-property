namespace RealEstateManager.Pages
{
    partial class MiscTransactionDetailForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewMiscTransactions;
        private System.Windows.Forms.GroupBox groupBoxMiscTransactions;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dataGridViewMiscTransactions = new DataGridView();
            groupBoxMiscTransactions = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMiscTransactions).BeginInit();
            groupBoxMiscTransactions.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewMiscTransactions
            // 
            dataGridViewMiscTransactions.AllowUserToAddRows = false;
            dataGridViewMiscTransactions.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.AliceBlue;
            dataGridViewMiscTransactions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewMiscTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewMiscTransactions.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewMiscTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewMiscTransactions.ColumnHeadersHeight = 29;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.AliceBlue;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewMiscTransactions.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewMiscTransactions.Dock = DockStyle.Fill;
            dataGridViewMiscTransactions.Location = new Point(15, 42);
            dataGridViewMiscTransactions.Name = "dataGridViewMiscTransactions";
            dataGridViewMiscTransactions.ReadOnly = true;
            dataGridViewMiscTransactions.RowHeadersWidth = 51;
            dataGridViewMiscTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewMiscTransactions.Size = new Size(1300, 411);
            dataGridViewMiscTransactions.TabIndex = 0;
            dataGridViewMiscTransactions.CellPainting += DataGridViewMiscTransactions_CellPainting;
            // 
            // groupBoxMiscTransactions
            // 
            groupBoxMiscTransactions.BackColor = SystemColors.Control;
            groupBoxMiscTransactions.Controls.Add(dataGridViewMiscTransactions);
            groupBoxMiscTransactions.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxMiscTransactions.ForeColor = Color.MidnightBlue;
            groupBoxMiscTransactions.Location = new Point(20, 20);
            groupBoxMiscTransactions.Name = "groupBoxMiscTransactions";
            groupBoxMiscTransactions.Padding = new Padding(15);
            groupBoxMiscTransactions.Size = new Size(1330, 468);
            groupBoxMiscTransactions.TabIndex = 1;
            groupBoxMiscTransactions.TabStop = false;
            groupBoxMiscTransactions.Text = "Miscellaneous Transactions";
            // 
            // MiscTransactionDetailForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1362, 500);
            Controls.Add(groupBoxMiscTransactions);
            Name = "MiscTransactionDetailForm";
            Text = "Miscellaneous Transactions";
            ((System.ComponentModel.ISupportInitialize)dataGridViewMiscTransactions).EndInit();
            groupBoxMiscTransactions.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}