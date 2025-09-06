namespace RealEstateManager.Pages
{
    partial class ManagePlotsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelProperty;
        private System.Windows.Forms.ComboBox comboBoxProperty;
        private System.Windows.Forms.Label labelPlotCount;
        private System.Windows.Forms.NumericUpDown numericUpDownPlotCount;
        private System.Windows.Forms.DataGridView dataGridViewPlots;
        private System.Windows.Forms.Button buttonAddPlot;
        private System.Windows.Forms.Button buttonEditPlot;
        private System.Windows.Forms.Button buttonDeletePlot;
        private System.Windows.Forms.GroupBox groupBoxPlots;
        private System.Windows.Forms.Label labelGridTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePlotsForm));
            labelProperty = new Label();
            comboBoxProperty = new ComboBox();
            labelPlotCount = new Label();
            numericUpDownPlotCount = new NumericUpDown();
            dataGridViewPlots = new DataGridView();
            buttonAddPlot = new Button();
            buttonEditPlot = new Button();
            buttonDeletePlot = new Button();
            groupBoxPlots = new GroupBox();
            labelGridTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPlotCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlots).BeginInit();
            groupBoxPlots.SuspendLayout();
            SuspendLayout();
            // 
            // labelProperty
            // 
            labelProperty.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelProperty.Location = new Point(41, 48);
            labelProperty.Margin = new Padding(4, 0, 4, 0);
            labelProperty.Name = "labelProperty";
            labelProperty.Size = new Size(179, 42);
            labelProperty.TabIndex = 0;
            labelProperty.Text = "Select Property:";
            // 
            // comboBoxProperty
            // 
            comboBoxProperty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProperty.Font = new Font("Segoe UI", 12F);
            comboBoxProperty.Location = new Point(234, 48);
            comboBoxProperty.Margin = new Padding(4);
            comboBoxProperty.Name = "comboBoxProperty";
            comboBoxProperty.Size = new Size(330, 36);
            comboBoxProperty.TabIndex = 1;
            comboBoxProperty.SelectedIndexChanged += ComboBoxProperty_SelectedIndexChanged;
            // 
            // labelPlotCount
            // 
            labelPlotCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelPlotCount.Location = new Point(688, 48);
            labelPlotCount.Margin = new Padding(4, 0, 4, 0);
            labelPlotCount.Name = "labelPlotCount";
            labelPlotCount.Size = new Size(206, 42);
            labelPlotCount.TabIndex = 2;
            labelPlotCount.Text = "Number of Plots:";
            // 
            // numericUpDownPlotCount
            // 
            numericUpDownPlotCount.Font = new Font("Segoe UI", 12F);
            numericUpDownPlotCount.Location = new Point(908, 48);
            numericUpDownPlotCount.Margin = new Padding(4);
            numericUpDownPlotCount.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownPlotCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownPlotCount.Name = "numericUpDownPlotCount";
            numericUpDownPlotCount.Size = new Size(110, 34);
            numericUpDownPlotCount.TabIndex = 3;
            numericUpDownPlotCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownPlotCount.ValueChanged += NumericUpDownPlotCount_ValueChanged;
            // 
            // dataGridViewPlots
            // 
            dataGridViewPlots.AllowUserToAddRows = false;
            dataGridViewPlots.AllowUserToDeleteRows = false;
            dataGridViewPlots.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPlots.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewPlots.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewPlots.ColumnHeadersHeight = 35;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewPlots.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewPlots.Location = new Point(41, 144);
            dataGridViewPlots.Margin = new Padding(4);
            dataGridViewPlots.Name = "dataGridViewPlots";
            dataGridViewPlots.RowHeadersWidth = 51;
            dataGridViewPlots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPlots.Size = new Size(976, 363);
            dataGridViewPlots.TabIndex = 5;
            // 
            // buttonAddPlot
            // 
            buttonAddPlot.BackColor = Color.Green;
            buttonAddPlot.FlatStyle = FlatStyle.Flat;
            buttonAddPlot.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonAddPlot.ForeColor = Color.White;
            buttonAddPlot.Location = new Point(41, 524);
            buttonAddPlot.Margin = new Padding(4);
            buttonAddPlot.Name = "buttonAddPlot";
            buttonAddPlot.Size = new Size(165, 36);
            buttonAddPlot.TabIndex = 6;
            buttonAddPlot.Text = "Add Plot";
            buttonAddPlot.UseVisualStyleBackColor = false;
            buttonAddPlot.Click += ButtonAddPlot_Click;
            // 
            // buttonEditPlot
            // 
            buttonEditPlot.BackColor = Color.Green;
            buttonEditPlot.FlatStyle = FlatStyle.Flat;
            buttonEditPlot.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonEditPlot.ForeColor = Color.White;
            buttonEditPlot.Location = new Point(220, 524);
            buttonEditPlot.Margin = new Padding(4);
            buttonEditPlot.Name = "buttonEditPlot";
            buttonEditPlot.Size = new Size(165, 36);
            buttonEditPlot.TabIndex = 7;
            buttonEditPlot.Text = "Edit Plot";
            buttonEditPlot.UseVisualStyleBackColor = false;
            buttonEditPlot.Click += ButtonEditPlot_Click;
            // 
            // buttonDeletePlot
            // 
            buttonDeletePlot.BackColor = Color.Green;
            buttonDeletePlot.FlatStyle = FlatStyle.Flat;
            buttonDeletePlot.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonDeletePlot.ForeColor = Color.White;
            buttonDeletePlot.Location = new Point(399, 524);
            buttonDeletePlot.Margin = new Padding(4);
            buttonDeletePlot.Name = "buttonDeletePlot";
            buttonDeletePlot.Size = new Size(165, 36);
            buttonDeletePlot.TabIndex = 8;
            buttonDeletePlot.Text = "Delete Plot";
            buttonDeletePlot.UseVisualStyleBackColor = false;
            buttonDeletePlot.Click += ButtonDeleteSelectedPlots_Click;
            // 
            // groupBoxPlots
            // 
            groupBoxPlots.Controls.Add(labelProperty);
            groupBoxPlots.Controls.Add(comboBoxProperty);
            groupBoxPlots.Controls.Add(labelPlotCount);
            groupBoxPlots.Controls.Add(numericUpDownPlotCount);
            groupBoxPlots.Controls.Add(dataGridViewPlots);
            groupBoxPlots.Controls.Add(buttonAddPlot);
            groupBoxPlots.Controls.Add(buttonEditPlot);
            groupBoxPlots.Controls.Add(buttonDeletePlot);
            groupBoxPlots.Controls.Add(labelGridTitle);
            groupBoxPlots.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxPlots.ForeColor = Color.MidnightBlue;
            groupBoxPlots.Location = new Point(24, 21);
            groupBoxPlots.Name = "groupBoxPlots";
            groupBoxPlots.Size = new Size(1030, 580);
            groupBoxPlots.TabIndex = 0;
            groupBoxPlots.TabStop = false;
            groupBoxPlots.Text = "Manage Plots";
            // 
            // labelGridTitle
            // 
            labelGridTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelGridTitle.ForeColor = Color.MidnightBlue;
            labelGridTitle.Location = new Point(41, 112);
            labelGridTitle.Name = "labelGridTitle";
            labelGridTitle.Size = new Size(300, 32);
            labelGridTitle.TabIndex = 4;
            labelGridTitle.Text = "List of Plots";
            // 
            // ManagePlotsForm
            // 
            
            BackColor = Color.FromArgb(245, 248, 255);
            ClientSize = new Size(1076, 620);
            Controls.Add(groupBoxPlots);
            Font = new Font("Segoe UI", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "ManagePlotsForm";
            Text = "Manage Plots";
            ((System.ComponentModel.ISupportInitialize)numericUpDownPlotCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlots).EndInit();
            groupBoxPlots.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}