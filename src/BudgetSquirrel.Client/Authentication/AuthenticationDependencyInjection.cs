using BudgetSquirrel.Client.Authentication.Login;
using BudgetSquirrel.Client.Authentication.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Client.Authentication
{
  public static class AuthenticationDependencyInjection
  {
    public static IServiceCollection AddAuthenticationServices(IServiceCollection services)
    {
      services.AddTransient<IRegistrationService, RegistrationService>();
      services.AddTransient<ILoginService, LoginService>();

      return services;
    }
  }
}