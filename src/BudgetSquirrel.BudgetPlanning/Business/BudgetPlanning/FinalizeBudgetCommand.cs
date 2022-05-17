using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Business.Aggregates;
using BudgetSquirrel.BudgetPlanning.Business.Funds;

namespace BudgetSquirrel.BudgetPlanning.Business.BudgetPlanning
{
  public class FinalizeBudgetCommand
  {
    private IFundRepository fundRepository;
    private IBudgetRepository budgetRepository;
    
    private int profileId;
    private int timeboxId;

    public FinalizeBudgetCommand(
      IFundRepository fundRepository,
      IBudgetRepository budgetRepository,
      int profileId,
      int timeboxId)
    {
      this.fundRepository = fundRepository;
      this.budgetRepository = budgetRepository;
      this.profileId = profileId;
      this.timeboxId = timeboxId;
    }

    public async Task Execute()
    {
      GetAllFundsAndBudgetsQuery allFundsAndBudgetsQuery = new GetAllFundsAndBudgetsQuery(
        this.timeboxId,
        this.profileId,
        this.fundRepository,
        this.budgetRepository);
      FundsAndBudgetsQueryResult queryResult = await allFundsAndBudgetsQuery.Query();

      BudgetSummaryAggregate budgetSummaryAggregate = new BudgetSummaryAggregate(queryResult.FundTree, queryResult.FundBudgets);
      if (!budgetSummaryAggregate.IsFullyAllocated)
      {
        throw new InvalidCommandOperationException("All budgets must be fully allocated to finalize the budget plan.");
      }

      await this.budgetRepository.SetBudgetIsFinalized(this.profileId, this.timeboxId, true);
    }
  }
}