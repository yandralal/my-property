using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq; // If needed for DataTable operations

namespace RealEstateManager
{
    public partial class LandingForm : Pages.BaseForm
    {
        public LandingForm()
        {
            InitializeComponent();
            LoadActiveProperties();
            SetupPlotGrid();
        }

        private void PropertyMenuItem_Click(object? sender, EventArgs e)
        {
            dataGridViewProperties.Visible = true;
            buttonAddProperty.Visible = true;
        }

        private void ButtonAddProperty_Click(object sender, EventArgs e)
        {
            var registerForm = new Pages.RegisterPropertyForm();
            registerForm.ShowDialog();
            LoadActiveProperties(); // Refresh grid after adding
        }

        private void ButtonManagePlots_Click(object sender, EventArgs e)
        {
            var managePlotsForm = new Pages.ManagePlotsForm();
            managePlotsForm.ShowDialog();
        }

        public void LoadActiveProperties()
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "SELECT Id, Title, Type, Status, Price, Owner, Phone, Address, City, State, ZipCode, Description FROM Property WHERE IsDeleted = 0";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                dataGridViewProperties.DataSource = dt;

                // Hide the ID column if it exists
                if (dataGridViewProperties.Columns["Id"] != null)
                {
                    dataGridViewProperties.Columns["Id"].Visible = false;
                }

                // Update property count label
                labelProperties.Text = $"Properties ({dt.Rows.Count})";
            }
        }

        private void SetupPlotGrid()
        {
            dataGridViewPlots.Columns.Clear();
            dataGridViewPlots.AutoGenerateColumns = false;

            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                Width = 60,
                ReadOnly = true,
                Visible = false
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlotNumber",
                HeaderText = "Plot Number",
                Width = 120
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerName",
                HeaderText = "Customer Name",
                Width = 150
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerPhone",
                HeaderText = "Customer Phone",
                Width = 120
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerEmail",
                HeaderText = "Customer Email",
                Width = 120
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 100
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Area",
                HeaderText = "Area",
                Width = 100
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SaleDate",
                HeaderText = "Sale Date",
                Width = 100
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SalePrice",
                HeaderText = "Sale Price",
                Width = 100
            });
            // Add AmountPaid and AmountBalance columns
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AmountPaid",
                HeaderText = "Amount Paid",
                Width = 100
            });
            dataGridViewPlots.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AmountBalance",
                HeaderText = "Amount Balance",
                Width = 110
            });
            var viewButtonColumn = new DataGridViewButtonColumn
            {
                Name = "ViewDetails",
                HeaderText = "Action",
                Text = "View",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dataGridViewPlots.Columns.Add(viewButtonColumn);

            dataGridViewPlots.CellContentClick -= dataGridViewPlots_CellContentClick;
            dataGridViewPlots.CellContentClick += dataGridViewPlots_CellContentClick;
        }

        private void LandingForm_Load(object sender, EventArgs e)
        {

        }

        private void registerSaleMenuItem_Click(object sender, EventArgs e)
        {
            var registerSaleForm = new Pages.RegisterSaleForm();
            registerSaleForm.ShowDialog();
        }

        private void DataGridViewProperties_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridViewProperties.CurrentRow != null)
            {
                var idCell = dataGridViewProperties.CurrentRow.Cells["Id"] ?? dataGridViewProperties.CurrentRow.Cells["Id"];
                if (idCell?.Value != null && int.TryParse(idCell.Value.ToString(), out int propertyId))
                {
                    LoadPlotsForProperty(propertyId);
                }
                else
                {
                    dataGridViewPlots.Rows.Clear();
                }
            }
            else
            {
                dataGridViewPlots.Rows.Clear();
            }
        }

        private void LoadPlotsForProperty(int propertyId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string plotQuery = @"
                SELECT 
                    p.Id, 
                    p.PlotNumber, 
                    p.Status, 
                    p.Area,
                    ps.SaleDate,
                    ps.SaleAmount,
                    ps.CustomerName AS CustomerName,
                    ps.CustomerPhone AS CustomerPhone,
                    ps.CustomerEmail AS CustomerEmail
                FROM Plot p
                LEFT JOIN PropertySale ps ON p.Id = ps.PlotId
                WHERE p.PropertyId = @PropertyId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(plotQuery, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);

                dataGridViewPlots.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int plotId = Convert.ToInt32(row["Id"]);
                    decimal saleAmount = row["SaleAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SaleAmount"]);

                    // Fetch transactions for this plot
                    decimal amountPaid = 0;
                    using (var transCmd = new SqlCommand(
                        "SELECT ISNULL(SUM(Amount), 0) FROM PlotTransaction WHERE PlotId = @PlotId AND IsDeleted = 0", conn))
                    {
                        transCmd.Parameters.AddWithValue("@PlotId", plotId);
                        var result = transCmd.ExecuteScalar();
                        amountPaid = result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                    }
                    decimal amountBalance = saleAmount - amountPaid;

                    dataGridViewPlots.Rows.Add(
                        row["Id"].ToString(),
                        row["PlotNumber"].ToString(),
                        row["CustomerName"] == DBNull.Value ? "" : row["CustomerName"].ToString(),
                        row["CustomerPhone"] == DBNull.Value ? "" : row["CustomerPhone"].ToString(),
                        row["CustomerEmail"] == DBNull.Value ? "" : row["CustomerEmail"].ToString(),
                        row["Status"].ToString(),
                        row["Area"].ToString(),
                        row["SaleDate"] == DBNull.Value ? "" : Convert.ToDateTime(row["SaleDate"]).ToShortDateString(),
                        saleAmount == 0 ? "" : saleAmount.ToString("N2"),
                        amountPaid.ToString("N2"),
                        amountBalance.ToString("N2")
                    );
                }

                labelPlots.Text = $"Plots ({dt.Rows.Count})";
            }
        }

        private void dataGridViewPlots_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewPlots.Columns[e.ColumnIndex].Name == "ViewDetails")
            {
                var row = dataGridViewPlots.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["Id"].Value?.ToString(), out int plotId))
                {
                    var detailsForm = new Pages.PlotDetailsForm(plotId);
                    detailsForm.ShowDialog();
                }
            }
        }

        private void registerTransactionMenuItem_Click(object sender, EventArgs e)
        {
            // Optionally, pass the selected plot ID, sale amount, and plot number if a plot is selected in the grid
            int? plotId = null;
            decimal? saleAmount = null;
            string? plotNumber = null;

            if (dataGridViewPlots.CurrentRow != null)
            {
                var idCell = dataGridViewPlots.CurrentRow.Cells["Id"];
                var saleAmountCell = dataGridViewPlots.CurrentRow.Cells["SalePrice"];
                var plotNumberCell = dataGridViewPlots.CurrentRow.Cells["PlotNumber"];

                if (idCell != null && int.TryParse(idCell.Value?.ToString(), out int selectedPlotId))
                {
                    plotId = selectedPlotId;
                }

                if (saleAmountCell != null && decimal.TryParse(saleAmountCell.Value?.ToString(), out decimal parsedSaleAmount))
                {
                    saleAmount = parsedSaleAmount;
                }

                if (plotNumberCell != null)
                {
                    plotNumber = plotNumberCell.Value?.ToString();
                }
            }

            var transactionForm = new Pages.RegisterTransactionForm(plotId, saleAmount, plotNumber);
            transactionForm.ShowDialog();
        }
    }
}