using System.Threading.Tasks;

namespace BudgetSquirrel.Backend.Biz.BudgetPlanning
{
  public interface IBudgetRepository
  {
    Task CreateBudget(string userEmail);
  }
}