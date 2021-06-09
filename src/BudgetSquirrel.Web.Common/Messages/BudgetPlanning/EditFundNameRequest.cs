namespace BudgetSquirrel.Web.Common.Messages.BudgetPlanning
{
  public class EditFundNameRequest
  {
    public int FundId { get; set; }
    public string NewName { get; set; }
  }
}