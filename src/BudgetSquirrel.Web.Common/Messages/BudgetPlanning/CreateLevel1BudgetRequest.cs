namespace BudgetSquirrel.Web.Common.Messages.BudgetPlanning
{
  public class CreateLevel1BudgetRequest
  {
    public string Name { get; set; }
    public decimal PlannedAmount { get; set; }
    public int TimeboxId { get; set; }
  }
}