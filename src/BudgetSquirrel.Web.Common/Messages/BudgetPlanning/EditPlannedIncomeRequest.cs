namespace BudgetSquirrel.Web.Common.Messages.BudgetPlanning
{
  public class EditPlannedIncomeRequest
  {
    public int FundId { get; set; }

    public int TimeboxId { get; set; }

    public decimal PlannedIncome { get; set; }
  }
}