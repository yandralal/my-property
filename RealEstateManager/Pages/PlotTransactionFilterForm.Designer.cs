namespace RealEstateManager.Pages
{
    partial class PlotTransactionFilterForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.GroupBox groupBoxResults;

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            labelFrom = new Label();
            labelTo = new Label();
            dateTimePickerFrom = new DateTimePicker();
            dateTimePickerTo = new DateTimePicker();
            buttonFilter = new Button();
            dataGridViewResults = new DataGridView();
            groupBoxFilter = new GroupBox();
            groupBoxResults = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).BeginInit();
            groupBoxFilter.SuspendLayout();
            groupBoxResults.SuspendLayout();
            SuspendLayout();
            // 
            // labelFrom
            // 
            labelFrom.AutoSize = true;
            labelFrom.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelFrom.ForeColor = Color.DarkSlateGray;
            labelFrom.Location = new Point(30, 35);
            labelFrom.Name = "labelFrom";
            labelFrom.Size = new Size(100, 23);
            labelFrom.TabIndex = 0;
            labelFrom.Text = "From Date:";
            // 
            // labelTo
            // 
            labelTo.AutoSize = true;
            labelTo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTo.ForeColor = Color.DarkSlateGray;
            labelTo.Location = new Point(340, 35);
            labelTo.Name = "labelTo";
            labelTo.Size = new Size(76, 23);
            labelTo.TabIndex = 2;
            labelTo.Text = "To Date:";
            // 
            // dateTimePickerFrom
            // 
            dateTimePickerFrom.Font = new Font("Segoe UI", 10F);
            dateTimePickerFrom.Location = new Point(130, 30);
            dateTimePickerFrom.Name = "dateTimePickerFrom";
            dateTimePickerFrom.Size = new Size(170, 30);
            dateTimePickerFrom.TabIndex = 1;
            // 
            // dateTimePickerTo
            // 
            dateTimePickerTo.Font = new Font("Segoe UI", 10F);
            dateTimePickerTo.Location = new Point(420, 30);
            dateTimePickerTo.Name = "dateTimePickerTo";
            dateTimePickerTo.Size = new Size(170, 30);
            dateTimePickerTo.TabIndex = 3;
            // 
            // buttonFilter
            // 
            buttonFilter.BackColor = Color.Green;
            buttonFilter.FlatStyle = FlatStyle.Flat;
            buttonFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonFilter.ForeColor = Color.White;
            buttonFilter.Location = new Point(620, 28);
            buttonFilter.Name = "buttonFilter";
            buttonFilter.Size = new Size(120, 36);
            buttonFilter.TabIndex = 4;
            buttonFilter.Text = "Filter";
            buttonFilter.UseVisualStyleBackColor = false;
            buttonFilter.Click += buttonFilter_Click;
            // 
            // dataGridViewResults
            // 
            dataGridViewResults.AllowUserToAddRows = false;
            dataGridViewResults.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.AliceBlue;
            dataGridViewResults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewResults.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewResults.ColumnHeadersHeight = 29;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewResults.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewResults.EnableHeadersVisualStyles = false;
            dataGridViewResults.GridColor = Color.LightGray;
            dataGridViewResults.Location = new Point(15, 35);
            dataGridViewResults.Name = "dataGridViewResults";
            dataGridViewResults.ReadOnly = true;
            dataGridViewResults.RowHeadersWidth = 51;
            dataGridViewResults.Size = new Size(1136, 413);
            dataGridViewResults.TabIndex = 0;
            // 
            // groupBoxFilter
            // 
            groupBoxFilter.BackColor = Color.AliceBlue;
            groupBoxFilter.Controls.Add(labelFrom);
            groupBoxFilter.Controls.Add(dateTimePickerFrom);
            groupBoxFilter.Controls.Add(labelTo);
            groupBoxFilter.Controls.Add(dateTimePickerTo);
            groupBoxFilter.Controls.Add(buttonFilter);
            groupBoxFilter.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxFilter.ForeColor = Color.MidnightBlue;
            groupBoxFilter.Location = new Point(20, 20);
            groupBoxFilter.Name = "groupBoxFilter";
            groupBoxFilter.Size = new Size(1157, 80);
            groupBoxFilter.TabIndex = 0;
            groupBoxFilter.TabStop = false;
            groupBoxFilter.Text = "Filter Transactions";
            // 
            // groupBoxResults
            // 
            groupBoxResults.BackColor = Color.AliceBlue;
            groupBoxResults.Controls.Add(dataGridViewResults);
            groupBoxResults.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxResults.ForeColor = Color.MidnightBlue;
            groupBoxResults.Location = new Point(20, 120);
            groupBoxResults.Name = "groupBoxResults";
            groupBoxResults.Size = new Size(1157, 454);
            groupBoxResults.TabIndex = 1;
            groupBoxResults.TabStop = false;
            groupBoxResults.Text = "Filtered Transactions";
            // 
            // PlotTransactionFilterForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1189, 586);
            Controls.Add(groupBoxResults);
            Controls.Add(groupBoxFilter);
            Font = new Font("Segoe UI", 10F);
            Name = "PlotTransactionFilterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Filter Plot Transactions by Date";
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).EndInit();
            groupBoxFilter.ResumeLayout(false);
            groupBoxFilter.PerformLayout();
            groupBoxResults.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}