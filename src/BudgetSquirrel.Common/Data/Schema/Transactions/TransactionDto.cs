using System;

namespace BudgetSquirrel.Common.Data.Schema.Transactions
{
  public class TransactionDto
  {
    public Guid Id { get; set; }
    public string VendorName { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateOfTransaction { get; set; }
    public string CheckNumber { get; set; }
  }
}