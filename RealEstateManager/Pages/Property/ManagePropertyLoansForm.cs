using Microsoft.Data.SqlClient;
using RealEstateManager.Entities;
using RealEstateManager.Pages.Property;
using System.Configuration;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class ManagePropertyLoansForm : BaseForm
    {
        private readonly Image _editIcon = Properties.Resources.edit;   // Add your edit icon to Resources
        private readonly Image _viewIcon = Properties.Resources.view;   // Add your view icon to Resources
        private readonly Image _deleteIcon = Properties.Resources.delete1; // Add your delete icon to Resources

        public ManagePropertyLoansForm()
        {
            InitializeComponent();
            LoadLoans();
            dataGridViewLoans.CellPainting += DataGridViewLoans_CellPainting;
            dataGridViewLoans.CellClick += DataGridViewLoans_CellClick;
        }

        private void LoadLoans()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"
                SELECT 
                    l.Id,
                    p.Title AS [Property],
                    l.LenderName AS [Lender Name],
                    l.LoanAmount AS [Loan Amount],
                    l.InterestRate AS [Interest Rate (%)],
                    l.Tenure, 
                    l.TotalInterest AS [Total Interest],
                    l.TotalRepayment AS [Total Repayment],
                    ISNULL(
                        (SELECT SUM(ISNULL(PrincipalAmount,0) + ISNULL(InterestAmount,0)) FROM PropertyLoanTransaction t WHERE t.PropertyLoanId = l.Id AND t.IsDeleted = 0), 0
                    ) AS [Total Paid],
                    (l.TotalRepayment - ISNULL(
                        (SELECT SUM(ISNULL(PrincipalAmount,0) + ISNULL(InterestAmount,0)) FROM PropertyLoanTransaction t WHERE t.PropertyLoanId = l.Id AND t.IsDeleted = 0), 0
                    )) AS [Balance]
                FROM PropertyLoan l
                LEFT JOIN Property p ON l.PropertyId = p.Id
                WHERE l.IsDeleted = 0";

            dataGridViewLoans.DataSource = null;
            dataGridViewLoans.Rows.Clear();
            dataGridViewLoans.Columns.Clear();

            dataGridViewLoans.AutoGenerateColumns = false;
            dataGridViewLoans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            var dt = new DataTable();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                conn.Open();
                adapter.Fill(dt);
            }

            // Add all columns you want to display
            dataGridViewLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Visible = false // Hide ID column
            });
            dataGridViewLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Property",
                DataPropertyName = "Property",
                HeaderText = "Property",
                Width = 140,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridViewLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Lender Name",
                DataPropertyName = "Lender Name",
                HeaderText = "Lender Name",
                Width = 140,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridViewLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Loan Amount",
                DataPropertyName = "Loan Amount",
                HeaderText = "Loan Amount",
                Width = 160,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dataGridViewLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Interest Rate (%)",
                DataPropertyName = "Interest Rate (%)",
                HeaderText = "Rate (%)",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridViewLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tenure",
                DataPropertyName = "Tenure",
                HeaderText = "Tenure (Months)",
                Width = 90,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridViewLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total Interest",
                DataPropertyName = "Total Interest",
                HeaderText = "Total Interest",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dataGridViewLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total Repayment",
                DataPropertyName = "Total Repayment",
                HeaderText = "Total Repayment",
                Width = 170,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dataGridViewLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total Paid",
                DataPropertyName = "Total Paid",
                HeaderText = "Total Paid",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dataGridViewLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Balance",
                DataPropertyName = "Balance",
                HeaderText = "Balance",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            var actionCol = new DataGridViewImageColumn
            {
                Name = "Action",
                HeaderText = "Action",
                Width = 120,
                ImageLayout = DataGridViewImageCellLayout.Normal,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dataGridViewLoans.Columns.Add(actionCol);
            dataGridViewLoans.DataSource = dt;
        }

        private void DataGridViewLoans_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewLoans.Columns[e.ColumnIndex].Name == "Action")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                var editIcon = Properties.Resources.edit;
                var viewIcon = Properties.Resources.view;
                var deleteIcon = Properties.Resources.delete1;

                int iconWidth = 24, iconHeight = 24, padding = 8;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;
                int x = e.CellBounds.Left + padding;

                // Draw edit icon (index 0)
                if (editIcon != null)
                    e.Graphics.DrawImage(editIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw view icon (index 1)
                if (viewIcon != null)
                    e.Graphics.DrawImage(viewIcon, new Rectangle(x, y, iconWidth, iconHeight));
                x += iconWidth + padding;

                // Draw delete icon (index 2)
                if (deleteIcon != null)
                    e.Graphics.DrawImage(deleteIcon, new Rectangle(x, y, iconWidth, iconHeight));

                e.Handled = true;
            }
        }

        private void DataGridViewLoans_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex >= dataGridViewLoans.Columns.Count || dataGridViewLoans.Columns[e.ColumnIndex].Name != "Action")
                return;

            var row = dataGridViewLoans.Rows[e.RowIndex];
            int id = Convert.ToInt32(row.Cells["Id"].Value);

            // Calculate which icon was clicked
            var cell = dataGridViewLoans[e.ColumnIndex, e.RowIndex];
            var mouse = dataGridViewLoans.PointToClient(Cursor.Position);
            var cellDisplayRect = dataGridViewLoans.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

            int iconWidth = 24;
            int padding = 8; // Use the same padding as in CellPainting
            int totalIcons = 3;
            int x = mouse.X - cellDisplayRect.Left;

            for (int i = 0; i < totalIcons; i++)
            {
                int iconStart = padding + i * (iconWidth + padding);
                int iconEnd = iconStart + iconWidth;
                if (x >= iconStart && x < iconEnd)
                {
                    if (i == 0) // Edit
                    {
                        var loan = GetLoanById(id);
                        if (loan != null)
                        {
                            var form = new PropertyLoanForm(loan);
                            if (form.ShowDialog() == DialogResult.OK)
                                LoadLoans();
                        }
                    }
                    else if (i == 1) // View
                    {
                        var loan = GetLoanById(id);
                        if (loan != null)
                        {
                            var detailsForm = new PropertyLoanDetailsForm(loan);
                            detailsForm.ShowDialog();
                        }
                    }
                    else if (i == 2) // Delete
                    {
                        if (MessageBox.Show("Are you sure you want to delete this loan?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            DeletePropertyLoan(id);
                            LoadLoans();
                        }
                    }
                    break;
                }
            }
        }

        private void DeletePropertyLoan(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = "UPDATE PropertyLoan SET IsDeleted = 1 WHERE Id = @Id";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private LoanTransaction? GetLoanById(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
            string query = @"
                SELECT 
                    l.Id,
                    l.PropertyId,
                    p.Title AS [Property],
                    l.LoanAmount,
                    l.LenderName,
                    l.InterestRate,
                    l.LoanDate,
                    l.TotalInterest,
                    l.TotalRepayment,
                    ISNULL(
                        (SELECT SUM(PrincipalAmount) + SUM(InterestAmount) FROM PropertyLoanTransaction t WHERE t.PropertyLoanId = l.Id AND t.IsDeleted = 0), 0
                    ) AS [TotalPaid],
                    (l.TotalRepayment - ISNULL(
                        (SELECT SUM(PrincipalAmount) + SUM(InterestAmount) FROM PropertyLoanTransaction t WHERE t.PropertyLoanId = l.Id AND t.IsDeleted = 0), 0
                    )) AS [Balance],
                    l.Remarks,
                    l.Tenure, -- assuming you have a Tenure column
                    l.CreatedDate
                FROM PropertyLoan l
                LEFT JOIN Property p ON l.PropertyId = p.Id
                WHERE l.Id = @Id AND l.IsDeleted = 0";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new LoanTransaction
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PropertyId = reader.IsDBNull(reader.GetOrdinal("PropertyId")) ? null : reader.GetInt32(reader.GetOrdinal("PropertyId")),
                            LoanAmount = reader.GetDecimal(reader.GetOrdinal("LoanAmount")),
                            LenderName = reader.GetString(reader.GetOrdinal("LenderName")),
                            InterestRate = reader.GetDecimal(reader.GetOrdinal("InterestRate")),
                            LoanDate = reader.GetDateTime(reader.GetOrdinal("LoanDate")),
                            TotalInterest = reader.GetDecimal(reader.GetOrdinal("TotalInterest")),
                            TotalRepayment = reader.GetDecimal(reader.GetOrdinal("TotalRepayment")),
                            TotalPaid = reader.GetDecimal(reader.GetOrdinal("TotalPaid")),
                            Balance = reader.GetDecimal(reader.GetOrdinal("Balance")),
                            Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
                            Tenure = reader.IsDBNull(reader.GetOrdinal("Tenure")) ? null : reader.GetInt32(reader.GetOrdinal("Tenure")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                        };
                    }
                }
            }
            return null;
        }

        private void ButtonAddLender_Click(object sender, EventArgs e)
        {
            var form = new PropertyLoanForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadLoans();
            }
        }
    }
}