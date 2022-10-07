using BudgetSquirrel.BudgetTracking.Business.Ports;
using BudgetSquirrel.BudgetPlanning.Data.Funds;
using BudgetSquirrel.BudgetPlanning.Data.History;
using BudgetSquirrel.BudgetTracking.Data.Budgets;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.BudgetTracking.Data.DependencyInjection
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddBudgetTrackingDataLayer(this IServiceCollection services)
    {
      services.AddTransient<IBudgetRepository, BudgetRepository>();
      services.AddTransient<ITimeboxRepository, TimeboxRepository>();
      services.AddTransient<IFundRepository, FundRepository>();
      return services;
    }
  }
}