namespace RealEstateManager.Pages
{
    partial class ViewAllAgentsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewAgents;
        private System.Windows.Forms.Label labelAgents;
        private System.Windows.Forms.GroupBox groupBoxAgents;
        private System.Windows.Forms.Button buttonRegisterAgent;

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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewAllAgentsForm));
            dataGridViewAgents = new DataGridView();
            labelAgents = new Label();
            groupBoxAgents = new GroupBox();
            buttonRegisterAgent = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAgents).BeginInit();
            groupBoxAgents.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewAgents
            // 
            dataGridViewAgents.AllowUserToAddRows = false;
            dataGridViewAgents.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.AliceBlue;
            dataGridViewAgents.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewAgents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewAgents.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewAgents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewAgents.ColumnHeadersHeight = 29;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.AliceBlue;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewAgents.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewAgents.EnableHeadersVisualStyles = false;
            dataGridViewAgents.GridColor =  SystemColors.WindowFrame;
            dataGridViewAgents.Location = new Point(15, 85);
            dataGridViewAgents.Name = "dataGridViewAgents";
            dataGridViewAgents.ReadOnly = true;
            dataGridViewAgents.RowHeadersWidth = 51;
            dataGridViewAgents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewAgents.Size = new Size(1060, 437);
            dataGridViewAgents.TabIndex = 0;
            dataGridViewAgents.CellMouseClick += dataGridViewAgents_CellMouseClick;
            dataGridViewAgents.CellPainting += dataGridViewAgents_CellPainting;
            // 
            // labelAgents
            // 
            labelAgents.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelAgents.ForeColor = Color.Black;
            labelAgents.Location = new Point(20, 41);
            labelAgents.Name = "labelAgents";
            labelAgents.Size = new Size(400, 30);
            labelAgents.TabIndex = 1;
            labelAgents.Text = "Agents";
            // 
            // groupBoxAgents
            // 
            groupBoxAgents.BackColor = Color.AliceBlue;
            groupBoxAgents.Controls.Add(labelAgents);
            groupBoxAgents.Controls.Add(buttonRegisterAgent);
            groupBoxAgents.Controls.Add(dataGridViewAgents);
            groupBoxAgents.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxAgents.ForeColor = Color.MidnightBlue;
            groupBoxAgents.Location = new Point(20, 20);
            groupBoxAgents.Name = "groupBoxAgents";
            groupBoxAgents.Padding = new Padding(15);
            groupBoxAgents.Size = new Size(1091, 534);
            groupBoxAgents.TabIndex = 2;
            groupBoxAgents.TabStop = false;
            groupBoxAgents.Text = "Agent Details";
            // 
            // buttonRegisterAgent
            // 
            buttonRegisterAgent.BackColor = Color.Green;
            buttonRegisterAgent.FlatStyle = FlatStyle.Flat;
            buttonRegisterAgent.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonRegisterAgent.ForeColor = Color.White;
            buttonRegisterAgent.Location = new Point(842, 31);
            buttonRegisterAgent.Name = "buttonRegisterAgent";
            buttonRegisterAgent.Size = new Size(233, 40);
            buttonRegisterAgent.TabIndex = 2;
            buttonRegisterAgent.Text = "Register Agent";
            buttonRegisterAgent.UseVisualStyleBackColor = false;
            buttonRegisterAgent.Click += ButtonRegisterAgent_Click;
            // 
            // ViewAllAgentsForm
            // 
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 255);
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1127, 571);
            Controls.Add(groupBoxAgents);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ViewAllAgentsForm";
            Text = "All Agents";
            ((System.ComponentModel.ISupportInitialize)dataGridViewAgents).EndInit();
            groupBoxAgents.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}