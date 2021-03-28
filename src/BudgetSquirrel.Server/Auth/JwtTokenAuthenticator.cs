using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace BudgetSquirrel.Server.Auth
{
  public class JwtTokenAuthenticator : IJwtTokenAuthenticator
  {
    private SecurityKey privateKey;

    public JwtTokenAuthenticator(SecurityKey privateKey)
    {
      this.privateKey = privateKey;
    }

    public string GenerateToken(string username)
    {
      Claim[] claims = new Claim[] { new Claim(ClaimTypes.Name, username) };
      SigningCredentials signingCredentials = new SigningCredentials(
        this.privateKey,
        SecurityAlgorithms.HmacSha256Signature);

      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
      SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = signingCredentials
      };

      SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}