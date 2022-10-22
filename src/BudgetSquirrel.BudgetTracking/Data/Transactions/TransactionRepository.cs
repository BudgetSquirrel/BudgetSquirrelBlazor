using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetTracking.Business.Ports;
using BudgetSquirrel.BudgetTracking.Domain.BudgetTracking;
using BudgetSquirrel.Common.Data.Infrastructure;
using BudgetSquirrel.Common.Data.Schema;
using BudgetSquirrel.Common.Data.Schema.Transactions;
using Dapper;

namespace BudgetSquirrel.BudgetTracking.Data.Transactions
{
  public class TransactionRepository : ITransactionRepository
  {
    private DbConnectionProvider dbConnectionProvider;

    public TransactionRepository(DbConnectionProvider dbConnectionProvider)
    {
      this.dbConnectionProvider = dbConnectionProvider;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsInDates(int fundId, DateTime startDate, DateTime endDate)
    {
      IEnumerable<TransactionDto> transactions;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        transactions = await conn.QueryAsync<TransactionDto>(
          $"EXEC {StoredProcedures.Transactions.GetTransactionsByFundAndDateRange} @FundId, @StartDate, @EndDate",
          new
          {
            FundId = fundId,
            StartDate = startDate,
            EndDate = endDate
          }
        );
      }
      return transactions.Select(t => TransactionConversions.ToDomain(t));
    }
  }
}