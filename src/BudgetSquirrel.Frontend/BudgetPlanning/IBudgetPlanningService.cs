using System.Threading.Tasks;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public interface IBudgetPlanningService
  {
    Task<BudgetPlanningContext> GetBudgetTree(int? timeboxId=null);

    Task EditPlannedIncome(int fundId, int timeboxId, decimal plannedIncome);
  }
}