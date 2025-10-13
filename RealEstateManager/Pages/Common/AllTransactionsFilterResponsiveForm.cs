using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class AllTransactionsFilterResponsiveForm : Form
    {
        public AllTransactionsFilterResponsiveForm()
        {
            InitializeComponent();
            comboBoxType.Items.AddRange(new[] { "Plot", "Property", "Agent", "Miscellaneous", "Property Loan" });
            comboBoxType.SelectedIndex = 0;
            comboBoxType.SelectedIndexChanged += ButtonFilter_Click;
            dateTimePickerFrom.Value = DateTime.Today.AddMonths(-1);
            dateTimePickerTo.Value = DateTime.Today;
            ButtonFilter_Click(this, EventArgs.Empty);
            ApplyGridStyle(dataGridViewResults);
        }

        private void ButtonFilter_Click(object? sender, EventArgs e)
        {
            DateTime fromDate = dateTimePickerFrom.Value.Date;
            DateTime toDate = dateTimePickerTo.Value.Date;
            if (fromDate > toDate)
            {
                MessageBox.Show("The 'From' date cannot be after the 'To' date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string type = comboBoxType.SelectedItem?.ToString() ?? "Property";
            toDate = toDate.AddDays(1).AddTicks(-1);
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = "";
            switch (type)
            {
                case "Property":
                    query = @"
                        SELECT 
                            pt.TransactionId,
                            pt.TransactionDate,
                            pt.TransactionType,
                            pt.Amount,
                            pt.PaymentMethod,
                            pt.ReferenceNumber,
                            pt.Notes,
                            pr.Title AS PropertyName
                        FROM PropertyTransaction pt
                        INNER JOIN Property pr ON pt.PropertyId = pr.Id
                        WHERE pt.IsDeleted = 0
                          AND pt.TransactionDate >= @FromDate
                          AND pt.TransactionDate <= @ToDate
                        ORDER BY pt.TransactionDate ASC";
                    break;
                case "Plot":
                    query = @"
                        SELECT 
                            pt.TransactionId,
                            pt.TransactionDate,
                            pt.TransactionType,
                            pt.Amount,
                            pt.PaymentMethod,
                            pt.ReferenceNumber,
                            pt.Notes,
                            p.PlotNumber,
                            pr.Title AS PropertyName
                        FROM PlotTransaction pt
                        INNER JOIN Plot p ON pt.PlotId = p.Id
                        INNER JOIN Property pr ON p.PropertyId = pr.Id
                        WHERE pt.IsDeleted = 0
                          AND pt.TransactionDate >= @FromDate
                          AND pt.TransactionDate <= @ToDate
                        ORDER BY pt.TransactionDate ASC";
                    break;
                case "Agent":
                    query = @"
                        SELECT 
                            at.TransactionId,
                            at.TransactionDate,
                            at.TransactionType,
                            at.Amount,
                            at.PaymentMethod,
                            at.ReferenceNumber,
                            at.Notes,
                            a.Name AS AgentName
                        FROM AgentTransaction at
                        INNER JOIN Agent a ON at.AgentId = a.Id
                        WHERE at.IsDeleted = 0
                          AND at.TransactionDate >= @FromDate
                          AND at.TransactionDate <= @ToDate
                        ORDER BY at.TransactionDate ASC";
                    break;
                case "Miscellaneous":
                    query = @"
                        SELECT 
                            mt.TransactionId,
                            mt.TransactionDate,
                            mt.Recipient,
                            mt.TransactionType,
                            mt.Amount,
                            mt.PaymentMethod,
                            mt.ReferenceNumber,
                            mt.Notes
                        FROM MiscTransaction mt
                        WHERE mt.IsDeleted = 0
                          AND mt.TransactionDate >= @FromDate
                          AND mt.TransactionDate <= @ToDate
                        ORDER BY mt.TransactionDate ASC";
                    break;
                case "Property Loan":
                    query = @"
                SELECT 
                    plt.Id AS TransactionId,
                    plt.TransactionDate,
                    plt.TransactionType,
                    plt.PrincipalAmount,
                    plt.InterestAmount,
                    plt.PaymentMethod,
                    plt.ReferenceNumber,
                    pl.LenderName,
                    pr.Title AS PropertyName
                FROM PropertyLoanTransaction plt
                LEFT JOIN PropertyLoan pl ON plt.PropertyLoanId = pl.Id
                LEFT JOIN Property pr ON pl.PropertyId = pr.Id
                WHERE plt.IsDeleted = 0
                  AND plt.TransactionDate >= @FromDate
                  AND plt.TransactionDate <= @ToDate
                ORDER BY plt.TransactionDate ASC";
                    break;
            }
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                dataGridViewResults.DataSource = null;
                dataGridViewResults.Rows.Clear();
                dataGridViewResults.Columns.Clear();
                dataGridViewResults.AutoGenerateColumns = false;
                dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridViewResults.AllowUserToResizeColumns = false;
                if (dt.Columns.Contains("TransactionId"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "TransactionId",
                        DataPropertyName = "TransactionId",
                        HeaderText = "TRN #",
                        Width = 80,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    });
                }
                if (dt.Columns.Contains("PropertyName"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "PropertyName",
                        DataPropertyName = "PropertyName",
                        HeaderText = "Property",
                        Width = 200,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    });
                }
                if (dt.Columns.Contains("Recipient"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "Recipient",
                        DataPropertyName = "Recipient",
                        HeaderText = "Recipient",
                        Width = 200,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    });
                }
                if (dt.Columns.Contains("LenderName"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "LenderName",
                        DataPropertyName = "LenderName",
                        HeaderText = "Lender",
                        Width = 200,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    });
                }
                if (dt.Columns.Contains("TransactionDate"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "TransactionDate",
                        DataPropertyName = "TransactionDate",
                        HeaderText = "TRN Date",
                        Width = 180,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                        DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy hh:mm tt" }
                    });
                }
                if (dt.Columns.Contains("TransactionType"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "TransactionType",
                        DataPropertyName = "TransactionType",
                        HeaderText = "TRN Type",
                        Width = 120,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    });
                }
                // Add InterestPaid column for Property Loan type
                if (type == "Property Loan")
                {
                    if (dt.Columns.Contains("InterestAmount"))
                    {
                        dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "InterestAmount",
                            DataPropertyName = "InterestAmount",
                            HeaderText = "Interest Paid",
                            Width = 150,
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                            DefaultCellStyle = new DataGridViewCellStyle { Format = "C" }
                        });
                    }
                    if (dt.Columns.Contains("PrincipalAmount"))
                    {
                        dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "PrincipalAmount",
                            DataPropertyName = "PrincipalAmount",
                            HeaderText = "Principal Paid",
                            Width = 150,
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                            DefaultCellStyle = new DataGridViewCellStyle { Format = "C" }
                        });
                    }
                }
                if (dt.Columns.Contains("Amount"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "Amount",
                        DataPropertyName = "Amount",
                        HeaderText = "Total Paid",
                        Width = 150,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                        DefaultCellStyle = new DataGridViewCellStyle { Format = "C" }
                    });
                }
                if (dt.Columns.Contains("PaymentMethod"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "PaymentMethod",
                        DataPropertyName = "PaymentMethod",
                        HeaderText = "Payment Method",
                        Width = 170,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    });
                }
                if (dt.Columns.Contains("ReferenceNumber"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "ReferenceNumber",
                        DataPropertyName = "ReferenceNumber",
                        HeaderText = "Reference #",
                        Width = 140,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    });
                }
                if (dt.Columns.Contains("Notes"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "Notes",
                        DataPropertyName = "Notes",
                        HeaderText = "Notes",
                        Width = 250,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    });
                }
               
                if (dt.Rows.Count > 0)
                {
                    dataGridViewResults.DataSource = dt;
                }
                else
                {
                    dataGridViewResults.RowCount = 0;
                    MessageBox.Show("No data found for the selected filter.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private static void ApplyGridStyle(DataGridView grid)
        {
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.ColumnHeadersHeight = 32;
            grid.ReadOnly = true;
            grid.RowHeadersWidth = 51;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.GridColor = Color.LightGray;
            grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(220, 237, 255),
                SelectionForeColor = Color.Black,
                BackColor = Color.White
            };
            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.MidnightBlue,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                SelectionBackColor = Color.MidnightBlue,
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.False
            };
            grid.EnableHeadersVisualStyles = false;
            grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 248, 255),
                ForeColor = Color.Black
            };
            grid.RowTemplate.Height = 28;
        }
    }
}
