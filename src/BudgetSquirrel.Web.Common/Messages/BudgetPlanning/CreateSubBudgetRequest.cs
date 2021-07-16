namespace BudgetSquirrel.Web.Common.Messages.BudgetPlanning
{
  public class CreateSubBudgetRequest
  {
    public string Name { get; set; }
    public decimal PlannedAmount { get; set; }
    public int ParentFundId { get; set; }
    public int TimeboxId { get; set; }
  }
}