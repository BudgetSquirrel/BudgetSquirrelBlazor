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

    public async Task<Guid> CreateTransaction(Transaction transaction)
    {
      using IDbConnection conn = this.dbConnectionProvider.GetConnection();

      await conn.ExecuteAsync(
        $"EXEC {StoredProcedures.Transactions.CreateTransaction} @TransactionId, @VendorName, @Description, @Amount, @DateOfTransaction, @CheckNumber",
        new
        {
          TransactionId = transaction.Id,
          transaction.VendorName,
          transaction.Description,
          transaction.Amount,
          transaction.DateOfTransaction,
          transaction.CheckNumber
        }
      );

      return transaction.Id;
    }

    public async Task DeleteTransaction(Guid transactionId)
    {
      using IDbConnection conn = this.dbConnectionProvider.GetConnection();

      await conn.ExecuteAsync(
        $"EXEC {StoredProcedures.Transactions.DeleteTransaction} @TransactionId",
        new
        {
          TransactionId = transactionId,
        }
      );
    }

    public async Task<Transaction> GetTransaction(Guid transactionId)
    {
      using IDbConnection conn = this.dbConnectionProvider.GetConnection();
      
      TransactionDto transaction = await conn.QuerySingleAsync<TransactionDto>(
        $"EXEC {StoredProcedures.Transactions.GetTransactionById} @TransactionId",
        new
        {
          TransactionId = transactionId
        }
      );

      return TransactionConversions.ToDomain(transaction);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsInDates(int fundId, DateTime startDate, DateTime endDate)
    {
      using IDbConnection conn = this.dbConnectionProvider.GetConnection();
      
      IEnumerable<TransactionDto> transactions = await conn.QueryAsync<TransactionDto>(
        $"EXEC {StoredProcedures.Transactions.GetTransactionsByFundAndDateRange} @FundId, @StartDate, @EndDate",
        new
        {
          FundId = fundId,
          StartDate = startDate,
          EndDate = endDate
        }
      );

      return transactions.Select(t => TransactionConversions.ToDomain(t));
    }

    public async Task RecordTransactionAllocation(Transaction transaction, int fundId, decimal amount, int timeboxId)
    {
      using IDbConnection conn = this.dbConnectionProvider.GetConnection();

      await conn.ExecuteAsync(
        $"EXEC {StoredProcedures.Transactions.CreateTransactionAllocation} @TransactionId, @FundId, @TimeboxId, @Amount",
        new
        {
          TransactionId = transaction.Id,
          FundId = fundId,
          Amount = amount,
          TimeboxId = timeboxId
        }
      );
    }
  }
}