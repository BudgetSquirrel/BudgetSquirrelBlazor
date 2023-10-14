using System;

namespace BudgetSquirrel.Web.Common.Messages.BudgetTracking.Transactions
{
  public class AddTransactionRequest
  {
    public AddTransactionRequest(
      int fundId,
      string vendorName,
      string description,
      decimal amount,
      DateTime dateOfTransaction,
      string checkNumber)
    {
      FundId = fundId;
      VendorName = vendorName;
      Description = description;
      Amount = amount;
      DateOfTransaction = dateOfTransaction;
      CheckNumber = checkNumber;
    }

    public int FundId { get; private set; }
    
    public string VendorName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Amount { get; set; } = 0;

    public DateTime DateOfTransaction { get; set; } = DateTime.Now;

    public string CheckNumber { get; set; } = string.Empty;
  }
}