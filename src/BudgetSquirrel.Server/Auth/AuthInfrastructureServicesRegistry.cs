using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Server.Auth
{
  public static class AuthInfrastructureServicesRegistry
  {
    public static void AddAuthInfrastructure(IServiceCollection services)
    {
      services.AddTransient<IAuthService, AuthService>();

      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {
            options.Events.OnRedirectToLogin = context =>
            {
              context.Response.StatusCode = 401;
              return Task.CompletedTask;
            };
            options.ExpireTimeSpan = TimeSpan.FromHours(5);
            options.SlidingExpiration = true;
          });

      services.AddAuthorization(options =>
      {
        options.AddPolicy("Authenticated", policy =>
        {
          policy.RequireAuthenticatedUser();
        });
      });
    }
  }
}