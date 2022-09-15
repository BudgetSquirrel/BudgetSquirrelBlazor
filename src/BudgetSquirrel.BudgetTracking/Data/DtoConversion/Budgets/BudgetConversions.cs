using BudgetSquirrel.BudgetTracking.Domain.BudgetPlanning;
using BudgetSquirrel.Common.Data.Schema.Budgets;

namespace BudgetSquirrel.BudgetTracking.Data.Budgets
{
  /// <summary>
  /// Conversion functions for budgets
  /// </summary>
  public static class BudgetConversions
  {
    public static Budget ToDomain(BudgetDto budgetDto)
    {
      return new Budget(budgetDto.PlannedAmount, budgetDto.IsFinalized);
    }
  }
}