using System.Data;
using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.BudgetPlanning;
using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;
using Dapper;
using BudgetPlanningProcedures = BudgetSquirrel.Backend.Dal.LocalDb.Schema.StoredProcedures.BudgetPlanning;

namespace BudgetSquirrel.Backend.Dal.LocalDb
{
  public class BudgetRepository : IBudgetRepository
  {
    private DbConnectionProvider dbConnectionProvider;

    public BudgetRepository(DbConnectionProvider dbConnectionProvider)
    {
      this.dbConnectionProvider = dbConnectionProvider;
    }
    
    public async Task CreateBudget(string userEmail)
    {
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        await conn.ExecuteAsync(
          $"EXEC {BudgetPlanningProcedures.CreateBudgetForUser} @Email",
          new {
            Email = userEmail
          });
      }
    }
  }
}