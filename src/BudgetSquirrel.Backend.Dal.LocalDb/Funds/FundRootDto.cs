using System.Collections.Generic;
using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;

namespace BudgetSquirrel.Core.Funds
{
  /// <summary>
  /// DTO for <see cref="FundRoot"/>
  /// </summary>
  public class FundRootDto : IDto<FundRoot>
  {
    public int FundRootId { get; set; }

    public FundRoot ToDomain()
    {
      return new FundRoot(this.FundRootId);
    }
  }
}