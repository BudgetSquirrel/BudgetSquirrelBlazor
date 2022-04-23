using BudgetSquirrel.Common.Data.Infrastructure;

namespace BudgetSquirrel.BudgetPlanning.Domain.BudgetPlanning
{
  /// <summary>
  /// DTO for <see cref="Budget"/>
  /// </summary>
  public class BudgetDto : IDto<Budget>
  {
    public decimal PlannedAmount { get; set; }

    public Budget ToDomain()
    {
      return new Budget(this.PlannedAmount);
    }
  }
}