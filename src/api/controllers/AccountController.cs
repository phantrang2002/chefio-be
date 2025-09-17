using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chefio.Infrastructure.Data;
using Chefio.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;
using System;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Chefio.Application.Constants;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly ChefioDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(ChefioDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        if (_context.Accounts.Any(a => a.Username == request.Username))
            return BadRequest(new ApiResponse(ApiStatus.Error, ApiMessages.ACCOUNT.USERNAME_EXISTED.Message));

        var account = new Account
        {
            Username = request.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role,
            IsActive = true
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.ACCOUNT.SIGNUP_SUCCESS.Message));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.Username == request.Username && a.IsActive);
        if (account == null || !BCrypt.Net.BCrypt.Verify(request.Password, account.Password))
            return BadRequest(new ApiResponse(ApiStatus.Error, ApiMessages.ACCOUNT.LOGIN_INVALID.Message));

        var token = GenerateJwtToken(account);
        return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.ACCOUNT.LOGIN_SUCCESS.Message, new { token }));
    }

    private string GenerateJwtToken(Account account)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, account.Username),
            new Claim(ClaimTypes.Role, account.Role.ToString()),
            new Claim("id", account.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}