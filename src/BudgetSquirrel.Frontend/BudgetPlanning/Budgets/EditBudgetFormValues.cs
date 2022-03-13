namespace BudgetSquirrel.Frontend.BudgetPlanning.Budgets
{
  public interface IEditPlannedAmountFormValues
  {
    int FundId { get; }
    decimal PlannedAmount { get; set; }
  }

  public class EditBudgetFormValues : IEditPlannedAmountFormValues
  {
    public EditBudgetFormValues(int fundId, string name, decimal plannedAmount)
    {
      this.FundId = fundId;
      this.Name = name;
      this.PlannedAmount = plannedAmount;
    }

    public int FundId { get; private set; }
    public string Name { get; set; }
    public decimal PlannedAmount { get; set; }
  }
}
