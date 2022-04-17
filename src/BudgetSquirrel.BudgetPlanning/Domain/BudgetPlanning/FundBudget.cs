using BudgetSquirrel.BudgetPlanning.Domain.Funds;

namespace BudgetSquirrel.BudgetPlanning.Domain.BudgetPlanning
{
  /// <summary>
  /// Links a fund to it's budget.
  /// </summary>
  public class FundBudget
  {
    public FundBudget(Budget budget, Fund fund)
    {
      this.Budget = budget;
      this.Fund = fund;
    }

    public Budget Budget { get; private set; }

    public Fund Fund { get; private set; }
  }
}