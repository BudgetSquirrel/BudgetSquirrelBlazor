using System.Collections.Generic;
using System.Linq;

namespace BudgetSquirrel.Frontend.BudgetTracking.Domain
{
  public partial class BudgetTrackingContext
  {
    public class FundRelationships
    {
      public FundRelationships(Fund parentFund, Budget budget, IEnumerable<FundRelationships> subFunds)
      {
        this.Fund = parentFund;
        this.Budget = budget;
        this.SubFunds = subFunds;
      }

      public Fund Fund { get; private set; }

      public Budget Budget { get; private set; }

      public IEnumerable<FundRelationships> SubFunds { get; private set; }
    }
  }
}