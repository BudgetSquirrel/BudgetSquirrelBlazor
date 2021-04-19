using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.BudgetPlanning;
using BudgetSquirrel.Backend.Biz.Funds;
using BudgetSquirrel.Core.History;
using Microsoft.AspNetCore.Mvc;

namespace BudgetSquirrel.Backend.Controllers
{
  [ApiController]
  [Route("backend/[controller]")]
  public class BudgetPlanningController : Controller
  {
    private IFundRepository fundRepository;
    private IBudgetRepository budgetRepository;
    private ITimeboxRepository timeboxRepository;

    public BudgetPlanningController(
      IFundRepository fundRepository,
      IBudgetRepository budgetRepository,
      ITimeboxRepository timeboxRepository)
    {
      this.fundRepository = fundRepository;
      this.budgetRepository = budgetRepository;
      this.timeboxRepository = timeboxRepository;
    }

    [HttpGet("context")]
    public Task<GetBudgetPlanningContextQuery.BudgetPlanningContext> GetContext(int timeboxId, int fundRootId)
    {
      GetBudgetPlanningContextQuery query = new GetBudgetPlanningContextQuery(
        this.fundRepository,
        this.budgetRepository,
        this.timeboxRepository,
        timeboxId,
        fundRootId);

      return query.Query();
    }
  }
}