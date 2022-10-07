using System.Threading.Tasks;
using BudgetSquirrel.BudgetTracking.Domain.BudgetPlanning;

namespace BudgetSquirrel.BudgetTracking.Business.Ports
{
  public interface IBudgetRepository
  {
    Task<Budget> GetBudget(int fundId, int timeboxId);
  }
}