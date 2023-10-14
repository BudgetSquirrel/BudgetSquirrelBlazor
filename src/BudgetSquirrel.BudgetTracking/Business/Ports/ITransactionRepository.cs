using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetTracking.Domain.BudgetTracking;
using BudgetSquirrel.BudgetTracking.Domain.History;

namespace BudgetSquirrel.BudgetTracking.Business.Ports
{
  public interface ITransactionRepository
  {
    /// <summary>
    /// Returns all transactions for the given fund within the given start and end date.
    /// </summary>
    /// <param name="fundId">The id of the fund for which to fetch the transactions</param>
    /// <param name="startDate">The first day for which to fetch the transactions (inclusive)</param>
    /// <param name="endDate">The last day for which to fetch the transactions (inclusive)</param>
    Task<IEnumerable<Transaction>> GetTransactionsInDates(int fundId, DateTime startDate, DateTime endDate);

    /// <summary>
    /// Creates the given transaction in the database.
    /// </summary>
    /// <param name="transaction">The transaction to create</param>
    Task<Guid> CreateTransaction(Transaction transaction);

    /// <summary>
    /// Returns the transaction with the given id.
    /// </summary>
    /// <param name="transactionId"></param>
    /// <returns></returns>
    Task<Transaction> GetTransaction(Guid transactionId);

    /// <summary>
    /// Records the allocation of a transaction to a fund in the database.
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="allocation"></param>
    /// <returns></returns>
    Task RecordTransactionAllocation(Transaction transaction, int fundId, decimal amount, int timeboxId);
  }
}