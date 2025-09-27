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
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            var agents = new List<object>();

            string agentQuery = @"SELECT Id, Name, Contact, Agency FROM Agent WHERE IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(agentQuery, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                var agentList = new List<(int Id, string Name, string Contact, string Agency)>();
                while (await reader.ReadAsync())
                {
                    agentList.Add((
                        reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                        reader["Name"] == DBNull.Value ? "" : reader["Name"].ToString(),
                        reader["Contact"] == DBNull.Value ? "" : reader["Contact"].ToString(),
                        reader["Agency"] == DBNull.Value ? "" : reader["Agency"].ToString()
                    ));
                }
                reader.Close();

                foreach (var agent in agentList)
                {
                    // Get total brokerage
                    decimal totalBrokerage = 0;
                    using (var cmdBrokerage = new SqlCommand(
                        "SELECT ISNULL(SUM(BrokerageAmount), 0) FROM PlotSale WHERE AgentId = @AgentId AND IsDeleted = 0", conn))
                    {
                        cmdBrokerage.Parameters.AddWithValue("@AgentId", agent.Id);
                        var result = await cmdBrokerage.ExecuteScalarAsync();
                        totalBrokerage = result != null ? Convert.ToDecimal(result) : 0m;
                    }

                    // Get total paid
                    decimal totalPaid = 0;
                    using (var cmdPaid = new SqlCommand(
                        "SELECT ISNULL(SUM(Amount), 0) FROM AgentTransaction WHERE AgentId = @AgentId AND IsDeleted = 0", conn))
                    {
                        cmdPaid.Parameters.AddWithValue("@AgentId", agent.Id);
                        var result = await cmdPaid.ExecuteScalarAsync();
                        totalPaid = result != null ? Convert.ToDecimal(result) : 0m;
                    }

                    agents.Add(new
                    {
                        agent.Id,
                        agent.Name,
                        agent.Contact,
                        agent.Agency,
                        TotalBrokerage = totalBrokerage,
                        Paid = totalPaid,
                        Balance = totalBrokerage - totalPaid
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching brokerage summary for all agents: {ex.Message}");
            }

            return Ok(agents);
        }

        [HttpGet("{agentId}")]
        public async Task<ActionResult<AgentDto>> GetAgentById(int agentId)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT Id, Name, Contact, Agency, IsDeleted, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
                FROM Agent
                WHERE Id = @AgentId AND IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AgentId", agentId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var agent = new AgentDto
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
                    };
                    return Ok(agent);
                }
                else
                {
                    return NotFound("Agent not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching agent: {ex.Message}");
            }
        }

        [HttpGet("{agentId}/transactions")]
        public async Task<ActionResult<IEnumerable<AgentTransactionDto>>> GetAgentTransactions(int agentId)
        {
            var transactions = new List<AgentTransactionDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT 
                    pt.TransactionId,
                    pt.AgentId,
                    pt.PlotId,
                    p.PropertyId,
                    pt.TransactionDate,
                    pt.Amount,
                    pt.PaymentMethod,
                    pt.ReferenceNumber,
                    pt.TransactionType,
                    pt.Notes,
                    pt.CreatedBy,
                    pt.CreatedDate,
                    pt.ModifiedBy,
                    pt.ModifiedDate,
                    pt.IsDeleted,
                    a.Name AS AgentName,
                    pr.Title AS PropertyName,
                    p.PlotNumber AS PlotName
                FROM AgentTransaction pt
                INNER JOIN Plot p ON pt.PlotId = p.Id
                INNER JOIN Agent a ON pt.AgentId = a.Id
                INNER JOIN Property pr ON p.PropertyId = pr.Id
                WHERE pt.AgentId = @AgentId AND pt.IsDeleted = 0 AND p.IsDeleted = 0 AND a.IsDeleted = 0 AND pr.IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AgentId", agentId);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    transactions.Add(new AgentTransactionDto
                    {
                        TransactionId = reader["TransactionId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TransactionId"]),
                        AgentId = reader["AgentId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AgentId"]),
                        PlotId = reader["PlotId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PlotId"]),
                        PropertyId = reader["PropertyId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PropertyId"]),
                        TransactionDate = (DateTime)(reader["TransactionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["TransactionDate"])),
                        Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                        PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? "" : reader["PaymentMethod"].ToString(),
                        ReferenceNumber = reader["ReferenceNumber"] == DBNull.Value ? "" : reader["ReferenceNumber"].ToString(),
                        TransactionType = reader["TransactionType"] == DBNull.Value ? "" : reader["TransactionType"].ToString(),
                        Notes = reader["Notes"] == DBNull.Value ? "" : reader["Notes"].ToString(),
                        CreatedBy = reader["CreatedBy"] == DBNull.Value ? "" : reader["CreatedBy"].ToString(),
                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? "" : reader["ModifiedBy"].ToString(),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                        IsDeleted = reader["IsDeleted"] != DBNull.Value && Convert.ToInt32(reader["IsDeleted"]) == 1,
                        AgentName = reader["AgentName"] == DBNull.Value ? "" : reader["AgentName"].ToString(),
                        PropertyName = reader["PropertyName"] == DBNull.Value ? "" : reader["PropertyName"].ToString(),
                        PlotName = reader["PlotName"] == DBNull.Value ? "" : reader["PlotName"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading agent transactions: {ex.Message}");
            }

            return Ok(transactions);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAgent(int id)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = User?.Identity?.Name ?? "Admin";
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                // Soft delete the agent
                string deleteAgent = @"UPDATE Agent SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE Id = @Id";
                using (var cmd = new SqlCommand(deleteAgent, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", now);
                    int rows = await cmd.ExecuteNonQueryAsync();
                    if (rows == 0)
                        return NotFound("Agent not found or already deleted.");
                }

                // Soft delete all agent transactions
                string deleteTransactions = @"UPDATE AgentTransaction SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE AgentId = @AgentId";
                using (var cmd = new SqlCommand(deleteTransactions, conn))
                {
                    cmd.Parameters.AddWithValue("@AgentId", id);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", now);
                    await cmd.ExecuteNonQueryAsync();
                }

                return Ok(new { Success = true, Message = "Agent and all related transactions deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting agent: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAgent([FromBody] AgentDto agent)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = User?.Identity?.Name ?? "Admin";
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string insert = @"INSERT INTO Agent
                    (Name, Contact, Agency, IsDeleted, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
                    VALUES (@Name, @Contact, @Agency, 0, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate);
                    SELECT SCOPE_IDENTITY();";
                using var cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@Name", agent.Name ?? "");
                cmd.Parameters.AddWithValue("@Contact", agent.Contact ?? "");
                cmd.Parameters.AddWithValue("@Agency", agent.Agency ?? "");
                cmd.Parameters.AddWithValue("@CreatedBy", userName);
                cmd.Parameters.AddWithValue("@CreatedDate", now);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                var newId = await cmd.ExecuteScalarAsync();
                return Ok(new { Success = true, AgentId = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating agent: {ex.Message}");
            }
        }

        [HttpPut("{agentId}")]
        public async Task<ActionResult> UpdateAgent(int agentId, [FromBody] AgentDto agent)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = User?.Identity?.Name ?? "Admin";
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                string update = @"
            UPDATE Agent
            SET Name = @Name,
                Contact = @Contact,
                Agency = @Agency,
                ModifiedBy = @ModifiedBy,
                ModifiedDate = @ModifiedDate
            WHERE Id = @Id AND IsDeleted = 0";

                using var cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@Id", agentId);
                cmd.Parameters.AddWithValue("@Name", agent.Name ?? "");
                cmd.Parameters.AddWithValue("@Contact", agent.Contact ?? "");
                cmd.Parameters.AddWithValue("@Agency", agent.Agency ?? "");
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);

                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Agent updated successfully." });
                else
                    return NotFound("Agent not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating agent: {ex.Message}");
            }
        }

        [HttpGet("by-property/{propertyId}")]
        public async Task<ActionResult<IEnumerable<AgentDto>>> GetAgentsByProperty(int propertyId)
        {
            var agents = new List<AgentDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT DISTINCT 
                    a.Id, a.Name, a.Contact, a.Agency, a.IsDeleted, a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate
                FROM PlotSale ps
                INNER JOIN Agent a ON ps.AgentId = a.Id
                WHERE ps.PropertyId = @PropertyId AND ps.IsDeleted = 0 AND a.IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
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
                return StatusCode(500, $"Error fetching agents for property: {ex.Message}");
            }

            return Ok(agents);
        }

        [HttpGet("plots/by-agent-property")]
        public async Task<ActionResult<IEnumerable<PlotDetailsDto>>> GetPlotsByAgentAndProperty([FromQuery] int agentId, [FromQuery] int propertyId)
        {
            var plots = new List<PlotDetailsDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
        SELECT p.Id, p.PropertyId, p.PlotNumber, p.Status, p.Area, ps.SaleId, ps.SaleDate, ps.SaleAmount, ps.CustomerName, ps.CustomerPhone, ps.CustomerEmail, ps.AgentId, ps.BrokerageAmount
        FROM PlotSale ps
        INNER JOIN Plot p ON ps.PlotId = p.Id
        WHERE ps.AgentId = @AgentId AND ps.PropertyId = @PropertyId AND ps.IsDeleted = 0 AND p.IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AgentId", agentId);
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    plots.Add(new PlotDetailsDto
                    {
                        Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                        PropertyId = reader["PropertyId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PropertyId"]),
                        PlotNumber = reader["PlotNumber"] == DBNull.Value ? "" : reader["PlotNumber"].ToString(),
                        Status = reader["Status"] == DBNull.Value ? "" : reader["Status"].ToString(),
                        Area = reader["Area"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Area"]),
                        SaleId = reader["SaleId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SaleId"]),
                        SaleDate = reader["SaleDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["SaleDate"]),
                        SaleAmount = reader["SaleAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["SaleAmount"]),
                        CustomerName = reader["CustomerName"] == DBNull.Value ? "" : reader["CustomerName"].ToString(),
                        CustomerPhone = reader["CustomerPhone"] == DBNull.Value ? "" : reader["CustomerPhone"].ToString(),
                        CustomerEmail = reader["CustomerEmail"] == DBNull.Value ? "" : reader["CustomerEmail"].ToString(),
                        AgentId = reader["AgentId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AgentId"]),
                        BrokerageAmount = reader["BrokerageAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BrokerageAmount"])
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching plot details: {ex.Message}");
            }

            return Ok(plots);
        }

        [HttpGet("brokerage-summary")]
        public async Task<ActionResult> GetBrokerageSummary([FromQuery] int propertyId, [FromQuery] int agentId, [FromQuery] int plotId)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT
                    ISNULL(SUM(ps.BrokerageAmount), 0) AS TotalBrokerage,
                    ISNULL((
                        SELECT SUM(at.Amount) FROM AgentTransaction at
                        WHERE at.PropertyId = ps.PropertyId AND at.PlotId = ps.PlotId AND at.AgentId = ps.AgentId AND at.IsDeleted = 0
                    ), 0) AS PaidTillDate
                FROM PlotSale ps
                WHERE ps.PropertyId = @PropertyId AND ps.PlotId = @PlotId AND ps.AgentId = @AgentId AND ps.IsDeleted = 0
                GROUP BY ps.PropertyId, ps.PlotId, ps.AgentId";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                cmd.Parameters.AddWithValue("@AgentId", agentId);
                cmd.Parameters.AddWithValue("@PlotId", plotId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    decimal totalBrokerage = reader["TotalBrokerage"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalBrokerage"]);
                    decimal paidTillDate = reader["PaidTillDate"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PaidTillDate"]);
                    decimal outstandingBrokerage = totalBrokerage - paidTillDate;

                    var result = new
                    {
                        TotalBrokerage = totalBrokerage,
                        PaidTillDate = paidTillDate,
                        OutstandingBrokerage = outstandingBrokerage
                    };
                    return Ok(result);
                }
                else
                {
                    return NotFound("No summary found for the selected property, agent, and plot.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching brokerage summary: {ex.Message}");
            }
        }

        [HttpPost("transactions")]
        public async Task<ActionResult> CreateAgentTransaction([FromBody] CreateAgentTransactionRequest request)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = User?.Identity?.Name ?? "Admin";
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string insert = @"INSERT INTO AgentTransaction
            (AgentId, PlotId, PropertyId, TransactionDate, Amount, PaymentMethod, ReferenceNumber, TransactionType, Notes, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted)
            VALUES (@AgentId, @PlotId, @PropertyId, @TransactionDate, @Amount, @PaymentMethod, @ReferenceNumber, @TransactionType, @Notes, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, 0);
            SELECT SCOPE_IDENTITY();";
                using var cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@AgentId", request.AgentId);
                cmd.Parameters.AddWithValue("@PlotId", request.PlotId);
                cmd.Parameters.AddWithValue("@PropertyId", request.PropertyId);
                cmd.Parameters.AddWithValue("@TransactionDate", request.TransactionDate);
                cmd.Parameters.AddWithValue("@Amount", request.Amount);
                cmd.Parameters.AddWithValue("@PaymentMethod", request.PaymentMethod ?? "");
                cmd.Parameters.AddWithValue("@ReferenceNumber", request.ReferenceNumber ?? "");
                cmd.Parameters.AddWithValue("@TransactionType", request.TransactionType ?? "");
                cmd.Parameters.AddWithValue("@Notes", request.Notes ?? "");
                cmd.Parameters.AddWithValue("@CreatedBy", userName);
                cmd.Parameters.AddWithValue("@CreatedDate", now);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                var newId = await cmd.ExecuteScalarAsync();
                return Ok(new { Success = true, TransactionId = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating agent transaction: {ex.Message}");
            }
        }

        [HttpDelete("transactions/{id}")]
        public async Task<ActionResult> DeleteAgentTransaction(int id)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = User?.Identity?.Name ?? "Admin";
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string delete = @"UPDATE AgentTransaction SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE TransactionId = @Id";
                using var cmd = new SqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Agent transaction deleted successfully." });
                else
                    return NotFound("Agent transaction not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting agent transaction: {ex.Message}");
            }
        }
    }
}