using System.Collections.Generic;

namespace BudgetSquirrel.BudgetPlanning.Domain.Funds
{
  /// <summary>
  /// Represents a wrapper around an entire tree of funds and budgets.
  /// This can be used to reason with a tree of funds without actually
  /// needing the details of that tree.
  ///
  /// In another sense, this is the abstraction for a tree of funds.
  /// </summary>
  public class Profile
  {
    public Profile(int profileId)
    {
      this.ProfileId = profileId;
    }

    public int ProfileId { get; private set; }
  }
}