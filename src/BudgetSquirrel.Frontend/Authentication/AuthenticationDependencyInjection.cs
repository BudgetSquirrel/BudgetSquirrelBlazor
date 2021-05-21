using BudgetSquirrel.Frontend.Authentication.Login;
using BudgetSquirrel.Frontend.Authentication.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Frontend.Authentication
{
  public static class AuthenticationDependencyInjection
  {
    public static IServiceCollection AddServices(IServiceCollection services)
    {
      services.AddTransient<IRegistrationService, RegistrationService>();
      services.AddSingleton<ILoginService, LoginService>();

      return services;
    }
  }
}