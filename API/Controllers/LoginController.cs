using DTO; // Adjust according to your namespace
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailService emailService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto userLogIn)
    {
        var user = await _userManager.FindByNameAsync(userLogIn.Username);
        user ??= await _userManager.FindByEmailAsync(userLogIn.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, userLogIn.Password))
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var expireMinutes = Convert.ToDouble(_configuration["Jwt:Expiration"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, userLogIn.Username),
            // Add roles as claims
            new Claim(ClaimTypes.Role, string.Join(",", roles))
            }),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }


    [HttpPost]
    public async Task<IActionResult> Register([FromBody] LogUpDto newUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new ApplicationUser
        {
            UserName = newUser.Username,
            Email = newUser.Email
        };

        var result = await _userManager.CreateAsync(user, newUser.Password);

        if (result.Succeeded)
        {
            return Ok("User registered successfully");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return BadRequest(ModelState);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();

        var userDtos = new List<object>();

        foreach (var user in users)
        {
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            userDtos.Add(new
            {
                user.Id,
                user.Email,
                IsAdmin = isAdmin
            });
        }

        return Ok(userDtos);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> ToggleAdmin([FromBody] ToggleAdminDto request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var isCurrentlyAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        if (isCurrentlyAdmin == request.IsAdmin)
        {
            return BadRequest("Admin status is already set to the requested value");
        }

        if (request.IsAdmin)
        {
            await _userManager.AddToRoleAsync(user, "Admin");
            await _userManager.RemoveFromRoleAsync(user, "User");

        }
        else
        {
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            await _userManager.AddToRoleAsync(user, "User");

        }

        return Ok("User admin status updated");
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return Ok("");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetUrl = $"{model.ResetUrl}?token={Uri.EscapeDataString(token)}&email={user.Email}";

        await _emailService.SendResetPasswordEmail(user.Email, resetUrl);

        return Ok("");
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest("Invalid email.");
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        if (result.Succeeded)
        {
            return Ok("Password has been reset successfully.");
        }

        return BadRequest("Failed to reset password.");
    }


}
