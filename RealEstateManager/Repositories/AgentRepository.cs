using Microsoft.Data.SqlClient;
using RealEstateManager.Entities;

namespace RealEstateManager.Repositories
{
    public static class AgentRepository
    {
        private static readonly string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";

        public static void AddAgent(Agent agent)
        {
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("INSERT INTO Agent (Name, Contact, Agency, CreatedDate) VALUES (@Name, @Contact, @Agency, @CreatedDate)", conn);
            cmd.Parameters.AddWithValue("@Name", agent.Name);
            cmd.Parameters.AddWithValue("@Contact", (object?)agent.Contact ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Agency", (object?)agent.Agency ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public static void UpdateAgent(Agent agent)
        {
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("UPDATE Agent SET Name = @Name, Contact = @Contact, Agency = @Agency WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", agent.Id);
            cmd.Parameters.AddWithValue("@Name", agent.Name);
            cmd.Parameters.AddWithValue("@Contact", (object?)agent.Contact ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Agency", (object?)agent.Agency ?? DBNull.Value);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public static void DeleteAgent(int agentId)
        {
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("DELETE FROM Agent WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", agentId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public static List<Agent> GetAllAgents()
        {
            var agents = new List<Agent>();
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("SELECT Id, Name, Contact, Agency FROM Agent", conn);
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
    }
}