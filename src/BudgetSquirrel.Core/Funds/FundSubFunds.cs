using System.Collections.Generic;

namespace BudgetSquirrel.Core.Funds
{
  public class FundSubFunds
  {
    public FundSubFunds(Fund parentFund, IEnumerable<FundSubFunds> subFunds)
    {
      this.Fund = parentFund;
      this.SubFunds = subFunds;
    }

    public Fund Fund { get; private set; }

    public IEnumerable<FundSubFunds> SubFunds { get; private set; }
  }
}