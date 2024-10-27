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
    public FundRelationships(Budget budget, Fund fund, IEnumerable<Transaction> transactions, decimal balance)
    {
      this.Budget = budget;
      this.Fund = fund;
      this.Transactions = transactions;
      this.Balance = balance;
    }

    public Budget Budget { get; private set; }

    public Fund Fund { get; private set; }

    /// <summary>
    /// The transactions allocated to this fund for a specific timebox.
    /// </summary>
    public IEnumerable<Transaction> Transactions { get; private set; }

    // TODO: This should be calculated from the FundRepository.GetFundBalance. This will take over for this.Fund.Balance.
    public decimal Balance { get; private set; }
  }
}