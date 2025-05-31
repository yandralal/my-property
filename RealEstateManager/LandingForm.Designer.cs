namespace RealEstateManager
{
    partial class LandingForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem propertyMenu;
        private ToolStripMenuItem registerPropertyMenuItem;
        private ToolStripMenuItem registerPlotMenuItem;
        private ToolStripMenuItem viewPropertiesMenuItem;
        private ToolStripMenuItem ownerDetailsMenuItem;
        private ToolStripMenuItem transactionsMenu;
        private ToolStripMenuItem viewTransactionsMenuItem;
        private ToolStripMenuItem registerTransactionMenuItem;
        private ToolStripMenuItem reportsMenu;
        private ToolStripMenuItem viewReportsMenuItem;
        private ToolStripMenuItem helpMenu;
        private ToolStripMenuItem helpMenuItem;
        private ToolStripMenuItem aboutMenuItem;
        private ToolStripMenuItem communicationMenu;
        private ToolStripMenuItem sendMessageToAllMenuItem;
        private ToolStripMenuItem agentOperationsMenu;
        private ToolStripMenuItem assignAgentMenuItem;
        private ToolStripMenuItem scheduleSiteVisitMenuItem;
        private ToolStripMenuItem approveOfferMenuItem;
        private ToolStripMenuItem registerSaleMenuItem;
        private ToolStripMenuItem handoverMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewProperties;
        private System.Windows.Forms.Button buttonAddProperty;
        private System.Windows.Forms.Button buttonManagePlots;

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
            menuStripMain = new MenuStrip();
            propertyMenu = new ToolStripMenuItem();
            registerPropertyMenuItem = new ToolStripMenuItem();
            registerPlotMenuItem = new ToolStripMenuItem();
            viewPropertiesMenuItem = new ToolStripMenuItem();
            ownerDetailsMenuItem = new ToolStripMenuItem();
            registerPlotToolStripMenuItem = new ToolStripMenuItem();
            transactionsMenu = new ToolStripMenuItem();
            viewTransactionsMenuItem = new ToolStripMenuItem();
            registerTransactionMenuItem = new ToolStripMenuItem();
            reportsMenu = new ToolStripMenuItem();
            viewReportsMenuItem = new ToolStripMenuItem();
            communicationMenu = new ToolStripMenuItem();
            sendMessageToAllMenuItem = new ToolStripMenuItem();
            agentOperationsMenu = new ToolStripMenuItem();
            assignAgentMenuItem = new ToolStripMenuItem();
            scheduleSiteVisitMenuItem = new ToolStripMenuItem();
            approveOfferMenuItem = new ToolStripMenuItem();
            registerSaleMenuItem = new ToolStripMenuItem();
            handoverMenuItem = new ToolStripMenuItem();
            helpMenu = new ToolStripMenuItem();
            helpMenuItem = new ToolStripMenuItem();
            aboutMenuItem = new ToolStripMenuItem();
            dataGridViewProperties = new DataGridView();
            buttonAddProperty = new Button();
            buttonManagePlots = new Button();
            menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProperties).BeginInit();
            SuspendLayout();
            // 
            // footerLabel
            // 
            footerLabel.Location = new Point(0, 772);
            footerLabel.Size = new Size(1758, 32);
            // 
            // menuStripMain
            // 
            menuStripMain.BackColor = Color.DodgerBlue;
            menuStripMain.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            menuStripMain.ForeColor = Color.Black;
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { propertyMenu, transactionsMenu, reportsMenu, communicationMenu, agentOperationsMenu, helpMenu });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(1758, 36);
            menuStripMain.TabIndex = 1;
            menuStripMain.Text = "menuStripMain";
            // 
            // propertyMenu
            // 
            propertyMenu.DropDownItems.AddRange(new ToolStripItem[] { registerPropertyMenuItem, registerPlotMenuItem, viewPropertiesMenuItem, ownerDetailsMenuItem, registerPlotToolStripMenuItem });
            propertyMenu.Name = "propertyMenu";
            propertyMenu.Size = new Size(59, 32);
            propertyMenu.Text = "File";
            // 
            // registerPropertyMenuItem
            // 
            registerPropertyMenuItem.Name = "registerPropertyMenuItem";
            registerPropertyMenuItem.Size = new Size(248, 32);
            registerPropertyMenuItem.Text = "Property";
            registerPropertyMenuItem.Click += PropertyMenuItem_Click;
            // 
            // registerPlotMenuItem
            // 
            registerPlotMenuItem.Name = "registerPlotMenuItem";
            registerPlotMenuItem.Size = new Size(248, 32);
            registerPlotMenuItem.Text = "Register Plot";
            registerPlotMenuItem.Click += ButtonManagePlots_Click;
            // 
            // viewPropertiesMenuItem
            // 
            viewPropertiesMenuItem.Name = "viewPropertiesMenuItem";
            viewPropertiesMenuItem.Size = new Size(248, 32);
            viewPropertiesMenuItem.Text = "View Properties";
            // 
            // ownerDetailsMenuItem
            // 
            ownerDetailsMenuItem.Name = "ownerDetailsMenuItem";
            ownerDetailsMenuItem.Size = new Size(248, 32);
            ownerDetailsMenuItem.Text = "Owner Details";
            // 
            // registerPlotToolStripMenuItem
            // 
            registerPlotToolStripMenuItem.Name = "registerPlotToolStripMenuItem";
            registerPlotToolStripMenuItem.Size = new Size(248, 32);
            registerPlotToolStripMenuItem.Text = "Register Plot";
            // 
            // transactionsMenu
            // 
            transactionsMenu.DropDownItems.AddRange(new ToolStripItem[] { viewTransactionsMenuItem, registerTransactionMenuItem });
            transactionsMenu.Name = "transactionsMenu";
            transactionsMenu.Size = new Size(144, 32);
            transactionsMenu.Text = "Transactions";
            // 
            // viewTransactionsMenuItem
            // 
            viewTransactionsMenuItem.Name = "viewTransactionsMenuItem";
            viewTransactionsMenuItem.Size = new Size(291, 32);
            viewTransactionsMenuItem.Text = "View Transactions";
            // 
            // registerTransactionMenuItem
            // 
            registerTransactionMenuItem.Name = "registerTransactionMenuItem";
            registerTransactionMenuItem.Size = new Size(291, 32);
            registerTransactionMenuItem.Text = "Register Transaction";
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
            // 
            // communicationMenu
            // 
            communicationMenu.DropDownItems.AddRange(new ToolStripItem[] { sendMessageToAllMenuItem });
            communicationMenu.Name = "communicationMenu";
            communicationMenu.Size = new Size(175, 32);
            communicationMenu.Text = "Communication";
            // 
            // sendMessageToAllMenuItem
            // 
            sendMessageToAllMenuItem.Name = "sendMessageToAllMenuItem";
            sendMessageToAllMenuItem.Size = new Size(395, 32);
            sendMessageToAllMenuItem.Text = "Send Message to All Customers";
            // 
            // agentOperationsMenu
            // 
            agentOperationsMenu.DropDownItems.AddRange(new ToolStripItem[] { assignAgentMenuItem, scheduleSiteVisitMenuItem, approveOfferMenuItem, registerSaleMenuItem, handoverMenuItem });
            agentOperationsMenu.Name = "agentOperationsMenu";
            agentOperationsMenu.Size = new Size(193, 32);
            agentOperationsMenu.Text = "Agent Operations";
            // 
            // assignAgentMenuItem
            // 
            assignAgentMenuItem.Name = "assignAgentMenuItem";
            assignAgentMenuItem.Size = new Size(279, 32);
            assignAgentMenuItem.Text = "Assign Agent";
            // 
            // scheduleSiteVisitMenuItem
            // 
            scheduleSiteVisitMenuItem.Name = "scheduleSiteVisitMenuItem";
            scheduleSiteVisitMenuItem.Size = new Size(279, 32);
            scheduleSiteVisitMenuItem.Text = "Schedule Site Visit";
            // 
            // approveOfferMenuItem
            // 
            approveOfferMenuItem.Name = "approveOfferMenuItem";
            approveOfferMenuItem.Size = new Size(279, 32);
            approveOfferMenuItem.Text = "Approve Offer";
            // 
            // registerSaleMenuItem
            // 
            registerSaleMenuItem.Name = "registerSaleMenuItem";
            registerSaleMenuItem.Size = new Size(279, 32);
            registerSaleMenuItem.Text = "Register Sale";
            registerSaleMenuItem.Click += registerSaleMenuItem_Click;
            // 
            // handoverMenuItem
            // 
            handoverMenuItem.Name = "handoverMenuItem";
            handoverMenuItem.Size = new Size(279, 32);
            handoverMenuItem.Text = "Handover/Delivery";
            // 
            // helpMenu
            // 
            helpMenu.DropDownItems.AddRange(new ToolStripItem[] { helpMenuItem, aboutMenuItem });
            helpMenu.Name = "helpMenu";
            helpMenu.Size = new Size(70, 32);
            helpMenu.Text = "Help";
            // 
            // helpMenuItem
            // 
            helpMenuItem.Name = "helpMenuItem";
            helpMenuItem.Size = new Size(156, 32);
            helpMenuItem.Text = "Help";
            // 
            // aboutMenuItem
            // 
            aboutMenuItem.Name = "aboutMenuItem";
            aboutMenuItem.Size = new Size(156, 32);
            aboutMenuItem.Text = "About";
            // 
            // dataGridViewProperties
            // 
            dataGridViewProperties.AllowUserToAddRows = false;
            dataGridViewProperties.AllowUserToDeleteRows = false;
            dataGridViewProperties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProperties.ColumnHeadersHeight = 29;
            dataGridViewProperties.Location = new Point(60, 112);
            dataGridViewProperties.Name = "dataGridViewProperties";
            dataGridViewProperties.ReadOnly = true;
            dataGridViewProperties.RowHeadersWidth = 51;
            dataGridViewProperties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProperties.Size = new Size(1579, 218);
            dataGridViewProperties.TabIndex = 3;
            dataGridViewProperties.Visible = false;
            // 
            // buttonAddProperty
            // 
            buttonAddProperty.BackColor = Color.Green;
            buttonAddProperty.FlatStyle = FlatStyle.Flat;
            buttonAddProperty.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonAddProperty.Location = new Point(60, 62);
            buttonAddProperty.Name = "buttonAddProperty";
            buttonAddProperty.Size = new Size(240, 40);
            buttonAddProperty.TabIndex = 2;
            buttonAddProperty.Text = "Register New Property";
            buttonAddProperty.UseVisualStyleBackColor = false;
            buttonAddProperty.Visible = false;
            buttonAddProperty.Click += ButtonAddProperty_Click;
            // 
            // buttonManagePlots
            // 
            buttonManagePlots.Location = new Point(280, 160);
            buttonManagePlots.Name = "buttonManagePlots";
            buttonManagePlots.Size = new Size(180, 35);
            buttonManagePlots.TabIndex = 1;
            buttonManagePlots.Text = "Manage Plots";
            buttonManagePlots.Click += ButtonManagePlots_Click;
            // 
            // LandingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 255);
            ClientSize = new Size(1758, 804);
            Controls.Add(menuStripMain);
            Controls.Add(buttonAddProperty);
            Controls.Add(dataGridViewProperties);
            Controls.Add(buttonManagePlots);
            ForeColor = Color.White;
            MainMenuStrip = menuStripMain;
            Name = "LandingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Landing Page";
            Load += LandingForm_Load;
            Controls.SetChildIndex(buttonManagePlots, 0);
            Controls.SetChildIndex(dataGridViewProperties, 0);
            Controls.SetChildIndex(buttonAddProperty, 0);
            Controls.SetChildIndex(menuStripMain, 0);
            Controls.SetChildIndex(footerLabel, 0);
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProperties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private ToolStripMenuItem registerPlotToolStripMenuItem;
    }
}