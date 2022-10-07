namespace BudgetSquirrel.Web.Common.Messages.BudgetTracking
{
  public class EditFundNameRequest
  {
    public int FundId { get; set; }
    public string NewName { get; set; }
  }
}