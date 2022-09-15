using BudgetSquirrel.BudgetPlanning.Business.Accounts;
using BudgetSquirrel.BudgetPlanning.Business.BudgetPlanning;
using BudgetSquirrel.BudgetPlanning.Business.Funds;
using BudgetSquirrel.BudgetPlanning.Business.History;
using BudgetSquirrel.BudgetPlanning.Data.Accounts;
using BudgetSquirrel.BudgetPlanning.Data.Funds;
using BudgetSquirrel.BudgetPlanning.Data.History;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.BudgetPlanning.Data.DependencyInjection
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddBudgetPlanningDataLayer(this IServiceCollection services)
    {
      services.AddTransient<IAccountRepository, AccountRepository>();
      services.AddTransient<IBudgetRepository, BudgetRepository>();
      services.AddTransient<ITimeboxRepository, TimeboxRepository>();
      services.AddTransient<IFundRepository, FundRepository>();
      return services;
    }
  }
}