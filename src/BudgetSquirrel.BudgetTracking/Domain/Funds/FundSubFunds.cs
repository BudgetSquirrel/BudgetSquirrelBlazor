using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetSquirrel.BudgetTracking.Domain.Funds
{
  public class FundSubFunds
  {
    public FundSubFunds(Fund parentFund, IEnumerable<FundSubFunds> subFunds, Func<DateTime, Task<decimal>> loadBalance)
    {
      this.Fund = parentFund;
      this.SubFunds = subFunds;
      this.loadBalance = loadBalance;
    }

    public Fund Fund { get; private set; }

    public IEnumerable<FundSubFunds> SubFunds { get; private set; }
    
    public async Task<decimal> GetBalance(DateTime asOf)
    {
      if (!this.balanceCache.HasValue)
      {
        this.balanceCache = await this.loadBalance.Invoke(asOf);
      }

      return this.balanceCache.Value;
    }
    private decimal? balanceCache;
    private Func<DateTime, Task<decimal>> loadBalance;
  }
}