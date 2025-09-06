using Microsoft.Data.SqlClient;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;

namespace RealEstateManager.Pages
{
    public partial class PropertyDetailsForm : BaseForm
    {
        private readonly int _propertyId;

        public PropertyDetailsForm(int propertyId)
        {
            InitializeComponent();
            
            _propertyId = propertyId;
            SetGeneratePdfButtonVisibility();
            dataGridViewTransactions.DataBindingComplete += DataGridViewTransactions_DataBindingComplete;
            dataGridViewTransactions.CellPainting += DataGridViewTransactions_CellPainting;
            dataGridViewTransactions.CellMouseClick += DataGridViewTransactions_CellMouseClick;
            LoadPropertyDetails();
        }

        private void SetGeneratePdfButtonVisibility()
        {
            var originalImage = Properties.Resources.pdf;
            var buttonSize = buttonGenerateReport.Size;
            int imgWidth = Math.Max(1, buttonSize.Width - 10);
            int imgHeight = Math.Max(1, buttonSize.Height - 10);
            var scaledImage = new Bitmap(originalImage, imgWidth, imgHeight);
            buttonGenerateReport.Image = scaledImage;
            buttonGenerateReport.ImageAlign = ContentAlignment.MiddleCenter;
            buttonGenerateReport.Text = ""; 
        }

        private void DataGridViewTransactions_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dataGridViewTransactions;

            // Set to None so custom widths are respected
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // Set font styles to match PlotDetailsForm
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Add action column if not already present
            if (!dgv.Columns.Contains("Action"))
            {
                var actionCol = new DataGridViewImageColumn
                {
                    Name = "Action",
                    HeaderText = "Action",
                    Width = 120,
                    ImageLayout = DataGridViewImageCellLayout.Normal
                };
                dgv.Columns.Add(actionCol);
            }

            if (dgv.Columns["TransactionId"] != null)
            {
                dgv.Columns["TransactionId"].HeaderText = "TRN ID";
                dgv.Columns["TransactionId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["TransactionId"].Width = 80;
            }
            if (dgv.Columns["Date"] != null)
            {
                dgv.Columns["Date"].HeaderText = "TRN Date";
                dgv.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Date"].Width = 180;
                dgv.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
            }
            if (dgv.Columns["TransactionType"] != null)
            {
                dgv.Columns["TransactionType"].HeaderText = "TRN Type";
                dgv.Columns["TransactionType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["TransactionType"].Width = 100;
            }
            if (dgv.Columns["Amount"] != null)
            {
                dgv.Columns["Amount"].HeaderText = "Amount";
                dgv.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Amount"].Width = 130;
                dgv.Columns["Amount"].DefaultCellStyle.Format = "C"; // Currency format
            }
            if (dgv.Columns["PaymentMethod"] != null)
            {
                dgv.Columns["PaymentMethod"].HeaderText = "Payment Method";
                dgv.Columns["PaymentMethod"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["PaymentMethod"].Width = 170;
            }
            if (dgv.Columns["ReferenceNumber"] != null)
            {
                dgv.Columns["ReferenceNumber"].HeaderText = "Reference #";
                dgv.Columns["ReferenceNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["ReferenceNumber"].Width = 140;
            }
            if (dgv.Columns["Notes"] != null)
            {
                dgv.Columns["Notes"].HeaderText = "Notes";
                dgv.Columns["Notes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Notes"].Width = 300;
            }
            if (dgv.Columns["Action"] != null)
            {
                dgv.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Action"].Width = 90;
            }
        }

        private void DataGridViewTransactions_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridViewTransactions.Columns[e.ColumnIndex].Name != "Action")
                return;

            var row = dataGridViewTransactions.Rows[e.RowIndex];
            var transactionId = row.Cells["TransactionId"].Value?.ToString();

            // Show a context menu for actions: View, Delete only
            var menu = new ContextMenuStrip();
            menu.Items.Add("View", null, (s, ea) => ViewTransaction(transactionId));
            menu.Items.Add("Delete", null, (s, ea) => DeleteTransaction(transactionId));
            var cellDisplayRectangle = dataGridViewTransactions.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            var location = dataGridViewTransactions.PointToScreen(new Point(cellDisplayRectangle.Left, cellDisplayRectangle.Bottom));
            menu.Show(location);
        }

        private void ViewTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            var form = new RegisterPropertyTransactionForm(transactionId, readOnly: true);
            form.ShowDialog();
        }

        private void DeleteTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            if (MessageBox.Show("Are you sure you want to delete this transaction?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
                string query = "UPDATE PropertyTransaction SET IsDeleted = 1 WHERE TransactionId = @TransactionId";
                try
                {
                    using (var conn = new SqlConnection(connectionString))
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Transaction deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh property details and grid
                    LoadPropertyDetails();

                    // Optionally refresh property grid in LandingForm if open
                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm is LandingForm landingForm)
                        {
                            landingForm.LoadActiveProperties();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting transaction: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadPropertyDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string propertyQuery = @"
                SELECT 
                    Title, Type, Status, Price, Owner, Phone, Address, City, State, ZipCode, Description, KhasraNo, Area,
                    ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0) AS AmountPaid,
                    (Price - ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0)) AS AmountBalance
                FROM Property p
                WHERE Id = @Id AND IsDeleted = 0";
            string transactionQuery = @"
                SELECT 
                    TransactionId,  
                    TransactionDate AS [Date], 
                    Amount, 
                    TransactionType,    -- Debit or Credit
                    PaymentMethod, 
                    ReferenceNumber, 
                    Notes
                FROM PropertyTransaction
                WHERE PropertyId = @Id AND IsDeleted = 0
                ORDER BY TransactionDate DESC";

            using (var conn = new SqlConnection(connectionString))
            using (var propertyCmd = new SqlCommand(propertyQuery, conn))
            using (var transactionCmd = new SqlCommand(transactionQuery, conn))
            {
                propertyCmd.Parameters.AddWithValue("@Id", _propertyId);
                transactionCmd.Parameters.AddWithValue("@Id", _propertyId);

                conn.Open();

                decimal buyPrice = 0;
                decimal amountPaid = 0;
                // Load property details
                using (var reader = propertyCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        labelTitleValue.Text = reader["Title"].ToString();
                        labelTypeValue.Text = reader["Type"].ToString();
                        labelStatusValue.Text = reader["Status"].ToString();
                        labelOwnerValue.Text = reader["Owner"].ToString();
                        labelPhoneValue.Text = reader["Phone"].ToString();
                        labelAddressValue.Text = reader["Address"].ToString();
                        labelCityValue.Text = reader["City"].ToString();
                        labelStateValue.Text = reader["State"].ToString();
                        labelZipValue.Text = reader["ZipCode"].ToString();
                        labelDescriptionValue.Text = reader["Description"].ToString();
                        labelKhasraNoValue.Text = reader["KhasraNo"]?.ToString() ?? "";
                        labelAreaValue.Text = reader["Area"] != DBNull.Value ? Convert.ToDecimal(reader["Area"]).ToString("N2") : "";

                        buyPrice = reader["Price"] is decimal bp ? bp : 0;
                        amountPaid = reader["AmountPaid"] is decimal ap ? ap : 0;
                        labelPropertyBuyPrice.Text = string.Format("{0:C}", buyPrice);
                        labelPropertyAmountPaid.Text = string.Format("{0:C}", amountPaid);
                        labelPropertyBalance.Text = string.Format("{0:C}", amountPaid);
                    }
                }

                // Load property transactions
                using (var adapter = new SqlDataAdapter(transactionCmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewTransactions.DataSource = dt;
                }

                decimal totalSaleAmount = 0;
                decimal totalPaid = 0;
                decimal totalBalance = 0;

                // Load summary details
                string summaryQuery = @"
                    SELECT 
                        (SELECT COUNT(*) FROM Plot WHERE PropertyId = @Id AND IsDeleted = 0) AS TotalPlots,
                        ISNULL((SELECT SUM(SaleAmount) FROM PlotSale ps INNER JOIN Plot p ON ps.PlotId = p.Id WHERE p.PropertyId = @Id AND p.IsDeleted = 0), 0) AS TotalSaleAmount,
                        ISNULL((SELECT SUM(Amount) FROM PlotTransaction pt INNER JOIN Plot p ON pt.PlotId = p.Id WHERE p.PropertyId = @Id AND pt.IsDeleted = 0), 0) AS AmountPaid
                ";
                using (var summaryCmd = new SqlCommand(summaryQuery, conn))
                {
                    summaryCmd.Parameters.AddWithValue("@Id", _propertyId);

                    using (var reader = summaryCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int totalPlots = reader["TotalPlots"] is int tp ? tp : 0;
                            totalSaleAmount = reader["TotalSaleAmount"] is decimal tsa ? tsa : 0;
                            totalPaid = reader["AmountPaid"] is decimal ap ? ap : 0;
                            totalBalance = totalSaleAmount - totalPaid;

                            labelTotalPlotsValue.Text = totalPlots.ToString();
                            labelTotalSaleAmountValue.Text = string.Format("{0:C}", totalSaleAmount);
                            labelTotalPaidValue.Text = string.Format("{0:C}", totalPaid);
                            labelTotalBalanceValue.Text = string.Format("{0:C}", totalBalance);
                        }
                    }
                }

                string brokerageQuery = @"
                    SELECT ISNULL(SUM(BrokerageAmount), 0) AS TotalBrokerage
                    FROM PlotSale ps
                    INNER JOIN Plot p ON ps.PlotId = p.Id
                    WHERE p.PropertyId = @Id AND p.IsDeleted = 0";
                decimal totalBrokerage = 0;
                using (var brokerageCmd = new SqlCommand(brokerageQuery, conn))
                {
                    brokerageCmd.Parameters.AddWithValue("@Id", _propertyId);
                    var result = brokerageCmd.ExecuteScalar();
                    totalBrokerage = result is decimal b ? b : 0;
                    labelTotalBrokerageValue.Text = string.Format("{0:C}", totalBrokerage);
                }

                // --- Loan summary ---
                var (totalLoanPrincipal, totalLoanInterest) = GetLoanSummary(_propertyId);
                labelTotalLoanPrincipalValue.Text = string.Format("{0:C}", totalLoanPrincipal);
                labelTotalLoanInterestValue.Text = string.Format("{0:C}", totalLoanInterest);

                // Assign total loan taken to labelPropertyAmountPaidLoanValue
                labelPropertyAmountPaidLoanValue.Text = string.Format("{0:C}", totalLoanPrincipal);

                // Calculate balance: Buy Price - (Total Loan + Total Cash Paid)
                decimal balance = buyPrice - (totalLoanPrincipal + amountPaid);
                labelPropertyBalance.Text = string.Format("{0:C}", balance);

                // --- Profit/Loss calculations ---
                decimal profitLossAfterLoan = totalSaleAmount - buyPrice - totalBrokerage - totalLoanInterest;

                // Always show as positive currency, color indicates profit/loss
                labelProfitLossAfterLoanValue.Text = Math.Abs(profitLossAfterLoan).ToString("C", CultureInfo.CurrentCulture);

                if (profitLossAfterLoan >= 0)
                    labelProfitLossAfterLoanValue.ForeColor = Color.Green;
                else
                    labelProfitLossAfterLoanValue.ForeColor = Color.Red;
            }
        }

        private static DataTable GetPropertyTransactions(int propertyId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"
                SELECT 
                    TransactionId,  
                    TransactionDate AS [Date], 
                    Amount, 
                    TransactionType,    -- Debit or Credit
                    PaymentMethod, 
                    ReferenceNumber, 
                    Notes
                FROM PropertyTransaction
                WHERE PropertyId = @PropertyId AND IsDeleted = 0
                ORDER BY TransactionDate DESC";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                return dt;
            }
        }

        private void DataGridViewTransactions_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e != null && e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewTransactions.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Only draw view and delete icons
                var viewIcon = Properties.Resources.view;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 12;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;

                // Draw view icon
                if (viewIcon != null && e.Graphics != null)
                    e.Graphics.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw delete icon
                if (deleteIcon != null && e.Graphics != null)
                    e.Graphics.DrawImage(deleteIcon, new Rectangle(x, y, iconWidth, iconHeight));

                e.Handled = true;
            }
        }

        private void DataGridViewTransactions_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewTransactions.Columns[e.ColumnIndex].Name == "Action")
            {
                int iconWidth = 24, padding = 12;
                int x = e.X - padding;
                int iconIndex = x / (iconWidth + padding);

                var row = dataGridViewTransactions.Rows[e.RowIndex];
                var transactionId = row.Cells["TransactionId"].Value?.ToString();

                switch (iconIndex)
                {
                    case 0:
                        ViewTransaction(transactionId);
                        break;
                    case 1:
                        DeleteTransaction(transactionId);
                        break;
                }
            }
        }

        private void ButtonGenerateReport_Click(object sender, EventArgs e)
        {
            // Use property title as file name, sanitized for file system
            string propertyTitle = labelTitleValue.Text?.Trim() ?? "PropertyReport";
            foreach (char c in Path.GetInvalidFileNameChars())
                propertyTitle = propertyTitle.Replace(c, '_');
            string fileName = propertyTitle + ".pdf";

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                sfd.FileName = fileName;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GeneratePdfReport(sfd.FileName);
                    MessageBox.Show("PDF report generated successfully.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void GeneratePdfReport(string filePath)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Property Report";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            int margin = 40;
            double y = margin;

            // --- Header: Logo, Org Name, Address (2 lines) ---
            // Prepare org info
            string orgName = "Jay Maa Durga Housing Agency";
            string orgAddressLine1 = "Building #1, Block #2, Mahakalkar Complex opp. Central Bank of India, Umred Road,";
            string orgAddressLine2 = "Dighori, Nagpur - 440034";

            // Logo
            double logoWidth = 50, logoHeight = 50;
            using (var logoStream = new MemoryStream())
            {
                Properties.Resources.logo.Save(logoStream, ImageFormat.Png);
                logoStream.Position = 0;
                XImage logo = XImage.FromStream(logoStream);
                gfx.DrawImage(logo, margin, margin, logoWidth, logoHeight);
            }

            // Org name (centered, pulled up)
            double orgNameY = margin + 4;
            gfx.DrawString(
                orgName,
                new XFont("Segoe UI", 18, XFontStyleEx.Bold),
                XBrushes.MidnightBlue,
                new XRect(0, orgNameY, page.Width.Point, 24),
                XStringFormats.TopCenter
            );

            // Address line 1 (centered, just below org name)
            double address1Y = orgNameY + 24 + 2;
            gfx.DrawString(
                orgAddressLine1,
                new XFont("Segoe UI", 9, XFontStyleEx.Regular),
                XBrushes.DimGray,
                new XRect(0, address1Y, page.Width.Point, 16),
                XStringFormats.TopCenter
            );

            // Address line 2 (centered, just below line 1)
            double address2Y = address1Y + 16;
            gfx.DrawString(
                orgAddressLine2,
                new XFont("Segoe UI", 9, XFontStyleEx.Regular),
                XBrushes.DimGray,
                new XRect(0, address2Y, page.Width.Point, 16),
                XStringFormats.TopCenter
            );

            // Set y to just below the address lines, with a small gap before the horizontal line
            y = margin + 4 + 24 + 2 + 16 + 16 + 8;

            // --- Light horizontal line before Property Details ---
            XPen lightPen = new XPen(XColors.LightGray, 1);
            gfx.DrawLine(lightPen, margin, y, page.Width.Point - margin, y);
            y += 10;

            // Property Details: 2 columns, titles bold, values aligned
            (string Title, string Value)[] details = {
                ("Title:", labelTitleValue.Text),
                ("Type:", labelTypeValue.Text),
                ("Status:", labelStatusValue.Text),
                ("Owner:", labelOwnerValue.Text),
                ("Phone:", labelPhoneValue.Text),
                ("Address:", labelAddressValue.Text),
                ("City:", labelCityValue.Text),
                ("State:", labelStateValue.Text),
                ("Zip:", labelZipValue.Text),
                ("Khasra No:", labelKhasraNoValue.Text),
                ("Area (sq.ft):", labelAreaValue.Text),
                ("Description:", labelDescriptionValue.Text),
                ("Buy Price:", labelPropertyBuyPrice.Text),
                ("Amount Paid:", labelPropertyAmountPaid.Text),
                ("Amount Balance:", labelPropertyBalance.Text)
            };

            double detailsX = margin;
            double detailsY = y;

            gfx.DrawString("Property Details", new XFont("Segoe UI", 14, XFontStyleEx.Bold), XBrushes.Black,
                new XRect(detailsX, detailsY, page.Width.Point - 2 * margin, 30), XStringFormats.TopLeft);
            detailsY += 28;

            // Split details into 2 columns
            int detailsPerCol = (int)Math.Ceiling(details.Length / 2.0);
            double colWidth = (page.Width.Point - 2 * margin) / 2;
            double leftX = margin;
            double rightX = margin + colWidth;
            double leftY = detailsY;
            double rightY = detailsY;

            // Calculate max title width for each column for alignment
            var titleFont = new XFont("Segoe UI", 10, XFontStyleEx.Bold);
            double leftMaxTitleWidth = 0, rightMaxTitleWidth = 0;
            for (int i = 0; i < details.Length; i++)
            {
                double w = gfx.MeasureString(details[i].Title, titleFont).Width;
                if (i < detailsPerCol)
                {
                    if (w > leftMaxTitleWidth) leftMaxTitleWidth = w;
                }
                else
                {
                    if (w > rightMaxTitleWidth) rightMaxTitleWidth = w;
                }
            }
            double gap = 40;

            for (int i = 0; i < details.Length; i++)
            {
                var (title, value) = details[i];
                if (i < detailsPerCol)
                {
                    double tx = leftX;
                    double ty = leftY;
                    gfx.DrawString(title, titleFont, XBrushes.Black,
                        new XRect(tx, ty, leftMaxTitleWidth, 18), XStringFormats.TopLeft);
                    gfx.DrawString(value, new XFont("Segoe UI", 10), XBrushes.Black,
                        new XRect(tx + leftMaxTitleWidth + gap, ty, colWidth - leftMaxTitleWidth - gap, 18), XStringFormats.TopLeft);
                    leftY += 16;
                }
                else
                {
                    double tx = rightX;
                    double ty = rightY;
                    gfx.DrawString(title, titleFont, XBrushes.Black,
                        new XRect(tx, ty, rightMaxTitleWidth, 18), XStringFormats.TopLeft);
                    gfx.DrawString(value, new XFont("Segoe UI", 10), XBrushes.Black,
                        new XRect(tx + rightMaxTitleWidth + gap, ty, colWidth - rightMaxTitleWidth - gap, 18), XStringFormats.TopLeft);
                    rightY += 16;
                }
            }
            y = Math.Max(leftY, rightY) + 10;

            // --- Light horizontal line before Summary ---
            gfx.DrawLine(lightPen, margin, y, page.Width.Point - margin, y);
            y += 10;

            // Summary: 2 columns, titles bold, values aligned
            (string Title, string Value)[] summary = {
                ("Total Plots:", labelTotalPlotsValue.Text),
                ("Total Sale Amount:", labelTotalSaleAmountValue.Text),
                ("Total Brokerage:", labelTotalBrokerageValue.Text),
                ("Total Loan Principal:", labelTotalLoanPrincipalValue.Text),
                ("Total Loan Interest:", labelTotalLoanInterestValue.Text),
                ("Profit/Loss:", labelProfitLossAfterLoanValue.Text)
            };

            double summaryX = margin;
            double summaryY = y;

            gfx.DrawString("Summary", new XFont("Segoe UI", 14, XFontStyleEx.Bold), XBrushes.Black,
                new XRect(summaryX, summaryY, page.Width.Point - 2 * margin, 30), XStringFormats.TopLeft);
            summaryY += 28;

            // Split summary into 2 columns
            int summaryPerCol = (int)Math.Ceiling(summary.Length / 2.0);
            double summaryColWidth = (page.Width.Point - 2 * margin) / 2;
            double summaryLeftX = margin;
            double summaryRightX = margin + summaryColWidth;
            double summaryLeftY = summaryY;
            double summaryRightY = summaryY;

            // Calculate max title width for each summary column for alignment
            double summaryLeftMaxTitleWidth = 0, summaryRightMaxTitleWidth = 0;
            for (int i = 0; i < summary.Length; i++)
            {
                double w = gfx.MeasureString(summary[i].Title, titleFont).Width;
                if (i < summaryPerCol)
                {
                    if (w > summaryLeftMaxTitleWidth) summaryLeftMaxTitleWidth = w;
                }
                else
                {
                    if (w > summaryRightMaxTitleWidth) summaryRightMaxTitleWidth = w;
                }
            }

            for (int i = 0; i < summary.Length; i++)
            {
                var (title, value) = summary[i];
                if (i < summaryPerCol)
                {
                    double tx = summaryLeftX;
                    double ty = summaryLeftY;
                    gfx.DrawString(title, titleFont, XBrushes.Black,
                        new XRect(tx, ty, summaryLeftMaxTitleWidth, 18), XStringFormats.TopLeft);
                    gfx.DrawString(value, new XFont("Segoe UI", 10), XBrushes.Black,
                        new XRect(tx + summaryLeftMaxTitleWidth + gap, ty, summaryColWidth - summaryLeftMaxTitleWidth - gap, 18), XStringFormats.TopLeft);
                    summaryLeftY += 16;
                }
                else
                {
                    double tx = summaryRightX;
                    double ty = summaryRightY;
                    gfx.DrawString(title, titleFont, XBrushes.Black,
                        new XRect(tx, ty, summaryRightMaxTitleWidth, 18), XStringFormats.TopLeft);
                    gfx.DrawString(value, new XFont("Segoe UI", 10), XBrushes.Black,
                        new XRect(tx + rightMaxTitleWidth + gap, ty, summaryColWidth - rightMaxTitleWidth - gap, 18), XStringFormats.TopLeft);
                    summaryRightY += 16;
                }
            }
            y = Math.Max(summaryLeftY, summaryRightY) + 10;

            // --- Light horizontal line before Transactions ---
            gfx.DrawLine(lightPen, margin, y, page.Width.Point - margin, y);
            y += 10;

            // Transactions grid with borders
            gfx.DrawString("Transactions", new XFont("Segoe UI", 12, XFontStyleEx.Bold), XBrushes.Black,
                new XRect(margin, y, page.Width.Point - 2 * margin, 20), XStringFormats.TopLeft);
            y += 22;

            var visibleColumns = dataGridViewTransactions.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible && c.Name != "Action")
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            // Set custom width for TRN Date column
            int[] colWidths = new int[visibleColumns.Count];
            int totalWidth = (int)(page.Width.Point - 2 * margin);
            int sumWidths = 0;
            for (int i = 0; i < visibleColumns.Count; i++)
            {
                if (visibleColumns[i].Name == "Date")
                    colWidths[i] = 200; // Increased width for TRN Date
                else
                    colWidths[i] = Math.Max(60, visibleColumns[i].Width);
                sumWidths += colWidths[i];
            }
            if (sumWidths > totalWidth)
            {
                double scale = (double)totalWidth / sumWidths;
                for (int i = 0; i < colWidths.Length; i++)
                    colWidths[i] = (int)(colWidths[i] * scale);
            }

            int colX = margin;
            int rowHeight = 20;
            // Draw header row with borders, all headers in bold
            for (int i = 0; i < visibleColumns.Count; i++)
            {
                gfx.DrawRectangle(XPens.Black, colX, y, colWidths[i], rowHeight);
                gfx.DrawString(visibleColumns[i].HeaderText, new XFont("Segoe UI", 9, XFontStyleEx.Bold), XBrushes.Black,
                    new XRect(colX + 2, y + 2, colWidths[i] - 4, rowHeight - 4), XStringFormats.TopLeft);
                colX += colWidths[i];
            }
            y += rowHeight;

            // Draw data rows with borders, format TRN Date column to AM/PM
            int maxRows = 15;
            int rowCount = 0;
            for (int rowIdx = 0; rowIdx < dataGridViewTransactions.Rows.Count; rowIdx++)
            {
                var row = dataGridViewTransactions.Rows[rowIdx];
                if (row.IsNewRow) continue;
                colX = margin;
                for (int i = 0; i < visibleColumns.Count; i++)
                {
                    gfx.DrawRectangle(XPens.Black, colX, y, colWidths[i], rowHeight);
                    var col = visibleColumns[i];
                    string value = row.Cells[col.Name].Value?.ToString() ?? "";

                    // Format TRN Date column to AM/PM
                    if (col.Name == "Date" && row.Cells[col.Name].Value is DateTime dt)
                    {
                        value = dt.ToString("dd/MM/yyyy hh:mm tt");
                    }
                    else if (col.Name == "Date" && DateTime.TryParse(value, out DateTime dt2))
                    {
                        value = dt2.ToString("dd/MM/yyyy hh:mm tt");
                    }

                    gfx.DrawString(value, new XFont("Segoe UI", 9), XBrushes.Black, new XRect(colX + 2, y + 2, colWidths[i] - 4, rowHeight - 4), XStringFormats.TopLeft);
                    colX += colWidths[i];
                }
                y += rowHeight;
                rowCount++;
                if (rowCount >= maxRows) break;
            }

            // Footer
            string footer = "© " + DateTime.Now.Year + " VVT Softwares Pvt. Ltd. All rights reserved.";
            gfx.DrawString(footer, new XFont("Segoe UI", 8), XBrushes.Gray, new XRect(margin, page.Height.Point - margin, page.Width.Point - 2 * margin, 20), XStringFormats.BottomLeft);

            document.Save(filePath);
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }

        private (decimal totalPrincipal, decimal totalInterest) GetLoanSummary(int propertyId)
        {
            decimal totalPrincipal = 0;
            decimal totalInterest = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT LoanAmount, TotalInterest FROM PropertyLoan WHERE PropertyId = @PropertyId AND IsDeleted = 0", conn))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        totalPrincipal += reader.GetDecimal(0);
                        totalInterest += reader.GetDecimal(1);
                    }
                }
            }
            return (totalPrincipal, totalInterest);
        }
    }
}