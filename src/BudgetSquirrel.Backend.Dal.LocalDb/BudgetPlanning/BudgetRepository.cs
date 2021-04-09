using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.BudgetPlanning;
using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;
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
    
    public Task CreateBudget(string userEmail)
    {
      // $"EXEC {AuthProcedures.CreateAccount} @FirstName, @LastName, @Email, @Password"
      throw new System.NotImplementedException();
    }
  }
}