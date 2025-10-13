namespace RealEstateManager.Pages
{
    partial class PropertyLoanTransactionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxTransactionEntry;
        private System.Windows.Forms.Label labelPropertyId;
        private System.Windows.Forms.TextBox textBoxPropertyId;
        private System.Windows.Forms.Label labelLenderName;
        private System.Windows.Forms.ComboBox comboBoxLenderName;
        private System.Windows.Forms.Label labelLoanAmount;
        private System.Windows.Forms.TextBox textBoxLoanAmount;
        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.Label labelBalanceAmount;
        private System.Windows.Forms.Label labelBalanceValue;   
        private System.Windows.Forms.Label labelPaymentMethod;
        private System.Windows.Forms.ComboBox comboBoxPaymentMethod;
        private System.Windows.Forms.Label labelReferenceNumber;
        private System.Windows.Forms.TextBox textBoxReferenceNumber;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label labelTransactionDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerTransactionDate;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelTransactionType;
        private System.Windows.Forms.ComboBox comboBoxTransactionType;
        private System.Windows.Forms.Label labelPayingFor;
        private System.Windows.Forms.ComboBox comboBoxPayingFor;
        private System.Windows.Forms.Label labelTotalAmount;
        private System.Windows.Forms.TextBox textBoxTotalAmount;
        private System.Windows.Forms.Label labelTotalPaid;
        private System.Windows.Forms.TextBox textBoxTotalPaid;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            groupBoxTransactionEntry = new GroupBox();
            labelPropertyId = new Label();
            textBoxPropertyId = new TextBox();
            labelLenderName = new Label();
            comboBoxLenderName = new ComboBox();
            labelLoanAmount = new Label();
            textBoxLoanAmount = new TextBox();
            labelAmount = new Label();
            textBoxAmount = new TextBox();
            labelBalanceAmount = new Label();
            labelBalanceValue = new Label();
            labelPaymentMethod = new Label();
            comboBoxPaymentMethod = new ComboBox();
            labelReferenceNumber = new Label();
            textBoxReferenceNumber = new TextBox();
            labelNotes = new Label();
            textBoxNotes = new TextBox();
            labelTransactionDate = new Label();
            dateTimePickerTransactionDate = new DateTimePicker();
            buttonSave = new Button();
            buttonCancel = new Button();
            labelTransactionType = new Label();
            comboBoxTransactionType = new ComboBox();
            labelPayingFor = new Label();
            comboBoxPayingFor = new ComboBox();
            labelTotalAmount = new Label();
            textBoxTotalAmount = new TextBox();
            labelTotalPaid = new Label();
            textBoxTotalPaid = new TextBox();
            groupBoxTransactionEntry.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxTransactionEntry
            // 
            groupBoxTransactionEntry.BackColor = Color.AliceBlue;
            groupBoxTransactionEntry.Controls.Add(labelPropertyId);
            groupBoxTransactionEntry.Controls.Add(textBoxPropertyId);
            groupBoxTransactionEntry.Controls.Add(labelLenderName);
            groupBoxTransactionEntry.Controls.Add(comboBoxLenderName);
            groupBoxTransactionEntry.Controls.Add(labelLoanAmount);
            groupBoxTransactionEntry.Controls.Add(textBoxLoanAmount);
            groupBoxTransactionEntry.Controls.Add(labelAmount);
            groupBoxTransactionEntry.Controls.Add(textBoxAmount);
            groupBoxTransactionEntry.Controls.Add(labelBalanceAmount);
            groupBoxTransactionEntry.Controls.Add(labelBalanceValue);
            groupBoxTransactionEntry.Controls.Add(labelPaymentMethod);
            groupBoxTransactionEntry.Controls.Add(comboBoxPaymentMethod);
            groupBoxTransactionEntry.Controls.Add(labelReferenceNumber);
            groupBoxTransactionEntry.Controls.Add(textBoxReferenceNumber);
            groupBoxTransactionEntry.Controls.Add(labelNotes);
            groupBoxTransactionEntry.Controls.Add(textBoxNotes);
            groupBoxTransactionEntry.Controls.Add(labelTransactionDate);
            groupBoxTransactionEntry.Controls.Add(dateTimePickerTransactionDate);
            groupBoxTransactionEntry.Controls.Add(buttonSave);
            groupBoxTransactionEntry.Controls.Add(buttonCancel);
            groupBoxTransactionEntry.Controls.Add(labelTransactionType);
            groupBoxTransactionEntry.Controls.Add(comboBoxTransactionType);
            groupBoxTransactionEntry.Controls.Add(labelPayingFor);
            groupBoxTransactionEntry.Controls.Add(comboBoxPayingFor);
            groupBoxTransactionEntry.Controls.Add(labelTotalAmount);
            groupBoxTransactionEntry.Controls.Add(textBoxTotalAmount);
            groupBoxTransactionEntry.Controls.Add(labelTotalPaid);
            groupBoxTransactionEntry.Controls.Add(textBoxTotalPaid);
            groupBoxTransactionEntry.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxTransactionEntry.ForeColor = Color.MidnightBlue;
            groupBoxTransactionEntry.Location = new Point(20, 10);
            groupBoxTransactionEntry.Name = "groupBoxTransactionEntry";
            groupBoxTransactionEntry.Padding = new Padding(15);
            groupBoxTransactionEntry.Size = new Size(605, 695);
            groupBoxTransactionEntry.TabIndex = 0;
            groupBoxTransactionEntry.TabStop = false;
            groupBoxTransactionEntry.Text = "Loan Transaction Entry";
            // 
            // labelPropertyId
            // 
            labelPropertyId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPropertyId.ForeColor = Color.DarkSlateGray;
            labelPropertyId.Location = new Point(27, 47);
            labelPropertyId.Name = "labelPropertyId";
            labelPropertyId.Size = new Size(166, 23);
            labelPropertyId.TabIndex = 0;
            labelPropertyId.Text = "Property:";
            // 
            // textBoxPropertyId
            // 
            textBoxPropertyId.BorderStyle = BorderStyle.FixedSingle;
            textBoxPropertyId.Font = new Font("Segoe UI", 10F);
            textBoxPropertyId.Location = new Point(229, 44);
            textBoxPropertyId.Name = "textBoxPropertyId";
            textBoxPropertyId.ReadOnly = true;
            textBoxPropertyId.Size = new Size(320, 30);
            textBoxPropertyId.TabIndex = 0;
            // 
            // labelLenderName
            // 
            labelLenderName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelLenderName.ForeColor = Color.DarkSlateGray;
            labelLenderName.Location = new Point(27, 86);
            labelLenderName.Name = "labelLenderName";
            labelLenderName.Size = new Size(166, 23);
            labelLenderName.TabIndex = 1;
            labelLenderName.Text = "Lender Name:";
            // 
            // comboBoxLenderName
            // 
            comboBoxLenderName.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLenderName.Font = new Font("Segoe UI", 10F);
            comboBoxLenderName.Location = new Point(229, 83);
            comboBoxLenderName.Name = "comboBoxLenderName";
            comboBoxLenderName.Size = new Size(320, 31);
            comboBoxLenderName.TabIndex = 1;
            // 
            // labelLoanAmount
            // 
            labelLoanAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelLoanAmount.ForeColor = Color.DarkSlateGray;
            labelLoanAmount.Location = new Point(27, 132);
            labelLoanAmount.Name = "labelLoanAmount";
            labelLoanAmount.Size = new Size(166, 23);
            labelLoanAmount.TabIndex = 2;
            labelLoanAmount.Text = "Loan Amount:";
            // 
            // textBoxLoanAmount
            // 
            textBoxLoanAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxLoanAmount.Font = new Font("Segoe UI", 10F);
            textBoxLoanAmount.Location = new Point(229, 129);
            textBoxLoanAmount.Name = "textBoxLoanAmount";
            textBoxLoanAmount.ReadOnly = true;
            textBoxLoanAmount.Size = new Size(320, 30);
            textBoxLoanAmount.TabIndex = 2;
            // 
            // labelAmount
            // 
            labelAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAmount.ForeColor = Color.DarkSlateGray;
            labelAmount.Location = new Point(27, 364);
            labelAmount.Name = "labelAmount";
            labelAmount.Size = new Size(140, 23);
            labelAmount.TabIndex = 6;
            labelAmount.Text = "Amount To Pay:";
            // 
            // textBoxAmount
            // 
            textBoxAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxAmount.Font = new Font("Segoe UI", 10F);
            textBoxAmount.Location = new Point(229, 361);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new Size(320, 30);
            textBoxAmount.TabIndex = 5;
            // 
            // labelBalanceAmount
            // 
            labelBalanceAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelBalanceAmount.ForeColor = Color.DarkSlateGray;
            labelBalanceAmount.Location = new Point(27, 410);
            labelBalanceAmount.Name = "labelBalanceAmount";
            labelBalanceAmount.Size = new Size(120, 23);
            labelBalanceAmount.TabIndex = 8;
            labelBalanceAmount.Text = "Balance:";
            // 
            // labelBalanceValue
            // 
            labelBalanceValue.BorderStyle = BorderStyle.FixedSingle;
            labelBalanceValue.Font = new Font("Segoe UI", 10F);
            labelBalanceValue.ForeColor = Color.Black;
            labelBalanceValue.Location = new Point(229, 407);
            labelBalanceValue.Name = "labelBalanceValue";
            labelBalanceValue.Size = new Size(320, 30);
            labelBalanceValue.TabIndex = 9;
            labelBalanceValue.Text = "0.00";
            // 
            // labelPaymentMethod
            // 
            labelPaymentMethod.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPaymentMethod.ForeColor = Color.DarkSlateGray;
            labelPaymentMethod.Location = new Point(27, 457);
            labelPaymentMethod.Name = "labelPaymentMethod";
            labelPaymentMethod.Size = new Size(166, 23);
            labelPaymentMethod.TabIndex = 10;
            labelPaymentMethod.Text = "Payment Method:";
            // 
            // comboBoxPaymentMethod
            // 
            comboBoxPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPaymentMethod.Font = new Font("Segoe UI", 10F);
            comboBoxPaymentMethod.Items.AddRange(new object[] { "Cash", "Cheque", "Bank Transfer", "Other" });
            comboBoxPaymentMethod.Location = new Point(229, 454);
            comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
            comboBoxPaymentMethod.Size = new Size(320, 31);
            comboBoxPaymentMethod.TabIndex = 6;
            // 
            // labelReferenceNumber
            // 
            labelReferenceNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelReferenceNumber.ForeColor = Color.DarkSlateGray;
            labelReferenceNumber.Location = new Point(27, 503);
            labelReferenceNumber.Name = "labelReferenceNumber";
            labelReferenceNumber.Size = new Size(120, 23);
            labelReferenceNumber.TabIndex = 12;
            labelReferenceNumber.Text = "Reference #:";
            // 
            // textBoxReferenceNumber
            // 
            textBoxReferenceNumber.BorderStyle = BorderStyle.FixedSingle;
            textBoxReferenceNumber.Font = new Font("Segoe UI", 10F);
            textBoxReferenceNumber.Location = new Point(229, 500);
            textBoxReferenceNumber.Name = "textBoxReferenceNumber";
            textBoxReferenceNumber.Size = new Size(320, 30);
            textBoxReferenceNumber.TabIndex = 7;
            // 
            // labelNotes
            // 
            labelNotes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelNotes.ForeColor = Color.DarkSlateGray;
            labelNotes.Location = new Point(27, 549);
            labelNotes.Name = "labelNotes";
            labelNotes.Size = new Size(120, 23);
            labelNotes.TabIndex = 14;
            labelNotes.Text = "Notes:";
            // 
            // textBoxNotes
            // 
            textBoxNotes.BorderStyle = BorderStyle.FixedSingle;
            textBoxNotes.Font = new Font("Segoe UI", 10F);
            textBoxNotes.Location = new Point(229, 546);
            textBoxNotes.Multiline = true;
            textBoxNotes.Name = "textBoxNotes";
            textBoxNotes.Size = new Size(320, 44);
            textBoxNotes.TabIndex = 8;
            // 
            // labelTransactionDate
            // 
            labelTransactionDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTransactionDate.ForeColor = Color.DarkSlateGray;
            labelTransactionDate.Location = new Point(27, 604);
            labelTransactionDate.Name = "labelTransactionDate";
            labelTransactionDate.Size = new Size(120, 23);
            labelTransactionDate.TabIndex = 16;
            labelTransactionDate.Text = "Date:";
            // 
            // dateTimePickerTransactionDate
            // 
            dateTimePickerTransactionDate.Font = new Font("Segoe UI", 10F);
            dateTimePickerTransactionDate.Location = new Point(229, 602);
            dateTimePickerTransactionDate.Name = "dateTimePickerTransactionDate";
            dateTimePickerTransactionDate.Size = new Size(320, 30);
            dateTimePickerTransactionDate.TabIndex = 9;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.Green;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonSave.ForeColor = Color.White;
            buttonSave.Location = new Point(263, 647);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(110, 35);
            buttonSave.TabIndex = 10;
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
            buttonCancel.Location = new Point(398, 647);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(110, 35);
            buttonCancel.TabIndex = 11;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // labelTransactionType
            // 
            labelTransactionType.AutoSize = true;
            labelTransactionType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTransactionType.ForeColor = Color.DarkSlateGray;
            labelTransactionType.Location = new Point(27, 318);
            labelTransactionType.Name = "labelTransactionType";
            labelTransactionType.Size = new Size(149, 23);
            labelTransactionType.TabIndex = 10;
            labelTransactionType.Text = "Transaction Type:";
            // 
            // comboBoxTransactionType
            // 
            comboBoxTransactionType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTransactionType.Font = new Font("Segoe UI", 10F);
            comboBoxTransactionType.Location = new Point(229, 315);
            comboBoxTransactionType.Name = "comboBoxTransactionType";
            comboBoxTransactionType.Size = new Size(320, 31);
            comboBoxTransactionType.TabIndex = 4;
            // 
            // labelPayingFor
            // 
            labelPayingFor.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPayingFor.ForeColor = Color.DarkSlateGray;
            labelPayingFor.Location = new Point(27, 174);
            labelPayingFor.Name = "labelPayingFor";
            labelPayingFor.Size = new Size(166, 23);
            labelPayingFor.TabIndex = 3;
            labelPayingFor.Text = "Paying For:";
            // 
            // comboBoxPayingFor
            // 
            comboBoxPayingFor.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPayingFor.Font = new Font("Segoe UI", 10F);
            comboBoxPayingFor.Items.AddRange(new object[] { "Interest", "Principal" });
            comboBoxPayingFor.Location = new Point(229, 171);
            comboBoxPayingFor.Name = "comboBoxPayingFor";
            comboBoxPayingFor.Size = new Size(320, 31);
            comboBoxPayingFor.TabIndex = 3;
            comboBoxPayingFor.SelectedIndexChanged += ComboBoxPayingFor_SelectedIndexChanged;
            // 
            // labelTotalAmount
            // 
            labelTotalAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalAmount.ForeColor = Color.DarkSlateGray;
            labelTotalAmount.Location = new Point(27, 223);
            labelTotalAmount.Name = "labelTotalAmount";
            labelTotalAmount.Size = new Size(166, 23);
            labelTotalAmount.TabIndex = 17;
            labelTotalAmount.Text = "Total Amount:";
            // 
            // textBoxTotalAmount
            // 
            textBoxTotalAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxTotalAmount.Font = new Font("Segoe UI", 10F);
            textBoxTotalAmount.Location = new Point(229, 220);
            textBoxTotalAmount.Name = "textBoxTotalAmount";
            textBoxTotalAmount.ReadOnly = true;
            textBoxTotalAmount.Size = new Size(320, 30);
            textBoxTotalAmount.TabIndex = 7;
            // 
            // labelTotalPaid
            // 
            labelTotalPaid.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalPaid.ForeColor = Color.DarkSlateGray;
            labelTotalPaid.Location = new Point(27, 272);
            labelTotalPaid.Name = "labelTotalPaid";
            labelTotalPaid.Size = new Size(196, 23);
            labelTotalPaid.TabIndex = 20;
            labelTotalPaid.Text = "Total Paid:";
            // 
            // textBoxTotalPaid
            // 
            textBoxTotalPaid.BorderStyle = BorderStyle.FixedSingle;
            textBoxTotalPaid.Font = new Font("Segoe UI", 10F);
            textBoxTotalPaid.Location = new Point(229, 269);
            textBoxTotalPaid.Name = "textBoxTotalPaid";
            textBoxTotalPaid.ReadOnly = true;
            textBoxTotalPaid.Size = new Size(320, 30);
            textBoxTotalPaid.TabIndex = 8;
            // 
            // PropertyLoanTransactionForm
            // 
            //AutoScaleDimensions = new SizeF(9F, 23F);
            BackColor = Color.AliceBlue;
            ClientSize = new Size(637, 708);
            Controls.Add(groupBoxTransactionEntry);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PropertyLoanTransactionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Property Loan Transaction";
            groupBoxTransactionEntry.ResumeLayout(false);
            groupBoxTransactionEntry.PerformLayout();
            ResumeLayout(false);
        }
    }
}