using System.Collections.Generic;

namespace BudgetSquirrel.Frontend.BudgetTracking.Domain
{
  public partial class BudgetTrackingContext
  {
    public class FundBudget
    {
      public FundBudget(Budget budget, int fundId, IEnumerable<Transaction> transactions)
      {
        this.Budget = budget;
        this.FundId = fundId;
        Transactions = transactions;
      }

      public Budget Budget { get; private set; }

      public int FundId { get; private set; }

      public IEnumerable<Transaction> Transactions { get; private set; }
    }
  }
}