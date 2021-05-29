using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public static class BudgetPlanningDependencyInjection
  {
    public static IServiceCollection AddServices(IServiceCollection services)
    {
      services.AddTransient<IBudgetPlanningService, BudgetPlanningService>();

      return services;
    }
  }
}