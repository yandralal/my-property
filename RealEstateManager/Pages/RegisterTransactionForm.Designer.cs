namespace RealEstateManager.Pages
{
    partial class RegisterTransactionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxTransactionEntry;
        private System.Windows.Forms.Label labelPlotId;
        private System.Windows.Forms.TextBox textBoxPlotId;
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

        private void InitializeComponent()
        {
            groupBoxTransactionEntry = new GroupBox();
            labelPlotId = new Label();
            textBoxPlotId = new TextBox();
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
            groupBoxTransactionEntry.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxTransactionEntry
            // 
            groupBoxTransactionEntry.BackColor = Color.AliceBlue;
            groupBoxTransactionEntry.Controls.Add(labelPlotId);
            groupBoxTransactionEntry.Controls.Add(textBoxPlotId);
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
            groupBoxTransactionEntry.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxTransactionEntry.ForeColor = Color.MidnightBlue;
            groupBoxTransactionEntry.Location = new Point(12, 12);
            groupBoxTransactionEntry.Name = "groupBoxTransactionEntry";
            groupBoxTransactionEntry.Padding = new Padding(15);
            groupBoxTransactionEntry.Size = new Size(620, 548);
            groupBoxTransactionEntry.TabIndex = 0;
            groupBoxTransactionEntry.TabStop = false;
            groupBoxTransactionEntry.Text = "Transaction Entry";
            // 
            // labelPlotId
            // 
            labelPlotId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPlotId.ForeColor = Color.DarkSlateGray;
            labelPlotId.Location = new Point(20, 40);
            labelPlotId.Name = "labelPlotId";
            labelPlotId.Size = new Size(120, 23);
            labelPlotId.TabIndex = 0;
            labelPlotId.Text = "Plot Number:";
            // 
            // textBoxPlotId
            // 
            textBoxPlotId.BorderStyle = BorderStyle.FixedSingle;
            textBoxPlotId.Font = new Font("Segoe UI", 10F);
            textBoxPlotId.Location = new Point(222, 37);
            textBoxPlotId.Name = "textBoxPlotId";
            textBoxPlotId.ReadOnly = true;
            textBoxPlotId.Size = new Size(300, 30);
            textBoxPlotId.TabIndex = 1;
            // 
            // labelSaleAmount
            // 
            labelSaleAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelSaleAmount.ForeColor = Color.DarkSlateGray;
            labelSaleAmount.Location = new Point(20, 86);
            labelSaleAmount.Name = "labelSaleAmount";
            labelSaleAmount.Size = new Size(120, 23);
            labelSaleAmount.TabIndex = 2;
            labelSaleAmount.Text = "Sale Amount:";
            // 
            // textBoxSaleAmount
            // 
            textBoxSaleAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxSaleAmount.Font = new Font("Segoe UI", 10F);
            textBoxSaleAmount.Location = new Point(222, 83);
            textBoxSaleAmount.Name = "textBoxSaleAmount";
            textBoxSaleAmount.ReadOnly = true;
            textBoxSaleAmount.Size = new Size(300, 30);
            textBoxSaleAmount.TabIndex = 3;
            // 
            // labelAmountPaidTillDate
            // 
            labelAmountPaidTillDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAmountPaidTillDate.ForeColor = Color.DarkSlateGray;
            labelAmountPaidTillDate.Location = new Point(20, 134);
            labelAmountPaidTillDate.Name = "labelAmountPaidTillDate";
            labelAmountPaidTillDate.Size = new Size(196, 23);
            labelAmountPaidTillDate.TabIndex = 4;
            labelAmountPaidTillDate.Text = "Amount Paid Till Date:";
            // 
            // textBoxAmountPaidTillDate
            // 
            textBoxAmountPaidTillDate.BorderStyle = BorderStyle.FixedSingle;
            textBoxAmountPaidTillDate.Font = new Font("Segoe UI", 10F);
            textBoxAmountPaidTillDate.Location = new Point(222, 131);
            textBoxAmountPaidTillDate.Name = "textBoxAmountPaidTillDate";
            textBoxAmountPaidTillDate.ReadOnly = true;
            textBoxAmountPaidTillDate.Size = new Size(300, 30);
            textBoxAmountPaidTillDate.TabIndex = 5;
            // 
            // labelAmount
            // 
            labelAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAmount.ForeColor = Color.DarkSlateGray;
            labelAmount.Location = new Point(20, 181);
            labelAmount.Name = "labelAmount";
            labelAmount.Size = new Size(140, 23);
            labelAmount.TabIndex = 6;
            labelAmount.Text = "Amount To Pay:";
            // 
            // textBoxAmount
            // 
            textBoxAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxAmount.Font = new Font("Segoe UI", 10F);
            textBoxAmount.Location = new Point(222, 178);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new Size(300, 30);
            textBoxAmount.TabIndex = 7;
            textBoxAmount.TextChanged += UpdateBalanceAmount;
            // 
            // labelBalanceAmount
            // 
            labelBalanceAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelBalanceAmount.ForeColor = Color.DarkSlateGray;
            labelBalanceAmount.Location = new Point(20, 225);
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
            labelBalanceValue.Location = new Point(222, 222);
            labelBalanceValue.Name = "labelBalanceValue";
            labelBalanceValue.Size = new Size(300, 30);
            labelBalanceValue.TabIndex = 9;
            labelBalanceValue.Text = "0.00";
            // 
            // labelPaymentMethod
            // 
            labelPaymentMethod.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPaymentMethod.ForeColor = Color.DarkSlateGray;
            labelPaymentMethod.Location = new Point(20, 270);
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
            comboBoxPaymentMethod.Location = new Point(222, 267);
            comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
            comboBoxPaymentMethod.Size = new Size(300, 31);
            comboBoxPaymentMethod.TabIndex = 11;
            // 
            // labelReferenceNumber
            // 
            labelReferenceNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelReferenceNumber.ForeColor = Color.DarkSlateGray;
            labelReferenceNumber.Location = new Point(20, 317);
            labelReferenceNumber.Name = "labelReferenceNumber";
            labelReferenceNumber.Size = new Size(120, 23);
            labelReferenceNumber.TabIndex = 12;
            labelReferenceNumber.Text = "Reference #:";
            // 
            // textBoxReferenceNumber
            // 
            textBoxReferenceNumber.BorderStyle = BorderStyle.FixedSingle;
            textBoxReferenceNumber.Font = new Font("Segoe UI", 10F);
            textBoxReferenceNumber.Location = new Point(222, 314);
            textBoxReferenceNumber.Name = "textBoxReferenceNumber";
            textBoxReferenceNumber.Size = new Size(300, 30);
            textBoxReferenceNumber.TabIndex = 13;
            // 
            // labelNotes
            // 
            labelNotes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelNotes.ForeColor = Color.DarkSlateGray;
            labelNotes.Location = new Point(20, 362);
            labelNotes.Name = "labelNotes";
            labelNotes.Size = new Size(120, 23);
            labelNotes.TabIndex = 14;
            labelNotes.Text = "Notes:";
            // 
            // textBoxNotes
            // 
            textBoxNotes.BorderStyle = BorderStyle.FixedSingle;
            textBoxNotes.Font = new Font("Segoe UI", 10F);
            textBoxNotes.Location = new Point(222, 359);
            textBoxNotes.Multiline = true;
            textBoxNotes.Name = "textBoxNotes";
            textBoxNotes.Size = new Size(300, 60);
            textBoxNotes.TabIndex = 15;
            // 
            // labelTransactionDate
            // 
            labelTransactionDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTransactionDate.ForeColor = Color.DarkSlateGray;
            labelTransactionDate.Location = new Point(20, 440);
            labelTransactionDate.Name = "labelTransactionDate";
            labelTransactionDate.Size = new Size(120, 23);
            labelTransactionDate.TabIndex = 16;
            labelTransactionDate.Text = "Date:";
            // 
            // dateTimePickerTransactionDate
            // 
            dateTimePickerTransactionDate.Font = new Font("Segoe UI", 10F);
            dateTimePickerTransactionDate.Location = new Point(222, 437);
            dateTimePickerTransactionDate.Name = "dateTimePickerTransactionDate";
            dateTimePickerTransactionDate.Size = new Size(300, 30);
            dateTimePickerTransactionDate.TabIndex = 17;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.Green;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonSave.ForeColor = Color.White;
            buttonSave.Location = new Point(222, 486);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(300, 40);
            buttonSave.TabIndex = 16;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += buttonSave_Click;
            // 
            // RegisterTransactionForm
            // 
            BackColor = Color.AliceBlue;
            ClientSize = new Size(644, 569);
            Controls.Add(groupBoxTransactionEntry);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegisterTransactionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Register Plot Transaction";
            groupBoxTransactionEntry.ResumeLayout(false);
            groupBoxTransactionEntry.PerformLayout();
            ResumeLayout(false);
        }
    }
}