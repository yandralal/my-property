using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyPropertyApi.Models;

namespace MyPropertyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MiscTransactionController : ControllerBase
    {
        private readonly IConfiguration _config;

        public MiscTransactionController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MiscTransactionDto>>> GetMiscTransactions()
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            var transactions = new List<MiscTransactionDto>();

            string query = @"
                SELECT 
                    TransactionId,
                    TransactionDate,
                    Amount,
                    PaymentMethod,
                    ReferenceNumber,
                    Recipient,
                    Notes,
                    TransactionType,
                    CreatedBy,
                    CreatedDate,
                    ModifiedBy,
                    ModifiedDate,
                    IsDeleted
                FROM MiscTransaction
                WHERE IsDeleted = 0
                ORDER BY TransactionDate DESC";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    transactions.Add(new MiscTransactionDto
                    {
                        TransactionId = reader["TransactionId"] == DBNull.Value ? "" : reader["TransactionId"].ToString(),
                        TransactionDate = reader["TransactionDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["TransactionDate"]),
                        Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                        PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? "" : reader["PaymentMethod"].ToString(),
                        ReferenceNumber = reader["ReferenceNumber"] == DBNull.Value ? "" : reader["ReferenceNumber"].ToString(),
                        Recipient = reader["Recipient"] == DBNull.Value ? "" : reader["Recipient"].ToString(),
                        Notes = reader["Notes"] == DBNull.Value ? "" : reader["Notes"].ToString(),
                        TransactionType = reader["TransactionType"] == DBNull.Value ? "" : reader["TransactionType"].ToString(),
                        CreatedBy = reader["CreatedBy"] == DBNull.Value ? "" : reader["CreatedBy"].ToString(),
                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? "" : reader["ModifiedBy"].ToString(),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                        IsDeleted = reader["IsDeleted"] != DBNull.Value && Convert.ToInt32(reader["IsDeleted"]) == 1
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching misc transactions: {ex.Message}");
            }

            return Ok(transactions);
        }

        [HttpGet("{transactionId}")]
        public async Task<ActionResult<MiscTransactionDto>> GetMiscTransactionById(string transactionId)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT 
                    TransactionId,
                    TransactionDate,
                    Amount,
                    PaymentMethod,
                    ReferenceNumber,
                    Recipient,
                    Notes,
                    TransactionType,
                    CreatedBy,
                    CreatedDate,
                    ModifiedBy,
                    ModifiedDate,
                    IsDeleted
                FROM MiscTransaction
                WHERE TransactionId = @TransactionId AND IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var transaction = new MiscTransactionDto
                    {
                        TransactionId = reader["TransactionId"] == DBNull.Value ? "" : reader["TransactionId"].ToString(),
                        TransactionDate = reader["TransactionDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["TransactionDate"]),
                        Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                        PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? "" : reader["PaymentMethod"].ToString(),
                        ReferenceNumber = reader["ReferenceNumber"] == DBNull.Value ? "" : reader["ReferenceNumber"].ToString(),
                        Recipient = reader["Recipient"] == DBNull.Value ? "" : reader["Recipient"].ToString(),
                        Notes = reader["Notes"] == DBNull.Value ? "" : reader["Notes"].ToString(),
                        TransactionType = reader["TransactionType"] == DBNull.Value ? "" : reader["TransactionType"].ToString(),
                        CreatedBy = reader["CreatedBy"] == DBNull.Value ? "" : reader["CreatedBy"].ToString(),
                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? "" : reader["ModifiedBy"].ToString(),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                        IsDeleted = reader["IsDeleted"] != DBNull.Value && Convert.ToInt32(reader["IsDeleted"]) == 1
                    };
                    return Ok(transaction);
                }
                else
                {
                    return NotFound("Misc transaction not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching misc transaction: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateMiscTransaction([FromBody] CreateMiscTransactionRequest request)
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
                string insert = @"
                    INSERT INTO MiscTransaction
                    (TransactionDate, Amount, PaymentMethod, ReferenceNumber, Recipient, Notes, TransactionType, CreatedBy, CreatedDate, IsDeleted)
                    VALUES
                    (@TransactionDate, @Amount, @PaymentMethod, @ReferenceNumber, @Recipient, @Notes, @TransactionType, @CreatedBy, @CreatedDate, 0);
                    SELECT SCOPE_IDENTITY();";
                using var cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@TransactionDate", request.TransactionDate);
                cmd.Parameters.AddWithValue("@Amount", request.Amount);
                cmd.Parameters.AddWithValue("@PaymentMethod", request.PaymentMethod ?? "");
                cmd.Parameters.AddWithValue("@ReferenceNumber", request.ReferenceNumber ?? "");
                cmd.Parameters.AddWithValue("@Recipient", request.Recipient ?? "");
                cmd.Parameters.AddWithValue("@Notes", request.Notes ?? "");
                cmd.Parameters.AddWithValue("@TransactionType", request.TransactionType ?? "");
                cmd.Parameters.AddWithValue("@CreatedBy", userName);
                cmd.Parameters.AddWithValue("@CreatedDate", now);

                var newId = await cmd.ExecuteScalarAsync();
                return Ok(new { Success = true, TransactionId = newId, Message = "Misc transaction created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating misc transaction: {ex.Message}");
            }
        }

        [HttpPut("{transactionId}")]
        public async Task<ActionResult> UpdateMiscTransaction(string transactionId, [FromBody] UpdateMiscTransactionRequest request)
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
                    UPDATE MiscTransaction SET
                        TransactionDate = @TransactionDate,
                        Amount = @Amount,
                        PaymentMethod = @PaymentMethod,
                        ReferenceNumber = @ReferenceNumber,
                        Recipient = @Recipient,
                        Notes = @Notes,
                        TransactionType = @TransactionType,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = @ModifiedDate
                    WHERE TransactionId = @TransactionId AND IsDeleted = 0";

                using var cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                cmd.Parameters.AddWithValue("@TransactionDate", request.TransactionDate);
                cmd.Parameters.AddWithValue("@Amount", request.Amount);
                cmd.Parameters.AddWithValue("@PaymentMethod", request.PaymentMethod ?? "");
                cmd.Parameters.AddWithValue("@ReferenceNumber", request.ReferenceNumber ?? "");
                cmd.Parameters.AddWithValue("@Recipient", request.Recipient ?? "");
                cmd.Parameters.AddWithValue("@Notes", request.Notes ?? "");
                cmd.Parameters.AddWithValue("@TransactionType", request.TransactionType ?? "");
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);

                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Misc transaction updated successfully." });
                else
                    return NotFound("Misc transaction not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating misc transaction: {ex.Message}");
            }
        }

        [HttpDelete("{transactionId}")]
        public async Task<ActionResult> DeleteMiscTransaction(string transactionId)
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

                string delete = @"
                    UPDATE MiscTransaction 
                    SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate 
                    WHERE TransactionId = @TransactionId";

                using var cmd = new SqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);

                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Misc transaction deleted successfully." });
                else
                    return NotFound("Misc transaction not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting misc transaction: {ex.Message}");
            }
        }
    }
}
