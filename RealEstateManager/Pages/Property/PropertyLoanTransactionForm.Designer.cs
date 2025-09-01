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
        private System.Windows.Forms.Label labelTotalInterest;
        private System.Windows.Forms.TextBox textBoxTotalInterest;
        private System.Windows.Forms.Label labelTotalInterestPaid;
        private System.Windows.Forms.TextBox textBoxTotalInterestPaid;
        private System.Windows.Forms.Label labelTotalPrinciplePaid;
        private System.Windows.Forms.TextBox textBoxTotalPrinciplePaid;
        private System.Windows.Forms.Label labelTotalPrinciple;
        private System.Windows.Forms.TextBox textBoxTotalPrinciple;

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
            labelTotalInterest = new Label();
            textBoxTotalInterest = new TextBox();
            labelTotalInterestPaid = new Label();
            textBoxTotalInterestPaid = new TextBox();
            labelTotalPrinciplePaid = new Label();
            textBoxTotalPrinciplePaid = new TextBox();
            labelTotalPrinciple = new Label();
            textBoxTotalPrinciple = new TextBox();
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
            groupBoxTransactionEntry.Controls.Add(labelTotalInterest);
            groupBoxTransactionEntry.Controls.Add(textBoxTotalInterest);
            groupBoxTransactionEntry.Controls.Add(labelTotalInterestPaid);
            groupBoxTransactionEntry.Controls.Add(textBoxTotalInterestPaid);
            groupBoxTransactionEntry.Controls.Add(labelTotalPrinciplePaid);
            groupBoxTransactionEntry.Controls.Add(textBoxTotalPrinciplePaid);
            groupBoxTransactionEntry.Controls.Add(labelTotalPrinciple);
            groupBoxTransactionEntry.Controls.Add(textBoxTotalPrinciple);
            groupBoxTransactionEntry.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxTransactionEntry.ForeColor = Color.MidnightBlue;
            groupBoxTransactionEntry.Location = new Point(20, 25);
            groupBoxTransactionEntry.Name = "groupBoxTransactionEntry";
            groupBoxTransactionEntry.Padding = new Padding(15);
            groupBoxTransactionEntry.Size = new Size(605, 802);
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
            labelLenderName.Location = new Point(27, 95);
            labelLenderName.Name = "labelLenderName";
            labelLenderName.Size = new Size(166, 23);
            labelLenderName.TabIndex = 1;
            labelLenderName.Text = "Lender Name:";
            // 
            // comboBoxLenderName
            // 
            comboBoxLenderName.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLenderName.Font = new Font("Segoe UI", 10F);
            comboBoxLenderName.Location = new Point(229, 92);
            comboBoxLenderName.Name = "comboBoxLenderName";
            comboBoxLenderName.Size = new Size(320, 31);
            comboBoxLenderName.TabIndex = 1;
            // 
            // labelLoanAmount
            // 
            labelLoanAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelLoanAmount.ForeColor = Color.DarkSlateGray;
            labelLoanAmount.Location = new Point(27, 143);
            labelLoanAmount.Name = "labelLoanAmount";
            labelLoanAmount.Size = new Size(166, 23);
            labelLoanAmount.TabIndex = 2;
            labelLoanAmount.Text = "Loan Amount:";
            // 
            // textBoxLoanAmount
            // 
            textBoxLoanAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxLoanAmount.Font = new Font("Segoe UI", 10F);
            textBoxLoanAmount.Location = new Point(229, 140);
            textBoxLoanAmount.Name = "textBoxLoanAmount";
            textBoxLoanAmount.ReadOnly = true;
            textBoxLoanAmount.Size = new Size(320, 30);
            textBoxLoanAmount.TabIndex = 2;
            // 
            // labelAmount
            // 
            labelAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAmount.ForeColor = Color.DarkSlateGray;
            labelAmount.Location = new Point(27, 429);
            labelAmount.Name = "labelAmount";
            labelAmount.Size = new Size(140, 23);
            labelAmount.TabIndex = 6;
            labelAmount.Text = "Amount To Pay:";
            // 
            // textBoxAmount
            // 
            textBoxAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxAmount.Font = new Font("Segoe UI", 10F);
            textBoxAmount.Location = new Point(229, 426);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new Size(320, 30);
            textBoxAmount.TabIndex = 5;
            // 
            // labelBalanceAmount
            // 
            labelBalanceAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelBalanceAmount.ForeColor = Color.DarkSlateGray;
            labelBalanceAmount.Location = new Point(27, 475);
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
            labelBalanceValue.Location = new Point(229, 472);
            labelBalanceValue.Name = "labelBalanceValue";
            labelBalanceValue.Size = new Size(320, 30);
            labelBalanceValue.TabIndex = 9;
            labelBalanceValue.Text = "0.00";
            // 
            // labelPaymentMethod
            // 
            labelPaymentMethod.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPaymentMethod.ForeColor = Color.DarkSlateGray;
            labelPaymentMethod.Location = new Point(27, 522);
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
            comboBoxPaymentMethod.Location = new Point(229, 519);
            comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
            comboBoxPaymentMethod.Size = new Size(320, 31);
            comboBoxPaymentMethod.TabIndex = 6;
            // 
            // labelReferenceNumber
            // 
            labelReferenceNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelReferenceNumber.ForeColor = Color.DarkSlateGray;
            labelReferenceNumber.Location = new Point(27, 568);
            labelReferenceNumber.Name = "labelReferenceNumber";
            labelReferenceNumber.Size = new Size(120, 23);
            labelReferenceNumber.TabIndex = 12;
            labelReferenceNumber.Text = "Reference #:";
            // 
            // textBoxReferenceNumber
            // 
            textBoxReferenceNumber.BorderStyle = BorderStyle.FixedSingle;
            textBoxReferenceNumber.Font = new Font("Segoe UI", 10F);
            textBoxReferenceNumber.Location = new Point(229, 565);
            textBoxReferenceNumber.Name = "textBoxReferenceNumber";
            textBoxReferenceNumber.Size = new Size(320, 30);
            textBoxReferenceNumber.TabIndex = 7;
            // 
            // labelNotes
            // 
            labelNotes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelNotes.ForeColor = Color.DarkSlateGray;
            labelNotes.Location = new Point(27, 614);
            labelNotes.Name = "labelNotes";
            labelNotes.Size = new Size(120, 23);
            labelNotes.TabIndex = 14;
            labelNotes.Text = "Notes:";
            // 
            // textBoxNotes
            // 
            textBoxNotes.BorderStyle = BorderStyle.FixedSingle;
            textBoxNotes.Font = new Font("Segoe UI", 10F);
            textBoxNotes.Location = new Point(229, 611);
            textBoxNotes.Multiline = true;
            textBoxNotes.Name = "textBoxNotes";
            textBoxNotes.Size = new Size(320, 60);
            textBoxNotes.TabIndex = 8;
            // 
            // labelTransactionDate
            // 
            labelTransactionDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTransactionDate.ForeColor = Color.DarkSlateGray;
            labelTransactionDate.Location = new Point(27, 691);
            labelTransactionDate.Name = "labelTransactionDate";
            labelTransactionDate.Size = new Size(120, 23);
            labelTransactionDate.TabIndex = 16;
            labelTransactionDate.Text = "Date:";
            // 
            // dateTimePickerTransactionDate
            // 
            dateTimePickerTransactionDate.Font = new Font("Segoe UI", 10F);
            dateTimePickerTransactionDate.Location = new Point(229, 689);
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
            buttonSave.Location = new Point(263, 741);
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
            buttonCancel.Location = new Point(398, 741);
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
            labelTransactionType.Location = new Point(27, 380);
            labelTransactionType.Name = "labelTransactionType";
            labelTransactionType.Size = new Size(149, 23);
            labelTransactionType.TabIndex = 10;
            labelTransactionType.Text = "Transaction Type:";
            // 
            // comboBoxTransactionType
            // 
            comboBoxTransactionType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTransactionType.Font = new Font("Segoe UI", 10F);
            comboBoxTransactionType.Location = new Point(229, 377);
            comboBoxTransactionType.Name = "comboBoxTransactionType";
            comboBoxTransactionType.Size = new Size(320, 31);
            comboBoxTransactionType.TabIndex = 4;
            // 
            // labelPayingFor
            // 
            labelPayingFor.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPayingFor.ForeColor = Color.DarkSlateGray;
            labelPayingFor.Location = new Point(27, 191);
            labelPayingFor.Name = "labelPayingFor";
            labelPayingFor.Size = new Size(166, 23);
            labelPayingFor.TabIndex = 3;
            labelPayingFor.Text = "Paying For:";
            // 
            // comboBoxPayingFor
            // 
            comboBoxPayingFor.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPayingFor.Font = new Font("Segoe UI", 10F);
            comboBoxPayingFor.Items.AddRange(new object[] { "Interest", "Principle" });
            comboBoxPayingFor.Location = new Point(229, 188);
            comboBoxPayingFor.Name = "comboBoxPayingFor";
            comboBoxPayingFor.Size = new Size(320, 31);
            comboBoxPayingFor.TabIndex = 3;
            comboBoxPayingFor.SelectedIndexChanged += ComboBoxPayingFor_SelectedIndexChanged;
            // 
            // labelTotalInterest
            // 
            labelTotalInterest.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalInterest.ForeColor = Color.DarkSlateGray;
            labelTotalInterest.Location = new Point(27, 240);
            labelTotalInterest.Name = "labelTotalInterest";
            labelTotalInterest.Size = new Size(166, 23);
            labelTotalInterest.TabIndex = 17;
            labelTotalInterest.Text = "Total Interest:";
            // 
            // textBoxTotalInterest
            // 
            textBoxTotalInterest.BorderStyle = BorderStyle.FixedSingle;
            textBoxTotalInterest.Font = new Font("Segoe UI", 10F);
            textBoxTotalInterest.Location = new Point(229, 237);
            textBoxTotalInterest.Name = "textBoxTotalInterest";
            textBoxTotalInterest.ReadOnly = true;
            textBoxTotalInterest.Size = new Size(320, 30);
            textBoxTotalInterest.TabIndex = 7;
            // 
            // labelTotalInterestPaid
            // 
            labelTotalInterestPaid.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalInterestPaid.ForeColor = Color.DarkSlateGray;
            labelTotalInterestPaid.Location = new Point(27, 287);
            labelTotalInterestPaid.Name = "labelTotalInterestPaid";
            labelTotalInterestPaid.Size = new Size(196, 23);
            labelTotalInterestPaid.TabIndex = 20;
            labelTotalInterestPaid.Text = "Total Interest Paid:";
            // 
            // textBoxTotalInterestPaid
            // 
            textBoxTotalInterestPaid.BorderStyle = BorderStyle.FixedSingle;
            textBoxTotalInterestPaid.Font = new Font("Segoe UI", 10F);
            textBoxTotalInterestPaid.Location = new Point(229, 284);
            textBoxTotalInterestPaid.Name = "textBoxTotalInterestPaid";
            textBoxTotalInterestPaid.ReadOnly = true;
            textBoxTotalInterestPaid.Size = new Size(320, 30);
            textBoxTotalInterestPaid.TabIndex = 8;
            // 
            // labelTotalPrinciplePaid
            // 
            labelTotalPrinciplePaid.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalPrinciplePaid.ForeColor = Color.DarkSlateGray;
            labelTotalPrinciplePaid.Location = new Point(27, 240);
            labelTotalPrinciplePaid.Name = "labelTotalPrinciplePaid";
            labelTotalPrinciplePaid.Size = new Size(196, 23);
            labelTotalPrinciplePaid.TabIndex = 20;
            labelTotalPrinciplePaid.Text = "Total Principle Paid:";
            // 
            // textBoxTotalPrinciplePaid
            // 
            textBoxTotalPrinciplePaid.BorderStyle = BorderStyle.FixedSingle;
            textBoxTotalPrinciplePaid.Font = new Font("Segoe UI", 10F);
            textBoxTotalPrinciplePaid.Location = new Point(229, 237);
            textBoxTotalPrinciplePaid.Name = "textBoxTotalPrinciplePaid";
            textBoxTotalPrinciplePaid.ReadOnly = true;
            textBoxTotalPrinciplePaid.Size = new Size(320, 30);
            textBoxTotalPrinciplePaid.TabIndex = 8;
            // 
            // labelTotalPrinciple
            // 
            labelTotalPrinciple.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalPrinciple.ForeColor = Color.DarkSlateGray;
            labelTotalPrinciple.Location = new Point(27, 333);
            labelTotalPrinciple.Name = "labelTotalPrinciple";
            labelTotalPrinciple.Size = new Size(196, 23);
            labelTotalPrinciple.TabIndex = 4;
            labelTotalPrinciple.Text = "Total Principle:";
            // 
            // textBoxTotalPrinciple
            // 
            textBoxTotalPrinciple.BorderStyle = BorderStyle.FixedSingle;
            textBoxTotalPrinciple.Font = new Font("Segoe UI", 10F);
            textBoxTotalPrinciple.Location = new Point(229, 330);
            textBoxTotalPrinciple.Name = "textBoxTotalPrinciple";
            textBoxTotalPrinciple.ReadOnly = true;
            textBoxTotalPrinciple.Size = new Size(320, 30);
            textBoxTotalPrinciple.TabIndex = 3;
            // 
            // PropertyLoanTransactionForm
            // 
            BackColor = Color.AliceBlue;
            ClientSize = new Size(637, 839);
            Controls.Add(groupBoxTransactionEntry);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
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