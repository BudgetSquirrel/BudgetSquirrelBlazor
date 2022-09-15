using BudgetSquirrel.Common.Data.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using BudgetSquirrel.BudgetPlanning.Data.DependencyInjection;
using BudgetSquirrel.BudgetTracking.Data.DependencyInjection;

namespace BudgetSquirrel.Backend.DependencyInjection
{
  public static class DalLocalDbServicesRegistry
  {
    public static void AddLocalDbDal(IServiceCollection services)
    {
      string connectionString = "Server=127.0.0.1;Database=BudgetSquirrel;User Id=sa;password=yourStrong(!)Password";
      services.AddTransient<DbConnectionProvider>(serviceProvider => new DbConnectionProvider(connectionString));

      services.AddBudgetPlanningDataLayer();
      services.AddBudgetTrackingDataLayer();
    }
  }
}