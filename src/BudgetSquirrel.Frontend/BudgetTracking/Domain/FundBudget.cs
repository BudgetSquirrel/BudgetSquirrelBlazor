namespace BudgetSquirrel.Frontend.BudgetTracking.Domain
{
  public partial class BudgetTrackingContext
  {
    public class FundBudget
    {
      public FundBudget(Budget budget, int fundId)
      {
        this.Budget = budget;
        this.FundId = fundId;
      }

      public Budget Budget { get; private set; }

      public int FundId { get; private set; }
    }
  }
}