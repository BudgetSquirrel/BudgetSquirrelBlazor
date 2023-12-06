using BudgetSquirrel.BudgetTracking.Domain.BudgetTracking;
using BudgetSquirrel.BudgetTracking.Domain.History;
using BudgetSquirrel.Common.Data.Schema.History;
using BudgetSquirrel.Common.Data.Schema.Transactions;

namespace BudgetSquirrel.BudgetTracking.Data.Transactions
{
  /// <summary>
  /// Conversion functions for timeboxes
  /// </summary>
  public static class TransactionConversions
  {
    public static Transaction ToDomain(TransactionDto transactionDto)
    {
      return new Transaction(
        transactionDto.Id,
        transactionDto.VendorName,
        transactionDto.Description,
        transactionDto.Amount,
        transactionDto.DateOfTransaction,
        transactionDto.CheckNumber);
    }
  }
}