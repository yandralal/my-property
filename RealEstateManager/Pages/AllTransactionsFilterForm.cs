using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class AllTransactionsFilterForm : BaseForm
    {
        public AllTransactionsFilterForm()
        {
            InitializeComponent();
            comboBoxType.Items.AddRange(new object[] { "Plot", "Property", "Agent", "Miscellaneous" });
            comboBoxType.SelectedIndex = 0;
            dateTimePickerFrom.Value = DateTime.Today.AddMonths(-1);
            dateTimePickerTo.Value = DateTime.Today;
            ButtonFilter_Click(this, EventArgs.Empty);
            ApplyGridStyle(dataGridViewResults);
        }

        private void ButtonFilter_Click(object sender, EventArgs e)
        {
            string type = comboBoxType.SelectedItem?.ToString() ?? "Property";
            DateTime fromDate = dateTimePickerFrom.Value.Date;
            DateTime toDate = dateTimePickerTo.Value.Date.AddDays(1).AddTicks(-1);

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

                // Clear and set up grid
                dataGridViewResults.DataSource = null;
                dataGridViewResults.Rows.Clear();
                dataGridViewResults.Columns.Clear();
                dataGridViewResults.AutoGenerateColumns = false;
                dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridViewResults.AllowUserToResizeColumns = false; 

                // Always add columns, even if no data
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
                        Width = 180,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    });
                }
                if (dt.Columns.Contains("PlotNumber"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "PlotNumber",
                        DataPropertyName = "PlotNumber",
                        HeaderText = "Plot Number",
                        Width = 120,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    });
                }
                if (dt.Columns.Contains("AgentName"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "AgentName",
                        DataPropertyName = "AgentName",
                        HeaderText = "Agent",
                        Width = 180,
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
                if (dt.Columns.Contains("Amount"))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "Amount",
                        DataPropertyName = "Amount",
                        HeaderText = "Amount",
                        Width = 130,
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
                        Width = 220,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    });
                }

                // Bind data if any rows, otherwise just show headers
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
    }
}