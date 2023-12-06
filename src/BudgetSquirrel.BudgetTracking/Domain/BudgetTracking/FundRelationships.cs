using System.Collections.Generic;
using BudgetSquirrel.BudgetTracking.Domain.BudgetPlanning;
using BudgetSquirrel.BudgetTracking.Domain.Funds;

namespace BudgetSquirrel.BudgetTracking.Domain.BudgetTracking
{
  /// <summary>
  /// Links a fund to it's budget.
  /// </summary>
  public class FundRelationships
  {
    public FundRelationships(Budget budget, Fund fund, IEnumerable<Transaction> transactions)
    {
      this.Budget = budget;
      this.Fund = fund;
      this.Transactions = transactions;
    }

    public Budget Budget { get; private set; }

    public Fund Fund { get; private set; }

    /// <summary>
    /// The transactions allocated to this fund for a specific timebox.
    /// </summary>
    public IEnumerable<Transaction> Transactions { get; private set; }
  }
}