using BudgetSquirrel.BudgetPlanning.Domain.Funds;
using BudgetSquirrel.Common.Data.Schema.Funds;

namespace BudgetSquirrel.BudgetPlanning.Data.DtoConversions.Funds
{
  /// <summary>
  /// Conversion functions for funds
  /// </summary>
  public static class FundConversions
  {
    public static Fund ToDomain(FundDto fundDto)
    {
      return new Fund(fundDto.Name, fundDto.Balance, fundDto.IsRoot, fundDto.ProfileId, fundDto.Id, fundDto.ParentFundId);
    }
  }
}