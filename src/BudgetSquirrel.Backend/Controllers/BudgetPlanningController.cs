using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.BudgetPlanning;
using BudgetSquirrel.Backend.Biz.Funds;
using BudgetSquirrel.Backend.Biz.History;
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
    public Task<GetBudgetPlanningContextQuery.BudgetPlanningContext> GetContext(int timeboxId, int profileId)
    {
      GetBudgetPlanningContextQuery query = new GetBudgetPlanningContextQuery(
        this.fundRepository,
        this.budgetRepository,
        this.timeboxRepository,
        timeboxId,
        profileId);

      return query.Query();
    }
  }
}