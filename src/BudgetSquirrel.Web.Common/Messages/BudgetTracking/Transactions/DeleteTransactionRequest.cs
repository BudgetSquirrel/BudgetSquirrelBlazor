using System;

namespace BudgetSquirrel.Web.Common.Messages.BudgetTracking.Transactions
{
  public class DeleteTransactionRequest
  {
    public DeleteTransactionRequest(
      Guid transactionId)
    {
      TransactionId = transactionId;
    }

    public Guid TransactionId { get; private set; }
  }
}