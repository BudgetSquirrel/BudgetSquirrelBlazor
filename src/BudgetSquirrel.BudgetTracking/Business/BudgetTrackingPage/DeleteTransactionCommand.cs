using System;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetTracking.Business.Ports;

namespace BudgetSquirrel.BudgetTracking.Business.BudgetTrackingPage
{
  public class DeleteTransactionCommand
  {
    private ITransactionRepository transactionRepository;

    private Guid transactionId;

    public DeleteTransactionCommand(
      ITransactionRepository transactionRepository,
      Guid transactionId)
    {
      this.transactionRepository = transactionRepository;
      this.transactionId = transactionId;
    }

    public Task Execute()
    {
      return this.transactionRepository.DeleteTransaction(this.transactionId);
    }
  }
}