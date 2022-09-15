using System.Threading.Tasks;
using BudgetSquirrel.Frontend.BudgetTracking.Domain;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage
{
  public class BudgetTrackingPageService : IBudgetTrackingPageService
  {
    public Task<BudgetTrackingContext> GetPageContext(int? timeboxId = null)
    {
      return Task.FromResult(new BudgetTrackingContext(null, null, true));
    }
  }
}