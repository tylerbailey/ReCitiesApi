using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReCitiesApi.Models.Entities;
using ReCitiesApi.Server.Options;
using ReCitiesApi.Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ReCitiesApi.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/auth")]
public class AuthController(UserManager<ApplicationUser> users, ITokenService tokenService, IOptions<JwtOptions> jwtOptions, IWebHostEnvironment env) : ControllerBase
{
    private const string TokenCookieName = "token";
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(1);

    private readonly UserManager<ApplicationUser> _users = users;
    private readonly ITokenService _tokenService = tokenService;
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly IWebHostEnvironment _env = env;

    /// <summary>Registers a new user account pending admin approval.</summary>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            return BadRequest(new { message = "Email error", errors = new string[] { "Email is required." } });

        if (string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { message = "Password error", errors = new string[] { "Password is required." } });

        if (string.IsNullOrWhiteSpace(request.UserName))
            return BadRequest(new { message = "User name error", errors = new string[] { "User name is required." } });

        if( _users.Users.Where(u => u.DisplayName != null && u.DisplayName.Equals(request.UserName)).Any())
        {
            return BadRequest(new { message = "User name error", errors = new string[] { "User name is already taken." } });
        }

        var user = new ApplicationUser
        {
            UserName = request.Email.Trim(),
            Email = request.Email.Trim(),
            DisplayName = request.UserName.Trim(),
            IsApproved = true            
        };

        var result = await _users.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(new
            {
                message = "Could not create account.",
                errors = result.Errors.Select(e => e.Description).ToArray()
            });

        //TODO: Load role from config or database instead of hardcoding it here
        var roleResult = await _users.AddToRoleAsync(user, "User");
        if (!roleResult.Succeeded)
            return BadRequest(new
            {
                message = "Account was created but could not assign the default role.",
                errors = roleResult.Errors.Select(e => e.Description).ToArray()
            });

        return Ok(new { message = "Account created!" });
    }

    /// <summary>Authenticates a user and sets the auth token cookie.</summary>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { message = "Email and password are required." });

        var user = await _users.FindByEmailAsync(request.Email.Trim());

        if (user is null || !await _users.CheckPasswordAsync(user, request.Password))
            return Unauthorized(new { message = "Invalid email or password." });

        if (!user.IsApproved)
            return StatusCode(StatusCodes.Status403Forbidden, new
            {
                message = "Your account is pending approval."
            });

        var roles = await _users.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user, roles);
        var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes);

        Response.Cookies.Append(TokenCookieName, token, CreateTokenCookieOptions(expires));

        return Ok(new
        {
            id = user.Id,
            email = user.Email,
            displayName = user.DisplayName,
            roles
        });
    }

    /// <summary>Logs the current user out by clearing the auth token cookie.</summary>
    [AllowAnonymous]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(TokenCookieName, CreateTokenCookieOptions(DateTimeOffset.UtcNow));
        return Ok();
    }

    /// <summary>Gets the currently authenticated user's profile information.</summary>
    [HttpGet("user")]
    public async Task<IActionResult> GetUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return Unauthorized();

        var user = await _users.FindByIdAsync(userId);
        if (user is null)
            return NotFound();

        var roles = await _users.GetRolesAsync(user);

        return Ok(new
        {
            id = user.Id,
            email = user.Email,
            displayName = user.DisplayName,
            roles
        });
    }
    [AllowAnonymous]
    [HttpGet("check-token-expiry")]
    public IActionResult CheckTokenExpiry()
    {
        if (!Request.Cookies.TryGetValue(TokenCookieName, out var token) || string.IsNullOrEmpty(token))
        {
            return NotFound("No auth cookie found.");
        }

        var handler = new JwtSecurityTokenHandler();

        if (!handler.CanReadToken(token))
        {
            return BadRequest("Cookie value is not a valid JWT.");
        }

        var jwt = handler.ReadJwtToken(token);
        var expiresAt = jwt.ValidTo; // already converted to UTC DateTime by the handler

        return Ok(new
        {
            expiresAtUtc = expiresAt,
            isExpired = expiresAt < DateTime.UtcNow,
            secondsRemaining = (expiresAt - DateTime.UtcNow).TotalSeconds
        });
    }

    /// <summary>Builds the cookie options used for the auth token cookie.</summary>
    private static CookieOptions CreateTokenCookieOptions(DateTimeOffset expires) => new()
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None,
        Path = "/",
        Expires = expires
    };


}

public record RegisterRequest(string Email, string Password, string ConfirmPassword, string UserName, int Neighborhood);
public record LoginRequest(string Email, string Password);
