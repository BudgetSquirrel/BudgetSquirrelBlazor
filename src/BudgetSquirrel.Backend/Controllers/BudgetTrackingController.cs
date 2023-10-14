using System;
using System.Threading.Tasks;
using BudgetSquirrel.Backend.Auth;
using BudgetSquirrel.Backend.Resolvers;
using BudgetSquirrel.BudgetPlanning.Domain.Accounts;
using BudgetSquirrel.BudgetTracking.Business.BudgetTrackingPage;
using BudgetSquirrel.BudgetTracking.Business.Ports;
using BudgetSquirrel.BudgetTracking.Domain.History;
using BudgetSquirrel.Web.Common.Messages.BudgetTracking;
using BudgetSquirrel.Web.Common.Messages.BudgetTracking.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetSquirrel.Backend.Controllers
{
  [ApiController]
  [Route("backend/budget-tracking")]
  [Authorize]
  public class BudgetTrackingController : Controller
  {
    private IAuthService authService;
    private IFundRepository fundRepository;
    private IBudgetRepository budgetRepository;
    private ITimeboxRepository timeboxRepository;
    private ITransactionRepository transactionRepository;

    public BudgetTrackingController(
      IAuthService authService,
      IFundRepository fundRepository,
      IBudgetRepository budgetRepository,
      ITimeboxRepository timeboxRepository,
      ITransactionRepository transactionRepository)
    {
      this.authService = authService;
      this.fundRepository = fundRepository;
      this.budgetRepository = budgetRepository;
      this.timeboxRepository = timeboxRepository;
      this.transactionRepository = transactionRepository;
    }
    
    [HttpGet("context")]
    public async Task<BudgetTrackingContextResponse> GetContext(int? timeboxId)
    {
      Account account = await this.authService.GetCurrentUser();
      
      GetBudgetTrackingContextQuery query = new GetBudgetTrackingContextQuery(
        this.fundRepository,
        this.budgetRepository,
        this.timeboxRepository,
        this.transactionRepository,
        timeboxId,
        account.ProfileId);

      BudgetTrackingPageContext response = await query.Query();
      return BudgetTrackingMessageResolvers.ToApiMessage(response);
    }

    [HttpPost("transactions")]
    public async Task CreateTransaction([FromBody] AddTransactionRequest request)
    {
      Account account = await this.authService.GetCurrentUser();

      Timebox timebox = await this.timeboxRepository.GetTimebox(account.ProfileId, DateTime.Now);
      
      CreateTransactionCommand command = new CreateTransactionCommand(
        this.transactionRepository,
        request.VendorName,
        request.Description,
        request.Amount,
        request.DateOfTransaction,
        request.CheckNumber,
        request.FundId,
        timebox.Id);

      await command.Execute();
    }
  }
}