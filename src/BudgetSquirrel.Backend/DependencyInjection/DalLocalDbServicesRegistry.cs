using BudgetSquirrel.Backend.Biz.Accounts;
using BudgetSquirrel.Backend.Biz.BudgetPlanning;
using BudgetSquirrel.Backend.Biz.Funds;
using BudgetSquirrel.Backend.Biz.History;
using BudgetSquirrel.Backend.Dal.LocalDb;
using BudgetSquirrel.Backend.Dal.LocalDb.Accounts;
using BudgetSquirrel.Backend.Dal.LocalDb.Funds;
using BudgetSquirrel.Backend.Dal.LocalDb.History;
using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;
using BudgetSquirrel.Core.History;
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