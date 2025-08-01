namespace RealEstateManager.Pages
{
    partial class AllTransactionsFilterForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.DataGridView dataGridViewResults;

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            groupBoxFilter = new GroupBox();
            labelType = new Label();
            comboBoxType = new ComboBox();
            labelFrom = new Label();
            dateTimePickerFrom = new DateTimePicker();
            labelTo = new Label();
            dateTimePickerTo = new DateTimePicker();
            buttonFilter = new Button();
            groupBoxResults = new GroupBox();
            dataGridViewResults = new DataGridView();
            groupBoxFilter.SuspendLayout();
            groupBoxResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).BeginInit();
            SuspendLayout();
            // 
            // groupBoxFilter
            // 
            groupBoxFilter.BackColor = Color.AliceBlue;
            groupBoxFilter.Controls.Add(labelType);
            groupBoxFilter.Controls.Add(comboBoxType);
            groupBoxFilter.Controls.Add(labelFrom);
            groupBoxFilter.Controls.Add(dateTimePickerFrom);
            groupBoxFilter.Controls.Add(labelTo);
            groupBoxFilter.Controls.Add(dateTimePickerTo);
            groupBoxFilter.Controls.Add(buttonFilter);
            groupBoxFilter.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxFilter.ForeColor = Color.MidnightBlue;
            groupBoxFilter.Location = new Point(20, 20);
            groupBoxFilter.Name = "groupBoxFilter";
            groupBoxFilter.Size = new Size(1348, 95);
            groupBoxFilter.TabIndex = 0;
            groupBoxFilter.TabStop = false;
            groupBoxFilter.Text = "Filter Transactions";
            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelType.ForeColor = Color.DarkSlateGray;
            labelType.Location = new Point(32, 47);
            labelType.Name = "labelType";
            labelType.Size = new Size(149, 23);
            labelType.TabIndex = 0;
            labelType.Text = "Transaction Type:";
            // 
            // comboBoxType
            // 
            comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxType.Font = new Font("Segoe UI", 10F);
            comboBoxType.Location = new Point(202, 42);
            comboBoxType.Name = "comboBoxType";
            comboBoxType.Size = new Size(180, 31);
            comboBoxType.TabIndex = 1;
            // 
            // labelFrom
            // 
            labelFrom.AutoSize = true;
            labelFrom.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelFrom.ForeColor = Color.DarkSlateGray;
            labelFrom.Location = new Point(425, 47);
            labelFrom.Name = "labelFrom";
            labelFrom.Size = new Size(100, 23);
            labelFrom.TabIndex = 2;
            labelFrom.Text = "From Date:";
            // 
            // dateTimePickerFrom
            // 
            dateTimePickerFrom.Font = new Font("Segoe UI", 10F);
            dateTimePickerFrom.Location = new Point(544, 42);
            dateTimePickerFrom.Name = "dateTimePickerFrom";
            dateTimePickerFrom.Size = new Size(170, 30);
            dateTimePickerFrom.TabIndex = 3;
            // 
            // labelTo
            // 
            labelTo.AutoSize = true;
            labelTo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTo.ForeColor = Color.DarkSlateGray;
            labelTo.Location = new Point(734, 47);
            labelTo.Name = "labelTo";
            labelTo.Size = new Size(76, 23);
            labelTo.TabIndex = 4;
            labelTo.Text = "To Date:";
            // 
            // dateTimePickerTo
            // 
            dateTimePickerTo.Font = new Font("Segoe UI", 10F);
            dateTimePickerTo.Location = new Point(831, 42);
            dateTimePickerTo.Name = "dateTimePickerTo";
            dateTimePickerTo.Size = new Size(170, 30);
            dateTimePickerTo.TabIndex = 5;
            // 
            // buttonFilter
            // 
            buttonFilter.BackColor = Color.Green;
            buttonFilter.FlatStyle = FlatStyle.Flat;
            buttonFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonFilter.ForeColor = Color.White;
            buttonFilter.Location = new Point(1039, 40);
            buttonFilter.Name = "buttonFilter";
            buttonFilter.Size = new Size(120, 36);
            buttonFilter.TabIndex = 6;
            buttonFilter.Text = "Filter";
            buttonFilter.UseVisualStyleBackColor = false;
            buttonFilter.Click += ButtonFilter_Click;
            // 
            // groupBoxResults
            // 
            groupBoxResults.BackColor = Color.AliceBlue;
            groupBoxResults.Controls.Add(dataGridViewResults);
            groupBoxResults.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxResults.ForeColor = Color.MidnightBlue;
            groupBoxResults.Location = new Point(20, 137);
            groupBoxResults.Name = "groupBoxResults";
            groupBoxResults.Size = new Size(1348, 500);
            groupBoxResults.TabIndex = 1;
            groupBoxResults.TabStop = false;
            groupBoxResults.Text = "Filtered Transactions";
            // 
            // dataGridViewResults
            // 
            dataGridViewResults.AllowUserToAddRows = false;
            dataGridViewResults.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = Color.AliceBlue;
            dataGridViewResults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewResults.BackgroundColor = Color.White;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = Color.White;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            dataGridViewResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewResults.ColumnHeadersHeight = 29;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.AliceBlue;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle9.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle9.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle9.SelectionForeColor = Color.Black;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            dataGridViewResults.DefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewResults.EnableHeadersVisualStyles = false;
            dataGridViewResults.GridColor = Color.LightGray;
            dataGridViewResults.Location = new Point(15, 35);
            dataGridViewResults.Name = "dataGridViewResults";
            dataGridViewResults.ReadOnly = true;
            dataGridViewResults.RowHeadersWidth = 51;
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewResults.Size = new Size(1318, 450);
            dataGridViewResults.TabIndex = 0;
            // 
            // AllTransactionsFilterForm
            // 
            //AutoScaleDimensions = new SizeF(9F, 23F);
            //AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1380, 650);
            Controls.Add(groupBoxResults);
            Controls.Add(groupBoxFilter);
            Font = new Font("Segoe UI", 10F);
            Name = "AllTransactionsFilterForm";
            Text = "Filter All Transactions";
            groupBoxFilter.ResumeLayout(false);
            groupBoxFilter.PerformLayout();
            groupBoxResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).EndInit();
            ResumeLayout(false);
        }
    }
}