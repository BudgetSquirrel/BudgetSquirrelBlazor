using System;
using BudgetSquirrel.Frontend.BudgetTracking.Domain;
using static BudgetSquirrel.Frontend.BudgetTracking.Domain.BudgetTrackingContext;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Transactions
{
  public class EditTransactionFormState
  {
    public EditTransactionFormState(Transaction original)
    {
      this.TransactionId = original.Id;
      this.FundId = original.FundId;
      this.IsExpense = original.Amount < 0;
      this.Amount = Math.Abs(original.Amount);
      this.VendorName = original.VendorName;
      this.Description = original.Description;
      this.DateOfTransaction = original.DateOfTransaction;
      this.CheckNumber = original.CheckNumber;
    }

    public Guid TransactionId { get; set; }

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