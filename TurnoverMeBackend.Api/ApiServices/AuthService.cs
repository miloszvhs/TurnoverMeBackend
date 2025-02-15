using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TurnoverMeBackend.Config.Configs;
using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.ApiServices;

public class AuthService(TurnoverMeDbContext dbContext, 
    UserManager<ApplicationUser> userManager,
    IOptions<JwtSettings> jwtSettings) : BaseService(dbContext)
{
    public async Task<bool> RegisterUser(LoginUser user)
    {
        var identityUser = new ApplicationUser
        {
            UserName = user.Email,
            Email = user.Email
        };

        var result = await userManager.CreateAsync(identityUser, user.Password);
        return result.Succeeded;
    }

    public async Task<LoginResponse> Login(LoginUser user)
    {
        var response = new LoginResponse();
        var identityUser = await userManager.FindByEmailAsync(user.Email);

        if (identityUser is null || (await userManager.CheckPasswordAsync(identityUser, user.Password)) == false)
            return response;

        response.IsLoggedIn = true;
        response.JwtToken = this.GenerateTokenString(identityUser);
        response.RefreshToken = this.GenerateRefreshTokenString();

        if (identityUser.ForcePasswordChange is true)
        {
            response.ForcePasswordChange = true;
            return response;
        }

        identityUser.RefreshToken = response.RefreshToken;
        identityUser.RefreshTokenExpiry = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.Value.ExpiryInMinutes));
        await userManager.UpdateAsync(identityUser);

        return response;
    }

    public async Task<LoginResponse> RefreshToken(RefreshTokenModel model)
    {
        var principal = GetTokenPrincipal(model.JwtToken);

        var response = new LoginResponse();
        if (principal?.Identity?.Name is null)
            return response;

        var identityUser = await userManager.FindByEmailAsync(principal.Identity.Name);
        if (identityUser is null || identityUser.RefreshToken != model.RefreshToken || identityUser.RefreshTokenExpiry < DateTime.Now)
            return response;

        response.IsLoggedIn = true;
        response.JwtToken = this.GenerateTokenString(identityUser);
        response.RefreshToken = this.GenerateRefreshTokenString();

        if (identityUser.ForcePasswordChange is true)
            response.ForcePasswordChange = identityUser.ForcePasswordChange; 
        
        identityUser.RefreshToken = response.RefreshToken;
        identityUser.RefreshTokenExpiry = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.Value.ExpiryInMinutes));
        await userManager.UpdateAsync(identityUser);

        return response;
    }

    private ClaimsPrincipal? GetTokenPrincipal(string token)
    {

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.SecretKey));

        var validation = new TokenValidationParameters
        {
            IssuerSigningKey = securityKey,
            ValidateLifetime = false,
            ValidateActor = false,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
        return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
    }

    private string GenerateRefreshTokenString()
    {
        var randomNumber = new byte[64];

        using (var numberGenerator = RandomNumberGenerator.Create())
        {
            numberGenerator.GetBytes(randomNumber);
        }

        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateTokenString(ApplicationUser applicationUser)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, applicationUser.Email),
            new(ClaimTypes.Sid, applicationUser.Id),
            new(ClaimTypes.GroupSid, applicationUser.GroupId ?? string.Empty),
        };

        foreach (var role in userManager.GetRolesAsync(applicationUser).GetAwaiter().GetResult())
            claims.Add(new Claim(ClaimTypes.Role, role));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.SecretKey));
        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var expiryTime = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.Value.ExpiryInMinutes));
        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: expiryTime,
            signingCredentials: creds
        );
        string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return tokenString;
    }

    public ChangePasswordResponse ChangePassword(ChangePasswordRequest changePassword)
    {
        var principal = GetTokenPrincipal(changePassword.Token);
        if (principal?.Identity?.Name is null)
            return new ChangePasswordResponse { Success = false, Message = "Invalid token" };

        var identityUser = userManager.FindByEmailAsync(principal.Identity.Name).GetAwaiter().GetResult();
        if (identityUser is null)
            return new ChangePasswordResponse { Success = false, Message = "User not found" };

        if (identityUser.ForcePasswordChange is not true)
            return new ChangePasswordResponse { Success = true, Message = "Password hasn't changed. User already did it." };
        
        var result = userManager.ChangePasswordAsync(identityUser, changePassword.CurrentPassword, changePassword.NewPassword).GetAwaiter().GetResult();
        if (!result.Succeeded)
            return new ChangePasswordResponse { Success = false, Message = string.Join(", ", result.Errors.Select(e => e.Description)) };

        return new ChangePasswordResponse { Success = true, Message = "Password changed successfully" };
    }
}

 public class RefreshTokenModel
 {
     public string JwtToken { get; set; }
     public string RefreshToken { get; set; }
 }

 public class LoginUser
 {
     public string Email { get; set; }
     public string Password { get; set; }
 }

 public class LoginResponse
 {
     public bool? ForcePasswordChange { get; set; }
     public bool IsLoggedIn { get; set; } = false;
     public string JwtToken { get; set; }
     public string RefreshToken { get; internal set; }
 }

 public class ChangePasswordRequest
 {
     public string CurrentPassword { get; set; }
     public string NewPassword { get; set; }
     public string Token { get; set; }
 }
 
 public class ChangePasswordResponse
 {
     public bool Success { get; set; }
     public string Message { get; set; }
 }