namespace RealEstateManager.Pages
{
    partial class RegisterSaleForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelProperty;
        private System.Windows.Forms.ComboBox comboBoxProperty;
        private System.Windows.Forms.Label labelPlot;
        private System.Windows.Forms.ComboBox comboBoxPlot;
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
        private System.Windows.Forms.Label labelPlotStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelProperty = new Label();
            comboBoxProperty = new ComboBox();
            labelPlot = new Label();
            comboBoxPlot = new ComboBox();
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
            labelPlotStatus = new Label();
            SuspendLayout();
            // 
            // labelProperty
            // 
            labelProperty.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelProperty.Location = new Point(30, 30);
            labelProperty.Name = "labelProperty";
            labelProperty.Size = new Size(120, 30);
            labelProperty.TabIndex = 0;
            labelProperty.Text = "Property:";
            // 
            // comboBoxProperty
            // 
            comboBoxProperty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProperty.Font = new Font("Segoe UI", 12F);
            comboBoxProperty.Location = new Point(223, 27);
            comboBoxProperty.Name = "comboBoxProperty";
            comboBoxProperty.Size = new Size(250, 36);
            comboBoxProperty.TabIndex = 1;
            comboBoxProperty.SelectedIndexChanged += ComboBoxProperty_SelectedIndexChanged;
            // 
            // labelPlot
            // 
            labelPlot.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelPlot.Location = new Point(30, 86);
            labelPlot.Name = "labelPlot";
            labelPlot.Size = new Size(120, 30);
            labelPlot.TabIndex = 2;
            labelPlot.Text = "Plot:";
            // 
            // comboBoxPlot
            // 
            comboBoxPlot.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPlot.Font = new Font("Segoe UI", 12F);
            comboBoxPlot.Location = new Point(223, 83);
            comboBoxPlot.Name = "comboBoxPlot";
            comboBoxPlot.Size = new Size(250, 36);
            comboBoxPlot.TabIndex = 3;
            comboBoxPlot.SelectedIndexChanged += comboBoxPlot_SelectedIndexChanged;
            // 
            // labelCustomerName
            // 
            labelCustomerName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelCustomerName.Location = new Point(30, 143);
            labelCustomerName.Name = "labelCustomerName";
            labelCustomerName.Size = new Size(178, 30);
            labelCustomerName.TabIndex = 4;
            labelCustomerName.Text = "Name:";
            // 
            // textBoxCustomerName
            // 
            textBoxCustomerName.Font = new Font("Segoe UI", 12F);
            textBoxCustomerName.Location = new Point(223, 140);
            textBoxCustomerName.Name = "textBoxCustomerName";
            textBoxCustomerName.Size = new Size(250, 34);
            textBoxCustomerName.TabIndex = 5;
            // 
            // labelCustomerPhone
            // 
            labelCustomerPhone.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelCustomerPhone.Location = new Point(30, 200);
            labelCustomerPhone.Name = "labelCustomerPhone";
            labelCustomerPhone.Size = new Size(178, 30);
            labelCustomerPhone.TabIndex = 6;
            labelCustomerPhone.Text = "Phone:";
            // 
            // textBoxCustomerPhone
            // 
            textBoxCustomerPhone.Font = new Font("Segoe UI", 12F);
            textBoxCustomerPhone.Location = new Point(223, 197);
            textBoxCustomerPhone.Name = "textBoxCustomerPhone";
            textBoxCustomerPhone.Size = new Size(250, 34);
            textBoxCustomerPhone.TabIndex = 7;
            // 
            // labelCustomerEmail
            // 
            labelCustomerEmail.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelCustomerEmail.Location = new Point(30, 257);
            labelCustomerEmail.Name = "labelCustomerEmail";
            labelCustomerEmail.Size = new Size(168, 30);
            labelCustomerEmail.TabIndex = 8;
            labelCustomerEmail.Text = "Email:";
            // 
            // textBoxCustomerEmail
            // 
            textBoxCustomerEmail.Font = new Font("Segoe UI", 12F);
            textBoxCustomerEmail.Location = new Point(223, 254);
            textBoxCustomerEmail.Name = "textBoxCustomerEmail";
            textBoxCustomerEmail.Size = new Size(250, 34);
            textBoxCustomerEmail.TabIndex = 9;
            // 
            // labelSaleAmount
            // 
            labelSaleAmount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelSaleAmount.Location = new Point(30, 314);
            labelSaleAmount.Name = "labelSaleAmount";
            labelSaleAmount.Size = new Size(140, 30);
            labelSaleAmount.TabIndex = 10;
            labelSaleAmount.Text = "Sale Amount:";
            // 
            // textBoxSaleAmount
            // 
            textBoxSaleAmount.Font = new Font("Segoe UI", 12F);
            textBoxSaleAmount.Location = new Point(223, 311);
            textBoxSaleAmount.Name = "textBoxSaleAmount";
            textBoxSaleAmount.Size = new Size(250, 34);
            textBoxSaleAmount.TabIndex = 11;
            // 
            // labelSaleDate
            // 
            labelSaleDate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelSaleDate.Location = new Point(30, 371);
            labelSaleDate.Name = "labelSaleDate";
            labelSaleDate.Size = new Size(140, 30);
            labelSaleDate.TabIndex = 12;
            labelSaleDate.Text = "Sale Date:";
            // 
            // dateTimePickerSaleDate
            // 
            dateTimePickerSaleDate.Font = new Font("Segoe UI", 12F);
            dateTimePickerSaleDate.Location = new Point(223, 368);
            dateTimePickerSaleDate.Name = "dateTimePickerSaleDate";
            dateTimePickerSaleDate.Size = new Size(250, 34);
            dateTimePickerSaleDate.TabIndex = 13;
            // 
            // buttonRegisterSale
            // 
            buttonRegisterSale.BackColor = Color.Green;
            buttonRegisterSale.FlatStyle = FlatStyle.Flat;
            buttonRegisterSale.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonRegisterSale.ForeColor = Color.White;
            buttonRegisterSale.Location = new Point(223, 422);
            buttonRegisterSale.Name = "buttonRegisterSale";
            buttonRegisterSale.Size = new Size(250, 40);
            buttonRegisterSale.TabIndex = 14;
            buttonRegisterSale.Text = "Register Sale";
            buttonRegisterSale.UseVisualStyleBackColor = false;
            buttonRegisterSale.Click += ButtonRegisterSale_Click;
            // 
            // labelPlotStatus
            // 
            labelPlotStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelPlotStatus.Location = new Point(490, 86);
            labelPlotStatus.Name = "labelPlotStatus";
            labelPlotStatus.Size = new Size(200, 30);
            labelPlotStatus.TabIndex = 15;
            labelPlotStatus.Text = "Status: -";
            // 
            // RegisterSaleForm
            // 
            BackColor = Color.FromArgb(245, 248, 255);
            ClientSize = new Size(806, 500);
            Controls.Add(labelProperty);
            Controls.Add(comboBoxProperty);
            Controls.Add(labelPlot);
            Controls.Add(comboBoxPlot);
            Controls.Add(labelPlotStatus);
            Controls.Add(labelCustomerName);
            Controls.Add(textBoxCustomerName);
            Controls.Add(labelCustomerPhone);
            Controls.Add(textBoxCustomerPhone);
            Controls.Add(labelCustomerEmail);
            Controls.Add(textBoxCustomerEmail);
            Controls.Add(labelSaleAmount);
            Controls.Add(textBoxSaleAmount);
            Controls.Add(labelSaleDate);
            Controls.Add(dateTimePickerSaleDate);
            Controls.Add(buttonRegisterSale);
            Font = new Font("Segoe UI", 12F);
            Name = "RegisterSaleForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Register Sale";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}