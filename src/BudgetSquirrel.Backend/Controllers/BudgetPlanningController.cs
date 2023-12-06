using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Business.BudgetPlanning;
using BudgetSquirrel.BudgetPlanning.Business.Funds;
using BudgetSquirrel.BudgetPlanning.Business.History;
using BudgetSquirrel.Backend.Resolvers;
using BudgetSquirrel.Web.Common.Messages.BudgetPlanning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BudgetSquirrel.BudgetPlanning.Domain.Accounts;
using BudgetSquirrel.Backend.Auth;

namespace BudgetSquirrel.Backend.Controllers
{
  [ApiController]
  [Route("backend/budget-planning")]
  [Authorize]
  public class BudgetPlanningController : Controller
  {
    private IAuthService authService;
    private IFundRepository fundRepository;
    private IBudgetRepository budgetRepository;
    private ITimeboxRepository timeboxRepository;

    public BudgetPlanningController(
      IAuthService authService,
      IFundRepository fundRepository,
      IBudgetRepository budgetRepository,
      ITimeboxRepository timeboxRepository)
    {
      this.authService = authService;
      this.fundRepository = fundRepository;
      this.budgetRepository = budgetRepository;
      this.timeboxRepository = timeboxRepository;
    }

    [HttpPost("finalize")]
    public async Task<IActionResult> FinalizeBudget([FromBody] FinalizeBudgetRequest request)
    {
      Account account = await this.authService.GetCurrentUser();
      
      FinalizeBudgetCommand cmd = new FinalizeBudgetCommand(
        this.fundRepository,
        this.budgetRepository,
        account.ProfileId,
        request.TimeboxId);

      await cmd.Execute();
      return Ok();
    }

    [HttpGet("context")]
    public async Task<BudgetPlanningContextResponse> GetContext(int? timeboxId)
    {
      Account account = await this.authService.GetCurrentUser();
      
      GetBudgetPlanningContextQuery query = new GetBudgetPlanningContextQuery(
        this.fundRepository,
        this.budgetRepository,
        this.timeboxRepository,
        timeboxId,
        account.ProfileId);

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

    [HttpPost("edit-fund-name")]
    public async Task<IActionResult> EditFundName([FromBody] EditFundNameRequest request)
    {
      EditFundNameCommand cmd = new EditFundNameCommand(
        this.fundRepository,
        request.FundId,
        request.NewName);

      await cmd.Execute(await cmd.Validate(await cmd.Load()));
      return Ok();
    }

    [HttpPost("delete/{fundId}/{timeboxId}")]
    public async Task<IActionResult> DeleteBudget([FromRoute] DeleteBudgetRequest request)
    {
      DeleteBudgetCommand cmd = new DeleteBudgetCommand(
        this.budgetRepository,
        request.FundId,
        request.TimeboxId);

      await cmd.Execute();
      return Ok();
    }

    [HttpPost("create-level1-budget")]
    public async Task<IActionResult> CreateLevel1Budget([FromBody] CreateLevel1BudgetRequest request)
    {
      Account account = await this.authService.GetCurrentUser();
      
      CreateLevel1BudgetCommand cmd = new CreateLevel1BudgetCommand(
        this.budgetRepository,
        this.fundRepository,
        this.timeboxRepository,
        account.ProfileId,
        request.TimeboxId,
        request.Name,
        request.PlannedAmount);

      await cmd.Execute(await cmd.Validate(await cmd.Load()));
      return Ok();
    }

    [HttpPost("create-sub-budget")]
    public async Task<IActionResult> CreateSubBudget([FromBody] CreateSubBudgetRequest request)
    {
      CreateSubBudgetCommand cmd = new CreateSubBudgetCommand(
        this.budgetRepository,
        this.fundRepository,
        this.timeboxRepository,
        request.ParentFundId,
        request.TimeboxId,
        request.Name,
        request.PlannedAmount);

      await cmd.Execute(await cmd.Validate(await cmd.Load()));
      return Ok();
    }
  }
}