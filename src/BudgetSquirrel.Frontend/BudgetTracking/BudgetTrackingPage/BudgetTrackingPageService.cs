using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.Frontend.BackendClient;
using BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Transactions;
using BudgetSquirrel.Frontend.BudgetTracking.Domain;
using BudgetSquirrel.Web.Common.Messages.BudgetTracking;
using BudgetSquirrel.Web.Common.Messages.BudgetTracking.Transactions;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage
{
  public class BudgetTrackingPageService : IBudgetTrackingPageService
  {
    private const string BudgetTrackingUri = "budget-tracking";
    private const string ContextEndpoint = BudgetTrackingUri + "/context";
    private const string AddTransactionEndpoint = BudgetTrackingUri + "/transactions/add";
    private const string DeleteTransactionEndpoint = BudgetTrackingUri + "/transactions/delete";
    private const string EditTransactionEndpoint = BudgetTrackingUri + "/transactions/edit";

    private IBackendClient backendClient;

    public BudgetTrackingPageService(IBackendClient backendClient)
    {
      this.backendClient = backendClient;
    }

    public Task CreateTransaction(AddTransactionFormState request)
    {
      decimal amount = request.Amount;
      
      if (request.IsExpense)
      {
        amount = -1 * Math.Abs(amount);
      }
      else
      {
        amount = Math.Abs(amount);
      }
      
      AddTransactionRequest apiRequest = new AddTransactionRequest(
        request.FundId,
        request.VendorName,
        request.Description,
        amount,
        request.DateOfTransaction,
        request.CheckNumber);

      return this.backendClient.ExecuteCommand(AddTransactionEndpoint, apiRequest);
    }

    public Task DeleteTransaction(Guid transactionId)
    {
      DeleteTransactionRequest apiRequest = new DeleteTransactionRequest(transactionId);
      return this.backendClient.ExecuteCommand(DeleteTransactionEndpoint, apiRequest);
    }

    public Task EditTransaction(EditTransactionFormState request)
    {
      decimal amount = request.Amount;
      
      if (request.IsExpense)
      {
        amount = -1 * Math.Abs(amount);
      }
      else
      {
        amount = Math.Abs(amount);
      }
      
      EditTransactionRequest apiRequest = new EditTransactionRequest(
        request.TransactionId,
        request.FundId,
        request.VendorName,
        request.Description,
        amount,
        request.DateOfTransaction,
        request.CheckNumber);

      return this.backendClient.ExecuteCommand(EditTransactionEndpoint, apiRequest);
    }

    public async Task<BudgetTrackingContext> GetPageContext(int? timeboxId = null)
    {
      BudgetTrackingContextResponse contextResponse = await this.backendClient.Fetch<BudgetTrackingContextResponse>(ContextEndpoint);
      BudgetTrackingContext context = BudgetTrackingResponseResolvers.ToFrontendDto(contextResponse);
      return context;
    }
  }
}