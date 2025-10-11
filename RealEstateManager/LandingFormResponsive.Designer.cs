using System.Windows.Forms;
using System.Drawing;

namespace RealEstateManager
{
    partial class LandingFormResponsive
    {
        private TableLayoutPanel mainLayout;
        private GroupBox groupBoxProperties;
        private GroupBox groupBoxPlots;
        private Panel propertiesHeaderPanel;
        private Label labelProperties;
        private Button buttonAddProperty;
        private Button buttonManagePlots;
        private DataGridView dataGridViewProperties;
        private Label labelPlots;
        private DataGridView dataGridViewPlots;
        private Panel panelFooter;
        private Label footerLabel;
        private FlowLayoutPanel propertiesButtonPanel;
        private Label labelWelcomeBanner;

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
        private ToolStripMenuItem operationsMenu;
        private ToolStripMenuItem approveOfferMenuItem;
        private ToolStripMenuItem registerSaleMenuItem;
        private ToolStripMenuItem agentTransactionToolStripMenuItem;
        private ToolStripMenuItem miscTransactionToolStripMenuItem;
        private ToolStripMenuItem changeBackgroundToolStripMenuItem;
        private ToolStripMenuItem downloadUserGuideMenuItem;
        private ToolStripMenuItem propertyLoanTransactionMenuItem;
        private ToolStripMenuItem managePropertyLoansMenuItem;
        private ToolStripMenuItem miscTransactionDetailsMenuItem;

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            mainLayout = new TableLayoutPanel();
            groupBoxProperties = new GroupBox();
            dataGridViewProperties = new DataGridView();
            propertiesHeaderPanel = new Panel();
            propertiesButtonPanel = new FlowLayoutPanel();
            buttonAddProperty = new Button();
            buttonManagePlots = new Button();
            labelProperties = new Label();
            groupBoxPlots = new GroupBox();
            dataGridViewPlots = new DataGridView();
            labelPlots = new Label();
            panelFooter = new Panel();
            footerLabel = new Label();
            menuStripMain = new MenuStrip();
            transactionsMenu = new ToolStripMenuItem();
            propertyTransactionsMenuItem = new ToolStripMenuItem();
            propertyLoanTransactionMenuItem = new ToolStripMenuItem();
            plotTransactionMenuItem = new ToolStripMenuItem();
            agentTransactionToolStripMenuItem = new ToolStripMenuItem();
            miscTransactionToolStripMenuItem = new ToolStripMenuItem();
            operationsMenu = new ToolStripMenuItem();
            registerSaleMenuItem = new ToolStripMenuItem();
            approveOfferMenuItem = new ToolStripMenuItem();
            managePropertyLoansMenuItem = new ToolStripMenuItem();
            miscTransactionDetailsMenuItem = new ToolStripMenuItem();
            reportsMenu = new ToolStripMenuItem();
            viewReportsMenuItem = new ToolStripMenuItem();
            communicationMenu = new ToolStripMenuItem();
            sendMessageToAllMenuItem = new ToolStripMenuItem();
            helpMenu = new ToolStripMenuItem();
            helpMenuItem = new ToolStripMenuItem();
            aboutMenuItem = new ToolStripMenuItem();
            changeBackgroundToolStripMenuItem = new ToolStripMenuItem();
            downloadUserGuideMenuItem = new ToolStripMenuItem();
            labelWelcomeBanner = new Label();
            mainLayout.SuspendLayout();
            groupBoxProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProperties).BeginInit();
            propertiesHeaderPanel.SuspendLayout();
            propertiesButtonPanel.SuspendLayout();
            groupBoxPlots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlots).BeginInit();
            panelFooter.SuspendLayout();
            menuStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1370F));
            mainLayout.Controls.Add(groupBoxProperties, 0, 0);
            mainLayout.Controls.Add(groupBoxPlots, 0, 1);
            mainLayout.Controls.Add(panelFooter, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 85);
            mainLayout.Margin = new Padding(3, 4, 3, 4);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 333F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            mainLayout.Size = new Size(1370, 664);
            mainLayout.TabIndex = 2;
            // 
            // groupBoxProperties
            // 
            groupBoxProperties.Controls.Add(dataGridViewProperties);
            groupBoxProperties.Controls.Add(propertiesHeaderPanel);
            groupBoxProperties.Dock = DockStyle.Fill;
            groupBoxProperties.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxProperties.ForeColor = Color.MidnightBlue;
            groupBoxProperties.Location = new Point(3, 4);
            groupBoxProperties.Margin = new Padding(3, 4, 3, 4);
            groupBoxProperties.Name = "groupBoxProperties";
            groupBoxProperties.Padding = new Padding(11, 13, 11, 13);
            groupBoxProperties.Size = new Size(1364, 325);
            groupBoxProperties.TabIndex = 0;
            groupBoxProperties.TabStop = false;
            groupBoxProperties.Text = "Properties";
            // 
            // dataGridViewProperties
            // 
            dataGridViewProperties.AllowUserToAddRows = false;
            dataGridViewProperties.AllowUserToDeleteRows = false;
            dataGridViewProperties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProperties.BackgroundColor = Color.White;
            dataGridViewProperties.ColumnHeadersHeight = 29;
            dataGridViewProperties.Dock = DockStyle.Fill;
            dataGridViewProperties.Location = new Point(11, 104);
            dataGridViewProperties.Margin = new Padding(3, 4, 3, 4);
            dataGridViewProperties.Name = "dataGridViewProperties";
            dataGridViewProperties.ReadOnly = true;
            dataGridViewProperties.RowHeadersWidth = 51;
            dataGridViewProperties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProperties.Size = new Size(1342, 208);
            dataGridViewProperties.TabIndex = 0;
            // 
            // propertiesHeaderPanel
            // 
            propertiesHeaderPanel.BackColor = SystemColors.Control;
            propertiesHeaderPanel.Controls.Add(propertiesButtonPanel);
            propertiesHeaderPanel.Controls.Add(labelProperties);
            propertiesHeaderPanel.Dock = DockStyle.Top;
            propertiesHeaderPanel.Location = new Point(11, 40);
            propertiesHeaderPanel.Margin = new Padding(3, 4, 3, 4);
            propertiesHeaderPanel.Name = "propertiesHeaderPanel";
            propertiesHeaderPanel.Size = new Size(1342, 64);
            propertiesHeaderPanel.TabIndex = 1;
            // 
            // propertiesButtonPanel
            // 
            propertiesButtonPanel.AutoSize = true;
            propertiesButtonPanel.Controls.Add(buttonAddProperty);
            propertiesButtonPanel.Controls.Add(buttonManagePlots);
            propertiesButtonPanel.Dock = DockStyle.Right;
            propertiesButtonPanel.Location = new Point(966, 0);
            propertiesButtonPanel.Margin = new Padding(0, 0, 10, 0);
            propertiesButtonPanel.Name = "propertiesButtonPanel";
            propertiesButtonPanel.Size = new Size(376, 64);
            propertiesButtonPanel.TabIndex = 0;
            propertiesButtonPanel.WrapContents = false;
            // 
            // buttonAddProperty
            // 
            buttonAddProperty.BackColor = Color.Green;
            buttonAddProperty.FlatAppearance.BorderSize = 0;
            buttonAddProperty.FlatStyle = FlatStyle.Flat;
            buttonAddProperty.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonAddProperty.ForeColor = Color.White;
            buttonAddProperty.Location = new Point(0, 0);
            buttonAddProperty.Margin = new Padding(0, 0, 10, 0);
            buttonAddProperty.Name = "buttonAddProperty";
            buttonAddProperty.Size = new Size(206, 36);
            buttonAddProperty.TabIndex = 1;
            buttonAddProperty.Text = "Register New Property";
            buttonAddProperty.UseVisualStyleBackColor = false;
            buttonAddProperty.Click += ButtonAddProperty_Click;
            // 
            // buttonManagePlots
            // 
            buttonManagePlots.BackColor = Color.Green;
            buttonManagePlots.FlatAppearance.BorderSize = 0;
            buttonManagePlots.FlatStyle = FlatStyle.Flat;
            buttonManagePlots.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonManagePlots.ForeColor = Color.White;
            buttonManagePlots.Location = new Point(216, 0);
            buttonManagePlots.Margin = new Padding(0);
            buttonManagePlots.Name = "buttonManagePlots";
            buttonManagePlots.Size = new Size(160, 36);
            buttonManagePlots.TabIndex = 0;
            buttonManagePlots.Text = "Manage Plots";
            buttonManagePlots.UseVisualStyleBackColor = false;
            buttonManagePlots.Click += ButtonManagePlots_Click;
            // 
            // labelProperties
            // 
            labelProperties.Dock = DockStyle.Left;
            labelProperties.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelProperties.ForeColor = Color.Black;
            labelProperties.Location = new Point(0, 0);
            labelProperties.Name = "labelProperties";
            labelProperties.Size = new Size(457, 64);
            labelProperties.TabIndex = 2;
            labelProperties.Text = "Properties (0)";
            labelProperties.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // groupBoxPlots
            // 
            groupBoxPlots.Controls.Add(dataGridViewPlots);
            groupBoxPlots.Controls.Add(labelPlots);
            groupBoxPlots.Dock = DockStyle.Fill;
            groupBoxPlots.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBoxPlots.ForeColor = Color.MidnightBlue;
            groupBoxPlots.Location = new Point(3, 337);
            groupBoxPlots.Margin = new Padding(3, 4, 3, 4);
            groupBoxPlots.Name = "groupBoxPlots";
            groupBoxPlots.Padding = new Padding(11, 13, 11, 13);
            groupBoxPlots.Size = new Size(1364, 270);
            groupBoxPlots.TabIndex = 1;
            groupBoxPlots.TabStop = false;
            groupBoxPlots.Text = "Plots";
            // 
            // dataGridViewPlots
            // 
            dataGridViewPlots.AllowUserToAddRows = false;
            dataGridViewPlots.AllowUserToDeleteRows = false;
            dataGridViewPlots.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPlots.BackgroundColor = Color.White;
            dataGridViewPlots.ColumnHeadersHeight = 29;
            dataGridViewPlots.Dock = DockStyle.Fill;
            dataGridViewPlots.GridColor = Color.LightGray;
            dataGridViewPlots.Location = new Point(11, 93);
            dataGridViewPlots.Margin = new Padding(3, 4, 3, 4);
            dataGridViewPlots.Name = "dataGridViewPlots";
            dataGridViewPlots.ReadOnly = true;
            dataGridViewPlots.RowHeadersWidth = 51;
            dataGridViewPlots.RowTemplate.Height = 28;
            dataGridViewPlots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPlots.Size = new Size(1342, 164);
            dataGridViewPlots.TabIndex = 0;
            // 
            // labelPlots
            // 
            labelPlots.Dock = DockStyle.Top;
            labelPlots.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelPlots.ForeColor = Color.Black;
            labelPlots.Location = new Point(11, 40);
            labelPlots.Name = "labelPlots";
            labelPlots.Size = new Size(1342, 53);
            labelPlots.TabIndex = 1;
            labelPlots.Text = "Plots (0)";
            labelPlots.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelFooter
            // 
            panelFooter.BackColor = Color.MidnightBlue;
            panelFooter.Controls.Add(footerLabel);
            panelFooter.Dock = DockStyle.Fill;
            panelFooter.Location = new Point(3, 615);
            panelFooter.Margin = new Padding(3, 4, 3, 4);
            panelFooter.Name = "panelFooter";
            panelFooter.Size = new Size(1364, 45);
            panelFooter.TabIndex = 2;
            // 
            // footerLabel
            // 
            footerLabel.Dock = DockStyle.Fill;
            footerLabel.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            footerLabel.ForeColor = Color.White;
            footerLabel.Location = new Point(0, 0);
            footerLabel.Name = "footerLabel";
            footerLabel.Size = new Size(1364, 45);
            footerLabel.TabIndex = 0;
            footerLabel.Text = "© VVT Softwares Pvt. Ltd. All rights reserved.";
            footerLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // menuStripMain
            // 
            menuStripMain.BackColor = SystemColors.Control;
            menuStripMain.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            menuStripMain.ForeColor = SystemColors.ControlText;
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { transactionsMenu, operationsMenu, reportsMenu, communicationMenu, helpMenu });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Padding = new Padding(6, 3, 0, 3);
            menuStripMain.Size = new Size(1370, 38);
            menuStripMain.TabIndex = 1;
            menuStripMain.Text = "menuStripMain";
            // 
            // transactionsMenu
            // 
            transactionsMenu.DropDownItems.AddRange(new ToolStripItem[] { propertyTransactionsMenuItem, propertyLoanTransactionMenuItem, plotTransactionMenuItem, agentTransactionToolStripMenuItem, miscTransactionToolStripMenuItem });
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
            // propertyLoanTransactionMenuItem
            // 
            propertyLoanTransactionMenuItem.Name = "propertyLoanTransactionMenuItem";
            propertyLoanTransactionMenuItem.Size = new Size(232, 32);
            propertyLoanTransactionMenuItem.Text = "Loan";
            propertyLoanTransactionMenuItem.Click += PropertyLoanTransactionMenuItem_Click;
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
            // operationsMenu
            // 
            operationsMenu.DropDownItems.AddRange(new ToolStripItem[] { registerSaleMenuItem, approveOfferMenuItem, managePropertyLoansMenuItem, miscTransactionDetailsMenuItem });
            operationsMenu.Name = "operationsMenu";
            operationsMenu.Size = new Size(130, 32);
            operationsMenu.Text = "Operations";
            // 
            // registerSaleMenuItem
            // 
            registerSaleMenuItem.Name = "registerSaleMenuItem";
            registerSaleMenuItem.Size = new Size(266, 32);
            registerSaleMenuItem.Text = "Sale Plot";
            registerSaleMenuItem.Click += RegisterSaleMenuItem_Click;
            // 
            // approveOfferMenuItem
            // 
            approveOfferMenuItem.Name = "approveOfferMenuItem";
            approveOfferMenuItem.Size = new Size(266, 32);
            approveOfferMenuItem.Text = "Agents";
            approveOfferMenuItem.Click += ViewAllAgentsMenuItem_Click;
            // 
            // managePropertyLoansMenuItem
            // 
            managePropertyLoansMenuItem.Name = "managePropertyLoansMenuItem";
            managePropertyLoansMenuItem.Size = new Size(266, 32);
            managePropertyLoansMenuItem.Text = "Loans";
            managePropertyLoansMenuItem.Click += ManagePropertyLoansMenuItem_Click;
            // 
            // miscTransactionDetailsMenuItem
            // 
            miscTransactionDetailsMenuItem.Name = "miscTransactionDetailsMenuItem";
            miscTransactionDetailsMenuItem.Size = new Size(266, 32);
            miscTransactionDetailsMenuItem.Text = "Misc Transactions";
            miscTransactionDetailsMenuItem.Click += MiscTransactionDetailsMenuItem_Click;
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
            helpMenu.DropDownItems.AddRange(new ToolStripItem[] { helpMenuItem, aboutMenuItem, changeBackgroundToolStripMenuItem, downloadUserGuideMenuItem });
            helpMenu.Name = "helpMenu";
            helpMenu.Size = new Size(70, 32);
            helpMenu.Text = "Help";
            // 
            // helpMenuItem
            // 
            helpMenuItem.Name = "helpMenuItem";
            helpMenuItem.Size = new Size(315, 32);
            helpMenuItem.Text = "Help";
            helpMenuItem.Click += helpMenuItem_Click;
            // 
            // aboutMenuItem
            // 
            aboutMenuItem.Name = "aboutMenuItem";
            aboutMenuItem.Size = new Size(315, 32);
            aboutMenuItem.Text = "About";
            aboutMenuItem.Click += aboutMenuItem_Click;
            // 
            // changeBackgroundToolStripMenuItem
            // 
            changeBackgroundToolStripMenuItem.Name = "changeBackgroundToolStripMenuItem";
            changeBackgroundToolStripMenuItem.Size = new Size(315, 32);
            changeBackgroundToolStripMenuItem.Text = "Customize Background";
            changeBackgroundToolStripMenuItem.Click += ChangeBackgroundToolStripMenuItem_Click;
            // 
            // downloadUserGuideMenuItem
            // 
            downloadUserGuideMenuItem.Name = "downloadUserGuideMenuItem";
            downloadUserGuideMenuItem.Size = new Size(315, 32);
            downloadUserGuideMenuItem.Text = "Download User Guide";
            downloadUserGuideMenuItem.Click += DownloadUserGuideMenuItem_Click;
            // 
            // labelWelcomeBanner
            // 
            labelWelcomeBanner.BackColor = Color.MidnightBlue;
            labelWelcomeBanner.Dock = DockStyle.Top;
            labelWelcomeBanner.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelWelcomeBanner.ForeColor = Color.White;
            labelWelcomeBanner.Location = new Point(0, 38);
            labelWelcomeBanner.Margin = new Padding(0);
            labelWelcomeBanner.Name = "labelWelcomeBanner";
            labelWelcomeBanner.Size = new Size(1370, 47);
            labelWelcomeBanner.TabIndex = 3;
            labelWelcomeBanner.Text = "Welcome to Jay Maa Durga Housing Agency";
            labelWelcomeBanner.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LandingFormResponsive
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 255);
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1370, 749);
            Controls.Add(mainLayout);
            Controls.Add(labelWelcomeBanner);
            Controls.Add(menuStripMain);
            ForeColor = Color.White;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1168, 715);
            Name = "LandingFormResponsive";
            Text = "Jay Maa Durga Housing Agency (Responsive)";
            WindowState = FormWindowState.Maximized;
            mainLayout.ResumeLayout(false);
            groupBoxProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewProperties).EndInit();
            propertiesHeaderPanel.ResumeLayout(false);
            propertiesHeaderPanel.PerformLayout();
            propertiesButtonPanel.ResumeLayout(false);
            groupBoxPlots.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlots).EndInit();
            panelFooter.ResumeLayout(false);
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
