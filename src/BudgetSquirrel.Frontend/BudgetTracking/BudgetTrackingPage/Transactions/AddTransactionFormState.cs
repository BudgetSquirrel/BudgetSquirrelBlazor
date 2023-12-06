using System;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Transactions
{
  public class AddTransactionFormState
  {
    public AddTransactionFormState(int fundId)
    {
      this.FundId = fundId;
    }
    
    public int FundId { get; private set; }
    
    public string VendorName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Amount { get; set; } = 0;

    public DateTime DateOfTransaction { get; set; } = DateTime.Now;

    public string CheckNumber { get; set; } = string.Empty;
  }
}