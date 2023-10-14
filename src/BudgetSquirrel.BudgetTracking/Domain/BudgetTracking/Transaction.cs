using System;
using System.Collections;
using System.Collections.Generic;

namespace BudgetSquirrel.BudgetTracking.Domain.BudgetTracking
{
  public class Transaction
  {
    public Transaction(
      Guid id,
      string vendorName,
      string description,
      decimal amount,
      DateTime dateOfTransaction,
      string checkNumber)
    {
      Id = id;
      VendorName = vendorName;
      Description = description;
      Amount = amount;
      DateOfTransaction = dateOfTransaction;
      CheckNumber = checkNumber;
    }

    public Guid Id { get; private set; }
    public string VendorName { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime DateOfTransaction { get; private set; }
    public string CheckNumber { get; private set; }

    /// <summary>
    /// Represents which funds this transaction is applied to along with how
    /// much money from this transaction is applied to each fund.
    /// </summary>
    public IEnumerable<TransactionAllocation> Allocations { get; private set; }

    public Transaction AsJustCreated(Guid createdId, Transaction justCreated)
    {
      return new Transaction(
        createdId,
        justCreated.VendorName,
        justCreated.Description,
        justCreated.Amount,
        justCreated.DateOfTransaction,
        justCreated.CheckNumber);
    }
  }
}