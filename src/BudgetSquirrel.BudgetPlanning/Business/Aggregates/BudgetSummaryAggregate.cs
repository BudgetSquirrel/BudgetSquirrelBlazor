using System.Collections.Generic;
using System.Linq;
using BudgetSquirrel.BudgetPlanning.Domain.BudgetPlanning;
using BudgetSquirrel.BudgetPlanning.Domain.Funds;

namespace BudgetSquirrel.BudgetPlanning.Business.Aggregates
{
  /// <summary>
  /// An aggregate that contains the budget planned amount and it's sub budget's
  /// in a tree of <see cref="BudgetSummaryAggregates" />.
  /// </summary>
  public class BudgetSummaryAggregate
  {
    public BudgetSummaryAggregate(FundSubFunds fundSubFundsTree, IEnumerable<FundBudget> allFlatBudgets)
    {
      FundBudget fundBudget = allFlatBudgets.SingleOrDefault(b => b.Fund.Id == fundSubFundsTree.Fund.Id);
      this.FundBudget = new FundBudget(fundBudget?.Budget, fundSubFundsTree.Fund);

      this.SubBudgets = fundSubFundsTree.SubFunds.Select(subFund => new BudgetSummaryAggregate(subFund, allFlatBudgets));
    }
    
    public FundBudget FundBudget { get; private set; }
    public IEnumerable<BudgetSummaryAggregate> SubBudgets { get; private set; }

    /// <summary>
    /// The amount that was planned in this <see cref="FundBudget"/>'s children.
    /// This is not recursive. It only looks one level down and ignores grandchildren.
    /// </summary>
    public decimal AmountAllocatedInSubBudgets => this.SubBudgets.Sum(budget => budget.FundBudget.Budget.PlannedAmount);

    /// <summary>
    /// Whether or not the <see cref="Budget.PlannedAmount"/> is the same as
    /// the amount that was planned in it's children. This is recursive. It
    /// will look at children and all of their children and so on and so on.
    /// </summary>
    public bool IsFullyAllocated
    {
      get
      {
        if (!this.SubBudgets.Any())
        {
          return true;
        }

        if (this.FundBudget.Budget.PlannedAmount != this.AmountAllocatedInSubBudgets)
        {
          return false;
        }
        
        if (this.SubBudgets.Any(child => !child.IsFullyAllocated))
        {
          return false;
        }
        return true;
      }
    }
  }
}