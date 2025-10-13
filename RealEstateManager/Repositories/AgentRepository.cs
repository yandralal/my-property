using Microsoft.Data.SqlClient;
using RealEstateManager.Entities;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace RealEstateManager.Repositories
{
    public static class AgentRepository
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MyPropertyDb"].ConnectionString;
        public static event Action? AgentsChanged;

        public static void AddAgent(Agent agent)
        {
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("INSERT INTO Agent (Name, Contact, Agency, CreatedDate, ModifiedDate, CreatedBy, ModifiedBy) VALUES (@Name, @Contact, @Agency, @CreatedDate, @ModifiedDate, @CreatedBy, @ModifiedBy)", conn);
            cmd.Parameters.AddWithValue("@Name", agent.Name);
            cmd.Parameters.AddWithValue("@Contact", (object?)agent.Contact ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Agency", (object?)agent.Agency ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", Environment.UserName);
            cmd.Parameters.AddWithValue("@ModifiedBy", Environment.UserName);
            conn.Open();
            cmd.ExecuteNonQuery();
            AgentsChanged?.Invoke();
        }

        public static void UpdateAgent(Agent agent)
        {
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("UPDATE Agent SET Name = @Name, Contact = @Contact, Agency = @Agency, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", agent.Id);
            cmd.Parameters.AddWithValue("@Name", agent.Name);
            cmd.Parameters.AddWithValue("@Contact", (object?)agent.Contact ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Agency", (object?)agent.Agency ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ModifiedBy", Environment.UserName);
            cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
            conn.Open();
            cmd.ExecuteNonQuery();
            AgentsChanged?.Invoke();
        }

        public static void DeleteAgent(int agentId)
        {
            // Calculate total brokerage and total paid for the agent
            decimal totalBrokerage = GetTotalBrokerage(agentId);
            decimal totalPaid = GetTotalPaid(agentId);

            // If there is pending brokerage, do not allow deletion
            if (totalBrokerage > totalPaid)
                throw new InvalidOperationException("Cannot delete agent: Pending brokerage exists.");

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("UPDATE Agent SET IsDeleted = 1 WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", agentId);
            conn.Open();
            cmd.ExecuteNonQuery();
            AgentsChanged?.Invoke();
        }

        public static List<Agent> GetAllAgents()
        {
            var agents = new List<Agent>();
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("SELECT Id, Name, Contact, Agency FROM Agent WHERE IsDeleted = 0", conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                agents.Add(new Agent
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Contact = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Agency = reader.IsDBNull(3) ? null : reader.GetString(3)
                });
            }
            return agents;
        }

        public static decimal GetTotalBrokerage(int agentId)
        {
            const string query = @"
            SELECT ISNULL(SUM(BrokerageAmount), 0)
            FROM PlotSale
            WHERE AgentId = @AgentId AND IsDeleted = 0";

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@AgentId", agentId);
            conn.Open();
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToDecimal(result) : 0m;
        }

        public static decimal GetTotalPaid(int agentId)
        {
            const string query = @"
            SELECT ISNULL(SUM(Amount), 0)
            FROM AgentTransaction
            WHERE AgentId = @AgentId AND IsDeleted = 0";

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@AgentId", agentId);
            conn.Open();
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToDecimal(result) : 0m;
        }

        public static void RaiseAgentsChanged()
        {
            AgentsChanged?.Invoke();
        }

        // Example usage for agent grid styling
        public static void ApplyLandingGridStyle(DataGridView grid)
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