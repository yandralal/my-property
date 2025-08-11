namespace RealEstateManager.Pages
{
    partial class RegisterPropertyTransactionForm
    {
        private System.Windows.Forms.GroupBox groupBoxTransactionEntry;
        private System.Windows.Forms.Label labelPropertyId;
        private System.Windows.Forms.TextBox textBoxPropertyId;
        private System.Windows.Forms.Label labelSaleAmount;
        private System.Windows.Forms.TextBox textBoxSaleAmount;
        private System.Windows.Forms.Label labelAmountPaidTillDate;
        private System.Windows.Forms.TextBox textBoxAmountPaidTillDate;
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
        private System.Windows.Forms.Label labelTransactionType;
        private System.Windows.Forms.ComboBox comboBoxTransactionType;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterPropertyTransactionForm));
            groupBoxTransactionEntry = new GroupBox();
            labelPropertyId = new Label();
            textBoxPropertyId = new TextBox();
            labelSaleAmount = new Label();
            textBoxSaleAmount = new TextBox();
            labelAmountPaidTillDate = new Label();
            textBoxAmountPaidTillDate = new TextBox();
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
            labelTransactionType = new Label();
            comboBoxTransactionType = new ComboBox();
            groupBoxTransactionEntry.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxTransactionEntry
            // 
            groupBoxTransactionEntry.BackColor = Color.AliceBlue;
            groupBoxTransactionEntry.Controls.Add(labelPropertyId);
            groupBoxTransactionEntry.Controls.Add(textBoxPropertyId);
            groupBoxTransactionEntry.Controls.Add(labelSaleAmount);
            groupBoxTransactionEntry.Controls.Add(textBoxSaleAmount);
            groupBoxTransactionEntry.Controls.Add(labelAmountPaidTillDate);
            groupBoxTransactionEntry.Controls.Add(textBoxAmountPaidTillDate);
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
            groupBoxTransactionEntry.Controls.Add(labelTransactionType);
            groupBoxTransactionEntry.Controls.Add(comboBoxTransactionType);
            groupBoxTransactionEntry.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxTransactionEntry.ForeColor = Color.MidnightBlue;
            groupBoxTransactionEntry.Location = new Point(17, 25);
            groupBoxTransactionEntry.Name = "groupBoxTransactionEntry";
            groupBoxTransactionEntry.Padding = new Padding(15);
            groupBoxTransactionEntry.Size = new Size(605, 610);
            groupBoxTransactionEntry.TabIndex = 0;
            groupBoxTransactionEntry.TabStop = false;
            groupBoxTransactionEntry.Text = "Transaction Entry";
            // 
            // labelPropertyId
            // 
            labelPropertyId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPropertyId.ForeColor = Color.DarkSlateGray;
            labelPropertyId.Location = new Point(27, 47);
            labelPropertyId.Name = "labelPropertyId";
            labelPropertyId.Size = new Size(120, 23);
            labelPropertyId.TabIndex = 0;
            labelPropertyId.Text = "Property Number:";
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
            // labelSaleAmount
            // 
            labelSaleAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelSaleAmount.ForeColor = Color.DarkSlateGray;
            labelSaleAmount.Location = new Point(27, 93);
            labelSaleAmount.Name = "labelSaleAmount";
            labelSaleAmount.Size = new Size(120, 23);
            labelSaleAmount.TabIndex = 2;
            labelSaleAmount.Text = "Buy Amount:";
            // 
            // textBoxSaleAmount
            // 
            textBoxSaleAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxSaleAmount.Font = new Font("Segoe UI", 10F);
            textBoxSaleAmount.Location = new Point(229, 90);
            textBoxSaleAmount.Name = "textBoxSaleAmount";
            textBoxSaleAmount.ReadOnly = true;
            textBoxSaleAmount.Size = new Size(320, 30);
            textBoxSaleAmount.TabIndex = 1;
            // 
            // labelAmountPaidTillDate
            // 
            labelAmountPaidTillDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAmountPaidTillDate.ForeColor = Color.DarkSlateGray;
            labelAmountPaidTillDate.Location = new Point(27, 141);
            labelAmountPaidTillDate.Name = "labelAmountPaidTillDate";
            labelAmountPaidTillDate.Size = new Size(196, 23);
            labelAmountPaidTillDate.TabIndex = 4;
            labelAmountPaidTillDate.Text = "Amount Paid Till Date:";
            // 
            // textBoxAmountPaidTillDate
            // 
            textBoxAmountPaidTillDate.BorderStyle = BorderStyle.FixedSingle;
            textBoxAmountPaidTillDate.Font = new Font("Segoe UI", 10F);
            textBoxAmountPaidTillDate.Location = new Point(229, 138);
            textBoxAmountPaidTillDate.Name = "textBoxAmountPaidTillDate";
            textBoxAmountPaidTillDate.ReadOnly = true;
            textBoxAmountPaidTillDate.Size = new Size(320, 30);
            textBoxAmountPaidTillDate.TabIndex = 2;
            // 
            // labelAmount
            // 
            labelAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAmount.ForeColor = Color.DarkSlateGray;
            labelAmount.Location = new Point(27, 238);
            labelAmount.Name = "labelAmount";
            labelAmount.Size = new Size(140, 23);
            labelAmount.TabIndex = 6;
            labelAmount.Text = "Amount To Pay:";
            // 
            // textBoxAmount
            // 
            textBoxAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxAmount.Font = new Font("Segoe UI", 10F);
            textBoxAmount.Location = new Point(229, 237);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new Size(320, 30);
            textBoxAmount.TabIndex = 4;
            textBoxAmount.TextChanged += UpdateBalanceAmount;
            textBoxAmount.Leave += textBoxAmount_Leave;
            // 
            // labelBalanceAmount
            // 
            labelBalanceAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelBalanceAmount.ForeColor = Color.DarkSlateGray;
            labelBalanceAmount.Location = new Point(27, 285);
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
            labelBalanceValue.Location = new Point(229, 282);
            labelBalanceValue.Name = "labelBalanceValue";
            labelBalanceValue.Size = new Size(320, 30);
            labelBalanceValue.TabIndex = 9;
            labelBalanceValue.Text = "0.00";
            // 
            // labelPaymentMethod
            // 
            labelPaymentMethod.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPaymentMethod.ForeColor = Color.DarkSlateGray;
            labelPaymentMethod.Location = new Point(27, 330);
            labelPaymentMethod.Name = "labelPaymentMethod";
            labelPaymentMethod.Size = new Size(140, 23);
            labelPaymentMethod.TabIndex = 10;
            labelPaymentMethod.Text = "Payment Method:";
            // 
            // comboBoxPaymentMethod
            // 
            comboBoxPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPaymentMethod.Font = new Font("Segoe UI", 10F);
            comboBoxPaymentMethod.Items.AddRange(new object[] { "Cash", "Cheque", "Bank Transfer", "Other" });
            comboBoxPaymentMethod.Location = new Point(229, 327);
            comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
            comboBoxPaymentMethod.Size = new Size(320, 31);
            comboBoxPaymentMethod.TabIndex = 5;
            // 
            // labelReferenceNumber
            // 
            labelReferenceNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelReferenceNumber.ForeColor = Color.DarkSlateGray;
            labelReferenceNumber.Location = new Point(27, 377);
            labelReferenceNumber.Name = "labelReferenceNumber";
            labelReferenceNumber.Size = new Size(120, 23);
            labelReferenceNumber.TabIndex = 12;
            labelReferenceNumber.Text = "Reference #:";
            // 
            // textBoxReferenceNumber
            // 
            textBoxReferenceNumber.BorderStyle = BorderStyle.FixedSingle;
            textBoxReferenceNumber.Font = new Font("Segoe UI", 10F);
            textBoxReferenceNumber.Location = new Point(229, 374);
            textBoxReferenceNumber.Name = "textBoxReferenceNumber";
            textBoxReferenceNumber.Size = new Size(320, 30);
            textBoxReferenceNumber.TabIndex = 6;
            // 
            // labelNotes
            // 
            labelNotes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelNotes.ForeColor = Color.DarkSlateGray;
            labelNotes.Location = new Point(27, 422);
            labelNotes.Name = "labelNotes";
            labelNotes.Size = new Size(120, 23);
            labelNotes.TabIndex = 14;
            labelNotes.Text = "Notes:";
            // 
            // textBoxNotes
            // 
            textBoxNotes.BorderStyle = BorderStyle.FixedSingle;
            textBoxNotes.Font = new Font("Segoe UI", 10F);
            textBoxNotes.Location = new Point(229, 419);
            textBoxNotes.Multiline = true;
            textBoxNotes.Name = "textBoxNotes";
            textBoxNotes.Size = new Size(320, 60);
            textBoxNotes.TabIndex = 7;
            // 
            // labelTransactionDate
            // 
            labelTransactionDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTransactionDate.ForeColor = Color.DarkSlateGray;
            labelTransactionDate.Location = new Point(27, 500);
            labelTransactionDate.Name = "labelTransactionDate";
            labelTransactionDate.Size = new Size(120, 23);
            labelTransactionDate.TabIndex = 16;
            labelTransactionDate.Text = "Date:";
            // 
            // dateTimePickerTransactionDate
            // 
            dateTimePickerTransactionDate.Font = new Font("Segoe UI", 10F);
            dateTimePickerTransactionDate.Location = new Point(229, 497);
            dateTimePickerTransactionDate.Name = "dateTimePickerTransactionDate";
            dateTimePickerTransactionDate.Size = new Size(300, 30);
            dateTimePickerTransactionDate.TabIndex = 8;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.Green;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonSave.ForeColor = Color.White;
            buttonSave.Location = new Point(262, 547);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(233, 40);
            buttonSave.TabIndex = 9;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += ButtonSave_Click;
            // 
            // labelTransactionType
            // 
            labelTransactionType.AutoSize = true;
            labelTransactionType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTransactionType.ForeColor = Color.DarkSlateGray;
            labelTransactionType.Location = new Point(27, 188);
            labelTransactionType.Name = "labelTransactionType";
            labelTransactionType.Size = new Size(149, 23);
            labelTransactionType.TabIndex = 10;
            labelTransactionType.Text = "Transaction Type:";
            // 
            // comboBoxTransactionType
            // 
            comboBoxTransactionType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTransactionType.Font = new Font("Segoe UI", 10F);
            comboBoxTransactionType.Location = new Point(229, 188);
            comboBoxTransactionType.Name = "comboBoxTransactionType";
            comboBoxTransactionType.Size = new Size(320, 31);
            comboBoxTransactionType.TabIndex = 3;
            // 
            // RegisterPropertyTransactionForm
            // 
            BackColor = Color.AliceBlue;
            ClientSize = new Size(637, 649);
            Controls.Add(groupBoxTransactionEntry);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "RegisterPropertyTransactionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Register Property Transaction";
            groupBoxTransactionEntry.ResumeLayout(false);
            groupBoxTransactionEntry.PerformLayout();
            ResumeLayout(false);
        }
    }
}