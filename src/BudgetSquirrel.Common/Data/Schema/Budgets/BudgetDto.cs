using BudgetSquirrel.Common.Data.Infrastructure;

namespace BudgetSquirrel.Common.Data.Schema.Budgets
{
  /// <summary>
  /// DTO for <see cref="Budget"/>
  /// </summary>
  public class BudgetDto
  {
    public decimal PlannedAmount { get; set; }
    public bool IsFinalized { get; set; }
  }
}