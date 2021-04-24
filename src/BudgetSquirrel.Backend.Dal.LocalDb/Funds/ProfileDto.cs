using System.Collections.Generic;
using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;

namespace BudgetSquirrel.Core.Funds
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