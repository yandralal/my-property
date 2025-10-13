namespace RealEstateManager.Pages.Property
{
    partial class ManagePropertyLoansResponsiveForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewLoans;
        private System.Windows.Forms.Button buttonAddLender;
        private System.Windows.Forms.Label labelLoans;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel loansHeaderPanel;

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
            panelMain = new Panel();
            dataGridViewLoans = new DataGridView();
            loansHeaderPanel = new Panel();
            labelLoans = new Label();
            buttonAddLender = new Button();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLoans).BeginInit();
            loansHeaderPanel.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(dataGridViewLoans);
            panelMain.Controls.Add(loansHeaderPanel);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Size = new Size(1535, 420);
            panelMain.TabIndex = 0;
            // 
            // dataGridViewLoans
            // 
            dataGridViewLoans.AllowUserToAddRows = false;
            dataGridViewLoans.AllowUserToDeleteRows = false;
            dataGridViewLoans.BackgroundColor = Color.White;
            dataGridViewLoans.ColumnHeadersHeight = 29;
            dataGridViewLoans.Dock = DockStyle.Fill;
            dataGridViewLoans.Location = new Point(20, 70);
            dataGridViewLoans.Name = "dataGridViewLoans";
            dataGridViewLoans.ReadOnly = true;
            dataGridViewLoans.RowHeadersWidth = 51;
            dataGridViewLoans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewLoans.Size = new Size(1495, 330);
            dataGridViewLoans.TabIndex = 0;
            // 
            // loansHeaderPanel
            // 
            loansHeaderPanel.Controls.Add(labelLoans);
            loansHeaderPanel.Controls.Add(buttonAddLender);
            loansHeaderPanel.Dock = DockStyle.Top;
            loansHeaderPanel.Location = new Point(20, 20);
            loansHeaderPanel.Name = "loansHeaderPanel";
            loansHeaderPanel.Size = new Size(1495, 50);
            loansHeaderPanel.TabIndex = 1;
            // 
            // labelLoans
            // 
            labelLoans.Dock = DockStyle.Left;
            labelLoans.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelLoans.ForeColor = Color.Black;
            labelLoans.Location = new Point(0, 0);
            labelLoans.Name = "labelLoans";
            labelLoans.Size = new Size(300, 50);
            labelLoans.TabIndex = 0;
            labelLoans.Text = "All Property Loan Lenders";
            labelLoans.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // buttonAddLender
            // 
            buttonAddLender.AutoSize = true;
            buttonAddLender.BackColor = Color.Green;
            buttonAddLender.Dock = DockStyle.None;
            buttonAddLender.FlatStyle = FlatStyle.Flat;
            buttonAddLender.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonAddLender.ForeColor = Color.White;
            buttonAddLender.Location = new Point(1355, 0);
            buttonAddLender.MinimumSize = new Size(0, 36);
            buttonAddLender.MaximumSize = new Size(200, 27);
            buttonAddLender.Size = new Size(200, 27);
            buttonAddLender.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAddLender.Location = new Point(loansHeaderPanel.Width - buttonAddLender.Width - 10, 7);
            buttonAddLender.TabIndex = 1;
            buttonAddLender.Text = "Add Lender";
            buttonAddLender.UseVisualStyleBackColor = false;
            // 
            // ManagePropertyLoansResponsiveForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 255);
            ClientSize = new Size(1535, 420);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(panelMain);
            Font = new Font("Segoe UI", 10F);
            Name = "ManagePropertyLoansResponsiveForm";
            Text = "Manage Property Loans";
            panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewLoans).EndInit();
            loansHeaderPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
