using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.BudgetPlanning;
using BudgetSquirrel.Backend.Biz.Funds;
using BudgetSquirrel.Backend.Biz.History;
using BudgetSquirrel.Backend.Resolvers;
using BudgetSquirrel.Web.Common.Messages.BudgetPlanning;
using Microsoft.AspNetCore.Mvc;

namespace BudgetSquirrel.Backend.Controllers
{
  [ApiController]
  [Route("backend/budget-planning")]
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
    public async Task<BudgetPlanningContextResponse> GetContext(int? timeboxId, int profileId)
    {
      GetBudgetPlanningContextQuery query = new GetBudgetPlanningContextQuery(
        this.fundRepository,
        this.budgetRepository,
        this.timeboxRepository,
        timeboxId,
        profileId);

      GetBudgetPlanningContextQuery.BudgetPlanningContext response = await query.Query();
      return BudgetPlanningMessageResolvers.ToApiMessage(response);
    }

    [HttpPost("edit-planned-income")]
    public async Task<IActionResult> EditPlannedIncome([FromBody] EditPlannedIncomeRequest request)
    {
      EditPlannedIncomeCommand cmd = new EditPlannedIncomeCommand(
        this.budgetRepository,
        request.FundId,
        request.TimeboxId,
        request.PlannedIncome);

      await cmd.Execute(await cmd.Validate(await cmd.Load()));
      return Ok();
    }
  }
}