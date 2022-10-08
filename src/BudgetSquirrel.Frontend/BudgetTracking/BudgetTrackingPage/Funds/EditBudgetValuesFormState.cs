namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Funds
{
  public interface IEditNameFormValues
  {
    int FundId { get; }
    string Name { get; set; }
  }

  public class EditBudgetFormValues : IEditNameFormValues
  {
    public EditBudgetFormValues(int fundId, string name)
    {
      this.FundId = fundId;
      this.Name = name;
    }

    public int FundId { get; private set; }
    public string Name { get; set; }
  }
}
