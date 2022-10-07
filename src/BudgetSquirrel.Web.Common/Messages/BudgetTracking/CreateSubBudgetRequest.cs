namespace BudgetSquirrel.Web.Common.Messages.BudgetTracking
{
  public class CreateSubBudgetRequest
  {
    public string Name { get; set; }
    public decimal PlannedAmount { get; set; }
    public int ParentFundId { get; set; }
    public int TimeboxId { get; set; }
  }
}