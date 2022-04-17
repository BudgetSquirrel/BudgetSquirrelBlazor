using System.Collections.Generic;
using BudgetSquirrel.BudgetPlanning.Data.Infrastructure;

namespace BudgetSquirrel.BudgetPlanning.Domain.Funds
{
  /// <summary>
  /// DTO for <see cref="Profile"/>
  /// </summary>
  public class ProfileDto : IDto<Profile>
  {
    public int ProfileId { get; set; }

    public Profile ToDomain()
    {
      return new Profile(this.ProfileId);
    }
  }
}