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
        private System.Windows.Forms.Label labelPlotStatus;
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
            labelPlotStatus = new Label();
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
            buttonRegisterSale = new Button();
            labelAgent = new Label();
            comboBoxAgent = new ComboBox();
            labelBrokerage = new Label();
            textBoxBrokerage = new TextBox();
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
            groupBoxSaleDetails.Controls.Add(labelPlotStatus);
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
            groupBoxSaleDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxSaleDetails.ForeColor = Color.MidnightBlue;
            groupBoxSaleDetails.Location = new Point(25, 20);
            groupBoxSaleDetails.Name = "groupBoxSaleDetails";
            groupBoxSaleDetails.Size = new Size(673, 557);
            groupBoxSaleDetails.TabIndex = 0;
            groupBoxSaleDetails.TabStop = false;
            groupBoxSaleDetails.Text = "Sale Details";
            // 
            // labelProperty
            // 
            labelProperty.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelProperty.ForeColor = Color.DarkSlateGray;
            labelProperty.Location = new Point(30, 48);
            labelProperty.Name = "labelProperty";
            labelProperty.Size = new Size(120, 30);
            labelProperty.TabIndex = 0;
            labelProperty.Text = "Property:";
            // 
            // comboBoxProperty
            // 
            comboBoxProperty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProperty.Font = new Font("Segoe UI", 10F);
            comboBoxProperty.Location = new Point(200, 45);
            comboBoxProperty.Name = "comboBoxProperty";
            comboBoxProperty.Size = new Size(250, 31);
            comboBoxProperty.TabIndex = 1;
            comboBoxProperty.SelectedIndexChanged += ComboBoxProperty_SelectedIndexChanged;
            // 
            // labelPlot
            // 
            labelPlot.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPlot.ForeColor = Color.DarkSlateGray;
            labelPlot.Location = new Point(30, 98);
            labelPlot.Name = "labelPlot";
            labelPlot.Size = new Size(120, 30);
            labelPlot.TabIndex = 2;
            labelPlot.Text = "Plot:";
            // 
            // comboBoxPlot
            // 
            comboBoxPlot.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPlot.Font = new Font("Segoe UI", 10F);
            comboBoxPlot.Location = new Point(200, 95);
            comboBoxPlot.Name = "comboBoxPlot";
            comboBoxPlot.Size = new Size(250, 31);
            comboBoxPlot.TabIndex = 3;
            comboBoxPlot.SelectedIndexChanged += ComboBoxPlot_SelectedIndexChanged;
            // 
            // labelPlotStatus
            // 
            labelPlotStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPlotStatus.ForeColor = Color.DarkSlateGray;
            labelPlotStatus.Location = new Point(478, 98);
            labelPlotStatus.Name = "labelPlotStatus";
            labelPlotStatus.Size = new Size(162, 30);
            labelPlotStatus.TabIndex = 4;
            labelPlotStatus.Text = "Status: -";
            // 
            // labelCustomerName
            // 
            labelCustomerName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCustomerName.ForeColor = Color.DarkSlateGray;
            labelCustomerName.Location = new Point(30, 148);
            labelCustomerName.Name = "labelCustomerName";
            labelCustomerName.Size = new Size(150, 30);
            labelCustomerName.TabIndex = 5;
            labelCustomerName.Text = "Name:";
            // 
            // textBoxCustomerName
            // 
            textBoxCustomerName.BorderStyle = BorderStyle.FixedSingle;
            textBoxCustomerName.Font = new Font("Segoe UI", 10F);
            textBoxCustomerName.Location = new Point(200, 145);
            textBoxCustomerName.Name = "textBoxCustomerName";
            textBoxCustomerName.Size = new Size(250, 30);
            textBoxCustomerName.TabIndex = 2;
            // 
            // labelCustomerPhone
            // 
            labelCustomerPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCustomerPhone.ForeColor = Color.DarkSlateGray;
            labelCustomerPhone.Location = new Point(30, 198);
            labelCustomerPhone.Name = "labelCustomerPhone";
            labelCustomerPhone.Size = new Size(150, 30);
            labelCustomerPhone.TabIndex = 7;
            labelCustomerPhone.Text = "Phone:";
            // 
            // textBoxCustomerPhone
            // 
            textBoxCustomerPhone.BorderStyle = BorderStyle.FixedSingle;
            textBoxCustomerPhone.Font = new Font("Segoe UI", 10F);
            textBoxCustomerPhone.Location = new Point(200, 195);
            textBoxCustomerPhone.Name = "textBoxCustomerPhone";
            textBoxCustomerPhone.Size = new Size(250, 30);
            textBoxCustomerPhone.TabIndex = 3;
            // 
            // labelCustomerEmail
            // 
            labelCustomerEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCustomerEmail.ForeColor = Color.DarkSlateGray;
            labelCustomerEmail.Location = new Point(30, 248);
            labelCustomerEmail.Name = "labelCustomerEmail";
            labelCustomerEmail.Size = new Size(150, 30);
            labelCustomerEmail.TabIndex = 9;
            labelCustomerEmail.Text = "Email:";
            // 
            // textBoxCustomerEmail
            // 
            textBoxCustomerEmail.BorderStyle = BorderStyle.FixedSingle;
            textBoxCustomerEmail.Font = new Font("Segoe UI", 10F);
            textBoxCustomerEmail.Location = new Point(200, 245);
            textBoxCustomerEmail.Name = "textBoxCustomerEmail";
            textBoxCustomerEmail.Size = new Size(250, 30);
            textBoxCustomerEmail.TabIndex = 4;
            // 
            // labelSaleAmount
            // 
            labelSaleAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelSaleAmount.ForeColor = Color.DarkSlateGray;
            labelSaleAmount.Location = new Point(30, 298);
            labelSaleAmount.Name = "labelSaleAmount";
            labelSaleAmount.Size = new Size(150, 30);
            labelSaleAmount.TabIndex = 11;
            labelSaleAmount.Text = "Sale Amount:";
            // 
            // textBoxSaleAmount
            // 
            textBoxSaleAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxSaleAmount.Font = new Font("Segoe UI", 10F);
            textBoxSaleAmount.Location = new Point(200, 295);
            textBoxSaleAmount.Name = "textBoxSaleAmount";
            textBoxSaleAmount.Size = new Size(250, 30);
            textBoxSaleAmount.TabIndex = 5;
            // 
            // labelSaleDate
            // 
            labelSaleDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelSaleDate.ForeColor = Color.DarkSlateGray;
            labelSaleDate.Location = new Point(30, 348);
            labelSaleDate.Name = "labelSaleDate";
            labelSaleDate.Size = new Size(150, 30);
            labelSaleDate.TabIndex = 13;
            labelSaleDate.Text = "Sale Date:";
            // 
            // dateTimePickerSaleDate
            // 
            dateTimePickerSaleDate.Font = new Font("Segoe UI", 10F);
            dateTimePickerSaleDate.Location = new Point(200, 345);
            dateTimePickerSaleDate.Name = "dateTimePickerSaleDate";
            dateTimePickerSaleDate.Size = new Size(250, 30);
            dateTimePickerSaleDate.TabIndex = 6;
            // 
            // buttonRegisterSale
            // 
            buttonRegisterSale.BackColor = Color.FromArgb(0, 123, 85);
            buttonRegisterSale.FlatStyle = FlatStyle.Flat;
            buttonRegisterSale.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonRegisterSale.ForeColor = Color.White;
            buttonRegisterSale.Location = new Point(200, 485);
            buttonRegisterSale.Name = "buttonRegisterSale";
            buttonRegisterSale.Size = new Size(250, 35);
            buttonRegisterSale.TabIndex = 9;
            buttonRegisterSale.Text = "Register Sale";
            buttonRegisterSale.UseVisualStyleBackColor = false;
            buttonRegisterSale.Click += ButtonRegisterSale_Click;
            // 
            // labelAgent
            // 
            labelAgent.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAgent.ForeColor = Color.DarkSlateGray;
            labelAgent.Location = new Point(30, 398);
            labelAgent.Name = "labelAgent";
            labelAgent.Size = new Size(150, 30);
            labelAgent.TabIndex = 16;
            labelAgent.Text = "Agent/Broker:";
            // 
            // comboBoxAgent
            // 
            comboBoxAgent.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAgent.Font = new Font("Segoe UI", 10F);
            comboBoxAgent.Location = new Point(200, 395);
            comboBoxAgent.Name = "comboBoxAgent";
            comboBoxAgent.Size = new Size(250, 31);
            comboBoxAgent.TabIndex = 7;
            // 
            // labelBrokerage
            // 
            labelBrokerage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelBrokerage.ForeColor = Color.DarkSlateGray;
            labelBrokerage.Location = new Point(30, 438);
            labelBrokerage.Name = "labelBrokerage";
            labelBrokerage.Size = new Size(150, 30);
            labelBrokerage.TabIndex = 18;
            labelBrokerage.Text = "Brokerage:";
            // 
            // textBoxBrokerage
            // 
            textBoxBrokerage.BorderStyle = BorderStyle.FixedSingle;
            textBoxBrokerage.Font = new Font("Segoe UI", 10F);
            textBoxBrokerage.Location = new Point(200, 435);
            textBoxBrokerage.Name = "textBoxBrokerage";
            textBoxBrokerage.Size = new Size(250, 30);
            textBoxBrokerage.TabIndex = 8;
            // 
            // RegisterPlotSaleForm
            // 
            BackColor = Color.FromArgb(245, 248, 255);
            ClientSize = new Size(707, 582);
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