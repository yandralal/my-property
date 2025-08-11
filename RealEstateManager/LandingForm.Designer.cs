using System.Windows.Forms;
using System.Drawing;

namespace RealEstateManager
{
    partial class LandingForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem propertyMenu;
        private ToolStripMenuItem registerPlotMenuItem;
        private ToolStripMenuItem transactionsMenu;
        private ToolStripMenuItem propertyTransactionsMenuItem;
        private ToolStripMenuItem plotTransactionMenuItem;
        private ToolStripMenuItem registerPlotToolStripMenuItem;
        private ToolStripMenuItem reportsMenu;
        private ToolStripMenuItem viewReportsMenuItem;
        private ToolStripMenuItem helpMenu;
        private ToolStripMenuItem helpMenuItem;
        private ToolStripMenuItem aboutMenuItem;
        private ToolStripMenuItem communicationMenu;
        private ToolStripMenuItem sendMessageToAllMenuItem;
        private ToolStripMenuItem agentOperationsMenu;
        private ToolStripMenuItem approveOfferMenuItem;
        private ToolStripMenuItem registerSaleMenuItem;
        private ToolStripMenuItem agentTransactionToolStripMenuItem;
        private ToolStripMenuItem miscTransactionToolStripMenuItem;
        private ToolStripMenuItem changeBackgroundToolStripMenuItem;
        private DataGridView dataGridViewProperties;
        private Button buttonAddProperty;
        private Button buttonManagePlots;
        private DataGridView dataGridViewPlots;
        private Label labelProperties;
        private GroupBox groupBoxProperties;
        private GroupBox groupBoxPlots;
        private Panel panelMain;
        private Panel panelFooter;
        private Label footerLabel;
        private TextBox textBoxPlotFilter;
        private Label labelPlotFilter;
        private Label labelPlots;
        private Label labelPropertyFilter;
        private TextBox textBoxPropertyFilter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LandingForm));
            menuStripMain = new MenuStrip();
            transactionsMenu = new ToolStripMenuItem();
            propertyTransactionsMenuItem = new ToolStripMenuItem();
            plotTransactionMenuItem = new ToolStripMenuItem();
            agentTransactionToolStripMenuItem = new ToolStripMenuItem();
            miscTransactionToolStripMenuItem = new ToolStripMenuItem();
            agentOperationsMenu = new ToolStripMenuItem();
            registerSaleMenuItem = new ToolStripMenuItem();
            approveOfferMenuItem = new ToolStripMenuItem();
            reportsMenu = new ToolStripMenuItem();
            viewReportsMenuItem = new ToolStripMenuItem();
            communicationMenu = new ToolStripMenuItem();
            sendMessageToAllMenuItem = new ToolStripMenuItem();
            helpMenu = new ToolStripMenuItem();
            helpMenuItem = new ToolStripMenuItem();
            aboutMenuItem = new ToolStripMenuItem();
            changeBackgroundToolStripMenuItem = new ToolStripMenuItem();
            propertyMenu = new ToolStripMenuItem();
            registerPlotToolStripMenuItem = new ToolStripMenuItem();
            registerPlotMenuItem = new ToolStripMenuItem();
            dataGridViewProperties = new DataGridView();
            buttonAddProperty = new Button();
            buttonManagePlots = new Button();
            dataGridViewPlots = new DataGridView();
            labelProperties = new Label();
            groupBoxProperties = new GroupBox();
            labelPropertyFilter = new Label();
            textBoxPropertyFilter = new TextBox();
            groupBoxPlots = new GroupBox();
            labelPlots = new Label();
            labelPlotFilter = new Label();
            textBoxPlotFilter = new TextBox();
            panelMain = new Panel();
            panelFooter = new Panel();
            footerLabel = new Label();
            menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlots).BeginInit();
            groupBoxProperties.SuspendLayout();
            groupBoxPlots.SuspendLayout();
            panelMain.SuspendLayout();
            panelFooter.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.BackColor = SystemColors.Control;
            menuStripMain.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            menuStripMain.ForeColor = SystemColors.ControlText;
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { transactionsMenu, agentOperationsMenu, reportsMenu, communicationMenu, helpMenu });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(1832, 36);
            menuStripMain.TabIndex = 1;
            menuStripMain.Text = "menuStripMain";
            // 
            // transactionsMenu
            // 
            transactionsMenu.DropDownItems.AddRange(new ToolStripItem[] { propertyTransactionsMenuItem, plotTransactionMenuItem, agentTransactionToolStripMenuItem, miscTransactionToolStripMenuItem });
            transactionsMenu.Name = "transactionsMenu";
            transactionsMenu.Size = new Size(144, 32);
            transactionsMenu.Text = "Transactions";
            // 
            // propertyTransactionsMenuItem
            // 
            propertyTransactionsMenuItem.Name = "propertyTransactionsMenuItem";
            propertyTransactionsMenuItem.Size = new Size(232, 32);
            propertyTransactionsMenuItem.Text = "Property";
            propertyTransactionsMenuItem.Click += PropertyTransactionsMenuItem_Click;
            // 
            // plotTransactionMenuItem
            // 
            plotTransactionMenuItem.Name = "plotTransactionMenuItem";
            plotTransactionMenuItem.Size = new Size(232, 32);
            plotTransactionMenuItem.Text = "Plot";
            plotTransactionMenuItem.Click += PlotTransactionMenuItem_Click;
            // 
            // agentTransactionToolStripMenuItem
            // 
            agentTransactionToolStripMenuItem.Name = "agentTransactionToolStripMenuItem";
            agentTransactionToolStripMenuItem.Size = new Size(232, 32);
            agentTransactionToolStripMenuItem.Text = "Agent";
            agentTransactionToolStripMenuItem.Click += AgentTransactionToolStripMenuItem_Click;
            // 
            // miscTransactionToolStripMenuItem
            // 
            miscTransactionToolStripMenuItem.Name = "miscTransactionToolStripMenuItem";
            miscTransactionToolStripMenuItem.Size = new Size(232, 32);
            miscTransactionToolStripMenuItem.Text = "Miscellaneous";
            miscTransactionToolStripMenuItem.Click += MiscTransactionToolStripMenuItem_Click;
            // 
            // agentOperationsMenu
            // 
            agentOperationsMenu.DropDownItems.AddRange(new ToolStripItem[] { registerSaleMenuItem, approveOfferMenuItem });
            agentOperationsMenu.Name = "agentOperationsMenu";
            agentOperationsMenu.Size = new Size(130, 32);
            agentOperationsMenu.Text = "Operations";
            // 
            // registerSaleMenuItem
            // 
            registerSaleMenuItem.Name = "registerSaleMenuItem";
            registerSaleMenuItem.Size = new Size(246, 32);
            registerSaleMenuItem.Text = "Sale Plot";
            registerSaleMenuItem.Click += RegisterSaleMenuItem_Click;
            // 
            // approveOfferMenuItem
            // 
            approveOfferMenuItem.Name = "approveOfferMenuItem";
            approveOfferMenuItem.Size = new Size(246, 32);
            approveOfferMenuItem.Text = "Manage Agents";
            approveOfferMenuItem.Click += ViewAllAgentsMenuItem_Click;
            // 
            // reportsMenu
            // 
            reportsMenu.DropDownItems.AddRange(new ToolStripItem[] { viewReportsMenuItem });
            reportsMenu.Name = "reportsMenu";
            reportsMenu.Size = new Size(100, 32);
            reportsMenu.Text = "Reports";
            // 
            // viewReportsMenuItem
            // 
            viewReportsMenuItem.Name = "viewReportsMenuItem";
            viewReportsMenuItem.Size = new Size(224, 32);
            viewReportsMenuItem.Text = "View Reports";
            viewReportsMenuItem.Click += ViewReportsMenuItem_Click;
            // 
            // communicationMenu
            // 
            communicationMenu.DropDownItems.AddRange(new ToolStripItem[] { sendMessageToAllMenuItem });
            communicationMenu.Name = "communicationMenu";
            communicationMenu.Size = new Size(150, 32);
            communicationMenu.Text = "Notifications";
            // 
            // sendMessageToAllMenuItem
            // 
            sendMessageToAllMenuItem.Name = "sendMessageToAllMenuItem";
            sendMessageToAllMenuItem.Size = new Size(241, 32);
            sendMessageToAllMenuItem.Text = "Send Messages";
            sendMessageToAllMenuItem.Click += SendMessageToAllMenuItem_Click;
            // 
            // helpMenu
            // 
            helpMenu.DropDownItems.AddRange(new ToolStripItem[] { helpMenuItem, aboutMenuItem, changeBackgroundToolStripMenuItem });
            helpMenu.Name = "helpMenu";
            helpMenu.Size = new Size(70, 32);
            helpMenu.Text = "Help";
            // 
            // helpMenuItem
            // 
            helpMenuItem.Name = "helpMenuItem";
            helpMenuItem.Size = new Size(315, 32);
            helpMenuItem.Text = "Help";
            // 
            // aboutMenuItem
            // 
            aboutMenuItem.Name = "aboutMenuItem";
            aboutMenuItem.Size = new Size(315, 32);
            aboutMenuItem.Text = "About";
            // 
            // changeBackgroundToolStripMenuItem
            // 
            changeBackgroundToolStripMenuItem.Name = "changeBackgroundToolStripMenuItem";
            changeBackgroundToolStripMenuItem.Size = new Size(315, 32);
            changeBackgroundToolStripMenuItem.Text = "Customize Background";
            changeBackgroundToolStripMenuItem.Click += ChangeBackgroundToolStripMenuItem_Click;
            // 
            // propertyMenu
            // 
            propertyMenu.DropDownItems.AddRange(new ToolStripItem[] { registerPlotToolStripMenuItem, registerPlotMenuItem });
            propertyMenu.Name = "propertyMenu";
            propertyMenu.Size = new Size(59, 32);
            propertyMenu.Text = "File";
            // 
            // registerPlotToolStripMenuItem
            // 
            registerPlotToolStripMenuItem.Name = "registerPlotToolStripMenuItem";
            registerPlotToolStripMenuItem.Size = new Size(206, 26);
            registerPlotToolStripMenuItem.Text = "Manage Property";
            registerPlotToolStripMenuItem.Click += RegisterPlotToolStripMenuItem_Click;
            // 
            // registerPlotMenuItem
            // 
            registerPlotMenuItem.Name = "registerPlotMenuItem";
            registerPlotMenuItem.Size = new Size(206, 26);
            registerPlotMenuItem.Text = "Manage Plots";
            registerPlotMenuItem.Click += ButtonManagePlots_Click;
            // 
            // dataGridViewProperties
            // 
            dataGridViewProperties.AllowUserToAddRows = false;
            dataGridViewProperties.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.AliceBlue;
            dataGridViewProperties.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewProperties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProperties.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewProperties.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewProperties.ColumnHeadersHeight = 29;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.AliceBlue;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewProperties.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewProperties.Location = new Point(20, 86);
            dataGridViewProperties.Name = "dataGridViewProperties";
            dataGridViewProperties.ReadOnly = true;
            dataGridViewProperties.RowHeadersWidth = 51;
            dataGridViewProperties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProperties.Size = new Size(1790, 201);
            dataGridViewProperties.TabIndex = 3;
            dataGridViewProperties.SelectionChanged += DataGridViewProperties_SelectionChanged;
            // 
            // buttonAddProperty
            // 
            buttonAddProperty.BackColor = Color.Green;
            buttonAddProperty.FlatStyle = FlatStyle.Flat;
            buttonAddProperty.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonAddProperty.ForeColor = Color.White;
            buttonAddProperty.Location = new Point(1411, 37);
            buttonAddProperty.Name = "buttonAddProperty";
            buttonAddProperty.Size = new Size(215, 36);
            buttonAddProperty.TabIndex = 2;
            buttonAddProperty.Text = "Register New Property";
            buttonAddProperty.UseVisualStyleBackColor = false;
            buttonAddProperty.Click += ButtonAddProperty_Click;
            // 
            // buttonManagePlots
            // 
            buttonManagePlots.BackColor = Color.Green;
            buttonManagePlots.FlatStyle = FlatStyle.Flat;
            buttonManagePlots.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonManagePlots.ForeColor = Color.White;
            buttonManagePlots.Location = new Point(1653, 37);
            buttonManagePlots.Name = "buttonManagePlots";
            buttonManagePlots.Size = new Size(134, 36);
            buttonManagePlots.TabIndex = 3;
            buttonManagePlots.Text = "Manage Plots";
            buttonManagePlots.UseVisualStyleBackColor = false;
            buttonManagePlots.Click += ButtonManagePlots_Click;
            // 
            // dataGridViewPlots
            // 
            dataGridViewPlots.AllowUserToAddRows = false;
            dataGridViewPlots.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = Color.AliceBlue;
            dataGridViewPlots.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewPlots.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPlots.BackgroundColor = Color.White;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dataGridViewPlots.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewPlots.ColumnHeadersHeight = 29;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.AliceBlue;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle6.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle6.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dataGridViewPlots.DefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewPlots.Location = new Point(20, 81);
            dataGridViewPlots.Name = "dataGridViewPlots";
            dataGridViewPlots.ReadOnly = true;
            dataGridViewPlots.RowHeadersWidth = 51;
            dataGridViewPlots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPlots.Size = new Size(1790, 375);
            dataGridViewPlots.TabIndex = 1;
            // 
            // labelProperties
            // 
            labelProperties.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelProperties.ForeColor = Color.Black;
            labelProperties.Location = new Point(20, 39);
            labelProperties.Name = "labelProperties";
            labelProperties.Size = new Size(400, 34);
            labelProperties.TabIndex = 4;
            labelProperties.Text = "Properties (0)";
            // 
            // groupBoxProperties
            // 
            groupBoxProperties.BackColor = SystemColors.Control;
            groupBoxProperties.Controls.Add(labelProperties);
            groupBoxProperties.Controls.Add(dataGridViewProperties);
            groupBoxProperties.Controls.Add(buttonAddProperty);
            groupBoxProperties.Controls.Add(buttonManagePlots);
            groupBoxProperties.Controls.Add(labelPropertyFilter);
            groupBoxProperties.Controls.Add(textBoxPropertyFilter);
            groupBoxProperties.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxProperties.ForeColor = Color.MidnightBlue;
            groupBoxProperties.Location = new Point(52, 60);
            groupBoxProperties.Name = "groupBoxProperties";
            groupBoxProperties.Padding = new Padding(15);
            groupBoxProperties.Size = new Size(1830, 308);
            groupBoxProperties.TabIndex = 1;
            groupBoxProperties.TabStop = false;
            groupBoxProperties.Text = "Properties";
            // 
            // labelPropertyFilter
            // 
            labelPropertyFilter.Location = new Point(591, 45);
            labelPropertyFilter.Name = "labelPropertyFilter";
            labelPropertyFilter.Size = new Size(84, 31);
            labelPropertyFilter.TabIndex = 5;
            labelPropertyFilter.Text = "Filter:";
            // 
            // textBoxPropertyFilter
            // 
            textBoxPropertyFilter.Location = new Point(681, 42);
            textBoxPropertyFilter.Name = "textBoxPropertyFilter";
            textBoxPropertyFilter.Size = new Size(250, 34);
            textBoxPropertyFilter.TabIndex = 6;
            textBoxPropertyFilter.TextChanged += TextBoxPropertyFilter_TextChanged;
            // 
            // groupBoxPlots
            // 
            groupBoxPlots.BackColor = SystemColors.Control;
            groupBoxPlots.Controls.Add(labelPlots);
            groupBoxPlots.Controls.Add(dataGridViewPlots);
            groupBoxPlots.Controls.Add(labelPlotFilter);
            groupBoxPlots.Controls.Add(textBoxPlotFilter);
            groupBoxPlots.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxPlots.ForeColor = Color.MidnightBlue;
            groupBoxPlots.Location = new Point(52, 396);
            groupBoxPlots.Name = "groupBoxPlots";
            groupBoxPlots.Padding = new Padding(15);
            groupBoxPlots.Size = new Size(1830, 474);
            groupBoxPlots.TabIndex = 2;
            groupBoxPlots.TabStop = false;
            groupBoxPlots.Text = "Plots";
            // 
            // labelPlots
            // 
            labelPlots.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelPlots.ForeColor = Color.Black;
            labelPlots.Location = new Point(20, 42);
            labelPlots.Name = "labelPlots";
            labelPlots.Size = new Size(400, 25);
            labelPlots.TabIndex = 5;
            labelPlots.Text = "Plots (0)";
            // 
            // labelPlotFilter
            // 
            labelPlotFilter.Location = new Point(591, 37);
            labelPlotFilter.Name = "labelPlotFilter";
            labelPlotFilter.Size = new Size(84, 36);
            labelPlotFilter.TabIndex = 6;
            labelPlotFilter.Text = "Filter:";
            // 
            // textBoxPlotFilter
            // 
            textBoxPlotFilter.Location = new Point(681, 39);
            textBoxPlotFilter.Name = "textBoxPlotFilter";
            textBoxPlotFilter.Size = new Size(250, 34);
            textBoxPlotFilter.TabIndex = 7;
            textBoxPlotFilter.TextChanged += TextBoxPlotFilter_TextChanged;
            // 
            // panelMain
            // 
            panelMain.BackColor = SystemColors.Control;
            panelMain.Controls.Add(groupBoxProperties);
            panelMain.Controls.Add(groupBoxPlots);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 36);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1832, 888);
            panelMain.TabIndex = 0;
            // 
            // panelFooter
            // 
            panelFooter.BackColor = Color.Red;
            panelFooter.Controls.Add(footerLabel);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Height = 32;
            panelFooter.Location = new Point(0, 924);
            panelFooter.Name = "panelFooter";
            panelFooter.Size = new Size(1832, 32);
            panelFooter.TabIndex = 1;
            // 
            // footerLabel
            // 
            footerLabel.BackColor = Color.Red;
            footerLabel.Dock = DockStyle.Fill;
            footerLabel.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            footerLabel.ForeColor = Color.White;
            footerLabel.Location = new Point(0, 0);
            footerLabel.Name = "footerLabel";
            footerLabel.Size = new Size(1832, 32);
            footerLabel.TabIndex = 0;
            footerLabel.Text = "©  VVT Softwares Pvt. Ltd. All rights reserved.";
            footerLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LandingForm
            // 
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 255);
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1832, 956);
            Controls.Add(panelFooter);
            Controls.Add(panelMain);
            Controls.Add(menuStripMain);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripMain;
            Name = "LandingForm";
            Text = "Jay Maa Durga Housing Agency";
            WindowState = FormWindowState.Maximized;
            Load += LandingForm_Load;
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlots).EndInit();
            groupBoxProperties.ResumeLayout(false);
            groupBoxProperties.PerformLayout();
            groupBoxPlots.ResumeLayout(false);
            groupBoxPlots.PerformLayout();
            panelMain.ResumeLayout(false);
            panelFooter.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}