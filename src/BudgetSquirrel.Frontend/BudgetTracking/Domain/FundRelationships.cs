using System.Collections.Generic;
using System.Linq;

namespace BudgetSquirrel.Frontend.BudgetTracking.Domain
{
  public partial class BudgetTrackingContext
  {
    public class FundRelationships
    {
      public FundRelationships(Fund parentFund, Budget budget, IEnumerable<Transaction> transactions, IEnumerable<FundRelationships> subFunds)
      {
        this.Fund = parentFund;
        this.Budget = budget;
        this.Transactions = transactions;
        this.SubFunds = subFunds;
      }

      public Fund Fund { get; private set; }

      public Budget Budget { get; private set; }

      public IEnumerable<Transaction> Transactions { get; private set; }

      public IEnumerable<FundRelationships> SubFunds { get; private set; }

      /// <summary>
      /// Finds the fund with the given id in the tree of funds.
      /// </summary>
      public FundRelationships? FindFund(int fundId)
      {
        if (this.Fund.Id == fundId)
        {
          return this;
        }

        foreach (FundRelationships subFund in this.SubFunds)
        {
          FundRelationships? foundFund = subFund.FindFund(fundId);
          if (foundFund != null)
          {
            return foundFund;
          }
        }

        return null;
      }
    }
  }
}