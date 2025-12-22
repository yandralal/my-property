using System.Windows.Forms;
using System.Drawing;

namespace RealEstateManager
{
    public partial class HomeForm : Form
    {
        private SplitContainer splitContainerMain;
        private Panel leftPanel;
        private Panel rightPanel;
        private Label labelWelcomeBanner;
        private Panel panelFooter;
        private Label footerLabel;
        private DataGridView dataGridViewProperties;
        private DataGridView dataGridViewPlots;

        private void InitializeComponent()
        {
            splitContainerMain = new SplitContainer();
            labelWelcomeBanner = new Label();
            leftPanel = new Panel();
            rightPanel = new Panel();
            panelFooter = new Panel();
            footerLabel = new Label();
            dataGridViewProperties = new DataGridView();
            dataGridViewPlots = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            panelFooter.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Location = new Point(0, labelWelcomeBanner.Height);
            splitContainerMain.Name = "splitContainerMain";
            splitContainerMain.Size = new Size(1200, 500);
            splitContainerMain.SplitterDistance = (int)(splitContainerMain.Width * 0.65); // 65% left, 35% right
            splitContainerMain.IsSplitterFixed = false;
            splitContainerMain.TabIndex = 0;
            splitContainerMain.Panel1.Controls.Add(leftPanel);
            splitContainerMain.Panel2.Controls.Add(rightPanel);
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(leftPanel);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(rightPanel);
            splitContainerMain.Size = new Size(1371, 708);
            splitContainerMain.SplitterDistance = (int)(splitContainerMain.Width * 0.65); // 65% left, 35% right
            splitContainerMain.SplitterWidth = 5;
            splitContainerMain.TabIndex = 0;
            // 
            // labelWelcomeBanner
            // 
            labelWelcomeBanner.BackColor = Color.MidnightBlue;
            labelWelcomeBanner.Dock = DockStyle.Top;
            labelWelcomeBanner.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelWelcomeBanner.ForeColor = Color.White;
            labelWelcomeBanner.Location = new Point(0, 0);
            labelWelcomeBanner.Name = "labelWelcomeBanner";
            labelWelcomeBanner.Size = new Size(1371, 47);
            labelWelcomeBanner.TabIndex = 1;
            labelWelcomeBanner.Text = "Welcome to Jay Maa Durga Housing Agency";
            labelWelcomeBanner.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // leftPanel
            // 
            leftPanel.BackColor = Color.White;
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.Location = new Point(0, 0);
            leftPanel.Margin = new Padding(3, 4, 3, 4);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(1337, 708);
            leftPanel.TabIndex = 0;
            leftPanel.Controls.Clear();
            // Add left margin for leftPanel
            leftPanel.Padding = new Padding(20, 20, 20, 20);

            // Set equal width for each button and apply standard styling for controls
            int buttonWidth = 180;
            int buttonHeight = 36;
            Font buttonFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            Color buttonBackColor = Color.ForestGreen;
            Color buttonForeColor = Color.White;
            Color groupBoxBackColor = Color.AliceBlue;
            Font groupBoxFont = new Font("Segoe UI", 12F, FontStyle.Bold);
            Color groupBoxForeColor = Color.MidnightBlue;
            Font labelFont = new Font("Segoe UI", 16F, FontStyle.Bold);
            Color labelBackColor = Color.MidnightBlue;
            Color labelForeColor = Color.White;

            // Properties GroupBox
            var groupBoxProperties = new GroupBox {
                Name = "groupBoxProperties",
                Text = "Properties",
                Font = groupBoxFont,
                ForeColor = groupBoxForeColor,
                BackColor = groupBoxBackColor,
                Dock = DockStyle.Top,
                Height = 220, // Increased height for better visibility
                Padding = new Padding(10, 15, 10, 10),
                Margin = new Padding(0, 0, 0, 32) // Add bottom margin
            };
            // 
            // dataGridViewProperties
            // 
            dataGridViewProperties.AllowUserToAddRows = false;
            dataGridViewProperties.AllowUserToDeleteRows = false;
            dataGridViewProperties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProperties.BackgroundColor = Color.White;
            dataGridViewProperties.ColumnHeadersHeight = 29;
            dataGridViewProperties.Dock = DockStyle.Fill;
            dataGridViewProperties.Location = new Point(10, 80);
            dataGridViewProperties.Name = "dataGridViewProperties";
            dataGridViewProperties.ReadOnly = true;
            dataGridViewProperties.RowHeadersWidth = 51;
            dataGridViewProperties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProperties.Size = new Size(1173, 154);
            dataGridViewProperties.TabIndex = 0;
            dataGridViewProperties.Font = new Font("Segoe UI", 9F);
            dataGridViewProperties.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle {
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.MidnightBlue,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dataGridViewProperties.DefaultCellStyle = new DataGridViewCellStyle {
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Black,
                BackColor = Color.White
            };
            dataGridViewProperties.RowTemplate.Height = 22;

            groupBoxProperties.Controls.Add(dataGridViewProperties);

            // Plots GroupBox
            var groupBoxPlots = new GroupBox {
                Name = "groupBoxPlots",
                Text = "Plots",
                Font = groupBoxFont,
                ForeColor = groupBoxForeColor,
                BackColor = groupBoxBackColor,
                Dock = DockStyle.Top,
                Height = 400, // Increased height for better visibility
                Padding = new Padding(10, 15, 10, 20),
                Margin = new Padding(0, 0, 0, 16) // Add bottom margin
            };
            dataGridViewPlots.AllowUserToAddRows = false;
            dataGridViewPlots.AllowUserToDeleteRows = false;
            dataGridViewPlots.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPlots.BackgroundColor = Color.White;
            dataGridViewPlots.ColumnHeadersHeight = 29;
            dataGridViewPlots.Dock = DockStyle.Fill;
            dataGridViewPlots.GridColor = Color.LightGray;
            dataGridViewPlots.Location = new Point(10, 72);
            dataGridViewPlots.Name = "dataGridViewPlots";
            dataGridViewPlots.ReadOnly = true;
            dataGridViewPlots.RowHeadersWidth = 51;
            dataGridViewPlots.RowTemplate.Height = 28;
            dataGridViewPlots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPlots.Size = new Size(1173, 330); // Increased height for grid
            dataGridViewPlots.TabIndex = 0;

            dataGridViewPlots.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.MidnightBlue,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dataGridViewPlots.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Black,
                BackColor = Color.White
            };
            dataGridViewPlots.RowTemplate.Height = 28;

            groupBoxPlots.Controls.Add(dataGridViewPlots);

            // Actions GroupBox
            var groupBoxActions = new GroupBox {
                Text = "Actions",
                Font = groupBoxFont,
                ForeColor = groupBoxForeColor,
                BackColor = groupBoxBackColor,
                Dock = DockStyle.Top,
                Height = 210,
                Padding = new Padding(10, 15, 10, 20),
                Margin = new Padding(0, 0, 0, 16)
            };
            var actionsPanel = new FlowLayoutPanel {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = false,
                WrapContents = true,
                AutoScroll = true,
                Height = groupBoxActions.Height - 20,
                Padding = new Padding(5)
            };
            string[] buttonNames = {
                "Add Property",
                "Manage Plots",
                "Sale Plot",
                "Add Lender",
                "Add Agent",
                "Property Transaction",
                "Plot Transaction",
                "Loan Transaction",
                "Agent Transaction",
                "Misc Transaction",
                "View Reports",
                "Settings"
            };
            actionsPanel.Controls.Clear();
            foreach (var name in buttonNames) {
                var btn = new Button {
                    Text = name,
                    Font = buttonFont,
                    BackColor = buttonBackColor,
                    ForeColor = buttonForeColor,
                    Margin = new Padding(5),
                    AutoSize = false,
                    Height = buttonHeight,
                    Width = buttonWidth,
                    FlatStyle = FlatStyle.Flat
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.DarkGreen;
                btn.FlatAppearance.MouseDownBackColor = Color.Green;
                actionsPanel.Controls.Add(btn);
            }
            groupBoxActions.Controls.Add(actionsPanel);

            // Welcome Banner Styling
            labelWelcomeBanner.Font = labelFont;
            labelWelcomeBanner.BackColor = labelBackColor;
            labelWelcomeBanner.ForeColor = labelForeColor;
            labelWelcomeBanner.TextAlign = ContentAlignment.MiddleCenter;

            // Footer Styling
            footerLabel.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            footerLabel.ForeColor = Color.White;
            footerLabel.TextAlign = ContentAlignment.MiddleCenter;
            panelFooter.BackColor = Color.MidnightBlue;

            // Add groupboxes to leftPanel
            leftPanel.Controls.Add(groupBoxActions);
            leftPanel.Controls.Add(groupBoxPlots);
            leftPanel.Controls.Add(groupBoxProperties);
            // 
            // rightPanel
            // 
            rightPanel.BackColor = Color.White;
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(0, 0);
            rightPanel.Margin = new Padding(3, 4, 3, 4);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new Size(29, 708);
            rightPanel.TabIndex = 0;
            rightPanel.Padding = new Padding(20, 20, 20, 20);

            // Selected Property GroupBox for rightPanel
            var groupBoxSelectedProperty = new GroupBox {
                Text = "Property Details",
                Font = groupBoxFont,
                ForeColor = groupBoxForeColor,
                BackColor = groupBoxBackColor,
                Dock = DockStyle.Top,
                Height = 240,
                Padding = new Padding(10, 15, 10, 10),
                Margin = new Padding(0, 0, 0, 16)
            };
            var propertyDetailsTableLeft = new TableLayoutPanel {
                Dock = DockStyle.Left,
                ColumnCount = 2,
                RowCount = 5,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                AutoSize = true,
                Width = 350
            };
            propertyDetailsTableLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            propertyDetailsTableLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            for (int i = 0; i < 5; i++)
                propertyDetailsTableLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            propertyDetailsTableLeft.Controls.Add(new Label { Text = "Type", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 0);
            propertyDetailsTableLeft.Controls.Add(new Label { Name = "lblPropertyType", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 0);
            propertyDetailsTableLeft.Controls.Add(new Label { Text = "Status", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 1);
            propertyDetailsTableLeft.Controls.Add(new Label { Name = "lblPropertyStatus", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 1);
            propertyDetailsTableLeft.Controls.Add(new Label { Text = "Amount Paid", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 2);
            propertyDetailsTableLeft.Controls.Add(new Label { Name = "lblAmountPaid", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 2);
            propertyDetailsTableLeft.Controls.Add(new Label { Text = "Total Brokerage", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 3);
            propertyDetailsTableLeft.Controls.Add(new Label { Name = "lblTotalBrokerage", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 3);
            propertyDetailsTableLeft.Controls.Add(new Label { Text = "Address", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 4);
            propertyDetailsTableLeft.Controls.Add(new Label { Name = "lblPropertyAddress", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 4);

            var propertyDetailsTableRight = new TableLayoutPanel {
                Dock = DockStyle.Right,
                ColumnCount = 2,
                RowCount = 3,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                AutoSize = true,
                Width = 350
            };
            propertyDetailsTableRight.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            propertyDetailsTableRight.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            for (int i = 0; i < 3; i++)
                propertyDetailsTableRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            propertyDetailsTableRight.Controls.Add(new Label { Text = "Loan Amount", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 0);
            propertyDetailsTableRight.Controls.Add(new Label { Name = "lblTotalLoanPrinciple", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 0);
            propertyDetailsTableRight.Controls.Add(new Label { Text = "Outstanding", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 1);
            propertyDetailsTableRight.Controls.Add(new Label { Name = "lblAmountBalance", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 1);
            propertyDetailsTableRight.Controls.Add(new Label { Text = "Total Plots", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 2);
            propertyDetailsTableRight.Controls.Add(new Label { Name = "lblTotalPlots", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 2);

            var propertyDetailsPanel = new Panel {
                Dock = DockStyle.Fill,
                Padding = new Padding(0)
            };
            propertyDetailsPanel.Controls.Add(propertyDetailsTableLeft);
            propertyDetailsPanel.Controls.Add(propertyDetailsTableRight);
            groupBoxSelectedProperty.Controls.Clear();
            groupBoxSelectedProperty.Controls.Add(propertyDetailsPanel);

            // Selected Plot GroupBox for rightPanel
            var groupBoxSelectedPlot = new GroupBox {
                Text = "Plot Details",
                Font = groupBoxFont,
                ForeColor = groupBoxForeColor,
                BackColor = groupBoxBackColor,
                Dock = DockStyle.Top,
                Height = 220,
                Padding = new Padding(10, 15, 10, 10),
                Margin = new Padding(0, 0, 0, 16)
            };
            var plotDetailsTableLeft = new TableLayoutPanel {
                Dock = DockStyle.Left,
                ColumnCount = 2,
                RowCount = 4,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                AutoSize = true,
                Width = 350
            };
            plotDetailsTableLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            plotDetailsTableLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            for (int i = 0; i < 4; i++)
                plotDetailsTableLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            plotDetailsTableLeft.Controls.Add(new Label { Text = "Plot No", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 0);
            plotDetailsTableLeft.Controls.Add(new Label { Name = "lblPlotNo", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 0);
            plotDetailsTableLeft.Controls.Add(new Label { Text = "Location", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 1);
            plotDetailsTableLeft.Controls.Add(new Label { Name = "lblPlotLocation", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 1);
            plotDetailsTableLeft.Controls.Add(new Label { Text = "Size", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 2);
            plotDetailsTableLeft.Controls.Add(new Label { Name = "lblPlotSize", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 2);
            plotDetailsTableLeft.Controls.Add(new Label { Text = "Price", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 3);
            plotDetailsTableLeft.Controls.Add(new Label { Name = "lblPlotPrice", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 3);

            var plotDetailsTableRight = new TableLayoutPanel {
                Dock = DockStyle.Right,
                ColumnCount = 2,
                RowCount = 4,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                AutoSize = true,
                Width = 350
            };
            plotDetailsTableRight.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            plotDetailsTableRight.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            for (int i = 0; i < 4; i++)
                plotDetailsTableRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            plotDetailsTableRight.Controls.Add(new Label { Text = "Status", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 0);
            plotDetailsTableRight.Controls.Add(new Label { Name = "lblPlotStatus", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 0);
            plotDetailsTableRight.Controls.Add(new Label { Text = "Email", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 1);
            plotDetailsTableRight.Controls.Add(new Label { Name = "lblPlotEmail", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 1);
            plotDetailsTableRight.Controls.Add(new Label { Text = "Amount Paid", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 2);
            plotDetailsTableRight.Controls.Add(new Label { Name = "lblPlotAmountPaid", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 2);
            plotDetailsTableRight.Controls.Add(new Label { Text = "Balance", Font = new Font(buttonFont, FontStyle.Bold), AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.DarkBlue }, 0, 3);
            plotDetailsTableRight.Controls.Add(new Label { Name = "lblPlotBalance", Font = buttonFont, AutoSize = true, Padding = new Padding(2, 6, 2, 6), ForeColor = Color.Black }, 1, 3);

            var plotDetailsPanel = new Panel {
                Dock = DockStyle.Fill,
                Padding = new Padding(0)
            };
            plotDetailsPanel.Controls.Add(plotDetailsTableLeft);
            plotDetailsPanel.Controls.Add(plotDetailsTableRight);
            groupBoxSelectedPlot.Controls.Clear();
            groupBoxSelectedPlot.Controls.Add(plotDetailsPanel);

            // Profit/Loss GroupBox for rightPanel
            var groupBoxProfitLoss = new GroupBox {
                Text = "Profit / Loss",
                Font = groupBoxFont,
                ForeColor = groupBoxForeColor,
                BackColor = groupBoxBackColor,
                Dock = DockStyle.Top,
                Height = 70,
                Padding = new Padding(10, 15, 10, 10),
                Margin = new Padding(0, 0, 0, 16)
            };
            var lblProfitLoss = new Label {
                Name = "lblProfitLoss",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.DarkGreen,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(2, 8, 2, 8),
                Text = "--"
            };
            groupBoxProfitLoss.Controls.Add(lblProfitLoss);

            // Add groupboxes to rightPanel
            rightPanel.Controls.Add(groupBoxProfitLoss);
            rightPanel.Controls.Add(groupBoxSelectedPlot);
            rightPanel.Controls.Add(groupBoxSelectedProperty);
            // 
            // panelFooter
            // 
            panelFooter.BackColor = Color.MidnightBlue;
            panelFooter.Controls.Add(footerLabel);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Location = new Point(0, 755);
            panelFooter.Margin = new Padding(3, 4, 3, 4);
            panelFooter.Name = "panelFooter";
            panelFooter.Size = new Size(1371, 45);
            panelFooter.TabIndex = 3;
            // 
            // footerLabel
            // 
            footerLabel.Dock = DockStyle.Fill;
            footerLabel.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            footerLabel.ForeColor = Color.White;
            footerLabel.Location = new Point(0, 0);
            footerLabel.Name = "footerLabel";
            footerLabel.Size = new Size(1371, 45);
            footerLabel.TabIndex = 0;
            footerLabel.Text = "© VVT Softwares Pvt. Ltd. All rights reserved.";
            footerLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 255);
            ClientSize = new Size(1371, 800);
            Controls.Add(splitContainerMain);
            Controls.Add(labelWelcomeBanner);
            Controls.Add(panelFooter);
            ForeColor = Color.White;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1168, 712);
            Name = "HomeForm";
            Text = "Jay Maa Durga Housing Agency (Home)";
            WindowState = FormWindowState.Maximized;
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            panelFooter.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
