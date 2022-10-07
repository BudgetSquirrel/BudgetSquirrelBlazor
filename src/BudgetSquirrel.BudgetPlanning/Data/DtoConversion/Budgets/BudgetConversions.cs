using BudgetSquirrel.BudgetPlanning.Domain.BudgetPlanning;
using BudgetSquirrel.Common.Data.Schema.Budgets;

namespace BudgetSquirrel.BudgetPlanning.Data.DtoConversions.Budgets
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