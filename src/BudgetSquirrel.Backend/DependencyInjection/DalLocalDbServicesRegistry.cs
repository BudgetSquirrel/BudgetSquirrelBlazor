using BudgetSquirrel.BudgetPlanning.Business.Accounts;
using BudgetSquirrel.BudgetPlanning.Business.BudgetPlanning;
using BudgetSquirrel.BudgetPlanning.Business.Funds;
using BudgetSquirrel.BudgetPlanning.Business.History;
using BudgetSquirrel.BudgetPlanning.Data;
using BudgetSquirrel.BudgetPlanning.Data.Accounts;
using BudgetSquirrel.BudgetPlanning.Data.Funds;
using BudgetSquirrel.BudgetPlanning.Data.History;
using BudgetSquirrel.BudgetPlanning.Data.Infrastructure;
using BudgetSquirrel.BudgetPlanning.Domain.History;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Backend.DependencyInjection
{
  public static class DalLocalDbServicesRegistry
  {
    public static void AddLocalDbDal(IServiceCollection services)
    {
      string connectionString = "Server=127.0.0.1;Database=BudgetSquirrel;User Id=sa;password=yourStrong(!)Password";
      services.AddTransient<DbConnectionProvider>(serviceProvider => new DbConnectionProvider(connectionString));

      services.AddTransient<IAccountRepository, AccountRepository>();
      services.AddTransient<IBudgetRepository, BudgetRepository>();
      services.AddTransient<ITimeboxRepository, TimeboxRepository>();
      services.AddTransient<IFundRepository, FundRepository>();
    }
  }
}