namespace RealEstateManager.Pages
{
    partial class RegisterMiscTransactionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxTransactionDetails;
        private System.Windows.Forms.Label labelRecipient;
        private System.Windows.Forms.TextBox textBoxRecipient;
        private System.Windows.Forms.Label labelTransactionDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerTransactionDate;
        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.Label labelPaymentMethod;
        private System.Windows.Forms.ComboBox comboBoxPaymentMethod;
        private System.Windows.Forms.Label labelReferenceNumber;
        private System.Windows.Forms.TextBox textBoxReferenceNumber;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label labelTransactionType;
        private System.Windows.Forms.ComboBox comboBoxTransactionType;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterMiscTransactionForm));
            groupBoxTransactionDetails = new GroupBox();
            labelRecipient = new Label();
            textBoxRecipient = new TextBox();
            labelTransactionDate = new Label();
            dateTimePickerTransactionDate = new DateTimePicker();
            labelAmount = new Label();
            textBoxAmount = new TextBox();
            labelPaymentMethod = new Label();
            comboBoxPaymentMethod = new ComboBox();
            labelReferenceNumber = new Label();
            textBoxReferenceNumber = new TextBox();
            labelNotes = new Label();
            textBoxNotes = new TextBox();
            labelTransactionType = new Label();
            comboBoxTransactionType = new ComboBox();
            buttonSave = new Button();
            buttonCancel = new Button();
            groupBoxTransactionDetails.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxTransactionDetails
            // 
            groupBoxTransactionDetails.BackColor = Color.AliceBlue;
            groupBoxTransactionDetails.Controls.Add(labelRecipient);
            groupBoxTransactionDetails.Controls.Add(textBoxRecipient);
            groupBoxTransactionDetails.Controls.Add(labelTransactionDate);
            groupBoxTransactionDetails.Controls.Add(dateTimePickerTransactionDate);
            groupBoxTransactionDetails.Controls.Add(labelAmount);
            groupBoxTransactionDetails.Controls.Add(textBoxAmount);
            groupBoxTransactionDetails.Controls.Add(labelPaymentMethod);
            groupBoxTransactionDetails.Controls.Add(comboBoxPaymentMethod);
            groupBoxTransactionDetails.Controls.Add(labelReferenceNumber);
            groupBoxTransactionDetails.Controls.Add(textBoxReferenceNumber);
            groupBoxTransactionDetails.Controls.Add(labelNotes);
            groupBoxTransactionDetails.Controls.Add(textBoxNotes);
            groupBoxTransactionDetails.Controls.Add(labelTransactionType);
            groupBoxTransactionDetails.Controls.Add(comboBoxTransactionType);
            groupBoxTransactionDetails.Controls.Add(buttonSave);
            groupBoxTransactionDetails.Controls.Add(buttonCancel);
            groupBoxTransactionDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxTransactionDetails.ForeColor = Color.MidnightBlue;
            groupBoxTransactionDetails.Location = new Point(20, 23);
            groupBoxTransactionDetails.Name = "groupBoxTransactionDetails";
            groupBoxTransactionDetails.Padding = new Padding(15);
            groupBoxTransactionDetails.Size = new Size(570, 506);
            groupBoxTransactionDetails.TabIndex = 0;
            groupBoxTransactionDetails.TabStop = false;
            groupBoxTransactionDetails.Text = "Transaction Entry";
            // 
            // labelRecipient
            // 
            labelRecipient.AutoSize = true;
            labelRecipient.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelRecipient.ForeColor = Color.DarkSlateGray;
            labelRecipient.Location = new Point(33, 55);
            labelRecipient.Name = "labelRecipient";
            labelRecipient.Size = new Size(90, 23);
            labelRecipient.TabIndex = 0;
            labelRecipient.Text = "Recipient:";
            // 
            // textBoxRecipient
            // 
            textBoxRecipient.BorderStyle = BorderStyle.FixedSingle;
            textBoxRecipient.Font = new Font("Segoe UI", 10F);
            textBoxRecipient.Location = new Point(194, 48);
            textBoxRecipient.Name = "textBoxRecipient";
            textBoxRecipient.Size = new Size(320, 30);
            textBoxRecipient.TabIndex = 0;
            // 
            // labelTransactionDate
            // 
            labelTransactionDate.AutoSize = true;
            labelTransactionDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTransactionDate.ForeColor = Color.DarkSlateGray;
            labelTransactionDate.Location = new Point(33, 374);
            labelTransactionDate.Name = "labelTransactionDate";
            labelTransactionDate.Size = new Size(53, 23);
            labelTransactionDate.TabIndex = 7;
            labelTransactionDate.Text = "Date:";
            // 
            // dateTimePickerTransactionDate
            // 
            dateTimePickerTransactionDate.Font = new Font("Segoe UI", 10F);
            dateTimePickerTransactionDate.Location = new Point(194, 374);
            dateTimePickerTransactionDate.Name = "dateTimePickerTransactionDate";
            dateTimePickerTransactionDate.Size = new Size(320, 30);
            dateTimePickerTransactionDate.TabIndex = 7;
            // 
            // labelAmount
            // 
            labelAmount.AutoSize = true;
            labelAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAmount.ForeColor = Color.DarkSlateGray;
            labelAmount.Location = new Point(33, 104);
            labelAmount.Name = "labelAmount";
            labelAmount.Size = new Size(80, 23);
            labelAmount.TabIndex = 2;
            labelAmount.Text = "Amount:";
            // 
            // textBoxAmount
            // 
            textBoxAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxAmount.Font = new Font("Segoe UI", 10F);
            textBoxAmount.Location = new Point(194, 97);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new Size(320, 30);
            textBoxAmount.TabIndex = 2;
            // 
            // labelPaymentMethod
            // 
            labelPaymentMethod.AutoSize = true;
            labelPaymentMethod.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPaymentMethod.ForeColor = Color.DarkSlateGray;
            labelPaymentMethod.Location = new Point(33, 199);
            labelPaymentMethod.Name = "labelPaymentMethod";
            labelPaymentMethod.Size = new Size(153, 23);
            labelPaymentMethod.TabIndex = 4;
            labelPaymentMethod.Text = "Payment Method:";
            // 
            // comboBoxPaymentMethod
            // 
            comboBoxPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPaymentMethod.Font = new Font("Segoe UI", 10F);
            comboBoxPaymentMethod.Location = new Point(194, 195);
            comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
            comboBoxPaymentMethod.Size = new Size(320, 31);
            comboBoxPaymentMethod.TabIndex = 4;
            // 
            // labelReferenceNumber
            // 
            labelReferenceNumber.AutoSize = true;
            labelReferenceNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelReferenceNumber.ForeColor = Color.DarkSlateGray;
            labelReferenceNumber.Location = new Point(33, 248);
            labelReferenceNumber.Name = "labelReferenceNumber";
            labelReferenceNumber.Size = new Size(109, 23);
            labelReferenceNumber.TabIndex = 5;
            labelReferenceNumber.Text = "Reference #:";
            // 
            // textBoxReferenceNumber
            // 
            textBoxReferenceNumber.BorderStyle = BorderStyle.FixedSingle;
            textBoxReferenceNumber.Font = new Font("Segoe UI", 10F);
            textBoxReferenceNumber.Location = new Point(194, 245);
            textBoxReferenceNumber.Name = "textBoxReferenceNumber";
            textBoxReferenceNumber.Size = new Size(320, 30);
            textBoxReferenceNumber.TabIndex = 5;
            // 
            // labelNotes
            // 
            labelNotes.AutoSize = true;
            labelNotes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelNotes.ForeColor = Color.DarkSlateGray;
            labelNotes.Location = new Point(33, 291);
            labelNotes.Name = "labelNotes";
            labelNotes.Size = new Size(61, 23);
            labelNotes.TabIndex = 6;
            labelNotes.Text = "Notes:";
            // 
            // textBoxNotes
            // 
            textBoxNotes.BorderStyle = BorderStyle.FixedSingle;
            textBoxNotes.Font = new Font("Segoe UI", 10F);
            textBoxNotes.Location = new Point(196, 292);
            textBoxNotes.Multiline = true;
            textBoxNotes.Name = "textBoxNotes";
            textBoxNotes.Size = new Size(318, 62);
            textBoxNotes.TabIndex = 6;
            // 
            // labelTransactionType
            // 
            labelTransactionType.AutoSize = true;
            labelTransactionType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTransactionType.ForeColor = Color.DarkSlateGray;
            labelTransactionType.Location = new Point(33, 148);
            labelTransactionType.Name = "labelTransactionType";
            labelTransactionType.Size = new Size(149, 23);
            labelTransactionType.TabIndex = 3;
            labelTransactionType.Text = "Transaction Type:";
            // 
            // comboBoxTransactionType
            // 
            comboBoxTransactionType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTransactionType.Font = new Font("Segoe UI", 10F);
            comboBoxTransactionType.Location = new Point(194, 145);
            comboBoxTransactionType.Name = "comboBoxTransactionType";
            comboBoxTransactionType.Size = new Size(320, 31);
            comboBoxTransactionType.TabIndex = 3;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.Green;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonSave.ForeColor = Color.White;
            buttonSave.Location = new Point(230, 432);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(110, 35);
            buttonSave.TabIndex = 8;
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
            buttonCancel.Location = new Point(363, 432);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(110, 35);
            buttonCancel.TabIndex = 9;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // RegisterMiscTransactionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            BackColor = Color.WhiteSmoke;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(606, 545);
            Controls.Add(groupBoxTransactionDetails);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "RegisterMiscTransactionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Register Miscellaneous Transaction";
            groupBoxTransactionDetails.ResumeLayout(false);
            groupBoxTransactionDetails.PerformLayout();
            ResumeLayout(false);
        }
    }
}