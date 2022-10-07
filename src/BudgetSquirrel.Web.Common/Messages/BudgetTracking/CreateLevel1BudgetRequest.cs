namespace BudgetSquirrel.Web.Common.Messages.BudgetTracking
{
  public class CreateLevel1BudgetRequest
  {
    public string Name { get; set; }
    public decimal PlannedAmount { get; set; }
    public int ProfileId { get; set; }
    public int TimeboxId { get; set; }
  }
}