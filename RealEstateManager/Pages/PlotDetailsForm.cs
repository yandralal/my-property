using Microsoft.Data.SqlClient;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace RealEstateManager.Pages
{
    public partial class PlotDetailsForm : BaseForm
    {
        private readonly int _plotId;

        public PlotDetailsForm(int plotId)
        {
            InitializeComponent();            
            _plotId = plotId;
            LoadPlotDetails();
            dataGridViewTransactions.DataBindingComplete += DataGridViewTransactions_DataBindingComplete;
            dataGridViewTransactions.CellPainting += DataGridViewTransactions_CellPainting;
            dataGridViewTransactions.CellMouseClick += DataGridViewTransactions_CellMouseClick;
        }

        private static DataTable GetPlotTransactions(int plotId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
            SELECT 
                TransactionId,
                TransactionDate,
                TransactionType,      -- Add this line
                Amount,
                PaymentMethod,
                ReferenceNumber,
                Notes
            FROM PlotTransaction
            WHERE PlotId = @PlotId AND IsDeleted = 0
            ORDER BY TransactionDate ASC";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PlotId", plotId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                return dt;
            }
        }

        private void LoadPlotDetails()
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
                SELECT 
                    p.Id, p.PlotNumber, p.Status, p.Area, p.PropertyId,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.IsDeleted,
                    ps.SaleDate, ps.SaleAmount, 
                    ps.CustomerName, ps.CustomerPhone, ps.CustomerEmail
                FROM Plot p
                LEFT JOIN PlotSale ps ON p.Id = ps.PlotId
                WHERE p.Id = @PlotId";

            // Variables to hold values for use outside the reader scope
            string customerName = "";
            string customerPhone = "";
            string customerEmail = "";
            string saleAmountStr = "";
            decimal amountPaid = 0;
            decimal balance = 0;

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PlotId", _plotId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        labelPlotId.Text = reader["Id"].ToString();
                        labelPlotNumber.Text = reader["PlotNumber"].ToString();
                        labelStatus.Text = reader["Status"].ToString();
                        labelArea.Text = reader["Area"].ToString();
                        labelCreatedBy.Text = reader["CreatedBy"].ToString();
                        labelCreatedDate.Text = reader["CreatedDate"] is DateTime cd ? cd.ToString("g") : "";
                        labelModifiedBy.Text = reader["ModifiedBy"].ToString();
                        labelModifiedDate.Text = reader["ModifiedDate"] is DateTime md ? md.ToString("g") : "";
                        labelIsDeleted.Text = reader["IsDeleted"] is bool isDel ? isDel.ToString() : "";
                        labelSaleDate.Text = reader["SaleDate"] is DateTime sd ? sd.ToShortDateString() : "";

                        customerName = reader["CustomerName"]?.ToString() ?? "";
                        customerPhone = reader["CustomerPhone"]?.ToString() ?? "";
                        customerEmail = reader["CustomerEmail"]?.ToString() ?? "";
                        saleAmountStr = reader["SaleAmount"]?.ToString() ?? "";

                        // Get paid amount and balance from PlotTransaction
                        string transQuery = @"
                            SELECT ISNULL(SUM(Amount), 0) AS PaidAmount
                            FROM PlotTransaction
                            WHERE PlotId = @PlotId AND IsDeleted = 0";
                        using (var transConn = new SqlConnection(connectionString))
                        using (var transCmd = new SqlCommand(transQuery, transConn))
                        {
                            transCmd.Parameters.AddWithValue("@PlotId", _plotId);
                            transConn.Open();
                            var paidObj = transCmd.ExecuteScalar();
                            amountPaid = paidObj != DBNull.Value ? Convert.ToDecimal(paidObj) : 0;
                        }
                        balance = (reader["SaleAmount"] is decimal sa ? sa : 0) - amountPaid;

                        labelPaidAmount.Text = amountPaid.ToString("N2");
                        labelBalanceAmount.Text = balance.ToString("N2");

                        // Format paid amount and balance as currency
                        labelPaidAmount.Text = string.Format("{0:C}", amountPaid);
                        labelBalanceAmount.Text = string.Format("{0:C}", balance);
                    }
                }
            }
            var (agentName, totalBrokerage, brokeragePaid, brokerageBalance) = DisplayAgentBrokerageDetails(_plotId);

            labelAgentName.Text = agentName;
            labelTotalBrokerage.Text = string.Format("{0:C}", totalBrokerage); 
            labelBrokeragePaid.Text = string.Format("{0:C}", brokeragePaid);
            labelBrokerageBalance.Text = string.Format("{0:C}", brokerageBalance); 

            // Load transactions and bind to grid
            var transactions = GetPlotTransactions(_plotId);
            dataGridViewTransactions.DataSource = transactions;
            dataGridViewTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set label texts with prefix
            labelCustomerName.Text = customerName;
            labelCustomerPhone.Text = customerPhone;
            labelCustomerEmail.Text = customerEmail;
            labelSaleAmount.Text = decimal.TryParse(saleAmountStr, out var saleAmount) ? string.Format("{0:C}", saleAmount) : "0.00";
            labelPaidAmount.Text = amountPaid.ToString("N2");
            labelBalanceAmount.Text = balance.ToString("N2");
            labelPaidAmount.Text = string.Format("{0:C}", amountPaid);
            labelBalanceAmount.Text = string.Format("{0:C}", balance);
        }

        private void DataGridViewTransactions_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dataGridViewTransactions;

            // Set to None so custom widths are respected
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

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
                dgv.Columns["TransactionId"].HeaderText = "TRN #";
                dgv.Columns["TransactionId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["TransactionId"].Width = 80;
            }
            if (dgv.Columns["TransactionDate"] != null)
            {
                dgv.Columns["TransactionDate"].HeaderText = "TRN Date";
                dgv.Columns["TransactionDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["TransactionDate"].Width = 180;
                dgv.Columns["TransactionDate"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
            }
            if (dgv.Columns["TransactionType"] != null)
            {
                dgv.Columns["TransactionType"].HeaderText = "TRN Type";
                dgv.Columns["TransactionType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["TransactionType"].Width = 120;
            }
            if (dgv.Columns["Amount"] != null)
            {
                dgv.Columns["Amount"].HeaderText = "Amount";
                dgv.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Amount"].Width = 150;
                dgv.Columns["Amount"].DefaultCellStyle.Format = "C"; 
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
                dgv.Columns["ReferenceNumber"].Width = 160;
            }
            if (dgv.Columns["Notes"] != null)
            {
                dgv.Columns["Notes"].HeaderText = "Notes";
                dgv.Columns["Notes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Notes"].Width = 250;
            }
            if (dgv.Columns["Action"] != null)
            {
                dgv.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Action"].Width = 120;
            }
        }

        private void DataGridViewTransactions_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewTransactions.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Load your icons from resources
                var viewIcon = Properties.Resources.view;
                var editIcon = Properties.Resources.edit;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 12;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;

                // Safely draw icons only if not null
                if (viewIcon != null && e.Graphics != null)
                {
                    e.Graphics.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                }
                x += iconWidth + padding;

                if (editIcon != null)
                {
                    e.Graphics.DrawImage(editIcon, new Rectangle(x, y, iconWidth, iconHeight));
                }
                x += iconWidth + padding;

                if (deleteIcon != null)
                {
                    e.Graphics.DrawImage(deleteIcon, new Rectangle(x, y, iconWidth, iconHeight));
                }

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
                        EditTransaction(transactionId);
                        break;
                    case 2:
                        DeleteTransaction(transactionId);
                        break;
                }
            }
        }

        private static void ViewTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            var form = new RegisterPlotTransactionForm(transactionId, readOnly: true);
            form.ShowDialog();
        }

        private void EditTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            var form = new RegisterPlotTransactionForm(transactionId, readOnly: false);
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Optionally refresh the grid here
                var transactions = GetPlotTransactions(_plotId);
                dataGridViewTransactions.DataSource = transactions;
            }
        }

        private void DeleteTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            if (MessageBox.Show("Are you sure you want to delete this transaction?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
                string query = "UPDATE PlotTransaction SET IsDeleted = 1 WHERE TransactionId = @TransactionId";
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

                    var transactions = GetPlotTransactions(_plotId);
                    dataGridViewTransactions.DataSource = transactions;

                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm is LandingForm landingForm)
                        {
                            int? propertyId = null;
                            string propertyIdQuery = "SELECT PropertyId FROM Plot WHERE Id = @PlotId";
                            using (var conn = new SqlConnection(connectionString))
                            using (var cmd = new SqlCommand(propertyIdQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@PlotId", _plotId);
                                conn.Open();
                                var result = cmd.ExecuteScalar();
                                if (result != null && result != DBNull.Value)
                                {
                                    propertyId = Convert.ToInt32(result);
                                }
                            }
                            if (propertyId.HasValue)
                            {
                                landingForm.LoadPlotsForProperty(propertyId.Value);
                            }
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

        private void ButtonGenerateReport_Click(object? sender, EventArgs e)
        {
            // Use property name and plot number as file name, sanitized for file system
            string propertyName = ""; // Default if not found
            // Try to get property name from database if not available on the form
            if (labelPlotId.Text is string plotIdStr && int.TryParse(plotIdStr, out int plotId))
            {
                string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
                string query = "SELECT pr.Title FROM Property pr INNER JOIN Plot p ON pr.Id = p.PropertyId WHERE p.Id = @PlotId";
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        propertyName = result.ToString() ?? "";
                }
            }
            if (string.IsNullOrWhiteSpace(propertyName))
                propertyName = "Property";

            string plotNumber = labelPlotNumber.Text?.Trim() ?? "Plot";
            // Sanitize both property name and plot number
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                propertyName = propertyName.Replace(c, '_');
                plotNumber = plotNumber.Replace(c, '_');
            }
            string fileName = $"{propertyName} - {plotNumber}.pdf";

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
            document.Info.Title = "Plot Report";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            int margin = 40;
            double logoWidth = 50;
            double logoHeight = 50;
            double y = margin;

            // Load logo once
            XImage? logo = null;
            var logoStream = new MemoryStream();
            Properties.Resources.logo.Save(logoStream, ImageFormat.Png);
            logoStream.Position = 0;
            logo = XImage.FromStream(logoStream);

            string orgName = "Jay Maa Durga Housing Agency";
            string orgAddressLine1 = "Building #1, Block #2, Mahakalkar Complex opp. Central Bank of India, Umred Road,";
            string orgAddressLine2 = "Dighori, Nagpur - 440034"; 

            // Helper to draw header (logo + org name + address) on a page
            void DrawHeader(XGraphics g, PdfPage pg)
            {
                // Logo (left)
                g.DrawImage(logo, margin, margin, logoWidth, logoHeight);

                // Org name (centered, slightly above the logo's vertical center)
                double orgNameY = margin + 4; // Move up by reducing offset (was margin)
                g.DrawString(
                    orgName,
                    new XFont("Segoe UI", 18, XFontStyleEx.Bold),
                    XBrushes.MidnightBlue,
                    new XRect(0, orgNameY, pg.Width.Point, 24), // Height 24 for better vertical alignment
                    XStringFormats.TopCenter
                );

                // Org address line 1 (centered, just below org name)
                double address1Y = orgNameY + 24 + 2; // 24 is font height, 2px gap
                g.DrawString(
                    orgAddressLine1,
                    new XFont("Segoe UI", 9, XFontStyleEx.Regular),
                    XBrushes.DimGray,
                    new XRect(0, address1Y, pg.Width.Point, 16),
                    XStringFormats.TopCenter
                );

                // Org address line 2 (centered, just below line 1)
                double address2Y = address1Y + 16; // 16 is font height
                g.DrawString(
                    orgAddressLine2,
                    new XFont("Segoe UI", 9, XFontStyleEx.Regular),
                    XBrushes.DimGray,
                    new XRect(0, address2Y, pg.Width.Point, 16),
                    XStringFormats.TopCenter
                );
            }

            // Draw header on first page
            DrawHeader(gfx, page);

            // Set y to just below the address lines, with a small gap before the horizontal line
            y = margin + 4 + 24 + 2 + 16 + 16 + 8; // orgNameY + orgNameHeight + gap + address1Height + address2Height + extra gap

            // --- Light horizontal line before Plot Details ---
            XPen lightPen = new XPen(XColors.LightGray, 1);
            gfx.DrawLine(lightPen, margin, y, page.Width.Point - margin, y);
            y += 10;

            // Plot Details: 2 columns, titles bold, values aligned
            (string Title, string Value)[] details = {
                ("Plot Number:", labelPlotNumber.Text),
                ("Status:", labelStatus.Text),
                ("Area:", labelArea.Text),
                ("Sale Date:", labelSaleDate.Text),
                ("Customer Name:", labelCustomerName.Text),
                ("Customer Phone:", labelCustomerPhone.Text),
                ("Customer Email:", labelCustomerEmail.Text),
                ("Sale Amount:", labelSaleAmount.Text),
                ("Paid Amount:", labelPaidAmount.Text),
                ("Balance:", labelBalanceAmount.Text),
                ("Agent Name:", labelAgentName.Text),
                ("Total Brokerage:", labelTotalBrokerage.Text),
                ("Brokerage Paid:", labelBrokeragePaid.Text),
                ("Brokerage Balance:", labelBrokerageBalance.Text)
            };

            double detailsX = margin;
            double detailsY = y;

            gfx.DrawString("Plot Details", new XFont("Segoe UI", 12, XFontStyleEx.Bold), XBrushes.Black,
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
                    leftY += 20; // Increased from 16 to 20 for extra vertical gap
                }
                else
                {
                    double tx = rightX;
                    double ty = rightY;
                    gfx.DrawString(title, titleFont, XBrushes.Black,
                        new XRect(tx, ty, rightMaxTitleWidth, 18), XStringFormats.TopLeft);
                    gfx.DrawString(value, new XFont("Segoe UI", 10), XBrushes.Black,
                        new XRect(tx + rightMaxTitleWidth + gap, ty, colWidth - rightMaxTitleWidth - gap, 18), XStringFormats.TopLeft);
                    rightY += 20; // Increased from 16 to 20 for extra vertical gap
                }
            }
            y = Math.Max(leftY, rightY) + 10;

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
                if (visibleColumns[i].Name == "TransactionDate")
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
                    if (col.Name == "TransactionDate" && row.Cells[col.Name].Value is DateTime dt)
                    {
                        value = dt.ToString("dd/MM/yyyy hh:mm tt");
                    }
                    else if (col.Name == "TransactionDate" && DateTime.TryParse(value, out DateTime dt2))
                    {
                        value = dt2.ToString("dd/MM/yyyy hh:mm tt");
                    }

                    gfx.DrawString(value, new XFont("Segoe UI", 9), XBrushes.Black,
                        new XRect(colX + 2, y + 2, colWidths[i] - 4, rowHeight - 4), XStringFormats.TopLeft);
                    colX += colWidths[i];
                }
                y += rowHeight;
                rowCount++;

                // If y is near the bottom, add a new page and draw header
                if (y + (rowHeight * 2) > ((XUnit.FromPoint(page.Height.Point) - XUnit.FromPoint(margin)).Point) && rowCount < dataGridViewTransactions.Rows.Count)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    DrawHeader(gfx, page);
                    y = margin + logoHeight + 18;

                    // Redraw section title and header row
                    gfx.DrawLine(lightPen, margin, y, page.Width.Point - margin, y);
                    y += 10;
                    gfx.DrawString("Transactions", new XFont("Segoe UI", 12, XFontStyleEx.Bold), XBrushes.Black,
                        new XRect(margin, y, page.Width.Point - 2 * margin, 20), XStringFormats.TopLeft);
                    y += 22;
                    colX = margin;
                    for (int i = 0; i < visibleColumns.Count; i++)
                    {
                        gfx.DrawRectangle(XPens.Black, colX, y, colWidths[i], rowHeight);
                        gfx.DrawString(visibleColumns[i].HeaderText, new XFont("Segoe UI", 9, XFontStyleEx.Bold), XBrushes.Black,
                            new XRect(colX + 2, y + 2, colWidths[i] - 4, rowHeight - 4), XStringFormats.TopLeft);
                        colX += colWidths[i];
                    }
                    y += rowHeight;
                }
                if (rowCount >= maxRows) break;
            }

            // Footer
            string footer = "© " + DateTime.Now.Year + " VVT Softwares Pvt. Ltd. All rights reserved.";
            gfx.DrawString(footer, new XFont("Segoe UI", 8), XBrushes.Gray,
                new XRect(margin, page.Height.Point - margin, page.Width.Point - 2 * margin, 20), XStringFormats.BottomLeft);

            document.Save(filePath);
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }

        private static (string agentName, decimal totalBrokerage, decimal brokeragePaid, decimal brokerageBalance) DisplayAgentBrokerageDetails(int plotId)
        {
            string agentName = "-";
            decimal totalBrokerage = 0, brokeragePaid = 0, brokerageBalance = 0;
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
                SELECT a.Name AS AgentName, 
                       ISNULL(ps.BrokerageAmount, 0) AS TotalBrokerage,
                       ISNULL((SELECT SUM(Amount) FROM AgentTransaction at WHERE at.AgentId = ps.AgentId AND at.PlotId = ps.PlotId AND at.IsDeleted = 0), 0) AS BrokeragePaid
                FROM PlotSale ps
                INNER JOIN Agent a ON ps.AgentId = a.Id
                WHERE ps.PlotId = @PlotId AND ps.IsDeleted = 0";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PlotId", plotId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        agentName = reader["AgentName"]?.ToString() ?? "-";
                        totalBrokerage = reader["TotalBrokerage"] != DBNull.Value ? Convert.ToDecimal(reader["TotalBrokerage"]) : 0;
                        brokeragePaid = reader["BrokeragePaid"] != DBNull.Value ? Convert.ToDecimal(reader["BrokeragePaid"]) : 0;
                        brokerageBalance = totalBrokerage - brokeragePaid;
                    }
                }
            }
            return (agentName, totalBrokerage, brokeragePaid, brokerageBalance);
        }
    }
}