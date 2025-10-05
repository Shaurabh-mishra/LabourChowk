using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LabourChowk_webapi.Data;
using LabourChowk_webapi.Models;
using LabourChowk_webapi.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LabourChowkContext _context;
    private readonly AuthService _authService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthController(LabourChowkContext context, AuthService authService, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _authService = authService;
        _passwordHasher = passwordHasher;
    }

    // Register endpoint (no admin registration via API)
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        dto.Role = dto.Role?.Trim();

        // // Restrict role choices and prevent creating Admin through this endpoint
        // if (dto.Role != "Worker" && dto.Role != "WorkPoster")
        //     return BadRequest("Role must be 'Worker' or 'WorkPoster'.");

        if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest("Username and password are required.");

        if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            return BadRequest("Username already exists.");

        var user = new User
        {
            Username = dto.Username,
            Role = dto.Role
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { user.Id, user.Username, user.Role });
    }

    // Login endpoint
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (user == null)
            return Unauthorized("Invalid username or password.");

        var verification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (verification == PasswordVerificationResult.Failed)
            return Unauthorized("Invalid username or password.");

        var token = _authService.GenerateJwtToken(user);
        return Ok(new { Token = token, Role = user.Role });
    }
}
