using System;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Transactions
{
  public class AddTransactionFormState
  {
    public AddTransactionFormState(int fundId)
    {
      this.FundId = fundId;
    }

    /// <summary>
    /// Whether or not this transaction is an expense (taking money out
    /// of the fund) or an income (putting money into the fund).
    /// </summary>
    public bool IsExpense { get; set; } = true;
    
    public int FundId { get; private set; }
    
    public string VendorName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Amount { get; set; } = 0;

    public DateTime DateOfTransaction { get; set; } = DateTime.Now;

    public string CheckNumber { get; set; } = string.Empty;
  }
}