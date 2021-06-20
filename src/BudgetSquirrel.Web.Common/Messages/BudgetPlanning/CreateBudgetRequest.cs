namespace BudgetSquirrel.Web.Common.Messages.BudgetPlanning
{
  public class CreateBudgetRequest
  {
    public string Name { get; set; }
    public decimal PlannedAmount { get; set; }
    public int ProfileId { get; set; }
    public int TimeboxId { get; set; }
  }
}