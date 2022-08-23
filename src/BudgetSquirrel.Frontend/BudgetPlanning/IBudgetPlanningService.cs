using System.Threading.Tasks;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public interface IBudgetPlanningService
  {
    Task<BudgetPlanningContext> GetBudgetTree(int? timeboxId=null);

    Task EditPlannedAmount(int fundId, int timeboxId, decimal plannedIncome);

    Task EditFundName(int fundId, string newName);

    Task CreateLevel1Budget(int profileId, int timeboxId, string name, decimal plannedAmount);

    Task CreateSubBudget(int parentFundId, int timeboxId, string name, decimal plannedAmount);

    Task DeleteBudget(int fundId, int timeboxId);
    
    Task FinalizeBudget(int profileId, int timeboxId);
  }
}