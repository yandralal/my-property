namespace RealEstateManager.Pages
{
    partial class RegisterTransactionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelPlotId;
        private System.Windows.Forms.TextBox textBoxPlotId;
        private System.Windows.Forms.Label labelSaleAmount;
        private System.Windows.Forms.TextBox textBoxSaleAmount;
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
        private System.Windows.Forms.Label labelAmountPaidTillDate;
        private System.Windows.Forms.TextBox textBoxAmountPaidTillDate;

        private void InitializeComponent()
        {
            labelPlotId = new Label();
            textBoxPlotId = new TextBox();
            labelSaleAmount = new Label();
            textBoxSaleAmount = new TextBox();
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
            labelAmountPaidTillDate = new Label();
            textBoxAmountPaidTillDate = new TextBox();
            SuspendLayout();
            // 
            // labelPlotId
            // 
            labelPlotId.Location = new Point(20, 20);
            labelPlotId.Name = "labelPlotId";
            labelPlotId.Size = new Size(100, 23);
            labelPlotId.TabIndex = 0;
            labelPlotId.Text = "Plot Number:";
            // 
            // textBoxPlotId
            // 
            textBoxPlotId.Location = new Point(225, 16);
            textBoxPlotId.Name = "textBoxPlotId";
            textBoxPlotId.Size = new Size(200, 27);
            textBoxPlotId.TabIndex = 1;
            // 
            // labelSaleAmount
            // 
            labelSaleAmount.Location = new Point(20, 60);
            labelSaleAmount.Name = "labelSaleAmount";
            labelSaleAmount.Size = new Size(100, 23);
            labelSaleAmount.TabIndex = 2;
            labelSaleAmount.Text = "Sale Amount:";
            // 
            // textBoxSaleAmount
            // 
            textBoxSaleAmount.Location = new Point(225, 56);
            textBoxSaleAmount.Name = "textBoxSaleAmount";
            textBoxSaleAmount.Size = new Size(200, 27);
            textBoxSaleAmount.TabIndex = 3;
            textBoxSaleAmount.TextChanged += UpdateBalanceAmount;
            // 
            // labelAmount
            // 
            labelAmount.Location = new Point(20, 140);
            labelAmount.Name = "labelAmount";
            labelAmount.Size = new Size(100, 23);
            labelAmount.TabIndex = 4;
            labelAmount.Text = "Amount To Pay:";
            // 
            // textBoxAmount
            // 
            textBoxAmount.Location = new Point(225, 137);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new Size(200, 27);
            textBoxAmount.TabIndex = 5;
            textBoxAmount.TextChanged += UpdateBalanceAmount;
            // 
            // labelBalanceAmount
            // 
            labelBalanceAmount.Location = new Point(20, 180);
            labelBalanceAmount.Name = "labelBalanceAmount";
            labelBalanceAmount.Size = new Size(100, 23);
            labelBalanceAmount.TabIndex = 6;
            labelBalanceAmount.Text = "Balance:";
            // 
            // labelBalanceValue
            // 
            labelBalanceValue.BorderStyle = BorderStyle.Fixed3D;
            labelBalanceValue.Location = new Point(225, 176);
            labelBalanceValue.Name = "labelBalanceValue";
            labelBalanceValue.Size = new Size(200, 23);
            labelBalanceValue.TabIndex = 7;
            labelBalanceValue.Text = "0.00";
            // 
            // labelPaymentMethod
            // 
            labelPaymentMethod.Location = new Point(20, 220);
            labelPaymentMethod.Name = "labelPaymentMethod";
            labelPaymentMethod.Size = new Size(100, 23);
            labelPaymentMethod.TabIndex = 8;
            labelPaymentMethod.Text = "Payment Method:";
            // 
            // comboBoxPaymentMethod
            // 
            comboBoxPaymentMethod.Items.AddRange(new object[] { "Cash", "Cheque", "Bank Transfer", "Other" });
            comboBoxPaymentMethod.Location = new Point(225, 216);
            comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
            comboBoxPaymentMethod.Size = new Size(200, 28);
            comboBoxPaymentMethod.TabIndex = 9;
            // 
            // labelReferenceNumber
            // 
            labelReferenceNumber.Location = new Point(20, 260);
            labelReferenceNumber.Name = "labelReferenceNumber";
            labelReferenceNumber.Size = new Size(100, 23);
            labelReferenceNumber.TabIndex = 10;
            labelReferenceNumber.Text = "Reference #:";
            // 
            // textBoxReferenceNumber
            // 
            textBoxReferenceNumber.Location = new Point(225, 256);
            textBoxReferenceNumber.Name = "textBoxReferenceNumber";
            textBoxReferenceNumber.Size = new Size(200, 27);
            textBoxReferenceNumber.TabIndex = 11;
            // 
            // labelNotes
            // 
            labelNotes.Location = new Point(20, 300);
            labelNotes.Name = "labelNotes";
            labelNotes.Size = new Size(100, 23);
            labelNotes.TabIndex = 12;
            labelNotes.Text = "Notes:";
            // 
            // textBoxNotes
            // 
            textBoxNotes.Location = new Point(225, 296);
            textBoxNotes.Multiline = true;
            textBoxNotes.Name = "textBoxNotes";
            textBoxNotes.Size = new Size(200, 60);
            textBoxNotes.TabIndex = 13;
            // 
            // labelTransactionDate
            // 
            labelTransactionDate.Location = new Point(20, 370);
            labelTransactionDate.Name = "labelTransactionDate";
            labelTransactionDate.Size = new Size(100, 23);
            labelTransactionDate.TabIndex = 14;
            labelTransactionDate.Text = "Date:";
            // 
            // dateTimePickerTransactionDate
            // 
            dateTimePickerTransactionDate.Location = new Point(225, 366);
            dateTimePickerTransactionDate.Name = "dateTimePickerTransactionDate";
            dateTimePickerTransactionDate.Size = new Size(200, 27);
            dateTimePickerTransactionDate.TabIndex = 15;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.Green;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonSave.ForeColor = Color.White;
            buttonSave.Location = new Point(225, 406);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(200, 40);
            buttonSave.TabIndex = 16;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += buttonSave_Click;
            // 
            // labelAmountPaidTillDate
            // 
            labelAmountPaidTillDate.Location = new Point(20, 100);
            labelAmountPaidTillDate.Name = "labelAmountPaidTillDate";
            labelAmountPaidTillDate.Size = new Size(120, 23);
            labelAmountPaidTillDate.TabIndex = 4;
            labelAmountPaidTillDate.Text = "Amount Paid Till Date:";
            // 
            // textBoxAmountPaidTillDate
            // 
            textBoxAmountPaidTillDate.Location = new Point(225, 97);
            textBoxAmountPaidTillDate.Name = "textBoxAmountPaidTillDate";
            textBoxAmountPaidTillDate.ReadOnly = true;
            textBoxAmountPaidTillDate.Size = new Size(200, 27);
            textBoxAmountPaidTillDate.TabIndex = 5;
            // 
            // RegisterTransactionForm
            // 
            ClientSize = new Size(553, 470);
            Controls.Add(labelPlotId);
            Controls.Add(textBoxPlotId);
            Controls.Add(labelSaleAmount);
            Controls.Add(textBoxSaleAmount);
            Controls.Add(labelAmountPaidTillDate);
            Controls.Add(textBoxAmountPaidTillDate);
            Controls.Add(labelAmount);
            Controls.Add(textBoxAmount);
            Controls.Add(labelBalanceAmount);
            Controls.Add(labelBalanceValue);
            Controls.Add(labelPaymentMethod);
            Controls.Add(comboBoxPaymentMethod);
            Controls.Add(labelReferenceNumber);
            Controls.Add(textBoxReferenceNumber);
            Controls.Add(labelNotes);
            Controls.Add(textBoxNotes);
            Controls.Add(labelTransactionDate);
            Controls.Add(dateTimePickerTransactionDate);
            Controls.Add(buttonSave);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegisterTransactionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Register Plot Transaction";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}