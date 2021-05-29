using System.Threading.Tasks;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public interface IBudgetPlanningService
  {
    Task<BudgetPlanningContext> GetBudgetTree(int? timeboxId=null);
  }
}