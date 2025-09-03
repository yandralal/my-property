using RealEstateManager.Entities;
using System;
using System.Windows.Forms;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;

namespace RealEstateManager.Pages.Property
{
    public partial class PropertyLoanDetailsForm : BaseForm
    {
        private readonly LoanTransaction _loan;

        public PropertyLoanDetailsForm(LoanTransaction loan)
        {
            InitializeComponent();
            _loan = loan;

            // DataGridView event handlers
            dataGridViewTransactions.DataBindingComplete += DataGridViewTransactions_DataBindingComplete;
            dataGridViewTransactions.CellPainting += DataGridViewTransactions_CellPainting;
            dataGridViewTransactions.CellMouseClick += DataGridViewTransactions_CellMouseClick;

            LoadPropertyDetails();
        }

        private void LoadPropertyDetails()
        {
            // Assign values to the value labels
            labelPropertyValue.Text = _loan.Property ?? "";
            labelLenderNameValue.Text = _loan.LenderName ?? "";
            labelLoanAmountValue.Text = _loan.LoanAmount.ToString("C2");
            labelInterestRateValue.Text = _loan.InterestRate.ToString("N2") + " %";
            labelTenureValue.Text = _loan.Tenure?.ToString() + " months" ?? "";
            labelLoanDateValue.Text = _loan.LoanDate.ToString("dd/MM/yyyy hh:mm tt");
            labelTotalInterestValue.Text = _loan.TotalInterest.ToString("C2");
            labelTotalRepayableValue.Text = _loan.TotalRepayable.ToString("C2");
            labelTotalPaidValue.Text = _loan.TotalPaid.ToString("C2");
            labelBalanceValue.Text = _loan.Balance.ToString("C2");

            // Load transactions for this loan
            if (_loan.Id > 0)
            {
                LoadLoanTransactions(_loan.Id);
                StyleTransactionGrid();
            }

            UpdateLoanSummaryFromGrid();
        }

        private void LoadLoanTransactions(int propertyLoanId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;

            // Fetch and set the property name
            if (_loan.PropertyId.HasValue)
            {
                string propertyName = "";
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand("SELECT Title FROM Property WHERE Id = @Id AND IsDeleted = 0", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", _loan.PropertyId.Value);
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        propertyName = result.ToString();
                }
                labelPropertyValue.Text = propertyName;
            }

            // Load transactions as before
            string query = @"
                SELECT 
                    Id AS [TransactionId],
                    TransactionDate AS [Date],
                    TransactionType,
                    PrincipalAmount,
                    InterestAmount,
                    PaymentMethod,
                    ReferenceNumber
                FROM PropertyLoanTransaction
                WHERE PropertyLoanId = @PropertyLoanId AND IsDeleted = 0
                ORDER BY TransactionDate DESC";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PropertyLoanId", propertyLoanId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                dataGridViewTransactions.DataSource = dt;
            }
        }

        private void StyleTransactionGrid()
        {
            var dgv = dataGridViewTransactions;
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.Columns.Clear(); // Always clear before adding

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TransactionId",
                DataPropertyName = "TransactionId",
                HeaderText = "TRN #",
                Width = 80
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Date",
                DataPropertyName = "Date",
                HeaderText = "TRN Date",
                Width = 180,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy hh:mm tt" }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TransactionType",
                DataPropertyName = "TransactionType",
                HeaderText = "Type",
                Width = 100
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrincipalAmount",
                DataPropertyName = "PrincipalAmount",
                HeaderText = "Principal Amt",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InterestAmount",
                DataPropertyName = "InterestAmount",
                HeaderText = "Interest Amt",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PaymentMethod",
                DataPropertyName = "PaymentMethod",
                HeaderText = "Payment Method",
                Width = 150
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ReferenceNumber",
                DataPropertyName = "ReferenceNumber",
                HeaderText = "Reference #",
                Width = 110
            });
            if (!dataGridViewTransactions.Columns.Contains("Action"))
            {
                var actionCol = new DataGridViewImageColumn
                {
                    Name = "Action",
                    HeaderText = "Action",
                    Width = 120,
                    ImageLayout = DataGridViewImageCellLayout.Normal
                };
                dataGridViewTransactions.Columns.Add(actionCol);
            }
        }

        private void ButtonGeneratePdf_Click(object? sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                sfd.FileName = $"Loan_{labelLenderName.Text}_{DateTime.Now:yyyyMMdd}.pdf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GeneratePdfReport(sfd.FileName);
                    MessageBox.Show("PDF report generated successfully.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
            }
        }

        private void GeneratePdfReport(string filePath)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Property Loan Report";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            int margin = 40;
            double y = margin;

            // Header
            gfx.DrawString("Property Loan Report", new Font("Segoe UI", 18, FontStyle.Bold), XBrushes.MidnightBlue, new XRect(0, y, page.Width, 30), XStringFormats.TopCenter);
            y += 40;

            // Loan Details
            gfx.DrawString("Loan Details", new Font("Segoe UI", 12, FontStyle.Bold), XBrushes.Black, margin, y);
            y += 24;

            string[] details = {
                $"Property: {labelProperty.Text}",
                $"Lender Name: {labelLenderName.Text}",
                $"Loan Amount: {labelLoanAmount.Text}",
                $"Interest Rate: {labelInterestRate.Text}",
                $"Tenure: {labelTenure.Text}",
                $"Loan Date: {labelLoanDate.Text}",
                $"Total Interest: {labelTotalInterest.Text}",
                $"Total Repayable: {labelTotalRepayable.Text}",
                $"Total Paid: {labelTotalPaid.Text}",
                $"Balance: {labelBalance.Text}"
            };

            foreach (var line in details)
            {
                gfx.DrawString(line, new XFont("Segoe UI", 10), XBrushes.Black, margin, y);
                y += 18;
            }

            y += 10;
            gfx.DrawLine(XPens.Gray, margin, y, page.Width - margin, y);
            y += 10;

            // Transactions Table Header
            gfx.DrawString("Transactions", new Font("Segoe UI", 12, FontStyle.Bold), XBrushes.Black, margin, y);
            y += 24;

            // Table columns
            var columns = new[] { "TRN #", "TRN Date", "Type", "Principal", "Interest", "Total Paid", "Payment Method", "Reference #", "Notes" };
            var colWidths = new[] { 50, 90, 50, 70, 70, 70, 90, 70, 120 };
            double x = margin;
            for (int i = 0; i < columns.Length; i++)
            {
                gfx.DrawString(columns[i], new Font("Segoe UI", 9, FontStyle.Bold), XBrushes.Black, new XRect(x, y, colWidths[i], 18), XStringFormats.TopLeft);
                x += colWidths[i];
            }
            y += 18;

            // Table rows
            foreach (DataGridViewRow row in dataGridViewTransactions.Rows)
            {
                if (row.IsNewRow) continue;
                x = margin;
                for (int i = 0; i < columns.Length; i++)
                {
                    string value = "";
                    switch (i)
                    {
                        case 0: value = row.Cells["TransactionId"].Value?.ToString() ?? ""; break;
                        case 1:
                            if (row.Cells["Date"].Value is DateTime dt)
                                value = dt.ToString("dd/MM/yyyy hh:mm tt");
                            else
                                value = row.Cells["Date"].Value?.ToString() ?? "";
                            break;
                        case 2: value = row.Cells["TransactionType"].Value?.ToString() ?? ""; break;
                        case 3: value = row.Cells["PrincipalAmount"].Value != null ? Convert.ToDecimal(row.Cells["PrincipalAmount"].Value).ToString("C2") : ""; break;
                        case 4: value = row.Cells["InterestAmount"].Value != null ? Convert.ToDecimal(row.Cells["InterestAmount"].Value).ToString("C2") : ""; break;
                        case 5: value = row.Cells["Amount"].Value != null ? Convert.ToDecimal(row.Cells["Amount"].Value).ToString("C2") : ""; break;
                        case 6: value = row.Cells["PaymentMethod"].Value?.ToString() ?? ""; break;
                        case 7: value = row.Cells["ReferenceNumber"].Value?.ToString() ?? ""; break;
                        case 8: value = row.Cells["Notes"].Value?.ToString() ?? ""; break;
                    }
                    gfx.DrawString(value, new XFont("Segoe UI", 9), XBrushes.Black, new XRect(x, y, colWidths[i], 18), XStringFormats.TopLeft);
                    x += colWidths[i];
                }
                y += 18;
                if (y > page.Height - 40) break; // Simple page break logic
            }

            document.Save(filePath);
        }

        private void DataGridViewTransactions_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dataGridViewTransactions;

            // Set to None so custom widths are respected
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // Set font styles
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Set column headers, widths, and formats as needed
            if (dgv.Columns["TransactionId"] != null)
            {
                dgv.Columns["TransactionId"].HeaderText = "TRN #";
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
            if (dgv.Columns["PrincipalAmount"] != null)
            {
                dgv.Columns["PrincipalAmount"].HeaderText = "Principal Amt";
                dgv.Columns["PrincipalAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["PrincipalAmount"].Width = 150;
                dgv.Columns["PrincipalAmount"].DefaultCellStyle.Format = "C2";
            }
            if (dgv.Columns["InterestAmount"] != null)
            {
                dgv.Columns["InterestAmount"].HeaderText = "Interest Amt";
                dgv.Columns["InterestAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["InterestAmount"].Width = 150;
                dgv.Columns["InterestAmount"].DefaultCellStyle.Format = "C2";
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
            if (dgv.Columns["Action"] != null)
            {
                dgv.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns["Action"].Width = 120;
            }
        }

        private void DataGridViewTransactions_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e != null && e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewTransactions.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                var viewIcon = Properties.Resources.view;
                var editIcon = Properties.Resources.edit;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 12;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;

                // Draw view icon
                if (viewIcon != null && e.Graphics != null)
                    e.Graphics.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw edit icon
                if (editIcon != null && e.Graphics != null)
                    e.Graphics.DrawImage(editIcon, new Rectangle(x, y, iconWidth, iconHeight));
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
                        EditTransaction(transactionId);
                        break;
                    case 2:
                        DeleteTransaction(transactionId);
                        break;
                }
            }
        }

        // You will need to implement these methods as per your business logic:
        private void ViewTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            using (var form = new PropertyLoanTransactionForm(Convert.ToInt32(transactionId), true)) // true = read-only
            {
                form.ShowDialog(this);
            }
        }

        private void EditTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            using (var form = new PropertyLoanTransactionForm(Convert.ToInt32(transactionId), false))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadLoanTransactions(_loan.Id);
                    StyleTransactionGrid();
                    UpdateLoanSummaryFromGrid();
                }
            }
        }

        private void DeleteTransaction(string? transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) return;
            var result = MessageBox.Show("Are you sure you want to delete this transaction?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("UPDATE PropertyLoanTransaction SET IsDeleted = 1 WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(transactionId));
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadLoanTransactions(_loan.Id);
            StyleTransactionGrid();
            UpdateLoanSummaryFromGrid();
        }

        private void UpdateLoanSummaryFromGrid()
        {
            if (dataGridViewTransactions.DataSource is DataTable dt)
            {
                decimal totalPrincipal = 0;
                decimal totalInterest = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["PrincipalAmount"] != DBNull.Value)
                        totalPrincipal += Convert.ToDecimal(row["PrincipalAmount"]);
                    if (row["InterestAmount"] != DBNull.Value)
                        totalInterest += Convert.ToDecimal(row["InterestAmount"]);
                }
                labelTotalPrincipalPaidValue.Text = totalPrincipal.ToString("C2");
                labelTotalInterestPaidValue.Text = totalInterest.ToString("C2");

                // Optionally, update total paid and balance if needed
                decimal totalPaid = totalPrincipal + totalInterest;
                labelTotalPaidValue.Text = totalPaid.ToString("C2");
                labelBalanceValue.Text = (_loan.TotalRepayable - totalPaid).ToString("C2");
            }
        }
    }
}