using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyPropertyApi.Models;

namespace MyPropertyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AgentController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AgentController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentDto>>> GetAgents()
        {
            var agents = new List<AgentDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT [Id], [Name], [Contact], [Agency], [IsDeleted], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]
                FROM Agent";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    agents.Add(new AgentDto
                    {
                        Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"] == DBNull.Value ? "" : reader["Name"].ToString(),
                        Contact = reader["Contact"] == DBNull.Value ? "" : reader["Contact"].ToString(),
                        Agency = reader["Agency"] == DBNull.Value ? "" : reader["Agency"].ToString(),
                        IsDeleted = reader["IsDeleted"] != DBNull.Value && Convert.ToInt32(reader["IsDeleted"]) == 1,
                        CreatedBy = reader["CreatedBy"] == DBNull.Value ? "" : reader["CreatedBy"].ToString(),
                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? "" : reader["ModifiedBy"].ToString(),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"])
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading agent database: {ex.Message}");
            }

            return Ok(agents);
        }
    }
}