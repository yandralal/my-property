using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyPropertyApi.Models;

namespace MyPropertyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PlotController : ControllerBase
    {
        private readonly IConfiguration _config;

        public PlotController(IConfiguration config)
        {
            _config = config;
        }

        private string GetUserName()
        {
            return User?.Identity?.Name ?? User?.FindFirst("UserId")?.Value ?? "Admin";
        }

        // Add a single plot
        [HttpPost]
        public async Task<ActionResult> AddPlot([FromBody] PlotDetailsDto plot)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string insert = @"INSERT INTO Plot
                    (PlotNumber, Status, Area, PropertyId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted)
                    VALUES (@PlotNumber, @Status, @Area, @PropertyId, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, 0);
                    SELECT SCOPE_IDENTITY();";
                using var cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@PlotNumber", plot.PlotNumber ?? "");
                cmd.Parameters.AddWithValue("@Status", plot.Status ?? "");
                cmd.Parameters.AddWithValue("@Area", plot.Area);
                cmd.Parameters.AddWithValue("@PropertyId", plot.PropertyId); 
                cmd.Parameters.AddWithValue("@CreatedBy", userName);
                cmd.Parameters.AddWithValue("@CreatedDate", now);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                var newId = await cmd.ExecuteScalarAsync();
                return Ok(new { Success = true, PlotId = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding plot: {ex.Message}");
            }
        }

        // Add multiple plots
        [HttpPost("bulk")]
        public async Task<ActionResult> AddPlotsBulk([FromBody] BulkAddPlotsRequest request)
        {
            var plots = request.Plots;
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                foreach (var plot in plots)
                {
                    string insert = @"INSERT INTO Plot
                        (PlotNumber, Status, Area, PropertyId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted)
                        VALUES (@PlotNumber, @Status, @Area, @PropertyId, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, 0);";
                    using var cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@PlotNumber", plot.PlotNumber ?? "");
                    cmd.Parameters.AddWithValue("@Status", plot.Status ?? "");
                    cmd.Parameters.AddWithValue("@Area", plot.Area);
                    cmd.Parameters.AddWithValue("@PropertyId", plot.PropertyId); 
                    cmd.Parameters.AddWithValue("@CreatedBy", userName);
                    cmd.Parameters.AddWithValue("@CreatedDate", now);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", now);
                    await cmd.ExecuteNonQueryAsync();
                }
                return Ok(new { Success = true, Message = "Plots added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding plots: {ex.Message}");
            }
        }

        // Edit a plot
        [HttpPut("{id}")]
        public async Task<ActionResult> EditPlot(int id, [FromBody] PlotDetailsDto plot)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string update = @"UPDATE Plot SET
                    PlotNumber = @PlotNumber,
                    Status = @Status,
                    Area = @Area,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                    WHERE Id = @Id AND IsDeleted = 0";
                using var cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@PlotNumber", plot.PlotNumber ?? "");
                cmd.Parameters.AddWithValue("@Status", plot.Status ?? "");
                cmd.Parameters.AddWithValue("@Area", plot.Area);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                cmd.Parameters.AddWithValue("@Id", id);
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Plot updated successfully." });
                else
                    return NotFound("Plot not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating plot: {ex.Message}");
            }
        }

        // Delete a plot (soft delete)
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlot(int id)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string delete = @"UPDATE Plot SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE Id = @Id";
                using var cmd = new SqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Plot deleted successfully." });
                else
                    return NotFound("Plot not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting plot: {ex.Message}");
            }
        }

        // Bulk delete plots (soft delete)
        [HttpPost("bulk-delete")]
        public async Task<ActionResult> BulkDeletePlots([FromBody] BulkDeletePlotsRequest request)
        {
            var plotIds = request.PlotIds;
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            if (plotIds == null)
                return BadRequest("No plot IDs provided.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                var idList = string.Join(",", plotIds);
                string delete = $"UPDATE Plot SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE Id IN ({idList})";
                using var cmd = new SqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                int rows = await cmd.ExecuteNonQueryAsync();

                return Ok(new { Success = true, Message = $"{rows} plot(s) deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting plots: {ex.Message}");
            }
        }

        // Get a plot by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PlotDetailsDto>> GetPlotById(int id)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT 
                    p.Id,   
                    ps.SaleId,
                    p.PropertyId,
                    p.PlotNumber,
                    p.Status,
                    p.Area,
                    ps.SaleDate,
                    ps.SaleAmount,
                    ps.CustomerName,
                    ps.CustomerPhone,
                    ps.CustomerEmail,
                    ps.AgentId,
                    ps.BrokerageAmount,
                    CASE WHEN ps.PlotId IS NOT NULL THEN 1 ELSE 0 END AS HasSale
                FROM Plot p
                LEFT JOIN PlotSale ps ON p.Id = ps.PlotId
                WHERE p.Id = @Id AND p.IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var saleAmount = reader["SaleAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["SaleAmount"]);
                    decimal amountPaid = 0;

                    // Get AmountPaid from PlotTransaction
                    using (var conn2 = new SqlConnection(connectionString))
                    {
                        await conn2.OpenAsync();
                        using (var transCmd = new SqlCommand("SELECT ISNULL(SUM(Amount), 0) FROM PlotTransaction WHERE PlotId = @PlotId AND IsDeleted = 0", conn2))
                        {
                            transCmd.Parameters.AddWithValue("@PlotId", id);
                            var result = await transCmd.ExecuteScalarAsync();
                            amountPaid = result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                        }
                    }
                    decimal amountBalance = saleAmount - amountPaid;

                    var plot = new PlotDetailsDto
                    {
                        Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                        SaleId = reader["SaleId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SaleId"]),
                        PropertyId = reader["PropertyId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PropertyId"]),
                        PlotNumber = reader["PlotNumber"] == DBNull.Value ? "" : reader["PlotNumber"].ToString(),
                        Status = reader["Status"] == DBNull.Value ? "" : reader["Status"].ToString(),
                        Area = reader["Area"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Area"]),
                        SaleDate = reader["SaleDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["SaleDate"]),
                        SaleAmount = saleAmount,
                        CustomerName = reader["CustomerName"] == DBNull.Value ? "" : reader["CustomerName"].ToString(),
                        CustomerPhone = reader["CustomerPhone"] == DBNull.Value ? "" : reader["CustomerPhone"].ToString(),
                        CustomerEmail = reader["CustomerEmail"] == DBNull.Value ? "" : reader["CustomerEmail"].ToString(),
                        HasSale = reader["HasSale"] != DBNull.Value && Convert.ToInt32(reader["HasSale"]) == 1,
                        AmountPaid = amountPaid,
                        AmountBalance = amountBalance,
                        AgentId = reader["AgentId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AgentId"]),
                        BrokerageAmount = reader["BrokerageAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BrokerageAmount"])
                    };
                    return Ok(plot);
                }
                else
                {
                    return NotFound($"Plot with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading plot database: {ex.Message}");
            }
        }

        // Add a plot sale
        [HttpPost("{plotId}/sale")]
        public async Task<ActionResult> AddPlotSale(int plotId, [FromBody] PlotSaleDto sale)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string insert = @"INSERT INTO PlotSale
                    (PropertyId, PlotId, CustomerName, CustomerPhone, CustomerEmail, SaleAmount, SaleDate, AgentId, BrokerageAmount, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted)
                    VALUES (@PropertyId, @PlotId, @CustomerName, @CustomerPhone, @CustomerEmail, @SaleAmount, @SaleDate, @AgentId, @BrokerageAmount, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, 0);
                    SELECT SCOPE_IDENTITY();";
                using var cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@PropertyId", sale.PropertyId);
                cmd.Parameters.AddWithValue("@PlotId", plotId);
                cmd.Parameters.AddWithValue("@CustomerName", sale.CustomerName ?? "");
                cmd.Parameters.AddWithValue("@CustomerPhone", sale.CustomerPhone ?? "");
                cmd.Parameters.AddWithValue("@CustomerEmail", sale.CustomerEmail ?? "");
                cmd.Parameters.AddWithValue("@SaleAmount", sale.SaleAmount);
                cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AgentId", sale.AgentId);
                cmd.Parameters.AddWithValue("@BrokerageAmount", sale.BrokerageAmount);
                cmd.Parameters.AddWithValue("@CreatedBy", userName);
                cmd.Parameters.AddWithValue("@CreatedDate", now);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                var newId = await cmd.ExecuteScalarAsync();
                return Ok(new { Success = true, PlotSaleId = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding plot sale: {ex.Message}");
            }
        }

        // Edit a plot sale
        [HttpPut("sale/{saleId}")]
        public async Task<ActionResult> EditPlotSale(int saleId, [FromBody] PlotSaleDto sale)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string update = @"UPDATE PlotSale SET
                    PropertyId = @PropertyId,
                    PlotId = @PlotId,
                    CustomerName = @CustomerName,
                    CustomerPhone = @CustomerPhone,
                    CustomerEmail = @CustomerEmail,
                    SaleAmount = @SaleAmount,
                    SaleDate = @SaleDate,
                    AgentId = @AgentId,
                    BrokerageAmount = @BrokerageAmount,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                    WHERE SaleId = @SaleId AND IsDeleted = 0";
                using var cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@PropertyId", sale.PropertyId);
                cmd.Parameters.AddWithValue("@PlotId", sale.PlotId);
                cmd.Parameters.AddWithValue("@CustomerName", sale.CustomerName ?? "");
                cmd.Parameters.AddWithValue("@CustomerPhone", sale.CustomerPhone ?? "");
                cmd.Parameters.AddWithValue("@CustomerEmail", sale.CustomerEmail ?? "");
                cmd.Parameters.AddWithValue("@SaleAmount", sale.SaleAmount);
                cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AgentId", sale.AgentId);
                cmd.Parameters.AddWithValue("@BrokerageAmount", sale.BrokerageAmount);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                cmd.Parameters.AddWithValue("@SaleId", saleId);
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Plot sale updated successfully." });
                else
                    return NotFound("Plot sale not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating plot sale: {ex.Message}");
            }
        }

        // Delete a plot sale (soft delete)
        [HttpDelete("sale/{saleId}")]
        public async Task<ActionResult> DeletePlotSale(int saleId)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string delete = @"UPDATE PlotSale SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE SaleId = @SaleId";
                using var cmd = new SqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@SaleId", saleId);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Plot sale deleted successfully." });
                else
                    return NotFound("Plot sale not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting plot sale: {ex.Message}");
            }
        }

        #region PlotTransaction

        [HttpGet("transactions/active")]
        public async Task<ActionResult<IEnumerable<PlotTransactionDto>>> GetActivePlotTransactions()
        {
            var transactions = new List<PlotTransactionDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT TransactionId, PlotId, TransactionDate, TransactionType, Amount, PaymentMethod, ReferenceNumber, Notes,
                       CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted
                FROM PlotTransaction
                WHERE IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    transactions.Add(new PlotTransactionDto
                    {
                        TransactionId = reader["TransactionId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TransactionId"]),
                        PlotId = reader["PlotId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PlotId"]),
                        TransactionDate = reader["TransactionDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["TransactionDate"]),
                        TransactionType = reader["TransactionType"] == DBNull.Value ? "" : reader["TransactionType"].ToString(),
                        Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                        PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? "" : reader["PaymentMethod"].ToString(),
                        ReferenceNumber = reader["ReferenceNumber"] == DBNull.Value ? "" : reader["ReferenceNumber"].ToString(),
                        Notes = reader["Notes"] == DBNull.Value ? "" : reader["Notes"].ToString(),
                        CreatedBy = reader["CreatedBy"] == DBNull.Value ? "" : reader["CreatedBy"].ToString(),
                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? "" : reader["ModifiedBy"].ToString(),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                        IsDeleted = reader["IsDeleted"] != DBNull.Value && Convert.ToInt32(reader["IsDeleted"]) == 1
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading plot transactions: {ex.Message}");
            }

            return Ok(transactions);
        }

        [HttpGet("transactions/{id}")]
        public async Task<ActionResult<PlotTransactionDto>> GetPlotTransactionById(int id)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT TransactionId, PlotId, TransactionDate, TransactionType, Amount, PaymentMethod, ReferenceNumber, Notes,
                       CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted
                FROM PlotTransaction
                WHERE TransactionId = @Id";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var transaction = new PlotTransactionDto
                    {
                        TransactionId = reader["TransactionId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TransactionId"]),
                        PlotId = reader["PlotId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PlotId"]),
                        TransactionDate = reader["TransactionDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["TransactionDate"]),
                        TransactionType = reader["TransactionType"] == DBNull.Value ? "" : reader["TransactionType"].ToString(),
                        Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                        PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? "" : reader["PaymentMethod"].ToString(),
                        ReferenceNumber = reader["ReferenceNumber"] == DBNull.Value ? "" : reader["ReferenceNumber"].ToString(),
                        Notes = reader["Notes"] == DBNull.Value ? "" : reader["Notes"].ToString(),
                        CreatedBy = reader["CreatedBy"] == DBNull.Value ? "" : reader["CreatedBy"].ToString(),
                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? "" : reader["ModifiedBy"].ToString(),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                        IsDeleted = reader["IsDeleted"] != DBNull.Value && Convert.ToInt32(reader["IsDeleted"]) == 1
                    };
                    return Ok(transaction);
                }
                else
                {
                    return NotFound($"PlotTransaction with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading plot transaction: {ex.Message}");
            }
        }

        [HttpPost("transactions")]
        public async Task<ActionResult> CreatePlotTransaction([FromBody] PlotTransactionDto transaction)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string insert = @"INSERT INTO PlotTransaction
                    (PlotId, TransactionDate, TransactionType, Amount, PaymentMethod, ReferenceNumber, Notes, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted)
                    VALUES (@PlotId, @TransactionDate, @TransactionType, @Amount, @PaymentMethod, @ReferenceNumber, @Notes, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, 0);
                    SELECT SCOPE_IDENTITY();";
                using var cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@PlotId", transaction.PlotId);
                cmd.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
                cmd.Parameters.AddWithValue("@TransactionType", transaction.TransactionType ?? "");
                cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                cmd.Parameters.AddWithValue("@PaymentMethod", transaction.PaymentMethod ?? "");
                cmd.Parameters.AddWithValue("@ReferenceNumber", transaction.ReferenceNumber ?? "");
                cmd.Parameters.AddWithValue("@Notes", transaction.Notes ?? "");
                cmd.Parameters.AddWithValue("@CreatedBy", userName);
                cmd.Parameters.AddWithValue("@CreatedDate", now);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                var newId = await cmd.ExecuteScalarAsync();
                return Ok(new { Success = true, TransactionId = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating plot transaction: {ex.Message}");
            }
        }

        [HttpPut("transactions/{id}")]
        public async Task<ActionResult> EditPlotTransaction(int id, [FromBody] PlotTransactionDto transaction)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string update = @"UPDATE PlotTransaction SET
                    PlotId = @PlotId,
                    TransactionDate = @TransactionDate,
                    TransactionType = @TransactionType,
                    Amount = @Amount,
                    PaymentMethod = @PaymentMethod,
                    ReferenceNumber = @ReferenceNumber,
                    Notes = @Notes,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                    WHERE TransactionId = @Id AND IsDeleted = 0";
                using var cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@PlotId", transaction.PlotId);
                cmd.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
                cmd.Parameters.AddWithValue("@TransactionType", transaction.TransactionType ?? "");
                cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                cmd.Parameters.AddWithValue("@PaymentMethod", transaction.PaymentMethod ?? "");
                cmd.Parameters.AddWithValue("@ReferenceNumber", transaction.ReferenceNumber ?? "");
                cmd.Parameters.AddWithValue("@Notes", transaction.Notes ?? "");
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                cmd.Parameters.AddWithValue("@Id", id);
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Plot transaction updated successfully." });
                else
                    return NotFound("Plot transaction not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating plot transaction: {ex.Message}");
            }
        }

        [HttpDelete("transactions/{id}")]
        public async Task<ActionResult> DeletePlotTransaction(int id)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string delete = @"UPDATE PlotTransaction SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE TransactionId = @Id";
                using var cmd = new SqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Plot transaction deleted successfully." });
                else
                    return NotFound("Plot transaction not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting plot transaction: {ex.Message}");
            }
        }

        // Get all transactions for a specific plot
        [HttpGet("{plotId}/transactions")]
        public async Task<ActionResult<IEnumerable<PlotTransactionDto>>> GetTransactionsForPlot(int plotId)
        {
            var transactions = new List<PlotTransactionDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT TransactionId, PlotId, TransactionDate, TransactionType, Amount, PaymentMethod, ReferenceNumber, Notes,
                       CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted
                FROM PlotTransaction
                WHERE IsDeleted = 0 AND PlotId = @PlotId";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PlotId", plotId);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    transactions.Add(new PlotTransactionDto
                    {
                        TransactionId = reader["TransactionId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TransactionId"]),
                        PlotId = reader["PlotId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PlotId"]),
                        TransactionDate = reader["TransactionDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["TransactionDate"]),
                        TransactionType = reader["TransactionType"] == DBNull.Value ? "" : reader["TransactionType"].ToString(),
                        Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                        PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? "" : reader["PaymentMethod"].ToString(),
                        ReferenceNumber = reader["ReferenceNumber"] == DBNull.Value ? "" : reader["ReferenceNumber"].ToString(),
                        Notes = reader["Notes"] == DBNull.Value ? "" : reader["Notes"].ToString(),
                        CreatedBy = reader["CreatedBy"] == DBNull.Value ? "" : reader["CreatedBy"].ToString(),
                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? "" : reader["ModifiedBy"].ToString(),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                        IsDeleted = reader["IsDeleted"] != DBNull.Value && Convert.ToInt32(reader["IsDeleted"]) == 1
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading plot transactions: {ex.Message}");
            }

            return Ok(transactions);
        }
        #endregion
    }
}