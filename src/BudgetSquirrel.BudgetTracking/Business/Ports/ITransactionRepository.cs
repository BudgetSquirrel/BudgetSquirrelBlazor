using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetTracking.Domain.BudgetTracking;

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
  }
}