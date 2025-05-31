namespace RealEstateManager.Pages
{
    partial class ManagePlotsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelProperty;
        private System.Windows.Forms.ComboBox comboBoxProperty;
        private System.Windows.Forms.Label labelWingNumber;
        private System.Windows.Forms.NumericUpDown numericUpDownWingNumber;
        private System.Windows.Forms.Label labelPlotCount;
        private System.Windows.Forms.NumericUpDown numericUpDownPlotCount;
        private System.Windows.Forms.Label labelWings;
        private System.Windows.Forms.DataGridView dataGridViewWings;
        private System.Windows.Forms.Button buttonAddWing;
        private System.Windows.Forms.Button buttonEditWing;
        private System.Windows.Forms.Button buttonDeleteWing;
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
            labelProperty = new System.Windows.Forms.Label();
            comboBoxProperty = new System.Windows.Forms.ComboBox();
            labelWingNumber = new System.Windows.Forms.Label();
            numericUpDownWingNumber = new System.Windows.Forms.NumericUpDown();
            labelPlotCount = new System.Windows.Forms.Label();
            numericUpDownPlotCount = new System.Windows.Forms.NumericUpDown();
            labelWings = new System.Windows.Forms.Label();
            dataGridViewWings = new System.Windows.Forms.DataGridView();
            buttonAddWing = new System.Windows.Forms.Button();
            buttonEditWing = new System.Windows.Forms.Button();
            buttonDeleteWing = new System.Windows.Forms.Button();
            labelPlots = new System.Windows.Forms.Label();
            dataGridViewPlots = new System.Windows.Forms.DataGridView();
            buttonAddPlot = new System.Windows.Forms.Button();
            buttonEditPlot = new System.Windows.Forms.Button();
            buttonDeletePlot = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(numericUpDownWingNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownPlotCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewWings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewPlots)).BeginInit();
            SuspendLayout();

            // labelProperty
            labelProperty.Text = "Select Property:";
            labelProperty.Location = new System.Drawing.Point(30, 30);
            labelProperty.Size = new System.Drawing.Size(130, 30);

            // comboBoxProperty
            comboBoxProperty.Location = new System.Drawing.Point(170, 30);
            comboBoxProperty.Size = new System.Drawing.Size(300, 30);
            comboBoxProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // labelWingNumber
            labelWingNumber.Text = "Wing Number:";
            labelWingNumber.Location = new System.Drawing.Point(500, 30);
            labelWingNumber.Size = new System.Drawing.Size(110, 30);

            // numericUpDownWingNumber
            numericUpDownWingNumber.Location = new System.Drawing.Point(620, 30);
            numericUpDownWingNumber.Size = new System.Drawing.Size(80, 30);
            numericUpDownWingNumber.Minimum = 1;
            numericUpDownWingNumber.Maximum = 26;
            numericUpDownWingNumber.Value = 1;

            // labelPlotCount
            labelPlotCount.Text = "Number of Plots:";
            labelPlotCount.Location = new System.Drawing.Point(720, 30);
            labelPlotCount.Size = new System.Drawing.Size(130, 30);

            // numericUpDownPlotCount
            numericUpDownPlotCount.Location = new System.Drawing.Point(860, 30);
            numericUpDownPlotCount.Size = new System.Drawing.Size(80, 30);
            numericUpDownPlotCount.Minimum = 1;
            numericUpDownPlotCount.Maximum = 1000;
            numericUpDownPlotCount.Value = 1;

            // labelWings
            labelWings.Text = "Wings";
            labelWings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            labelWings.Location = new System.Drawing.Point(30, 80);
            labelWings.Size = new System.Drawing.Size(100, 30);

            // dataGridViewWings
            dataGridViewWings.Location = new System.Drawing.Point(30, 110);
            dataGridViewWings.Size = new System.Drawing.Size(400, 350);
            dataGridViewWings.ReadOnly = true;
            dataGridViewWings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridViewWings.AllowUserToAddRows = false;
            dataGridViewWings.AllowUserToDeleteRows = false;
            dataGridViewWings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // buttonAddWing
            buttonAddWing.Text = "Add Wing";
            buttonAddWing.Location = new System.Drawing.Point(30, 470);
            buttonAddWing.Size = new System.Drawing.Size(120, 35);

            // buttonEditWing
            buttonEditWing.Text = "Edit Wing";
            buttonEditWing.Location = new System.Drawing.Point(160, 470);
            buttonEditWing.Size = new System.Drawing.Size(120, 35);

            // buttonDeleteWing
            buttonDeleteWing.Text = "Delete Wing";
            buttonDeleteWing.Location = new System.Drawing.Point(290, 470);
            buttonDeleteWing.Size = new System.Drawing.Size(120, 35);

            // labelPlots
            labelPlots.Text = "Plots";
            labelPlots.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            labelPlots.Location = new System.Drawing.Point(470, 80);
            labelPlots.Size = new System.Drawing.Size(100, 30);

            // dataGridViewPlots
            dataGridViewPlots.Location = new System.Drawing.Point(470, 110);
            dataGridViewPlots.Size = new System.Drawing.Size(470, 350);
            dataGridViewPlots.ReadOnly = true;
            dataGridViewPlots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPlots.AllowUserToAddRows = false;
            dataGridViewPlots.AllowUserToDeleteRows = false;
            dataGridViewPlots.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // buttonAddPlot
            buttonAddPlot.Text = "Add Plot";
            buttonAddPlot.Location = new System.Drawing.Point(470, 470);
            buttonAddPlot.Size = new System.Drawing.Size(120, 35);

            // buttonEditPlot
            buttonEditPlot.Text = "Edit Plot";
            buttonEditPlot.Location = new System.Drawing.Point(600, 470);
            buttonEditPlot.Size = new System.Drawing.Size(120, 35);

            // buttonDeletePlot
            buttonDeletePlot.Text = "Delete Plot";
            buttonDeletePlot.Location = new System.Drawing.Point(730, 470);
            buttonDeletePlot.Size = new System.Drawing.Size(120, 35);

            // ManagePlotsForm
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(980, 530);
            Controls.Add(labelProperty);
            Controls.Add(comboBoxProperty);
            Controls.Add(labelWingNumber);
            Controls.Add(numericUpDownWingNumber);
            Controls.Add(labelPlotCount);
            Controls.Add(numericUpDownPlotCount);
            Controls.Add(labelWings);
            Controls.Add(dataGridViewWings);
            Controls.Add(buttonAddWing);
            Controls.Add(buttonEditWing);
            Controls.Add(buttonDeleteWing);
            Controls.Add(labelPlots);
            Controls.Add(dataGridViewPlots);
            Controls.Add(buttonAddPlot);
            Controls.Add(buttonEditPlot);
            Controls.Add(buttonDeletePlot);
            Name = "ManagePlotsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Manage Wings and Plots";
            ((System.ComponentModel.ISupportInitialize)(numericUpDownWingNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownPlotCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewWings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewPlots)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}