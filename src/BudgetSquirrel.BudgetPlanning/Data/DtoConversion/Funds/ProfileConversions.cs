using BudgetSquirrel.BudgetPlanning.Domain.Funds;
using BudgetSquirrel.Common.Data.Schema.Funds;

namespace BudgetSquirrel.BudgetPlanning.Data.DtoConversions.Funds
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