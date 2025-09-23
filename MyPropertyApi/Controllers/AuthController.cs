using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using MyPropertyApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyPropertyApi.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config) => _config = config;

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new LoginResponse { Success = false, Message = "Please enter both username and password." });
            }

            string? connectionString = _config.GetConnectionString("MyPropertyDb");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return StatusCode(500, new LoginResponse { Success = false, Message = "Database connection string is missing." });
            }
            string userName = string.Empty;
            bool isValid = false;
            string? backgroundColor = "#F5F7FA";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT UserId, UserName, PasswordHash, BackgroundColor FROM [Login] WHERE UserName = @username";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", request.Username);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                userName = reader["UserName"]?.ToString()?.Trim() ?? "";
                                string dbPassword = reader["PasswordHash"]?.ToString()?.Trim() ?? "";
                                if (request.Username.Equals(userName, StringComparison.OrdinalIgnoreCase) && request.Password == dbPassword)
                                {
                                    isValid = true;
                                    backgroundColor = reader["BackgroundColor"]?.ToString() ?? backgroundColor;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LoginResponse { Success = false, Message = "Error reading user database: " + ex.Message });
            }

            if (isValid && !string.IsNullOrEmpty(userName))
            {
                // Generate JWT token
                var jwtKey = _config["Jwt:Key"] ?? "MyPropertyApiSecretKey12345";
                var jwtIssuer = _config["Jwt:Issuer"] ?? "MyPropertyApi";
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userName),
                    new Claim("UserId", userName),
                    new Claim("BackgroundColor", backgroundColor ?? "#F5F7FA"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: jwtIssuer,
                    audience: null,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(8),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new LoginResponse
                {
                    Success = true,
                    UserId = userName,
                    BackgroundColor = backgroundColor,
                    Message = "Login successful.",
                    Token = tokenString // Add Token property to LoginResponse
                });
            }
            else
            {
                return Unauthorized(new LoginResponse { Success = false, Message = "Invalid username or password." });
            }
        }
    }
}