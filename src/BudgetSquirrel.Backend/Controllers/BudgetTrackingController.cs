using System.Threading.Tasks;
using BudgetSquirrel.Backend.Resolvers;
using BudgetSquirrel.BudgetTracking.Business.BudgetTrackingPage;
using BudgetSquirrel.BudgetTracking.Business.Ports;
using BudgetSquirrel.Web.Common.Messages.BudgetTracking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetSquirrel.Backend.Controllers
{
  [ApiController]
  [Route("backend/budget-tracking")]
  [Authorize]
  public class BudgetTrackingController : Controller
  {
    private IFundRepository fundRepository;
    private IBudgetRepository budgetRepository;
    private ITimeboxRepository timeboxRepository;

    public BudgetTrackingController(
      IFundRepository fundRepository,
      IBudgetRepository budgetRepository,
      ITimeboxRepository timeboxRepository)
    {
      this.fundRepository = fundRepository;
      this.budgetRepository = budgetRepository;
      this.timeboxRepository = timeboxRepository;
    }
    
    [HttpGet("context")]
    public async Task<BudgetTrackingContextResponse> GetContext(int? timeboxId, int profileId)
    {
      GetBudgetTrackingContextQuery query = new GetBudgetTrackingContextQuery(
        this.fundRepository,
        this.budgetRepository,
        this.timeboxRepository,
        timeboxId,
        profileId);

      BudgetTrackingPageContext response = await query.Query();
      return BudgetTrackingMessageResolvers.ToApiMessage(response);
    }
  }
}