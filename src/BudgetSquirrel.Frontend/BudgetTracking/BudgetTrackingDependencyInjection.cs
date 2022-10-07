using BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Frontend.BudgetTracking
{
  public static class BudgetTrackingDependencyInjection
  {
    public static IServiceCollection AddServices(IServiceCollection services)
    {
      services.AddTransient<IBudgetTrackingPageService, BudgetTrackingPageService>();

      return services;
    }
  }
}