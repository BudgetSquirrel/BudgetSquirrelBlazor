using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Frontend.Infrastructure
{
  public static class InfrastructureDependencyInjection
  {
    public static IServiceCollection AddServices(IServiceCollection services)
    {
      services.AddTransient<ICookieService, CookieService>();

      return services;
    }
  }
}