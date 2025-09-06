namespace RealEstateManager.Pages
{
    partial class ManagePropertyLoansForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewLoans;
        private System.Windows.Forms.Label labelLoans;
        private System.Windows.Forms.GroupBox groupBoxLoans;
        private System.Windows.Forms.Button buttonAddLender;

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
            dataGridViewLoans = new DataGridView();
            labelLoans = new Label();
            groupBoxLoans = new GroupBox();
            buttonAddLender = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLoans).BeginInit();
            groupBoxLoans.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewLoans
            // 
            dataGridViewLoans.AllowUserToAddRows = false;
            dataGridViewLoans.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.AliceBlue;
            dataGridViewLoans.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewLoans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLoans.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewLoans.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewLoans.ColumnHeadersHeight = 29;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.AliceBlue;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewLoans.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewLoans.Location = new Point(15, 85);
            dataGridViewLoans.Name = "dataGridViewLoans";
            dataGridViewLoans.ReadOnly = true;
            dataGridViewLoans.RowHeadersWidth = 51;
            dataGridViewLoans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewLoans.Size = new Size(1469, 437);
            dataGridViewLoans.TabIndex = 0;
            // 
            // labelLoans
            // 
            labelLoans.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelLoans.ForeColor = Color.Black;
            labelLoans.Location = new Point(20, 41);
            labelLoans.Name = "labelLoans";
            labelLoans.Size = new Size(400, 30);
            labelLoans.TabIndex = 1;
            labelLoans.Text = "All Property Loan Lenders";
            // 
            // groupBoxLoans
            // 
            groupBoxLoans.BackColor = Color.AliceBlue;
            groupBoxLoans.Controls.Add(labelLoans);
            groupBoxLoans.Controls.Add(buttonAddLender);
            groupBoxLoans.Controls.Add(dataGridViewLoans);
            groupBoxLoans.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxLoans.ForeColor = Color.MidnightBlue;
            groupBoxLoans.Location = new Point(18, 20);
            groupBoxLoans.Name = "groupBoxLoans";
            groupBoxLoans.Padding = new Padding(15);
            groupBoxLoans.Size = new Size(1502, 534);
            groupBoxLoans.TabIndex = 2;
            groupBoxLoans.TabStop = false;
            groupBoxLoans.Text = "Loan Lenders";
            // 
            // buttonAddLender
            // 
            buttonAddLender.BackColor = Color.Green;
            buttonAddLender.FlatStyle = FlatStyle.Flat;
            buttonAddLender.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonAddLender.ForeColor = Color.White;
            buttonAddLender.Location = new Point(1284, 31);
            buttonAddLender.Name = "buttonAddLender";
            buttonAddLender.Size = new Size(200, 40);
            buttonAddLender.TabIndex = 2;
            buttonAddLender.Text = "Add Lender";
            buttonAddLender.UseVisualStyleBackColor = false;
            buttonAddLender.Click += ButtonAddLender_Click;
            // 
            // ManagePropertyLoansForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 255);
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1534, 571);
            Controls.Add(groupBoxLoans);
            MaximizeBox = false;
            Name = "ManagePropertyLoansForm";
            Text = "Manage Property Loans";
            ((System.ComponentModel.ISupportInitialize)dataGridViewLoans).EndInit();
            groupBoxLoans.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}