using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common.Abstraction;
using Domain.Entities.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class TokenProvider : ITokenProvider
{
    private readonly IConfiguration _configuration;

    public TokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    // helper methods
    private SigningCredentials GetSigningCredentials()
    {
        var key = _configuration["JwtSettings:Key"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }
    
    private List<Claim> GetClaims (User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id)
        };

        return claims;
    }

    private JwtSecurityToken TokenOptions(SigningCredentials signingCredentials,
        List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
            issuer: _configuration["JwtSettings:Issuer"]!,
            audience: _configuration["JwtSettings:Audience"]!,
            signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.Now.AddMinutes(15)

        );

        return tokenOptions;
    }
    
    // endregion helper methods

    public string CreateToken(User user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(user);
        var tokenOptions = TokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    
    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }


}