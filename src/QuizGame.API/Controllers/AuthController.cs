using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using QuizGame.Application.Interfaces;
using System.Security.Claims;

namespace QuizGame.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IGameSessionService _gameSessionService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IConfiguration configuration,
        IGameSessionService gameSessionService,
        ILogger<AuthController> logger)
    {
        _configuration = configuration;
        _gameSessionService = gameSessionService;
        _logger = logger;
    }

    [HttpPost("login")]
    [EnableRateLimiting("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var gmUsername = _configuration["GM_USERNAME"] ?? "admin";
        var gmPassword = _configuration["GM_PASSWORD"];

        if (string.IsNullOrEmpty(gmPassword))
        {
            _logger.LogError("GM_PASSWORD not configured");
            return StatusCode(500, new { error = "Server configuration error" });
        }

        if (request.Username != gmUsername || request.Password != gmPassword)
        {
            _logger.LogWarning("Failed login attempt for username: {Username}", request.Username);
            await Task.Delay(2000); // Prevent timing attacks
            return Unauthorized(new { error = "Invalid credentials" });
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, gmUsername),
            new Claim(ClaimTypes.Role, "GM")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        _logger.LogInformation("GM logged in successfully");
        return Ok(new { success = true, role = "GM" });
    }

    [HttpPost("logout")]
    [Authorize(Policy = "GM")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        _logger.LogInformation("GM logged out");
        return Ok(new { success = true });
    }

    [HttpPost("viewer/verify")]
    [EnableRateLimiting("viewer_verify")]
    public async Task<IActionResult> VerifyViewerCode([FromBody] ViewerCodeRequest request)
    {
        var isValid = _gameSessionService.VerifyViewerCode(request.Code);

        if (!isValid)
        {
            _logger.LogWarning("Failed viewer code verification attempt: {Code}", request.Code);
            await Task.Delay(1000); // Prevent brute force
            return Unauthorized(new { error = "Invalid viewer code" });
        }

        var sessionVersion = _gameSessionService.GetCurrentSessionVersion();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, "Viewer"),
            new Claim("SessionVersion", sessionVersion.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        _logger.LogInformation("Viewer code verified successfully");
        return Ok(new { success = true, role = "Viewer" });
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        _logger.LogInformation("GetCurrentUser called. IsAuthenticated: {IsAuth}, Identity: {Identity}",
            User.Identity?.IsAuthenticated, User.Identity?.Name);

        if (!User.Identity?.IsAuthenticated ?? true)
        {
            _logger.LogWarning("User not authenticated");
            return Ok(new { authenticated = false });
        }

        var role = User.IsInRole("GM") ? "GM" : User.IsInRole("Viewer") ? "Viewer" : null;
        var sessionVersion = User.FindFirst("SessionVersion")?.Value;

        _logger.LogInformation("User authenticated. Role: {Role}, Username: {Username}", role, User.Identity?.Name);

        // Verify viewer session is still valid
        if (role == "Viewer")
        {
            var currentVersion = _gameSessionService.GetCurrentSessionVersion();
            if (sessionVersion != currentVersion.ToString())
            {
                // Session invalidated, sign out
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _logger.LogInformation("Viewer session invalidated");
                return Ok(new { authenticated = false, reason = "session_invalidated" });
            }
        }

        return Ok(new
        {
            authenticated = true,
            role,
            username = User.Identity?.Name
        });
    }
}

public record LoginRequest(string Username, string Password);
public record ViewerCodeRequest(string Code);
