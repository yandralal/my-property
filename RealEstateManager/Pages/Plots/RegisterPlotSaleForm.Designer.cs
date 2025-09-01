namespace RealEstateManager.Pages
{
    partial class RegisterPlotSaleForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxSaleDetails;
        private System.Windows.Forms.Label labelProperty;
        private System.Windows.Forms.ComboBox comboBoxProperty;
        private System.Windows.Forms.Label labelPlot;
        private System.Windows.Forms.ComboBox comboBoxPlot;
        private System.Windows.Forms.Label labelPlotStatusTitle;
        private System.Windows.Forms.Label labelPlotStatusValue;
        private System.Windows.Forms.Label labelCustomerName;
        private System.Windows.Forms.TextBox textBoxCustomerName;
        private System.Windows.Forms.Label labelCustomerPhone;
        private System.Windows.Forms.TextBox textBoxCustomerPhone;
        private System.Windows.Forms.Label labelCustomerEmail;
        private System.Windows.Forms.TextBox textBoxCustomerEmail;
        private System.Windows.Forms.Label labelSaleAmount;
        private System.Windows.Forms.TextBox textBoxSaleAmount;
        private System.Windows.Forms.Label labelSaleDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerSaleDate;
        private System.Windows.Forms.Button buttonRegisterSale;
        private System.Windows.Forms.Label labelAgent;
        private System.Windows.Forms.ComboBox comboBoxAgent;
        private System.Windows.Forms.Label labelBrokerage;
        private System.Windows.Forms.TextBox textBoxBrokerage;
        private System.Windows.Forms.Button buttonCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterPlotSaleForm));
            groupBoxSaleDetails = new GroupBox();
            labelProperty = new Label();
            comboBoxProperty = new ComboBox();
            labelPlot = new Label();
            comboBoxPlot = new ComboBox();
            labelPlotStatusTitle = new Label();
            labelPlotStatusValue = new Label();
            labelCustomerName = new Label();
            textBoxCustomerName = new TextBox();
            labelCustomerPhone = new Label();
            textBoxCustomerPhone = new TextBox();
            labelCustomerEmail = new Label();
            textBoxCustomerEmail = new TextBox();
            labelSaleAmount = new Label();
            textBoxSaleAmount = new TextBox();
            labelSaleDate = new Label();
            dateTimePickerSaleDate = new DateTimePicker();
            labelAgent = new Label();
            comboBoxAgent = new ComboBox();
            labelBrokerage = new Label();
            textBoxBrokerage = new TextBox();
            buttonRegisterSale = new Button();
            buttonCancel = new Button();
            groupBoxSaleDetails.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxSaleDetails
            // 
            groupBoxSaleDetails.BackColor = Color.AliceBlue;
            groupBoxSaleDetails.Controls.Add(labelProperty);
            groupBoxSaleDetails.Controls.Add(comboBoxProperty);
            groupBoxSaleDetails.Controls.Add(labelPlot);
            groupBoxSaleDetails.Controls.Add(comboBoxPlot);
            groupBoxSaleDetails.Controls.Add(labelPlotStatusTitle);
            groupBoxSaleDetails.Controls.Add(labelPlotStatusValue);
            groupBoxSaleDetails.Controls.Add(labelCustomerName);
            groupBoxSaleDetails.Controls.Add(textBoxCustomerName);
            groupBoxSaleDetails.Controls.Add(labelCustomerPhone);
            groupBoxSaleDetails.Controls.Add(textBoxCustomerPhone);
            groupBoxSaleDetails.Controls.Add(labelCustomerEmail);
            groupBoxSaleDetails.Controls.Add(textBoxCustomerEmail);
            groupBoxSaleDetails.Controls.Add(labelSaleAmount);
            groupBoxSaleDetails.Controls.Add(textBoxSaleAmount);
            groupBoxSaleDetails.Controls.Add(labelSaleDate);
            groupBoxSaleDetails.Controls.Add(dateTimePickerSaleDate);
            groupBoxSaleDetails.Controls.Add(labelAgent);
            groupBoxSaleDetails.Controls.Add(comboBoxAgent);
            groupBoxSaleDetails.Controls.Add(labelBrokerage);
            groupBoxSaleDetails.Controls.Add(textBoxBrokerage);
            groupBoxSaleDetails.Controls.Add(buttonRegisterSale);
            groupBoxSaleDetails.Controls.Add(buttonCancel);
            groupBoxSaleDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxSaleDetails.ForeColor = Color.MidnightBlue;
            groupBoxSaleDetails.Location = new Point(19, 23);
            groupBoxSaleDetails.Name = "groupBoxSaleDetails";
            groupBoxSaleDetails.Size = new Size(563, 599);
            groupBoxSaleDetails.TabIndex = 0;
            groupBoxSaleDetails.TabStop = false;
            groupBoxSaleDetails.Text = "Sale Details";
            // 
            // labelProperty
            // 
            labelProperty.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelProperty.ForeColor = Color.DarkSlateGray;
            labelProperty.Location = new Point(44, 48);
            labelProperty.Name = "labelProperty";
            labelProperty.Size = new Size(134, 30);
            labelProperty.TabIndex = 0;
            labelProperty.Text = "Property:";
            // 
            // comboBoxProperty
            // 
            comboBoxProperty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProperty.Font = new Font("Segoe UI", 10F);
            comboBoxProperty.Location = new Point(186, 44);
            comboBoxProperty.Name = "comboBoxProperty";
            comboBoxProperty.Size = new Size(320, 31);
            comboBoxProperty.TabIndex = 1;
            comboBoxProperty.SelectedIndexChanged += ComboBoxProperty_SelectedIndexChanged;
            // 
            // labelPlot
            // 
            labelPlot.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPlot.ForeColor = Color.DarkSlateGray;
            labelPlot.Location = new Point(44, 98);
            labelPlot.Name = "labelPlot";
            labelPlot.Size = new Size(116, 30);
            labelPlot.TabIndex = 2;
            labelPlot.Text = "Plot:";
            // 
            // comboBoxPlot
            // 
            comboBoxPlot.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPlot.Font = new Font("Segoe UI", 10F);
            comboBoxPlot.Location = new Point(186, 94);
            comboBoxPlot.Name = "comboBoxPlot";
            comboBoxPlot.Size = new Size(320, 31);
            comboBoxPlot.TabIndex = 3;
            comboBoxPlot.SelectedIndexChanged += ComboBoxPlot_SelectedIndexChanged;
            // 
            // labelPlotStatusTitle
            // 
            labelPlotStatusTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPlotStatusTitle.ForeColor = Color.DarkSlateGray;
            labelPlotStatusTitle.Location = new Point(44, 142);
            labelPlotStatusTitle.Name = "labelPlotStatusTitle";
            labelPlotStatusTitle.Size = new Size(84, 30);
            labelPlotStatusTitle.TabIndex = 4;
            labelPlotStatusTitle.Text = "Status:";
            // 
            // labelPlotStatusValue
            // 
            labelPlotStatusValue.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPlotStatusValue.ForeColor = Color.Black;
            labelPlotStatusValue.Location = new Point(186, 142);
            labelPlotStatusValue.Name = "labelPlotStatusValue";
            labelPlotStatusValue.Size = new Size(320, 30);
            labelPlotStatusValue.TabIndex = 5;
            // 
            // labelCustomerName
            // 
            labelCustomerName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCustomerName.ForeColor = Color.DarkSlateGray;
            labelCustomerName.Location = new Point(44, 192);
            labelCustomerName.Name = "labelCustomerName";
            labelCustomerName.Size = new Size(134, 30);
            labelCustomerName.TabIndex = 5;
            labelCustomerName.Text = "Name:";
            // 
            // textBoxCustomerName
            // 
            textBoxCustomerName.BorderStyle = BorderStyle.FixedSingle;
            textBoxCustomerName.Font = new Font("Segoe UI", 10F);
            textBoxCustomerName.Location = new Point(186, 188);
            textBoxCustomerName.Name = "textBoxCustomerName";
            textBoxCustomerName.Size = new Size(320, 30);
            textBoxCustomerName.TabIndex = 2;
            // 
            // labelCustomerPhone
            // 
            labelCustomerPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCustomerPhone.ForeColor = Color.DarkSlateGray;
            labelCustomerPhone.Location = new Point(44, 242);
            labelCustomerPhone.Name = "labelCustomerPhone";
            labelCustomerPhone.Size = new Size(134, 30);
            labelCustomerPhone.TabIndex = 7;
            labelCustomerPhone.Text = "Phone:";
            // 
            // textBoxCustomerPhone
            // 
            textBoxCustomerPhone.BorderStyle = BorderStyle.FixedSingle;
            textBoxCustomerPhone.Font = new Font("Segoe UI", 10F);
            textBoxCustomerPhone.Location = new Point(186, 238);
            textBoxCustomerPhone.Name = "textBoxCustomerPhone";
            textBoxCustomerPhone.Size = new Size(320, 30);
            textBoxCustomerPhone.TabIndex = 3;
            // 
            // labelCustomerEmail
            // 
            labelCustomerEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCustomerEmail.ForeColor = Color.DarkSlateGray;
            labelCustomerEmail.Location = new Point(44, 292);
            labelCustomerEmail.Name = "labelCustomerEmail";
            labelCustomerEmail.Size = new Size(134, 30);
            labelCustomerEmail.TabIndex = 9;
            labelCustomerEmail.Text = "Email:";
            // 
            // textBoxCustomerEmail
            // 
            textBoxCustomerEmail.BorderStyle = BorderStyle.FixedSingle;
            textBoxCustomerEmail.Font = new Font("Segoe UI", 10F);
            textBoxCustomerEmail.Location = new Point(186, 288);
            textBoxCustomerEmail.Name = "textBoxCustomerEmail";
            textBoxCustomerEmail.Size = new Size(320, 30);
            textBoxCustomerEmail.TabIndex = 4;
            // 
            // labelSaleAmount
            // 
            labelSaleAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelSaleAmount.ForeColor = Color.DarkSlateGray;
            labelSaleAmount.Location = new Point(44, 342);
            labelSaleAmount.Name = "labelSaleAmount";
            labelSaleAmount.Size = new Size(134, 30);
            labelSaleAmount.TabIndex = 11;
            labelSaleAmount.Text = "Sale Amount:";
            // 
            // textBoxSaleAmount
            // 
            textBoxSaleAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxSaleAmount.Font = new Font("Segoe UI", 10F);
            textBoxSaleAmount.Location = new Point(186, 338);
            textBoxSaleAmount.Name = "textBoxSaleAmount";
            textBoxSaleAmount.Size = new Size(320, 30);
            textBoxSaleAmount.TabIndex = 5;
            // 
            // labelSaleDate
            // 
            labelSaleDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelSaleDate.ForeColor = Color.DarkSlateGray;
            labelSaleDate.Location = new Point(44, 392);
            labelSaleDate.Name = "labelSaleDate";
            labelSaleDate.Size = new Size(134, 30);
            labelSaleDate.TabIndex = 13;
            labelSaleDate.Text = "Sale Date:";
            // 
            // dateTimePickerSaleDate
            // 
            dateTimePickerSaleDate.Font = new Font("Segoe UI", 10F);
            dateTimePickerSaleDate.Location = new Point(186, 388);
            dateTimePickerSaleDate.Name = "dateTimePickerSaleDate";
            dateTimePickerSaleDate.Size = new Size(320, 30);
            dateTimePickerSaleDate.TabIndex = 6;
            // 
            // labelAgent
            // 
            labelAgent.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAgent.ForeColor = Color.DarkSlateGray;
            labelAgent.Location = new Point(44, 442);
            labelAgent.Name = "labelAgent";
            labelAgent.Size = new Size(134, 30);
            labelAgent.TabIndex = 16;
            labelAgent.Text = "Agent/Broker:";
            // 
            // comboBoxAgent
            // 
            comboBoxAgent.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAgent.Font = new Font("Segoe UI", 10F);
            comboBoxAgent.Location = new Point(186, 438);
            comboBoxAgent.Name = "comboBoxAgent";
            comboBoxAgent.Size = new Size(320, 31);
            comboBoxAgent.TabIndex = 7;
            // 
            // labelBrokerage
            // 
            labelBrokerage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelBrokerage.ForeColor = Color.DarkSlateGray;
            labelBrokerage.Location = new Point(44, 490);
            labelBrokerage.Name = "labelBrokerage";
            labelBrokerage.Size = new Size(134, 30);
            labelBrokerage.TabIndex = 18;
            labelBrokerage.Text = "Brokerage:";
            // 
            // textBoxBrokerage
            // 
            textBoxBrokerage.BorderStyle = BorderStyle.FixedSingle;
            textBoxBrokerage.Font = new Font("Segoe UI", 10F);
            textBoxBrokerage.Location = new Point(186, 486);
            textBoxBrokerage.Name = "textBoxBrokerage";
            textBoxBrokerage.Size = new Size(320, 30);
            textBoxBrokerage.TabIndex = 8;
            // 
            // buttonRegisterSale
            // 
            buttonRegisterSale.BackColor = Color.Green;
            buttonRegisterSale.FlatStyle = FlatStyle.Flat;
            buttonRegisterSale.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonRegisterSale.ForeColor = Color.White;
            buttonRegisterSale.Location = new Point(230, 532);
            buttonRegisterSale.Name = "buttonRegisterSale";
            buttonRegisterSale.Size = new Size(110, 35);
            buttonRegisterSale.TabIndex = 9;
            buttonRegisterSale.Text = "Save";
            buttonRegisterSale.UseVisualStyleBackColor = false;
            buttonRegisterSale.Click += ButtonRegisterSale_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.Gray;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.White;
            buttonCancel.Location = new Point(362, 532);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(110, 35);
            buttonCancel.TabIndex = 10;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // RegisterPlotSaleForm
            // 
            BackColor = Color.FromArgb(245, 248, 255);
            ClientSize = new Size(599, 638);
            Controls.Add(groupBoxSaleDetails);
            Font = new Font("Segoe UI", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "RegisterPlotSaleForm";
            Text = "Register Sale";
            groupBoxSaleDetails.ResumeLayout(false);
            groupBoxSaleDetails.PerformLayout();
            ResumeLayout(false);
        }
    }
}