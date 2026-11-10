using System;
using static BudgetSquirrel.Frontend.BudgetTracking.Domain.BudgetTrackingContext;

namespace BudgetSquirrel.Frontend.BudgetTracking.Domain
{
  public class Transaction
  {
    public Transaction(
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

    public Guid Id { get; private set; }
    public int FundId { get; private set; }
    public string VendorName { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime DateOfTransaction { get; private set; }
    public string CheckNumber { get; private set; }
  }
}