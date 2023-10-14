using BudgetSquirrel.BudgetTracking.Domain.Funds;
using BudgetSquirrel.BudgetTracking.Domain.History;

namespace BudgetSquirrel.BudgetTracking.Domain.BudgetTracking
{
  /// <summary>
  /// Represents the application of money from a particular transaction to a particular fund.
  /// If a transaction only applies to one fund, then there will only be one allocation. However,
  /// if a transaction applies to multiple funds, then there will be one allocation per fund
  /// that the transaction applies to.
  /// </summary>
  public class TransactionAllocation
  {
    /// <summary>
    /// Creates a new TransactionAllocation to a specific fund with a particular amount.
    /// </summary>
    /// <param name="fund"></param>
    /// <param name="timebox"></param>
    /// <param name="amount"></param>
    public TransactionAllocation(
      Fund fund,
      decimal amount,
      Timebox timebox)
    {
      Fund = fund;
      Amount = amount;
      Timebox = timebox;
    }
    
    /// <summary>
    /// The fund to which the transaction is applied.
    /// </summary>
    public Fund Fund { get; private set; }

    /// <summary>
    /// The amount of money from the transaction that was applied to the fund.
    /// </summary>
    public decimal Amount { get; private set; }

    /// <summary>
    /// The timebox in which this transaction was applied to the fund.
    /// </summary>
    public Timebox Timebox { get; private set; } 
  }
}