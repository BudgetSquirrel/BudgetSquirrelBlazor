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

    private int? timeBoxId;
    private int profileId;

    public GetBudgetTrackingContextQuery(
      IFundRepository fundRepository,
      IBudgetRepository budgetRepository,
      ITimeboxRepository timeboxRepository,
      int? timeBoxId,
      int profileId)
    {
      this.budgetRepository = budgetRepository;
      this.timeboxRepository = timeboxRepository;
      this.timeBoxId = timeBoxId;
      this.profileId = profileId;
      this.fundRepository = fundRepository;
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
        timebox.Id,
        this.profileId,
        this.fundRepository,
        this.budgetRepository);
      FundsAndBudgetsQueryResult queryResult = await fundsAndBudgetsQuery.Query();

      return new BudgetTrackingPageContext(profile, queryResult.FundTree, queryResult.FundBudgets, timebox);
    }
  }
}