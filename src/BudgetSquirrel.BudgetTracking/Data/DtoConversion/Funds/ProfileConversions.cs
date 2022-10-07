using BudgetSquirrel.BudgetTracking.Domain.Funds;
using BudgetSquirrel.Common.Data.Schema.Funds;

namespace BudgetSquirrel.BudgetTracking.Data.Funds
{
  /// <summary>
  /// Conversion functions for profiles
  /// </summary>
  public static class ProfileConversions
  {
    public static Profile ToDomain(ProfileDto profileDto)
    {
      return new Profile(profileDto.ProfileId);
    }
  }
}