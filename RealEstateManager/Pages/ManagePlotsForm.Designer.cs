namespace RealEstateManager.Pages
{
    partial class ManagePlotsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelProperty;
        private System.Windows.Forms.ComboBox comboBoxProperty;
        private System.Windows.Forms.Label labelPlotCount;
        private System.Windows.Forms.NumericUpDown numericUpDownPlotCount;
        private System.Windows.Forms.Label labelPlots;
        private System.Windows.Forms.DataGridView dataGridViewPlots;
        private System.Windows.Forms.Button buttonAddPlot;
        private System.Windows.Forms.Button buttonEditPlot;
        private System.Windows.Forms.Button buttonDeletePlot;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePlotsForm));
            labelProperty = new Label();
            comboBoxProperty = new ComboBox();
            labelPlotCount = new Label();
            numericUpDownPlotCount = new NumericUpDown();
            labelPlots = new Label();
            dataGridViewPlots = new DataGridView();
            buttonAddPlot = new Button();
            buttonEditPlot = new Button();
            buttonDeletePlot = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPlotCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlots).BeginInit();
            SuspendLayout();
            // 
            // labelProperty
            // 
            labelProperty.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelProperty.Location = new Point(41, 42);
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
            comboBoxProperty.Location = new Point(234, 42);
            comboBoxProperty.Margin = new Padding(4);
            comboBoxProperty.Name = "comboBoxProperty";
            comboBoxProperty.Size = new Size(330, 36);
            comboBoxProperty.TabIndex = 1;
            comboBoxProperty.SelectedIndexChanged += comboBoxProperty_SelectedIndexChanged;
            // 
            // labelPlotCount
            // 
            labelPlotCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelPlotCount.Location = new Point(688, 42);
            labelPlotCount.Margin = new Padding(4, 0, 4, 0);
            labelPlotCount.Name = "labelPlotCount";
            labelPlotCount.Size = new Size(206, 42);
            labelPlotCount.TabIndex = 2;
            labelPlotCount.Text = "Number of Plots:";
            // 
            // numericUpDownPlotCount
            // 
            numericUpDownPlotCount.Font = new Font("Segoe UI", 12F);
            numericUpDownPlotCount.Location = new Point(908, 42);
            numericUpDownPlotCount.Margin = new Padding(4);
            numericUpDownPlotCount.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownPlotCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownPlotCount.Name = "numericUpDownPlotCount";
            numericUpDownPlotCount.Size = new Size(110, 34);
            numericUpDownPlotCount.TabIndex = 3;
            numericUpDownPlotCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownPlotCount.ValueChanged += numericUpDownPlotCount_ValueChanged;
            // 
            // labelPlots
            // 
            labelPlots.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelPlots.Location = new Point(41, 112);
            labelPlots.Margin = new Padding(4, 0, 4, 0);
            labelPlots.Name = "labelPlots";
            labelPlots.Size = new Size(138, 42);
            labelPlots.TabIndex = 4;
            labelPlots.Text = "Plots";
            // 
            // dataGridViewPlots
            // 
            dataGridViewPlots.AllowUserToAddRows = false;
            dataGridViewPlots.AllowUserToDeleteRows = false;
            dataGridViewPlots.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPlots.BackgroundColor = Color.White;
            dataGridViewPlots.ColumnHeadersHeight = 35;
            dataGridViewPlots.Location = new Point(41, 154);
            dataGridViewPlots.Margin = new Padding(4);
            dataGridViewPlots.Name = "dataGridViewPlots";
            dataGridViewPlots.RowHeadersWidth = 51;
            dataGridViewPlots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPlots.Size = new Size(976, 490);
            dataGridViewPlots.TabIndex = 5;
            // 
            // buttonAddPlot
            // 
            buttonAddPlot.BackColor = Color.Green;
            buttonAddPlot.FlatStyle = FlatStyle.Flat;
            buttonAddPlot.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonAddPlot.ForeColor = Color.White;
            buttonAddPlot.Location = new Point(41, 658);
            buttonAddPlot.Margin = new Padding(4);
            buttonAddPlot.Name = "buttonAddPlot";
            buttonAddPlot.Size = new Size(165, 36);
            buttonAddPlot.TabIndex = 6;
            buttonAddPlot.Text = "Add Plot";
            buttonAddPlot.UseVisualStyleBackColor = false;
            buttonAddPlot.Click += buttonAddPlot_Click;
            // 
            // buttonEditPlot
            // 
            buttonEditPlot.BackColor = Color.Green;
            buttonEditPlot.FlatStyle = FlatStyle.Flat;
            buttonEditPlot.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonEditPlot.ForeColor = Color.White;
            buttonEditPlot.Location = new Point(220, 658);
            buttonEditPlot.Margin = new Padding(4);
            buttonEditPlot.Name = "buttonEditPlot";
            buttonEditPlot.Size = new Size(165, 36);
            buttonEditPlot.TabIndex = 7;
            buttonEditPlot.Text = "Edit Plot";
            buttonEditPlot.UseVisualStyleBackColor = false;
            buttonEditPlot.Click += buttonEditPlot_Click;
            // 
            // buttonDeletePlot
            // 
            buttonDeletePlot.BackColor = Color.Green;
            buttonDeletePlot.FlatStyle = FlatStyle.Flat;
            buttonDeletePlot.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonDeletePlot.ForeColor = Color.White;
            buttonDeletePlot.Location = new Point(399, 658);
            buttonDeletePlot.Margin = new Padding(4);
            buttonDeletePlot.Name = "buttonDeletePlot";
            buttonDeletePlot.Size = new Size(165, 36);
            buttonDeletePlot.TabIndex = 8;
            buttonDeletePlot.Text = "Delete Plot";
            buttonDeletePlot.UseVisualStyleBackColor = false;
            buttonDeletePlot.Click += buttonDeleteSelectedPlots_Click;
            // 
            // ManagePlotsForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 255);
            ClientSize = new Size(1072, 710);
            Controls.Add(labelProperty);
            Controls.Add(comboBoxProperty);
            Controls.Add(labelPlotCount);
            Controls.Add(numericUpDownPlotCount);
            Controls.Add(labelPlots);
            Controls.Add(dataGridViewPlots);
            Controls.Add(buttonAddPlot);
            Controls.Add(buttonEditPlot);
            Controls.Add(buttonDeletePlot);
            Font = new Font("Segoe UI", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "ManagePlotsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Plots";
            ((System.ComponentModel.ISupportInitialize)numericUpDownPlotCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlots).EndInit();
            ResumeLayout(false);
        }
    }
}