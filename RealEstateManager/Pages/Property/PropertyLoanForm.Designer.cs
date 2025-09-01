namespace RealEstateManager.Pages
{
    partial class PropertyLoanForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxLoan;
        private System.Windows.Forms.Label labelPropertyId;
        private System.Windows.Forms.ComboBox comboBoxProperty;
        private System.Windows.Forms.Label labelLoanAmount;
        private System.Windows.Forms.TextBox textBoxLoanAmount;
        private System.Windows.Forms.Label labelLenderName;
        private System.Windows.Forms.TextBox textBoxLenderName;
        private System.Windows.Forms.Label labelInterestRate;
        private System.Windows.Forms.TextBox textBoxInterestRate;
        private System.Windows.Forms.Label labelTenure;
        private System.Windows.Forms.TextBox textBoxTenure;
        private System.Windows.Forms.Label labelTotalRepayable;
        private System.Windows.Forms.TextBox textBoxTotalRepayable;
        private System.Windows.Forms.Label labelLoanDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerLoanDate;
        private System.Windows.Forms.Label labelRemarks;
        private System.Windows.Forms.TextBox textBoxRemarks;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelTotalInterest;
        private System.Windows.Forms.TextBox textBoxTotalInterest;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            groupBoxLoan = new GroupBox();
            labelPropertyId = new Label();
            comboBoxProperty = new ComboBox();
            labelLoanAmount = new Label();
            textBoxLoanAmount = new TextBox();
            labelLenderName = new Label();
            textBoxLenderName = new TextBox();
            labelInterestRate = new Label();
            textBoxInterestRate = new TextBox();
            labelTenure = new Label();
            textBoxTenure = new TextBox();
            labelTotalRepayable = new Label();
            textBoxTotalRepayable = new TextBox();
            labelLoanDate = new Label();
            dateTimePickerLoanDate = new DateTimePicker();
            labelRemarks = new Label();
            textBoxRemarks = new TextBox();
            buttonSave = new Button();
            buttonCancel = new Button();
            labelTotalInterest = new Label();
            textBoxTotalInterest = new TextBox();
            groupBoxLoan.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxLoan
            // 
            groupBoxLoan.Controls.Add(labelPropertyId);
            groupBoxLoan.Controls.Add(comboBoxProperty);
            groupBoxLoan.Controls.Add(labelLoanAmount);
            groupBoxLoan.Controls.Add(textBoxLoanAmount);
            groupBoxLoan.Controls.Add(labelLenderName);
            groupBoxLoan.Controls.Add(textBoxLenderName);
            groupBoxLoan.Controls.Add(labelInterestRate);
            groupBoxLoan.Controls.Add(textBoxInterestRate);
            groupBoxLoan.Controls.Add(labelTenure);
            groupBoxLoan.Controls.Add(textBoxTenure);
            groupBoxLoan.Controls.Add(labelTotalInterest);
            groupBoxLoan.Controls.Add(textBoxTotalInterest);
            groupBoxLoan.Controls.Add(labelTotalRepayable);
            groupBoxLoan.Controls.Add(textBoxTotalRepayable);
            groupBoxLoan.Controls.Add(labelLoanDate);
            groupBoxLoan.Controls.Add(dateTimePickerLoanDate);
            groupBoxLoan.Controls.Add(labelRemarks);
            groupBoxLoan.Controls.Add(textBoxRemarks);
            groupBoxLoan.Controls.Add(buttonSave);
            groupBoxLoan.Controls.Add(buttonCancel);
            groupBoxLoan.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxLoan.ForeColor = Color.MidnightBlue;
            groupBoxLoan.Location = new Point(19, 18);
            groupBoxLoan.Name = "groupBoxLoan";
            groupBoxLoan.Size = new Size(565, 530);
            groupBoxLoan.TabIndex = 0;
            groupBoxLoan.TabStop = false;
            groupBoxLoan.Text = "Loan Details";
            // 
            // labelPropertyId
            // 
            labelPropertyId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPropertyId.ForeColor = Color.DarkSlateGray;
            labelPropertyId.Location = new Point(40, 50);
            labelPropertyId.Name = "labelPropertyId";
            labelPropertyId.Size = new Size(120, 25);
            labelPropertyId.TabIndex = 0;
            labelPropertyId.Text = "Property:";
            // 
            // comboBoxProperty
            // 
            comboBoxProperty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProperty.Font = new Font("Segoe UI", 10F);
            comboBoxProperty.Location = new Point(202, 48);
            comboBoxProperty.Name = "comboBoxProperty";
            comboBoxProperty.Size = new Size(320, 31);
            comboBoxProperty.TabIndex = 1;
            // 
            // labelLoanAmount
            // 
            labelLoanAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelLoanAmount.ForeColor = Color.DarkSlateGray;
            labelLoanAmount.Location = new Point(40, 100);
            labelLoanAmount.Name = "labelLoanAmount";
            labelLoanAmount.Size = new Size(134, 25);
            labelLoanAmount.TabIndex = 2;
            labelLoanAmount.Text = "Loan Amount:";
            // 
            // textBoxLoanAmount
            // 
            textBoxLoanAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxLoanAmount.Font = new Font("Segoe UI", 10F);
            textBoxLoanAmount.Location = new Point(202, 98);
            textBoxLoanAmount.Name = "textBoxLoanAmount";
            textBoxLoanAmount.Size = new Size(320, 30);
            textBoxLoanAmount.TabIndex = 3;
            // 
            // labelLenderName
            // 
            labelLenderName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelLenderName.ForeColor = Color.DarkSlateGray;
            labelLenderName.Location = new Point(40, 147);
            labelLenderName.Name = "labelLenderName";
            labelLenderName.Size = new Size(156, 25);
            labelLenderName.TabIndex = 4;
            labelLenderName.Text = "Lender Name:";
            // 
            // textBoxLenderName
            // 
            textBoxLenderName.BorderStyle = BorderStyle.FixedSingle;
            textBoxLenderName.Font = new Font("Segoe UI", 10F);
            textBoxLenderName.Location = new Point(202, 145);
            textBoxLenderName.Name = "textBoxLenderName";
            textBoxLenderName.Size = new Size(320, 30);
            textBoxLenderName.TabIndex = 5;
            // 
            // labelInterestRate
            // 
            labelInterestRate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelInterestRate.ForeColor = Color.DarkSlateGray;
            labelInterestRate.Location = new Point(40, 192);
            labelInterestRate.Name = "labelInterestRate";
            labelInterestRate.Size = new Size(156, 25);
            labelInterestRate.TabIndex = 6;
            labelInterestRate.Text = "Interest Rate (%):";
            // 
            // textBoxInterestRate
            // 
            textBoxInterestRate.BorderStyle = BorderStyle.FixedSingle;
            textBoxInterestRate.Font = new Font("Segoe UI", 10F);
            textBoxInterestRate.Location = new Point(202, 190);
            textBoxInterestRate.Name = "textBoxInterestRate";
            textBoxInterestRate.Size = new Size(320, 30);
            textBoxInterestRate.TabIndex = 7;
            // 
            // labelTenure
            // 
            labelTenure.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTenure.ForeColor = Color.DarkSlateGray;
            labelTenure.Location = new Point(40, 237);
            labelTenure.Name = "labelTenure";
            labelTenure.Size = new Size(156, 25);
            labelTenure.TabIndex = 8;
            labelTenure.Text = "Tenure (Months):";
            // 
            // textBoxTenure
            // 
            textBoxTenure.BorderStyle = BorderStyle.FixedSingle;
            textBoxTenure.Font = new Font("Segoe UI", 10F);
            textBoxTenure.Location = new Point(202, 235);
            textBoxTenure.Name = "textBoxTenure";
            textBoxTenure.Size = new Size(320, 30);
            textBoxTenure.TabIndex = 9;
            // 
            // labelTotalInterest
            // 
            labelTotalInterest.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalInterest.ForeColor = Color.DarkSlateGray;
            labelTotalInterest.Location = new Point(40, 282);
            labelTotalInterest.Name = "labelTotalInterest";
            labelTotalInterest.Size = new Size(156, 25);
            labelTotalInterest.TabIndex = 10;
            labelTotalInterest.Text = "Total Interest:";
            // 
            // textBoxTotalInterest
            // 
            textBoxTotalInterest.BorderStyle = BorderStyle.FixedSingle;
            textBoxTotalInterest.Font = new Font("Segoe UI", 10F);
            textBoxTotalInterest.Location = new Point(202, 280);
            textBoxTotalInterest.Name = "textBoxTotalInterest";
            textBoxTotalInterest.ReadOnly = true;
            textBoxTotalInterest.Size = new Size(320, 30);
            textBoxTotalInterest.TabIndex = 11;
            textBoxTotalInterest.Enabled = false;
            // 
            // labelTotalRepayable
            // 
            labelTotalRepayable.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalRepayable.ForeColor = Color.DarkSlateGray;
            labelTotalRepayable.Location = new Point(40, 327);
            labelTotalRepayable.Name = "labelTotalRepayable";
            labelTotalRepayable.Size = new Size(156, 25);
            labelTotalRepayable.TabIndex = 12;
            labelTotalRepayable.Text = "Total Repayable:";
            // 
            // textBoxTotalRepayable
            // 
            textBoxTotalRepayable.BorderStyle = BorderStyle.FixedSingle;
            textBoxTotalRepayable.Font = new Font("Segoe UI", 10F);
            textBoxTotalRepayable.Location = new Point(202, 325);
            textBoxTotalRepayable.Name = "textBoxTotalRepayable";
            textBoxTotalRepayable.ReadOnly = true;
            textBoxTotalRepayable.Size = new Size(320, 30);
            textBoxTotalRepayable.TabIndex = 13;
            // 
            // labelLoanDate
            // 
            labelLoanDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelLoanDate.ForeColor = Color.DarkSlateGray;
            labelLoanDate.Location = new Point(40, 372);
            labelLoanDate.Name = "labelLoanDate";
            labelLoanDate.Size = new Size(120, 25);
            labelLoanDate.TabIndex = 14;
            labelLoanDate.Text = "Loan Date:";
            // 
            // dateTimePickerLoanDate
            // 
            dateTimePickerLoanDate.Font = new Font("Segoe UI", 10F);
            dateTimePickerLoanDate.Location = new Point(202, 370);
            dateTimePickerLoanDate.Name = "dateTimePickerLoanDate";
            dateTimePickerLoanDate.Size = new Size(320, 30);
            dateTimePickerLoanDate.TabIndex = 15;
            // 
            // labelRemarks
            // 
            labelRemarks.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelRemarks.ForeColor = Color.DarkSlateGray;
            labelRemarks.Location = new Point(40, 417);
            labelRemarks.Name = "labelRemarks";
            labelRemarks.Size = new Size(120, 25);
            labelRemarks.TabIndex = 16;
            labelRemarks.Text = "Remarks:";
            // 
            // textBoxRemarks
            // 
            textBoxRemarks.BorderStyle = BorderStyle.FixedSingle;
            textBoxRemarks.Font = new Font("Segoe UI", 10F);
            textBoxRemarks.Location = new Point(202, 415);
            textBoxRemarks.Name = "textBoxRemarks";
            textBoxRemarks.Size = new Size(320, 30);
            textBoxRemarks.TabIndex = 17;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.Green;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonSave.ForeColor = Color.White;
            buttonSave.Location = new Point(236, 472);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(110, 35);
            buttonSave.TabIndex = 18;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += ButtonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.Gray;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.White;
            buttonCancel.Location = new Point(365, 472);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(110, 35);
            buttonCancel.TabIndex = 19;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // PropertyLoanForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 560);
            Controls.Add(groupBoxLoan);
            Name = "PropertyLoanForm";
            Text = "Property Loan";
            groupBoxLoan.ResumeLayout(false);
            groupBoxLoan.PerformLayout();
            ResumeLayout(false);
        }
    }
}