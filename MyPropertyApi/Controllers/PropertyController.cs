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
                    ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0) AS TotalLoanPrinciple,
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
                        TotalLoanPrinciple = reader["TotalLoanPrinciple"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalLoanPrinciple"]),
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
                    ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0) AS TotalLoanPrinciple,
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
                        TotalLoanPrinciple = reader["TotalLoanPrinciple"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalLoanPrinciple"]),
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

        #region Loans

        [HttpGet("loans")]
        public async Task<ActionResult<IEnumerable<PropertyLoanDto>>> GetActivePropertyLoans()
        {
            var loans = new List<PropertyLoanDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
            SELECT 
                pl.Id, pl.PropertyId, p.Title AS PropertyName, pl.LoanAmount, pl.LenderName, pl.InterestRate, pl.Tenure, 
                pl.TotalInterest, pl.TotalRepayment, pl.LoanDate, pl.Remarks, pl.CreatedDate, pl.CreatedBy, 
                pl.ModifiedBy, pl.ModifiedDate, pl.IsDeleted,
                ISNULL((
                    SELECT SUM(ISNULL(plt.PrincipleAmount,0) + ISNULL(plt.InterestAmount,0))
                    FROM PropertyLoanTransaction plt
                    WHERE plt.PropertyLoanId = pl.Id AND plt.IsDeleted = 0
                ), 0) AS TotalPaid,
                ISNULL((
                    SELECT ISNULL(SUM(plt.PrincipleAmount),0) FROM PropertyLoanTransaction plt WHERE plt.PropertyLoanId = pl.Id AND plt.IsDeleted = 0
                ),0) AS TotalPrincipalPaid,
                ISNULL((
                    SELECT ISNULL(SUM(plt.InterestAmount),0) FROM PropertyLoanTransaction plt WHERE plt.PropertyLoanId = pl.Id AND plt.IsDeleted = 0
                ),0) AS TotalInterestPaid
            FROM PropertyLoan pl
            INNER JOIN Property p ON pl.PropertyId = p.Id
            WHERE pl.IsDeleted = 0 AND p.IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var totalRepayment = reader["TotalRepayment"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalRepayment"]);
                    var totalPaid = reader["TotalPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalPaid"]);
                    loans.Add(new PropertyLoanDto
                    {
                        Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                        PropertyId = reader["PropertyId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PropertyId"]),
                        PropertyName = reader["PropertyName"]?.ToString() ?? "",
                        LoanAmount = reader["LoanAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["LoanAmount"]),
                        LenderName = reader["LenderName"]?.ToString() ?? "",
                        InterestRate = reader["InterestRate"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["InterestRate"]),
                        Tenure = reader["Tenure"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Tenure"]),
                        TotalInterest = reader["TotalInterest"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalInterest"]),
                        TotalRepayment = totalRepayment,
                        LoanDate = reader["LoanDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["LoanDate"]),
                        Remarks = reader["Remarks"]?.ToString() ?? "",
                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedDate"]),
                        CreatedBy = reader["CreatedBy"]?.ToString() ?? "",
                        ModifiedBy = reader["ModifiedBy"]?.ToString() ?? "",
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                        IsDeleted = reader["IsDeleted"] != DBNull.Value && Convert.ToInt32(reader["IsDeleted"]) == 1,
                        TotalPaid = totalPaid,
                        Outstanding = totalRepayment - totalPaid,
                        TotalPrincipalPaid = reader["TotalPrincipalPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalPrincipalPaid"]),
                        TotalInterestPaid = reader["TotalInterestPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalInterestPaid"])
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching property loans: {ex.Message}");
            }

            return Ok(loans);
        }

        [HttpPost("loan")]
        public async Task<ActionResult> CreatePropertyLoan([FromBody] PropertyLoanDto loan)
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
                string insert = @"
                    INSERT INTO PropertyLoan
                        (PropertyId, LoanAmount, LenderName, InterestRate, Tenure, TotalInterest, TotalRepayment, LoanDate, Remarks, CreatedDate, CreatedBy, ModifiedBy, ModifiedDate, IsDeleted)
                    VALUES
                        (@PropertyId, @LoanAmount, @LenderName, @InterestRate, @Tenure, @TotalInterest, @TotalRepayment, @LoanDate, @Remarks, @CreatedDate, @CreatedBy, @ModifiedBy, @ModifiedDate, 0);
                    SELECT SCOPE_IDENTITY();";
                using var cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@PropertyId", loan.PropertyId);
                cmd.Parameters.AddWithValue("@LoanAmount", loan.LoanAmount);
                cmd.Parameters.AddWithValue("@LenderName", loan.LenderName ?? "");
                cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                cmd.Parameters.AddWithValue("@Tenure", loan.Tenure);
                cmd.Parameters.AddWithValue("@TotalInterest", loan.TotalInterest);
                cmd.Parameters.AddWithValue("@TotalRepayment", loan.TotalRepayment);
                cmd.Parameters.AddWithValue("@LoanDate", loan.LoanDate);
                cmd.Parameters.AddWithValue("@Remarks", loan.Remarks ?? "");
                cmd.Parameters.AddWithValue("@CreatedDate", now);
                cmd.Parameters.AddWithValue("@CreatedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                var newId = await cmd.ExecuteScalarAsync();
                return Ok(new { Success = true, PropertyLoanId = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating property loan: {ex.Message}");
            }
        }

        [HttpPut("loan/{propertyLoanId}")]
        public async Task<ActionResult> EditPropertyLoan(int propertyLoanId, [FromBody] PropertyLoanDto loan)
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
                string update = @"
            UPDATE PropertyLoan SET
                PropertyId = @PropertyId,
                LoanAmount = @LoanAmount,
                LenderName = @LenderName,
                InterestRate = @InterestRate,
                Tenure = @Tenure,
                TotalInterest = @TotalInterest,
                TotalRepayment = @TotalRepayment,
                LoanDate = @LoanDate,
                Remarks = @Remarks,
                ModifiedBy = @ModifiedBy,
                ModifiedDate = @ModifiedDate
            WHERE Id = @Id AND IsDeleted = 0";
                using var cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@Id", propertyLoanId);
                cmd.Parameters.AddWithValue("@PropertyId", loan.PropertyId);
                cmd.Parameters.AddWithValue("@LoanAmount", loan.LoanAmount);
                cmd.Parameters.AddWithValue("@LenderName", loan.LenderName ?? "");
                cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                cmd.Parameters.AddWithValue("@Tenure", loan.Tenure);
                cmd.Parameters.AddWithValue("@TotalInterest", loan.TotalInterest);
                cmd.Parameters.AddWithValue("@TotalRepayment", loan.TotalRepayment);
                cmd.Parameters.AddWithValue("@LoanDate", loan.LoanDate);
                cmd.Parameters.AddWithValue("@Remarks", loan.Remarks ?? "");
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Property loan updated successfully!", PropertyLoanId = propertyLoanId });
                else
                    return NotFound("Property loan not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating property loan: {ex.Message}");
            }
        }

        [HttpDelete("loan/{propertyLoanId}")]
        public async Task<ActionResult> DeletePropertyLoan(int propertyLoanId)
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
                string delete = @"UPDATE PropertyLoan SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE Id = @Id";
                using var cmd = new SqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@Id", propertyLoanId);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Property loan deleted successfully." });
                else
                    return NotFound("Property loan not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting property loan: {ex.Message}");
            }
        }

        [HttpGet("loan/{propertyLoanId}")]
        public async Task<ActionResult<PropertyLoanDto>> GetPropertyLoanById(int propertyLoanId)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
                SELECT 
                    pl.Id, pl.PropertyId, p.Title AS PropertyName, pl.LoanAmount, pl.LenderName, pl.InterestRate, pl.Tenure, 
                    pl.TotalInterest, pl.TotalRepayment, pl.LoanDate, pl.Remarks, pl.CreatedDate, pl.CreatedBy, 
                    pl.ModifiedBy, pl.ModifiedDate, pl.IsDeleted,
                    ISNULL((
                        SELECT SUM(ISNULL(plt.PrincipleAmount,0) + ISNULL(plt.InterestAmount,0))
                        FROM PropertyLoanTransaction plt
                        WHERE plt.PropertyLoanId = pl.Id AND plt.IsDeleted = 0
                    ), 0) AS TotalPaid,
                    ISNULL((
                        SELECT ISNULL(SUM(plt.PrincipleAmount),0) FROM PropertyLoanTransaction plt WHERE plt.PropertyLoanId = pl.Id AND plt.IsDeleted = 0
                    ),0) AS TotalPrincipalPaid,
                    ISNULL((
                        SELECT ISNULL(SUM(plt.InterestAmount),0) FROM PropertyLoanTransaction plt WHERE plt.PropertyLoanId = pl.Id AND plt.IsDeleted = 0
                    ),0) AS TotalInterestPaid
                FROM PropertyLoan pl
                INNER JOIN Property p ON pl.PropertyId = p.Id
                WHERE pl.Id = @PropertyLoanId AND pl.IsDeleted = 0 AND p.IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PropertyLoanId", propertyLoanId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var totalRepayment = reader["TotalRepayment"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalRepayment"]);
                    var totalPaid = reader["TotalPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalPaid"]);
                    var totalPrincipalPaid = reader["TotalPrincipalPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalPrincipalPaid"]);
                    var totalInterestPaid = reader["TotalInterestPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalInterestPaid"]);
                    var loan = new PropertyLoanDto
                    {
                        Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                        PropertyId = reader["PropertyId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PropertyId"]),
                        PropertyName = reader["PropertyName"]?.ToString() ?? "",
                        LoanAmount = reader["LoanAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["LoanAmount"]),
                        LenderName = reader["LenderName"]?.ToString() ?? "",
                        InterestRate = reader["InterestRate"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["InterestRate"]),
                        Tenure = reader["Tenure"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Tenure"]),
                        TotalInterest = reader["TotalInterest"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalInterest"]),
                        TotalRepayment = totalRepayment,
                        LoanDate = reader["LoanDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["LoanDate"]),
                        Remarks = reader["Remarks"]?.ToString() ?? "",
                        CreatedBy = reader["CreatedBy"]?.ToString() ?? "",
                        ModifiedBy = reader["ModifiedBy"]?.ToString() ?? "",
                        IsDeleted = reader["IsDeleted"] != DBNull.Value && Convert.ToInt32(reader["IsDeleted"]) == 1,
                        TotalPaid = totalPaid,
                        Outstanding = totalRepayment - totalPaid,
                        TotalPrincipalPaid = totalPrincipalPaid,
                        TotalInterestPaid = totalInterestPaid
                    };
                    return Ok(loan);
                }
                else
                {
                    return NotFound("Property loan not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching property loan: {ex.Message}");
            }
        }

        [HttpGet("{propertyLoanId}/loan-transactions")]
        public async Task<ActionResult<IEnumerable<PropertyLoanTransactionDto>>> GetActivePropertyLoanTransactions(int propertyLoanId)
        {
            var transactions = new List<PropertyLoanTransactionDto>();
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string query = @"
            SELECT Id, PropertyId, PropertyLoanId, LenderName, PrincipleAmount, InterestAmount, TransactionType, TransactionDate,
                   PaymentMethod, ReferenceNumber, Notes, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, IsDeleted
            FROM PropertyLoanTransaction
            WHERE PropertyLoanId = @PropertyLoanId AND IsDeleted = 0";

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PropertyLoanId", propertyLoanId);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    transactions.Add(new PropertyLoanTransactionDto
                    {
                        Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                        PropertyId = reader["PropertyId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PropertyId"]),
                        PropertyLoanId = reader["PropertyLoanId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PropertyLoanId"]),
                        LenderName = reader["LenderName"]?.ToString() ?? "",
                        PrincipleAmount = reader["PrincipleAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PrincipleAmount"]),
                        InterestAmount = reader["InterestAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["InterestAmount"]),
                        TransactionType = reader["TransactionType"]?.ToString() ?? "",
                        TransactionDate = reader["TransactionDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["TransactionDate"]),
                        PaymentMethod = reader["PaymentMethod"]?.ToString() ?? "",
                        ReferenceNumber = reader["ReferenceNumber"]?.ToString() ?? "",
                        Notes = reader["Notes"]?.ToString() ?? "",
                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedDate"]),
                        CreatedBy = reader["CreatedBy"]?.ToString() ?? "",
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                        ModifiedBy = reader["ModifiedBy"]?.ToString() ?? "",
                        IsDeleted = reader["IsDeleted"] != DBNull.Value && Convert.ToInt32(reader["IsDeleted"]) == 1
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching property loan transactions: {ex.Message}");
            }

            return Ok(transactions);
        }

        [HttpPost("loan-transactions")]
        public async Task<ActionResult> CreatePropertyLoanTransaction([FromBody] CreatePropertyLoanTransactionRequest request)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            string userName = GetUserName();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                // Parse amount strings (may contain commas) to decimal
                decimal totalAmount = 0;
                decimal interestAmount = 0;
                decimal PrincipleAmount = 0;
                if (!string.IsNullOrWhiteSpace(request.Amount))
                {
                    var cleaned = request.Amount.Replace(",", "");
                    decimal.TryParse(cleaned, out totalAmount);
                }
                if (!string.IsNullOrWhiteSpace(request.Interest))
                {
                    var cleaned = request.Interest.Replace(",", "");
                    decimal.TryParse(cleaned, out interestAmount);
                }

                switch (request.PayingFor?.ToLowerInvariant())
                {
                    case "interest":
                        interestAmount = totalAmount;
                        PrincipleAmount = 0;
                        break;
                    case "principle":
                        PrincipleAmount = totalAmount;
                        interestAmount = 0;
                        break;  
                }

                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                string insert = @"INSERT INTO PropertyLoanTransaction
                    (PropertyId, PropertyLoanId, LenderName, TransactionType, TransactionDate, PrincipleAmount, InterestAmount, PaymentMethod, ReferenceNumber, Notes, CreatedDate, CreatedBy, ModifiedBy, ModifiedDate, IsDeleted)
                    VALUES (@PropertyId, @PropertyLoanId, @LenderName, @TransactionType, @TransactionDate, @PrincipleAmount, @InterestAmount, @PaymentMethod, @ReferenceNumber, @Notes, @CreatedDate, @CreatedBy, @ModifiedBy, @ModifiedDate, 0);
                    SELECT SCOPE_IDENTITY();";
                using var cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@PropertyId", request.PropertyId);
                cmd.Parameters.AddWithValue("@PropertyLoanId", request.PropertyLoanId);
                cmd.Parameters.AddWithValue("@LenderName", request.LenderName ?? "");
                cmd.Parameters.AddWithValue("@TransactionType", request.TransactionType ?? "");
                cmd.Parameters.AddWithValue("@TransactionDate", request.TransactionDate);
                cmd.Parameters.AddWithValue("@PrincipleAmount", PrincipleAmount);
                cmd.Parameters.AddWithValue("@InterestAmount", interestAmount);
                cmd.Parameters.AddWithValue("@PaymentMethod", request.PaymentMethod ?? "");
                cmd.Parameters.AddWithValue("@ReferenceNumber", request.ReferenceNumber ?? "");
                cmd.Parameters.AddWithValue("@Notes", request.Notes ?? "");
                cmd.Parameters.AddWithValue("@CreatedDate", now);
                cmd.Parameters.AddWithValue("@CreatedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);

                var newId = await cmd.ExecuteScalarAsync();
                return Ok(new { Success = true, TransactionId = newId, PrincipleAmount = PrincipleAmount, InterestAmount = interestAmount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating property loan transaction: {ex.Message}");
            }
        }

        [HttpDelete("loan-transactions/{id}")]
        public async Task<ActionResult> DeletePropertyLoanTransaction(int id)
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
                string delete = @"UPDATE PropertyLoanTransaction SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE Id = @Id";
                using var cmd = new SqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@ModifiedBy", userName);
                cmd.Parameters.AddWithValue("@ModifiedDate", now);
                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                    return Ok(new { Success = true, Message = "Property loan transaction deleted successfully." });
                else
                    return NotFound("Property loan transaction not found or already deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting property loan transaction: {ex.Message}");
            }
        }

        [HttpGet("{propertyId}/details")]
        public async Task<ActionResult<PropertyDetailsSummaryDto>> GetPropertyDetailsSummary(int propertyId)
        {
            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                return StatusCode(500, "Database connection string is missing.");

            var summary = new PropertyDetailsSummaryDto();

            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                // Get property details with financial summary
                string propertyQuery = @"
                    SELECT 
                        Id, Title, Type, Status, Price, Owner, Phone, Address, City, State, ZipCode, Description, KhasraNo, Area,
                        ISNULL((SELECT SUM(Amount) FROM PropertyTransaction pt WHERE pt.PropertyId = p.Id AND pt.IsDeleted = 0), 0) AS AmountPaid,
                        ISNULL((SELECT SUM(LoanAmount) FROM PropertyLoan WHERE PropertyId = p.Id AND IsDeleted = 0), 0) AS TotalLoanPrinciple
                    FROM Property p
                    WHERE Id = @Id AND IsDeleted = 0";

                using (var propertyCmd = new SqlCommand(propertyQuery, conn))
                {
                    propertyCmd.Parameters.AddWithValue("@Id", propertyId);
                    using var reader = await propertyCmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        summary.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        summary.Title = reader["Title"]?.ToString() ?? "";
                        summary.Type = reader["Type"]?.ToString() ?? "";
                        summary.Status = reader["Status"]?.ToString() ?? "";
                        summary.Owner = reader["Owner"]?.ToString() ?? "";
                        summary.Phone = reader["Phone"]?.ToString() ?? "";
                        summary.Address = reader["Address"]?.ToString() ?? "";
                        summary.City = reader["City"]?.ToString() ?? "";
                        summary.State = reader["State"]?.ToString() ?? "";
                        summary.ZipCode = reader["ZipCode"]?.ToString() ?? "";
                        summary.Description = reader["Description"]?.ToString() ?? "";
                        summary.KhasraNo = reader["KhasraNo"]?.ToString() ?? "";
                        summary.Area = reader["Area"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Area"]);
                        summary.BuyPrice = reader["Price"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Price"]);
                        summary.AmountPaid = reader["AmountPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["AmountPaid"]);
                        summary.TotalLoanPrinciple = reader["TotalLoanPrinciple"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalLoanPrinciple"]);
                        summary.AmountBalance = summary.BuyPrice - (summary.TotalLoanPrinciple + summary.AmountPaid);
                    }
                    else
                    {
                        return NotFound($"Property with ID {propertyId} not found.");
                    }
                }

                // Get plot summary
                string plotSummaryQuery = @"
                    SELECT 
                        (SELECT COUNT(*) FROM Plot WHERE PropertyId = @Id AND IsDeleted = 0) AS TotalPlots,
                        (SELECT COUNT(*) 
                         FROM Plot p 
                         WHERE p.PropertyId = @Id 
                         AND p.IsDeleted = 0 
                         AND NOT EXISTS (SELECT 1 FROM PlotSale ps WHERE ps.PlotId = p.Id)) AS AvailablePlots,
                        (SELECT COUNT(*) 
                         FROM Plot p 
                         WHERE p.PropertyId = @Id 
                         AND p.IsDeleted = 0 
                         AND EXISTS (SELECT 1 FROM PlotSale ps WHERE ps.PlotId = p.Id)) AS BookedPlots,
                        ISNULL((SELECT SUM(SaleAmount) FROM PlotSale ps INNER JOIN Plot p ON ps.PlotId = p.Id WHERE p.PropertyId = @Id AND p.IsDeleted = 0), 0) AS TotalSaleAmount,
                        ISNULL((SELECT SUM(Amount) FROM PlotTransaction pt INNER JOIN Plot p ON pt.PlotId = p.Id WHERE p.PropertyId = @Id AND pt.IsDeleted = 0), 0) AS TotalPlotsPaid";

                using (var plotCmd = new SqlCommand(plotSummaryQuery, conn))
                {
                    plotCmd.Parameters.AddWithValue("@Id", propertyId);
                    using var reader = await plotCmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        summary.TotalPlots = reader["TotalPlots"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalPlots"]);
                        summary.AvailablePlots = reader["AvailablePlots"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AvailablePlots"]);
                        summary.BookedPlots = reader["BookedPlots"] == DBNull.Value ? 0 : Convert.ToInt32(reader["BookedPlots"]);
                        summary.TotalSaleAmount = reader["TotalSaleAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalSaleAmount"]);
                        summary.TotalPlotsPaid = reader["TotalPlotsPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalPlotsPaid"]);
                        summary.TotalPlotsBalance = summary.TotalSaleAmount - summary.TotalPlotsPaid;
                    }
                }

                // Get brokerage summary
                string brokerageQuery = @"
                    SELECT ISNULL(SUM(BrokerageAmount), 0) AS TotalBrokerage
                    FROM PlotSale ps
                    INNER JOIN Plot p ON ps.PlotId = p.Id
                    WHERE p.PropertyId = @Id AND p.IsDeleted = 0";

                using (var brokerageCmd = new SqlCommand(brokerageQuery, conn))
                {
                    brokerageCmd.Parameters.AddWithValue("@Id", propertyId);
                    var result = await brokerageCmd.ExecuteScalarAsync();
                    summary.TotalBrokerage = result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                }

                // Get loan interest summary
                string loanInterestQuery = @"
                    SELECT ISNULL(SUM(TotalInterest), 0) AS TotalLoanInterest
                    FROM PropertyLoan
                    WHERE PropertyId = @Id AND IsDeleted = 0";

                using (var loanCmd = new SqlCommand(loanInterestQuery, conn))
                {
                    loanCmd.Parameters.AddWithValue("@Id", propertyId);
                    var result = await loanCmd.ExecuteScalarAsync();
                    summary.TotalLoanInterest = result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                }

                // Calculate profit/loss
                summary.ProfitLossAfterLoan = summary.TotalSaleAmount - summary.BuyPrice - summary.TotalBrokerage - summary.TotalLoanInterest;

                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching property details: {ex.Message}");
            }
        }

        #endregion
    }
}