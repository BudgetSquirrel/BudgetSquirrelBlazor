using System;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetTracking.Business.Ports;
using BudgetSquirrel.BudgetTracking.Business.Queries;
using BudgetSquirrel.BudgetTracking.Domain.Funds;
using BudgetSquirrel.BudgetTracking.Domain.History;

namespace BudgetSquirrel.BudgetTracking.Business.BudgetTrackingPage
{
  public class GetBudgetTrackingContextQuery
  {
    private IFundRepository fundRepository;
    private IBudgetRepository budgetRepository;
    private ITimeboxRepository timeboxRepository;
    private ITransactionRepository transactionRepository;

    private int? timeBoxId;
    private int profileId;

    public GetBudgetTrackingContextQuery(
      IFundRepository fundRepository,
      IBudgetRepository budgetRepository,
      ITimeboxRepository timeboxRepository,
      ITransactionRepository transactionRepository,
      int? timeBoxId,
      int profileId)
    {
      this.fundRepository = fundRepository;
      this.budgetRepository = budgetRepository;
      this.timeboxRepository = timeboxRepository;
      this.transactionRepository = transactionRepository;
      this.timeBoxId = timeBoxId;
      this.profileId = profileId;
    }

    public async Task<BudgetTrackingPageContext> Query()
    {
      Timebox timebox;
      if (this.timeBoxId.HasValue)
      {
        timebox = await this.timeboxRepository.GetTimebox(this.timeBoxId.Value);
      }
      else
      {
        timebox = await this.timeboxRepository.GetTimebox(this.profileId, DateTime.Now);
      }

      Profile profile = await this.fundRepository.GetProfile(this.profileId);

      GetAllFundsAndBudgetsQuery fundsAndBudgetsQuery = new GetAllFundsAndBudgetsQuery(
        timebox,
        this.profileId,
        this.fundRepository,
        this.budgetRepository,
        this.transactionRepository);
      FundsAndBudgetsQueryResult queryResult = await fundsAndBudgetsQuery.Query();

      return new BudgetTrackingPageContext(profile, queryResult.FundTree, queryResult.FundRelationships, timebox);
    }
  }
}