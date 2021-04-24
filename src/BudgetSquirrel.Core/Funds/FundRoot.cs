using System.Collections.Generic;

namespace BudgetSquirrel.Core.Funds
{
  /// <summary>
  /// Represents a wrapper around an entire tree of funds and budgets.
  /// This can be used to reason with a tree of funds without actually
  /// needing the details of that tree.
  ///
  /// In another sense, this is the abstraction for a tree of funds.
  /// </summary>
  public class FundRoot
  {
    public FundRoot(int fundRootId)
    {
      this.FundRootId = fundRootId;
    }

    public int FundRootId { get; private set; }
  }
}