using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PrivateHospitals.Application.Interfaces;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.Application.Services;

public class TokenService: ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> jwtSettings)
    {
        if (jwtSettings?.Value == null)
        {
            throw new ArgumentNullException(nameof(jwtSettings), "JwtSettings is not configured.");
        }
        _jwtSettings = jwtSettings.Value;
        
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)); 
        Console.WriteLine($"Issuer: {_jwtSettings.Issuer}");
        Console.WriteLine($"Audience: {_jwtSettings.Audience}");
        Console.WriteLine($"TokenExpirationDays: {_jwtSettings.TokenExpirationDays}");
        Console.WriteLine($"SigningKey: {_jwtSettings.SigningKey}");
        
    }

    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
        };
    
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(_jwtSettings.TokenExpirationDays), 
            SigningCredentials = creds,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience 
        };
    
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

}