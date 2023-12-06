using System;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetTracking.Business.Ports;
using BudgetSquirrel.BudgetTracking.Domain.BudgetTracking;

namespace BudgetSquirrel.BudgetTracking.Business.BudgetTrackingPage
{
  public class CreateTransactionCommand
  {
    private ITransactionRepository transactionRepository;

    private string vendorName;
    private string description;
    private decimal amount;
    private DateTime dateOfTransaction;
    private string checkNumber;
    private int fundId;
    private int timeboxId;

    public CreateTransactionCommand(
      ITransactionRepository transactionRepository,
      string vendorName,
      string description,
      decimal amount,
      DateTime dateOfTransaction,
      string checkNumber,
      int fundId,
      int timeboxId)
    {
      this.transactionRepository = transactionRepository;
      this.vendorName = vendorName;
      this.description = description;
      this.amount = amount;
      this.dateOfTransaction = dateOfTransaction;
      this.checkNumber = checkNumber;
      this.fundId = fundId;
      this.timeboxId = timeboxId;
    }

    public async Task Execute()
    {
      Transaction transaction = new Transaction(
        Guid.NewGuid(),
        this.vendorName,
        this.description,
        this.amount,
        this.dateOfTransaction,
        this.checkNumber);

      Guid transactionId = await this.transactionRepository.CreateTransaction(transaction);
      transaction = await this.transactionRepository.GetTransaction(transactionId);

      await this.transactionRepository.RecordTransactionAllocation(transaction, this.fundId, this.amount, this.timeboxId);
    }
  }
}