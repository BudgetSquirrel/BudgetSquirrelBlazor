namespace BudgetSquirrel.BudgetTracking.Domain.BudgetPlanning
{
  /// <summary>
  /// Represents the amount of money the user has planned to put into this fund.
  ///
  /// Often, for funds that represent a line item for expenses, this will represent
  /// how much the user is allowing themselves to spend for the fund. However, for
  /// funds that they treat as sinking funds that they are setting savings aside in,
  /// this will be the amount that they plan to save into the fund.
  /// </summary>
  public class Budget
  {
    public Budget(decimal plannedAmount, bool isFinalized)
    {
      this.PlannedAmount = plannedAmount;
      this.IsFinalized = isFinalized;
    }

    public decimal PlannedAmount { get; set; }

    public bool IsFinalized { get; private set; }
  }
}