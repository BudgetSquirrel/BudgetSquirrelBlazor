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
    private ITransactionRepository transactionRepository;

    public BudgetTrackingController(
      IFundRepository fundRepository,
      IBudgetRepository budgetRepository,
      ITimeboxRepository timeboxRepository,
      ITransactionRepository transactionRepository)
    {
      this.fundRepository = fundRepository;
      this.budgetRepository = budgetRepository;
      this.timeboxRepository = timeboxRepository;
      this.transactionRepository = transactionRepository;
    }
    
    [HttpGet("context")]
    public async Task<BudgetTrackingContextResponse> GetContext(int? timeboxId, int profileId)
    {
      GetBudgetTrackingContextQuery query = new GetBudgetTrackingContextQuery(
        this.fundRepository,
        this.budgetRepository,
        this.timeboxRepository,
        this.transactionRepository,
        timeboxId,
        profileId);

      BudgetTrackingPageContext response = await query.Query();
      return BudgetTrackingMessageResolvers.ToApiMessage(response);
    }
  }
}