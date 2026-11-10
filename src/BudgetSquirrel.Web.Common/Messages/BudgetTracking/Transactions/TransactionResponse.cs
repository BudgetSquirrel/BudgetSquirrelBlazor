using System;

namespace BudgetSquirrel.Web.Common.Messages.BudgetTracking.Transactions
{
  public class TransactionResponse
  {
    public TransactionResponse()
    {
    }

    public TransactionResponse(
      Guid id,
      int fundId,
      string vendorName,
      string description,
      decimal amount,
      DateTime dateOfTransaction,
      string checkNumber)
    {
      Id = id;
      FundId = fundId;
      VendorName = vendorName;
      Description = description;
      Amount = amount;
      DateOfTransaction = dateOfTransaction;
      CheckNumber = checkNumber;
    }

    public Guid Id { get; set; }
    public int FundId { get; set; }
    public string VendorName { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateOfTransaction { get; set; }
    public string CheckNumber { get; set; }
  }
}