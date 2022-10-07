namespace BudgetSquirrel.Frontend.BudgetTracking.Domain
{
  public partial class BudgetTrackingContext
  {
    public TimeboxDetails Timebox { get; private set; }
    
    public FundRelationships FundTree { get; private set; }

    public bool isFinalized { get; private set; }

    public BudgetTrackingContext(FundRelationships fundTree, TimeboxDetails timebox, bool isFinalized)
    {
      this.FundTree = fundTree;
      this.Timebox = timebox;
      this.isFinalized = isFinalized;
    }
  }
}