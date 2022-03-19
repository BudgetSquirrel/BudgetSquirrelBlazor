namespace BudgetSquirrel.Frontend.BudgetPlanning.Budgets
{
  public interface IDeleteBudgetFormValues
  {
    int FundId { get; }
  }

  public class DeleteBudgetFormValues : IDeleteBudgetFormValues
  {
    public DeleteBudgetFormValues(int fundId)
    {
      this.FundId = fundId;
    }

    public int FundId { get; private set; }
  }
}
