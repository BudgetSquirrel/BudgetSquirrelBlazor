using BudgetSquirrel.Server.Biz.Accounts;
using BudgetSquirrel.Server.Dal.LocalDb.Accounts;
using BudgetSquirrel.Server.Dal.LocalDb.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Server.DependencyInjection
{
  public static class DalLocalDbServicesRegistry
  {
    public static void ConfigureAuthDal(IServiceCollection services)
    {
      string connectionString = "Server=127.0.0.1;Database=BudgetSquirrel;User Id=sa;password=yourStrong(!)Password";
      services.AddTransient<DbConnectionProvider>(serviceProvider => new DbConnectionProvider(connectionString));

      services.AddTransient<IAccountRepository, AccountRepository>();
    }
  }
}