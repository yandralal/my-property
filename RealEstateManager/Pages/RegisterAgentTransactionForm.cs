using Microsoft.Data.SqlClient;
using RealEstateManager.Entities;
using RealEstateManager.Repositories;
using System.Data;

namespace RealEstateManager.Pages
{
    public partial class RegisterAgentTransactionForm : BaseForm
    {
        private readonly int? _agentId;
        private readonly string? _transactionId;
        private decimal _amountPaidTillDate = 0;

        public RegisterAgentTransactionForm(int? agentId = null)
        {
            InitializeComponent();
            _agentId = agentId;

            if (_agentId.HasValue)
            {
                comboBoxAgent.Text = _agentId.Value.ToString();
            }

            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(new[] { "Credit", "Debit" });
            comboBoxTransactionType.SelectedIndex = 0;

            textBoxAmount.TextChanged += UpdateBalanceAmount;
        }

        public RegisterAgentTransactionForm(string transactionId, bool readOnly = false)
        {
            InitializeComponent();
            _transactionId = transactionId;

            // Ensure payment methods are loaded
            comboBoxPaymentMethod.Items.Clear();
            comboBoxPaymentMethod.Items.AddRange(new object[] { "Cash", "Cheque", "Bank Transfer", "Other" });

            // Load agents and properties first
            LoadAgents();
            LoadProperties();

            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(new[] { "Credit", "Debit" });

            // Load transaction details from DB
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"
                SELECT 
                    at.AgentId,
                    at.PropertyId,
                    at.PlotId,
                    at.TransactionDate,
                    at.Amount,
                    at.PaymentMethod,
                    at.ReferenceNumber,
                    at.Notes,
                    at.TransactionType
                FROM AgentTransaction at
                WHERE at.TransactionId = @TransactionId AND at.IsDeleted = 0";

            int? agentId = null, propertyId = null, plotId = null;
            decimal amount = 0;

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        agentId = reader["AgentId"] != DBNull.Value ? Convert.ToInt32(reader["AgentId"]) : null;
                        propertyId = reader["PropertyId"] != DBNull.Value ? Convert.ToInt32(reader["PropertyId"]) : null;
                        plotId = reader["PlotId"] != DBNull.Value ? Convert.ToInt32(reader["PlotId"]) : null;

                        dateTimePickerTransactionDate.Value = reader["TransactionDate"] != DBNull.Value ? Convert.ToDateTime(reader["TransactionDate"]) : DateTime.Now;
                        amount = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]) : 0;
                        textBoxAmount.Text = amount.ToString("N2");
                        comboBoxPaymentMethod.Text = reader["PaymentMethod"]?.ToString() ?? "";
                        textBoxReferenceNumber.Text = reader["ReferenceNumber"]?.ToString() ?? "";
                        textBoxNotes.Text = reader["Notes"]?.ToString() ?? "";
                        comboBoxTransactionType.Text = reader["TransactionType"]?.ToString() ?? "Credit";
                    }
                }
            }

            // Set selected agent
            if (agentId.HasValue)
                comboBoxAgent.SelectedValue = agentId.Value;

            // Set selected property
            if (propertyId.HasValue)
                comboBoxProperty.SelectedValue = propertyId.Value;

            // Load plots for the selected property and agent, then set selected plot
            if (propertyId.HasValue && agentId.HasValue)
            {
                LoadPlotsForPropertyAndAgent(propertyId.Value, agentId.Value);
                if (plotId.HasValue)
                    comboBoxPlotNumber.SelectedValue = plotId.Value;
            }
            else
            {
                comboBoxPlotNumber.DataSource = null;
            }

            // Map total brokerage, amount paid till date, and balance
            UpdateTotalBrokerage();
            UpdateAmountPaidTillDate();
            UpdateBalance();

            SetFieldsReadOnly(readOnly);
            buttonSave.Visible = !readOnly;
            buttonSave.Enabled = !readOnly;
            textBoxAmount.TextChanged += UpdateBalanceAmount;
        }

        public RegisterAgentTransactionForm()
        {
            InitializeComponent();
            comboBoxPaymentMethod.Items.Clear();
            comboBoxPaymentMethod.Items.AddRange(new object[] { "Cash", "Cheque", "Bank Transfer", "Other" });
            comboBoxTransactionType.Items.Clear();
            comboBoxTransactionType.Items.AddRange(new[] { "Credit", "Debit" });

            LoadProperties();
            LoadAgents();

            comboBoxProperty.SelectedIndexChanged += ComboBoxProperty_SelectedIndexChanged;
            comboBoxAgent.SelectedIndexChanged += ComboBoxAgent_SelectedIndexChanged;
            comboBoxPlotNumber.SelectedIndexChanged += ComboBoxPlotNumber_SelectedIndexChanged;

            textBoxTotalBrokerage.TextChanged += (s, e) => UpdateBalanceOnLoad();
            textBoxAmountPaidTillDate.TextChanged += TextBoxAmountPaidTillDate_TextChanged;
            textBoxAmount.TextChanged += UpdateBalanceAmount;
        }

        private void RegisterAgentTransactionForm_Load(object sender, EventArgs e)
        {
            LoadAgents();
        }

        private void LoadAgents()
        {
            var agents = AgentRepository.GetAllAgents();
            comboBoxAgent.DataSource = agents;
            comboBoxAgent.DisplayMember = "Name";
            comboBoxAgent.ValueMember = "Id";
            comboBoxAgent.SelectedIndex = -1; // No selection by default
        }

        private void LoadProperties()
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "SELECT Id, Title FROM Property WHERE IsDeleted = 0";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                comboBoxProperty.DataSource = dt;
                comboBoxProperty.DisplayMember = "Title";
                comboBoxProperty.ValueMember = "Id";
                comboBoxProperty.SelectedIndex = -1;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxAgent.SelectedItem is not Agent selectedAgent)
            {
                MessageBox.Show("Please select an agent.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxAgent.Focus();
                return;
            }

            int agentId = selectedAgent.Id;

            if (!decimal.TryParse(textBoxAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string paymentMethod = comboBoxPaymentMethod.Text;
            string referenceNumber = textBoxReferenceNumber.Text;
            string notes = textBoxNotes.Text;
            string transactionType = comboBoxTransactionType.Text;
            string userName = Environment.UserName;

            // Use SelectedValue for plotId
            int? plotId = null;
            if (comboBoxPlotNumber.SelectedValue != null && int.TryParse(comboBoxPlotNumber.SelectedValue.ToString(), out int parsedPlotId))
                plotId = parsedPlotId;

            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";

            if (!string.IsNullOrEmpty(_transactionId))
            {
                // UPDATE existing transaction
                string update = @"
                    UPDATE AgentTransaction SET
                        TransactionDate = @TransactionDate,
                        Amount = @Amount,
                        PaymentMethod = @PaymentMethod,
                        ReferenceNumber = @ReferenceNumber,
                        Notes = @Notes,
                        TransactionType = @TransactionType,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = @ModifiedDate,
                        PlotId = @PlotId
                    WHERE TransactionId = @TransactionId AND IsDeleted = 0";

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(update, conn))
                {
                    cmd.Parameters.AddWithValue("@TransactionDate", dateTimePickerTransactionDate.Value);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.Parameters.AddWithValue("@TransactionType", transactionType);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@TransactionId", _transactionId);
                    cmd.Parameters.AddWithValue("@PlotId", (object?)plotId ?? DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Transaction updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // INSERT new transaction
                string insert = @"
                    INSERT INTO AgentTransaction
                    (AgentId, PropertyId, TransactionDate, Amount, PaymentMethod, ReferenceNumber, Notes, TransactionType, CreatedBy, CreatedDate, IsDeleted, PlotId)
                    VALUES
                    (@AgentId, @PropertyId, @TransactionDate, @Amount, @PaymentMethod, @ReferenceNumber, @Notes, @TransactionType, @CreatedBy, @CreatedDate, 0, @PlotId)";

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(insert, conn))
                {
                    cmd.Parameters.AddWithValue("@AgentId", agentId);

                    // Get PropertyId from comboBoxProperty
                    int propertyId = comboBoxProperty.SelectedValue is int val ? val : Convert.ToInt32(comboBoxProperty.SelectedValue);
                    cmd.Parameters.AddWithValue("@PropertyId", propertyId);

                    cmd.Parameters.AddWithValue("@TransactionDate", dateTimePickerTransactionDate.Value);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.Parameters.AddWithValue("@TransactionType", transactionType);
                    cmd.Parameters.AddWithValue("@CreatedBy", userName);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PlotId", (object?)plotId ?? DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Transaction registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            AgentRepository.RaiseAgentsChanged();
            this.DialogResult = DialogResult.OK;
        }

        private void SetFieldsReadOnly(bool readOnly)
        {
            dateTimePickerTransactionDate.Enabled = !readOnly;
            textBoxAmount.ReadOnly = readOnly;
            comboBoxPaymentMethod.Enabled = !readOnly;
            textBoxReferenceNumber.ReadOnly = readOnly;
            textBoxNotes.ReadOnly = readOnly;
            comboBoxTransactionType.Enabled = !readOnly;
            comboBoxAgent.Enabled = !readOnly;
            comboBoxProperty.Enabled = !readOnly;
            comboBoxPlotNumber.Enabled = !readOnly;
            textBoxTotalBrokerage.ReadOnly = true; // Always readonly, calculated
            textBoxAmountPaidTillDate.ReadOnly = true; // Always readonly, calculated
            labelBalanceValue.Enabled = false; // Label, not editable
            buttonSave.Enabled = !readOnly;
        }

        private void ComboBoxProperty_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadPlots();
        }

        private void ComboBoxAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPlots(); // Your existing method to reload plots for the agent
            UpdateTotalBrokerage();
            UpdateAmountPaidTillDate();
            UpdateBalance();
        }

        private void ComboBoxPlotNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTotalBrokerage();
            UpdateAmountPaidTillDate();
            UpdateBalance();
        }

        private void LoadPlots()
        {
            if (comboBoxProperty.SelectedValue is int propertyId && comboBoxAgent.SelectedValue is int agentId)
            {
                string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
                string query = @"SELECT p.Id, p.PlotNumber 
                                 FROM Plot p
                                 INNER JOIN PlotSale ps ON p.Id = ps.PlotId
                                 WHERE p.PropertyId = @PropertyId AND ps.AgentId = @AgentId AND p.IsDeleted = 0";
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                    cmd.Parameters.AddWithValue("@AgentId", agentId);
                    var dt = new DataTable();
                    conn.Open();
                    adapter.Fill(dt);
                    comboBoxPlotNumber.DataSource = dt;
                    comboBoxPlotNumber.DisplayMember = "PlotNumber";
                    comboBoxPlotNumber.ValueMember = "Id";
                    comboBoxPlotNumber.SelectedIndex = -1;
                }
            }
            else
            {
                comboBoxPlotNumber.DataSource = null;
            }
        }

        private void UpdateTotalBrokerage()
        {
            if (comboBoxAgent.SelectedValue is int agentId &&
                comboBoxPlotNumber.SelectedValue is int plotId &&
                comboBoxProperty.SelectedValue is int propertyId)
            {
                string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
                string query = @"SELECT ISNULL(BrokerageAmount, 0) 
                                 FROM PlotSale 
                                 WHERE AgentId = @AgentId AND PlotId = @PlotId AND PropertyId = @PropertyId AND IsDeleted = 0";
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AgentId", agentId);
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    textBoxTotalBrokerage.Text = result != null ? Convert.ToDecimal(result).ToString("N2") : "0.00";
                }
            }
            else
            {
                textBoxTotalBrokerage.Text = "0.00";
            }
        }

        private void UpdateAmountPaidTillDate()
        {
            if (comboBoxAgent.SelectedValue is int agentId && comboBoxPlotNumber.SelectedValue is int plotId)
            {
                string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
                string query = @"SELECT ISNULL(SUM(Amount), 0) 
                                 FROM AgentTransaction 
                                 WHERE AgentId = @AgentId AND PlotId = @PlotId AND IsDeleted = 0";
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AgentId", agentId);
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    textBoxAmountPaidTillDate.Text = result != null ? Convert.ToDecimal(result).ToString("N2") : "0.00";
                }
            }
            else
            {
                textBoxAmountPaidTillDate.Text = "0.00";
            }
        }

        private void UpdateBalance()
        {
            decimal totalBrokerage = 0;
            decimal.TryParse(textBoxTotalBrokerage.Text, out totalBrokerage);
            decimal amountToPay = 0, paid = 0;
            decimal.TryParse(textBoxAmount.Text, out amountToPay);
            decimal.TryParse(textBoxAmountPaidTillDate.Text, out paid);
            labelBalanceValue.Text = (totalBrokerage - (amountToPay + paid)).ToString("N2");
        }

        // Called when total brokerage or amount paid till date changes (not new payment)
        private void UpdateBalanceOnLoad()
        {
            decimal totalBrokerage = 0;
            decimal.TryParse(textBoxTotalBrokerage.Text, out totalBrokerage);
            labelBalanceValue.Text = (totalBrokerage - _amountPaidTillDate).ToString("N2");
        }

        // Called when amount paid till date changes
        private void TextBoxAmountPaidTillDate_TextChanged(object sender, EventArgs e)
        {
            decimal.TryParse(textBoxAmountPaidTillDate.Text, out _amountPaidTillDate);
            UpdateBalanceOnLoad();
        }

        // Called when amount to pay (new payment) changes
        private void UpdateBalanceAmount(object sender, EventArgs e)
        {
            decimal totalBrokerage = 0;
            decimal newPaid = 0;
            decimal.TryParse(textBoxTotalBrokerage.Text, out totalBrokerage);
            decimal.TryParse(textBoxAmount.Text, out newPaid);
            labelBalanceValue.Text = (totalBrokerage - (_amountPaidTillDate + newPaid)).ToString("N2");
        }

        // Helper to load plots for a specific property and agent
        private void LoadPlotsForPropertyAndAgent(int propertyId, int agentId)
        {
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = @"SELECT p.Id, p.PlotNumber 
                             FROM Plot p
                             INNER JOIN PlotSale ps ON p.Id = ps.PlotId
                             WHERE p.PropertyId = @PropertyId AND ps.AgentId = @AgentId AND p.IsDeleted = 0";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                cmd.Parameters.AddWithValue("@AgentId", agentId);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                comboBoxPlotNumber.DataSource = dt;
                comboBoxPlotNumber.DisplayMember = "PlotNumber";
                comboBoxPlotNumber.ValueMember = "Id";
                comboBoxPlotNumber.SelectedIndex = -1;
            }
        }
    }
}