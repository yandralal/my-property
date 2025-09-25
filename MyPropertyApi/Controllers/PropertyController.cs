using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyPropertyApi.Models;

namespace MyPropertyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private readonly IConfiguration _config;

        public PropertyController(IConfiguration config)
        {
            _config = config;
        }

        private string GetUserName()
        {
            return User?.Identity?.Name ?? User?.FindFirst("UserId")?.Value ?? "Admin";
        }

        #region Property
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<PropertyDetailsDto>>> GetActiveProperties()
        {
            var properties = new List<PropertyDetailsDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT 
                    p.Id, 
                    p.Title, 
                    p.Type, 
                    p.Status, 
                    p.Price AS [BuyPrice], 
                    p.Owner, 
                    p.Description,
                    p.Phone,
                    ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0) AS AmountPaid,
                    ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0) AS TotalLoanPrincipal,
                    (p.Price 
                        - ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0)
                        - ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0)
                    ) AS AmountBalance,
                    p.KhasraNo,
                    p.Area,
                    p.Address,
                    p.City,
                    p.State,
                    p.ZipCode
                FROM Property p
                WHERE p.IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(query, conn);
                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    properties.Add(new PropertyDetailsDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Title = reader["Title"]?.ToString() ?? "",
                        Type = reader["Type"]?.ToString() ?? "",
                        Status = reader["Status"]?.ToString() ?? "",
                        BuyPrice = reader["BuyPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BuyPrice"]),
                        Owner = reader["Owner"]?.ToString() ?? "",
                        Description = reader["Description"]?.ToString() ?? "",
                        Phone = reader["Phone"]?.ToString() ?? "",
                        AmountPaid = reader["AmountPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["AmountPaid"]),
                        TotalLoanPrincipal = reader["TotalLoanPrincipal"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalLoanPrincipal"]),
                        AmountBalance = reader["AmountBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["AmountBalance"]),
                        KhasraNo = reader["KhasraNo"]?.ToString() ?? "",
                        Area = reader["Area"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Area"]),
                        Address = reader["Address"]?.ToString() ?? "",
                        City = reader["City"]?.ToString() ?? "",
                        State = reader["State"]?.ToString() ?? "",
                        ZipCode = reader["ZipCode"]?.ToString() ?? ""
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading property database: {ex.Message}");
            }

            return Ok(properties);
        }

        [HttpGet("{propertyId}/plots")]
        public async Task<ActionResult<IEnumerable<PlotDetailsDto>>> GetPlotsForProperty(int propertyId)
        {
            var plots = new List<PlotDetailsDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string plotQuery = @"
                SELECT 
                    p.Id, 
                    p.PropertyId,
                    p.PlotNumber, 
                    p.Status, 
                    p.Area,
                    ps.SaleId,
                    ps.SaleDate,
                    ps.SaleAmount,
                    ps.CustomerName AS CustomerName,
                    ps.CustomerPhone AS CustomerPhone,
                    ps.CustomerEmail AS CustomerEmail,
                    CASE WHEN ps.PlotId IS NOT NULL THEN 1 ELSE 0 END AS HasSale
                FROM Plot p
                LEFT JOIN PlotSale ps ON p.Id = ps.PlotId
                WHERE p.PropertyId = @PropertyId AND p.IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(plotQuery, conn);
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var plotId = reader.GetInt32(reader.GetOrdinal("Id"));
                    var saleAmount = reader["SaleAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["SaleAmount"]);
                    decimal amountPaid = 0;

                    // Use a separate connection for the AmountPaid query
                    using (var conn2 = new SqlConnection(connectionString))
                    {
                        await conn2.OpenAsync();
                        using (var transCmd = new SqlCommand("SELECT ISNULL(SUM(Amount), 0) FROM PlotTransaction WHERE PlotId = @PlotId AND IsDeleted = 0", conn2))
                        {
                            transCmd.Parameters.AddWithValue("@PlotId", plotId);
                            var result = await transCmd.ExecuteScalarAsync();
                            amountPaid = result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                        }
                    }
                    decimal amountBalance = saleAmount - amountPaid;

                    plots.Add(new PlotDetailsDto
                    {
                        Id = plotId,
                        PropertyId = reader.GetInt32(reader.GetOrdinal("PropertyId")),
                        SaleId = reader["SaleId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SaleId"]),
                        PlotNumber = reader["PlotNumber"]?.ToString() ?? "",
                        Status = reader["Status"]?.ToString() ?? "",
                        Area = reader["Area"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Area"]),
                        SaleDate = reader["SaleDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["SaleDate"]),
                        SaleAmount = saleAmount,
                        CustomerName = reader["CustomerName"]?.ToString() ?? "",
                        CustomerPhone = reader["CustomerPhone"]?.ToString() ?? "",
                        CustomerEmail = reader["CustomerEmail"]?.ToString() ?? "",
                        HasSale = reader["HasSale"] != DBNull.Value && Convert.ToInt32(reader["HasSale"]) == 1,
                        AmountPaid = amountPaid,
                        AmountBalance = amountBalance
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading plot database: {ex.Message}");
            }

            return Ok(plots);
        }

        [HttpGet("{propertyId}/plots/basic")]
        public async Task<ActionResult<IEnumerable<PlotDetailsDto>>> GetBasicPlotsForProperty(int propertyId)
        {
            var plots = new List<PlotDetailsDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT 
                    Id, 
                    PropertyId,
                    PlotNumber, 
                    Status, 
                    Area
                FROM Plot
                WHERE PropertyId = @PropertyId AND IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    plots.Add(new PlotDetailsDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        PropertyId = reader.GetInt32(reader.GetOrdinal("PropertyId")), 
                        PlotNumber = reader["PlotNumber"]?.ToString() ?? "",
                        Status = reader["Status"]?.ToString() ?? "",
                        Area = reader["Area"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Area"])
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading plot database: {ex.Message}");
            }

            return Ok(plots);
        }

        [HttpGet("{propertyId}")]
        public async Task<ActionResult<PropertyDetailsDto>> GetPropertyById(int propertyId)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT 
                    p.Id, 
                    p.Title, 
                    p.Type, 
                    p.Status, 
                    p.Price AS [BuyPrice], 
                    p.Owner, 
                    p.Description,
                    p.Phone,
                    ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0) AS AmountPaid,
                    ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0) AS TotalLoanPrincipal,
                    (p.Price 
                        - ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0)
                        - ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0)
                    ) AS AmountBalance,
                    p.KhasraNo,
                    p.Area,
                    p.Address,
                    p.City,
                    p.State,
                    p.ZipCode
                FROM Property p
                WHERE p.IsDeleted = 0 AND p.Id = @PropertyId";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var property = new PropertyDetailsDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Title = reader["Title"]?.ToString() ?? "",
                        Type = reader["Type"]?.ToString() ?? "",
                        Status = reader["Status"]?.ToString() ?? "",
                        BuyPrice = reader["BuyPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BuyPrice"]),
                        Owner = reader["Owner"]?.ToString() ?? "",
                        Description = reader["Description"]?.ToString() ?? "",
                        Phone = reader["Phone"]?.ToString() ?? "",
                        AmountPaid = reader["AmountPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["AmountPaid"]),
                        TotalLoanPrincipal = reader["TotalLoanPrincipal"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalLoanPrincipal"]),
                        AmountBalance = reader["AmountBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["AmountBalance"]),
                        KhasraNo = reader["KhasraNo"]?.ToString() ?? "",
                        Area = reader["Area"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Area"]),
                        Address = reader["Address"]?.ToString() ?? "",
                        City = reader["City"]?.ToString() ?? "",
                        State = reader["State"]?.ToString() ?? "",
                        ZipCode = reader["ZipCode"]?.ToString() ?? ""
                    };
                    return Ok(property);
                }
                else
                {
                    return NotFound($"Property with ID {propertyId} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading property database: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterProperty([FromBody] RegisterPropertyRequest request)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userIdentifier = GetUserName();
            string modifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string createdDate = modifiedDate;
            int isDeleted = 0;

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                string insert = @"INSERT INTO Property
                    ([Title], [Type], [Status], [Price], [Owner], [KhasraNo], [Phone], [Address], [City], [State], [ZipCode], [Description],
                     [Area], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted])
                    VALUES
                    (@Title, @Type, @Status, @Price, @Owner, @KhasraNo, @Phone, @Address, @City, @State, @ZipCode, @Description,
                     @Area, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @IsDeleted);
                    SELECT SCOPE_IDENTITY();";
                using var cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@Title", request.Title);
                cmd.Parameters.AddWithValue("@Type", request.Type);
                cmd.Parameters.AddWithValue("@Status", request.Status);
                cmd.Parameters.AddWithValue("@Price", request.Price.ToString("F2"));
                cmd.Parameters.AddWithValue("@Owner", request.Owner);
                cmd.Parameters.AddWithValue("@Phone", request.Phone);
                cmd.Parameters.AddWithValue("@Address", request.Address ?? "");
                cmd.Parameters.AddWithValue("@City", request.City ?? "");
                cmd.Parameters.AddWithValue("@State", request.State ?? "");
                cmd.Parameters.AddWithValue("@ZipCode", request.ZipCode ?? "");
                cmd.Parameters.AddWithValue("@Description", request.Description ?? "");
                cmd.Parameters.AddWithValue("@CreatedBy", userIdentifier);
                cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
                cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                cmd.Parameters.AddWithValue("@IsDeleted", isDeleted);
                cmd.Parameters.AddWithValue("@KhasraNo", request.KhasraNo ?? "");
                cmd.Parameters.AddWithValue("@Area", request.Area.ToString("F2"));
                var newId = await cmd.ExecuteScalarAsync();
                return Ok(new { Success = true, Message = "Property registered successfully!", PropertyId = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditProperty(int id, [FromBody] RegisterPropertyRequest request)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userIdentifier = GetUserName();
            string modifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                string update = @"UPDATE Property SET
                    Title = @Title,
                    Type = @Type,
                    Status = @Status,
                    Price = @Price,
                    Owner = @Owner,
                    KhasraNo = @KhasraNo,
                    Phone = @Phone,
                    Address = @Address,
                    City = @City,
                    State = @State,
                    ZipCode = @ZipCode,
                    Description = @Description,
                    Area = @Area,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                    WHERE Id = @Id AND IsDeleted = 0";
                using var cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@Title", request.Title);
                cmd.Parameters.AddWithValue("@Type", request.Type);
                cmd.Parameters.AddWithValue("@Status", request.Status);
                cmd.Parameters.AddWithValue("@Price", request.Price.ToString("F2"));
                cmd.Parameters.AddWithValue("@Owner", request.Owner);
                cmd.Parameters.AddWithValue("@Phone", request.Phone);
                cmd.Parameters.AddWithValue("@Address", request.Address ?? "");
                cmd.Parameters.AddWithValue("@City", request.City ?? "");
                cmd.Parameters.AddWithValue("@State", request.State ?? "");
                cmd.Parameters.AddWithValue("@ZipCode", request.ZipCode ?? "");
                cmd.Parameters.AddWithValue("@Description", request.Description ?? "");
                cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@KhasraNo", request.KhasraNo ?? "");
                cmd.Parameters.AddWithValue("@Area", request.Area.ToString("F2"));
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Property updated successfully!", PropertyId = id });
                else
                    return NotFound("Property not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProperty(int id)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userIdentifier = GetUserName();
            string modifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                // Soft delete the property
                string deleteProperty = @"UPDATE Property SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE Id = @Id";
                using (var cmd = new SqlCommand(deleteProperty, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                    cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                    int rows = await cmd.ExecuteNonQueryAsync();

                    if (rows == 0)
                        return NotFound("Property not found or already deleted.");
                }

                // Soft delete all plots under the property
                string deletePlots = @"UPDATE Plot SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE PropertyId = @PropertyId";
                using (var cmd = new SqlCommand(deletePlots, conn))
                {
                    cmd.Parameters.AddWithValue("@PropertyId", id);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                    cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                    await cmd.ExecuteNonQueryAsync();
                }

                // Soft delete all plot transactions for plots under the property
                string deletePlotTransactions = @"
                    UPDATE PlotTransaction 
                    SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate 
                    WHERE PlotId IN (SELECT Id FROM Plot WHERE PropertyId = @PropertyId)";
                using (var cmd = new SqlCommand(deletePlotTransactions, conn))
                {
                    cmd.Parameters.AddWithValue("@PropertyId", id);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                    cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                    await cmd.ExecuteNonQueryAsync();
                }

                // Soft delete all property transactions
                string deletePropertyTransactions = @"UPDATE PropertyTransaction SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE PropertyId = @PropertyId";
                using (var cmd = new SqlCommand(deletePropertyTransactions, conn))
                {
                    cmd.Parameters.AddWithValue("@PropertyId", id);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                    cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                    await cmd.ExecuteNonQueryAsync();
                }

                // Soft delete all plot sales for plots under the property
                string deletePlotSales = @"
                    UPDATE PlotSale 
                    SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate 
                    WHERE PropertyId = @PropertyId";
                using (var cmd = new SqlCommand(deletePlotSales, conn))
                {
                    cmd.Parameters.AddWithValue("@PropertyId", id);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userIdentifier);
                    cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                    await cmd.ExecuteNonQueryAsync();
                }

                return Ok(new { Success = true, Message = "Property and all related plots, plot sales, and transactions deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting property: {ex.Message}");
            }
        }

        #endregion

        #region Property Transaction
        
        [HttpPost("transactions")]
        public async Task<ActionResult> CreatePropertyTransaction([FromBody] PropertyTransactionDto transaction)
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
                string insert = @"INSERT INTO PropertyTransaction
                    (PropertyId, TransactionDate, Amount, PaymentMethod, ReferenceNumber, TransactionType, Notes, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted)
                    VALUES (@PropertyId, @TransactionDate, @Amount, @PaymentMethod, @ReferenceNumber, @TransactionType, @Notes, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, 0);
                    SELECT SCOPE_IDENTITY();";
                using var cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@PropertyId", transaction.PropertyId);
                cmd.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
                cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                cmd.Parameters.AddWithValue("@PaymentMethod", transaction.PaymentMethod ?? "");
                cmd.Parameters.AddWithValue("@ReferenceNumber", transaction.ReferenceNumber ?? "");
                cmd.Parameters.AddWithValue("@TransactionType", transaction.TransactionType ?? "");
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
                return StatusCode(500, $"Error creating property transaction: {ex.Message}");
            }
        }

        [HttpDelete("transactions/{id}")]
        public async Task<ActionResult> DeletePropertyTransaction(int id)
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
                string delete = @"UPDATE PropertyTransaction SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE TransactionId = @Id";
                using var cmd = new SqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Property transaction deleted successfully." });
                else
                    return NotFound("Property transaction not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting property transaction: {ex.Message}");
            }
        }

        [HttpGet("{propertyId}/transaction-info")]
        public async Task<ActionResult> GetPropertyTransactionInfo(int propertyId)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT 
                    p.Title AS Property,
                    p.Price AS PurchaseAmount,
                    ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0) AS TotalLoan,
                    ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0) AS PaidTillDate,
                    (p.Price 
                        - ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0)
                        - ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0)
                    ) AS OutstandingBalance
                FROM Property p
                WHERE p.Id = @PropertyId AND p.IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var result = new
                    {
                        Property = reader["Property"]?.ToString() ?? "",
                        PurchaseAmount = reader["PurchaseAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PurchaseAmount"]),
                        TotalLoan = reader["TotalLoan"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalLoan"]),
                        PaidTillDate = reader["PaidTillDate"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PaidTillDate"]),
                        OutstandingBalance = reader["OutstandingBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["OutstandingBalance"])
                    };
                    return Ok(result);
                }
                else
                {
                    return NotFound($"Property with ID {propertyId} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching property transaction info: {ex.Message}");
            }
        }

        [HttpGet("{propertyId}/transactions")]
        public async Task<ActionResult<IEnumerable<PropertyTransactionDto>>> GetTransactionsForProperty(int propertyId)
        {
            var transactions = new List<PropertyTransactionDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT TransactionId, PropertyId, TransactionDate, Amount, PaymentMethod, ReferenceNumber, TransactionType, Notes,
                       CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsDeleted
                FROM PropertyTransaction
                WHERE IsDeleted = 0 AND PropertyId = @PropertyId";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    transactions.Add(new PropertyTransactionDto
                    {
                        TransactionId = reader["TransactionId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TransactionId"]),
                        PropertyId = reader["PropertyId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PropertyId"]),
                        TransactionDate = reader["TransactionDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["TransactionDate"]),
                        Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                        PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? "" : reader["PaymentMethod"].ToString(),
                        ReferenceNumber = reader["ReferenceNumber"] == DBNull.Value ? "" : reader["ReferenceNumber"].ToString(),
                        TransactionType = reader["TransactionType"] == DBNull.Value ? "" : reader["TransactionType"].ToString(),
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
                return StatusCode(500, $"Error reading property transactions: {ex.Message}");
            }

            return Ok(transactions);
        }

        #endregion
    }
}