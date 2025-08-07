namespace RealEstateManager.Pages
{
    partial class RegisterAgentTransactionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelAgentId;
        private System.Windows.Forms.ComboBox comboBoxAgent;
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
        private System.Windows.Forms.Label labelPlotId;
        private System.Windows.Forms.ComboBox comboBoxPlotNumber;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBoxTransactionDetails;
        private System.Windows.Forms.Label labelTotalBrokerage;
        private System.Windows.Forms.TextBox textBoxTotalBrokerage;
        private System.Windows.Forms.Label labelAmountPaidTillDate;
        private System.Windows.Forms.TextBox textBoxAmountPaidTillDate;
        private System.Windows.Forms.Label labelBalance;
        private System.Windows.Forms.Label labelBalanceValue;
        private System.Windows.Forms.Label labelProperty;
        private System.Windows.Forms.ComboBox comboBoxProperty;

        protected override void Dispose(bool disposing)         
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterAgentTransactionForm));
            groupBoxTransactionDetails = new GroupBox();
            labelAgentId = new Label();
            buttonSave = new Button();
            comboBoxAgent = new ComboBox();
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
            labelPlotId = new Label();
            comboBoxPlotNumber = new ComboBox();
            labelTotalBrokerage = new Label();
            textBoxTotalBrokerage = new TextBox();
            labelAmountPaidTillDate = new Label();
            textBoxAmountPaidTillDate = new TextBox();
            labelBalance = new Label();
            labelBalanceValue = new Label();
            labelProperty = new Label();
            comboBoxProperty = new ComboBox();
            groupBoxTransactionDetails.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxTransactionDetails
            // 
            groupBoxTransactionDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxTransactionDetails.BackColor = Color.AliceBlue;
            groupBoxTransactionDetails.Controls.Add(labelAgentId);
            groupBoxTransactionDetails.Controls.Add(buttonSave);
            groupBoxTransactionDetails.Controls.Add(comboBoxAgent);
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
            groupBoxTransactionDetails.Controls.Add(labelPlotId);
            groupBoxTransactionDetails.Controls.Add(comboBoxPlotNumber);
            groupBoxTransactionDetails.Controls.Add(labelTotalBrokerage);
            groupBoxTransactionDetails.Controls.Add(textBoxTotalBrokerage);
            groupBoxTransactionDetails.Controls.Add(labelAmountPaidTillDate);
            groupBoxTransactionDetails.Controls.Add(textBoxAmountPaidTillDate);
            groupBoxTransactionDetails.Controls.Add(labelBalance);
            groupBoxTransactionDetails.Controls.Add(labelBalanceValue);
            groupBoxTransactionDetails.Controls.Add(labelProperty);
            groupBoxTransactionDetails.Controls.Add(comboBoxProperty);
            groupBoxTransactionDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxTransactionDetails.ForeColor = Color.MidnightBlue;
            groupBoxTransactionDetails.Location = new Point(24, 24);
            groupBoxTransactionDetails.Name = "groupBoxTransactionDetails";
            groupBoxTransactionDetails.Padding = new Padding(15);
            groupBoxTransactionDetails.Size = new Size(585, 681);
            groupBoxTransactionDetails.TabIndex = 0;
            groupBoxTransactionDetails.TabStop = false;
            groupBoxTransactionDetails.Text = "Transaction Entry";
            // 
            // labelAgentId
            // 
            labelAgentId.AutoSize = true;
            labelAgentId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAgentId.ForeColor = Color.DarkSlateGray;
            labelAgentId.Location = new Point(38, 95);
            labelAgentId.Name = "labelAgentId";
            labelAgentId.Size = new Size(64, 23);
            labelAgentId.TabIndex = 0;
            labelAgentId.Text = "Agent:";
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.Green;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonSave.ForeColor = Color.White;
            buttonSave.Location = new Point(240, 617);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(300, 40);
            buttonSave.TabIndex = 11;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += ButtonSave_Click;
            // 
            // comboBoxAgent
            // 
            comboBoxAgent.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAgent.Font = new Font("Segoe UI", 10F);
            comboBoxAgent.Location = new Point(240, 92);
            comboBoxAgent.Name = "comboBoxAgent";
            comboBoxAgent.Size = new Size(300, 31);
            comboBoxAgent.TabIndex = 1;
            // 
            // labelTransactionDate
            // 
            labelTransactionDate.AutoSize = true;
            labelTransactionDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTransactionDate.ForeColor = Color.DarkSlateGray;
            labelTransactionDate.Location = new Point(40, 558);
            labelTransactionDate.Name = "labelTransactionDate";
            labelTransactionDate.Size = new Size(53, 23);
            labelTransactionDate.TabIndex = 12;
            labelTransactionDate.Text = "Date:";
            // 
            // dateTimePickerTransactionDate
            // 
            dateTimePickerTransactionDate.Font = new Font("Segoe UI", 10F);
            dateTimePickerTransactionDate.Location = new Point(240, 555);
            dateTimePickerTransactionDate.Name = "dateTimePickerTransactionDate";
            dateTimePickerTransactionDate.Size = new Size(300, 30);
            dateTimePickerTransactionDate.TabIndex = 10;
            // 
            // labelAmount
            // 
            labelAmount.AutoSize = true;
            labelAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAmount.ForeColor = Color.DarkSlateGray;
            labelAmount.Location = new Point(40, 333);
            labelAmount.Name = "labelAmount";
            labelAmount.Size = new Size(136, 23);
            labelAmount.TabIndex = 13;
            labelAmount.Text = "Amount To Pay:";
            // 
            // textBoxAmount
            // 
            textBoxAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxAmount.Font = new Font("Segoe UI", 10F);
            textBoxAmount.Location = new Point(240, 330);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new Size(300, 30);
            textBoxAmount.TabIndex = 6;
            // 
            // labelPaymentMethod
            // 
            labelPaymentMethod.AutoSize = true;
            labelPaymentMethod.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPaymentMethod.ForeColor = Color.DarkSlateGray;
            labelPaymentMethod.Location = new Point(40, 425);
            labelPaymentMethod.Name = "labelPaymentMethod";
            labelPaymentMethod.Size = new Size(153, 23);
            labelPaymentMethod.TabIndex = 14;
            labelPaymentMethod.Text = "Payment Method:";
            // 
            // comboBoxPaymentMethod
            // 
            comboBoxPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPaymentMethod.Font = new Font("Segoe UI", 10F);
            comboBoxPaymentMethod.Location = new Point(240, 422);
            comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
            comboBoxPaymentMethod.Size = new Size(300, 31);
            comboBoxPaymentMethod.TabIndex = 7;
            // 
            // labelReferenceNumber
            // 
            labelReferenceNumber.AutoSize = true;
            labelReferenceNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelReferenceNumber.ForeColor = Color.DarkSlateGray;
            labelReferenceNumber.Location = new Point(40, 471);
            labelReferenceNumber.Name = "labelReferenceNumber";
            labelReferenceNumber.Size = new Size(109, 23);
            labelReferenceNumber.TabIndex = 15;
            labelReferenceNumber.Text = "Reference #:";
            // 
            // textBoxReferenceNumber
            // 
            textBoxReferenceNumber.BorderStyle = BorderStyle.FixedSingle;
            textBoxReferenceNumber.Font = new Font("Segoe UI", 10F);
            textBoxReferenceNumber.Location = new Point(240, 468);
            textBoxReferenceNumber.Name = "textBoxReferenceNumber";
            textBoxReferenceNumber.Size = new Size(300, 30);
            textBoxReferenceNumber.TabIndex = 8;
            // 
            // labelNotes
            // 
            labelNotes.AutoSize = true;
            labelNotes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelNotes.ForeColor = Color.DarkSlateGray;
            labelNotes.Location = new Point(40, 517);
            labelNotes.Name = "labelNotes";
            labelNotes.Size = new Size(61, 23);
            labelNotes.TabIndex = 16;
            labelNotes.Text = "Notes:";
            // 
            // textBoxNotes
            // 
            textBoxNotes.BorderStyle = BorderStyle.FixedSingle;
            textBoxNotes.Font = new Font("Segoe UI", 10F);
            textBoxNotes.Location = new Point(240, 514);
            textBoxNotes.Multiline = true;
            textBoxNotes.Name = "textBoxNotes";
            textBoxNotes.Size = new Size(300, 62);
            textBoxNotes.TabIndex = 9;
            // 
            // labelTransactionType
            // 
            labelTransactionType.AutoSize = true;
            labelTransactionType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTransactionType.ForeColor = Color.DarkSlateGray;
            labelTransactionType.Location = new Point(40, 285);
            labelTransactionType.Name = "labelTransactionType";
            labelTransactionType.Size = new Size(149, 23);
            labelTransactionType.TabIndex = 17;
            labelTransactionType.Text = "Transaction Type:";
            // 
            // comboBoxTransactionType
            // 
            comboBoxTransactionType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTransactionType.Font = new Font("Segoe UI", 10F);
            comboBoxTransactionType.Location = new Point(240, 282);
            comboBoxTransactionType.Name = "comboBoxTransactionType";
            comboBoxTransactionType.Size = new Size(300, 31);
            comboBoxTransactionType.TabIndex = 5;
            // 
            // labelPlotId
            // 
            labelPlotId.AutoSize = true;
            labelPlotId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPlotId.ForeColor = Color.DarkSlateGray;
            labelPlotId.Location = new Point(40, 141);
            labelPlotId.Name = "labelPlotId";
            labelPlotId.Size = new Size(119, 23);
            labelPlotId.TabIndex = 18;
            labelPlotId.Text = "Plot Number:";
            // 
            // comboBoxPlotNumber
            // 
            comboBoxPlotNumber.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPlotNumber.Font = new Font("Segoe UI", 10F);
            comboBoxPlotNumber.Location = new Point(240, 138);
            comboBoxPlotNumber.Name = "comboBoxPlotNumber";
            comboBoxPlotNumber.Size = new Size(300, 31);
            comboBoxPlotNumber.TabIndex = 2;
            // 
            // labelTotalBrokerage
            // 
            labelTotalBrokerage.AutoSize = true;
            labelTotalBrokerage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalBrokerage.ForeColor = Color.DarkSlateGray;
            labelTotalBrokerage.Location = new Point(40, 190);
            labelTotalBrokerage.Name = "labelTotalBrokerage";
            labelTotalBrokerage.Size = new Size(142, 23);
            labelTotalBrokerage.TabIndex = 19;
            labelTotalBrokerage.Text = "Total Brokerage:";
            // 
            // textBoxTotalBrokerage
            // 
            textBoxTotalBrokerage.BorderStyle = BorderStyle.FixedSingle;
            textBoxTotalBrokerage.Font = new Font("Segoe UI", 10F);
            textBoxTotalBrokerage.Location = new Point(240, 187);
            textBoxTotalBrokerage.Name = "textBoxTotalBrokerage";
            textBoxTotalBrokerage.ReadOnly = true;
            textBoxTotalBrokerage.Size = new Size(300, 30);
            textBoxTotalBrokerage.TabIndex = 3;
            // 
            // labelAmountPaidTillDate
            // 
            labelAmountPaidTillDate.AutoSize = true;
            labelAmountPaidTillDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAmountPaidTillDate.ForeColor = Color.DarkSlateGray;
            labelAmountPaidTillDate.Location = new Point(40, 237);
            labelAmountPaidTillDate.Name = "labelAmountPaidTillDate";
            labelAmountPaidTillDate.Size = new Size(193, 23);
            labelAmountPaidTillDate.TabIndex = 20;
            labelAmountPaidTillDate.Text = "Amount Paid Till Date:";
            // 
            // textBoxAmountPaidTillDate
            // 
            textBoxAmountPaidTillDate.BorderStyle = BorderStyle.FixedSingle;
            textBoxAmountPaidTillDate.Font = new Font("Segoe UI", 10F);
            textBoxAmountPaidTillDate.Location = new Point(240, 234);
            textBoxAmountPaidTillDate.Name = "textBoxAmountPaidTillDate";
            textBoxAmountPaidTillDate.ReadOnly = true;
            textBoxAmountPaidTillDate.Size = new Size(300, 30);
            textBoxAmountPaidTillDate.TabIndex = 4;
            // 
            // labelBalance
            // 
            labelBalance.AutoSize = true;
            labelBalance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelBalance.ForeColor = Color.DarkSlateGray;
            labelBalance.Location = new Point(40, 380);
            labelBalance.Name = "labelBalance";
            labelBalance.Size = new Size(76, 23);
            labelBalance.TabIndex = 21;
            labelBalance.Text = "Balance:";
            // 
            // labelBalanceValue
            // 
            labelBalanceValue.BorderStyle = BorderStyle.FixedSingle;
            labelBalanceValue.Font = new Font("Segoe UI", 10F);
            labelBalanceValue.ForeColor = Color.Black;
            labelBalanceValue.Location = new Point(240, 377);
            labelBalanceValue.Name = "labelBalanceValue";
            labelBalanceValue.Size = new Size(300, 30);
            labelBalanceValue.TabIndex = 22;
            labelBalanceValue.Text = "0.00";
            labelBalanceValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelProperty
            // 
            labelProperty.AutoSize = true;
            labelProperty.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelProperty.ForeColor = Color.DarkSlateGray;
            labelProperty.Location = new Point(38, 49);
            labelProperty.Name = "labelProperty";
            labelProperty.Size = new Size(85, 23);
            labelProperty.TabIndex = 23;
            labelProperty.Text = "Property:";
            // 
            // comboBoxProperty
            // 
            comboBoxProperty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProperty.Font = new Font("Segoe UI", 10F);
            comboBoxProperty.Location = new Point(240, 46);
            comboBoxProperty.Name = "comboBoxProperty";
            comboBoxProperty.Size = new Size(300, 31);
            comboBoxProperty.TabIndex = 0;
            // 
            // RegisterAgentTransactionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            BackColor = Color.WhiteSmoke;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(633, 729);
            Controls.Add(groupBoxTransactionDetails);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "RegisterAgentTransactionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Register Agent Transaction";
            groupBoxTransactionDetails.ResumeLayout(false);
            groupBoxTransactionDetails.PerformLayout();
            ResumeLayout(false);
        }
    }
}