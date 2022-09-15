using System.Data;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetTracking.Data.Budgets;
using BudgetSquirrel.BudgetTracking.Business.Ports;
using BudgetSquirrel.BudgetTracking.Domain.BudgetPlanning;
using BudgetSquirrel.Common.Data.Infrastructure;
using BudgetSquirrel.Common.Data.Schema.Budgets;
using BudgetPlanningProcedures = BudgetSquirrel.Common.Data.Schema.StoredProcedures.BudgetPlanning;
using Dapper;

namespace BudgetSquirrel.BudgetTracking.Data.Budgets
{
  public class BudgetRepository : IBudgetRepository
  {
    private DbConnectionProvider dbConnectionProvider;

    public BudgetRepository(DbConnectionProvider dbConnectionProvider)
    {
      this.dbConnectionProvider = dbConnectionProvider;
    }

    public async Task<Budget> GetBudget(int fundId, int timeboxId)
    {
      BudgetDto budget;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        budget = await conn.QuerySingleAsync<BudgetDto>(
          $"EXEC {BudgetPlanningProcedures.GetBudgetForFund} @FundId, @TimeboxId",
          new
          {
            FundId = fundId,
            TimeboxId = timeboxId
          }
        );
      }
      return BudgetConversions.ToDomain(budget);
    }
  }
}