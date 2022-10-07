namespace BudgetSquirrel.Frontend.BudgetTracking.Domain
{
  public partial class BudgetTrackingContext
  {
    public class Budget
    {
      public Budget(decimal plannedAmount)
      {
        this.PlannedAmount = plannedAmount;
      }

      public decimal PlannedAmount { get; private set; }
    }
  }
}