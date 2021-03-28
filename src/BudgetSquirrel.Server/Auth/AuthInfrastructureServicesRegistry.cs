using System.Text;
using BudgetSquirrel.Server.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BudgetSquirrel.Server.Auth
{
  public static class AuthInfrastructureServicesRegistry
  {
    public static void AddAuthInfrastructure(IServiceCollection services, HostingConfiguration hostingConfiguration)
    {
      string privateKeyString = hostingConfiguration.FrontendJwtSecret;
      byte[] privateKeyBytes = Encoding.ASCII.GetBytes(privateKeyString);
      SecurityKey privateKey = new SymmetricSecurityKey(privateKeyBytes);

      services.AddTransient<IAuthService, AuthService>();

      services.AddTransient<IJwtTokenAuthenticator>(services => new JwtTokenAuthenticator(privateKey));

      services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options => {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = privateKey,
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
    }
  }
}