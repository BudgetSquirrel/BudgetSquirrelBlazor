using System.Threading.Tasks;
using BudgetSquirrel.Frontend.BudgetTracking.Domain;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage
{
  public interface IBudgetTrackingPageService
  {
    Task<BudgetTrackingContext> GetPageContext(int? timeboxId = null);
  }
}