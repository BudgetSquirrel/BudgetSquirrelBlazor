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
    private const string AddTransactionEndpoint = BudgetTrackingUri + "/transactions";
    private const string DeleteTransactionEndpoint = BudgetTrackingUri + "/transactions/delete";

    private IBackendClient backendClient;

    public BudgetTrackingPageService(IBackendClient backendClient)
    {
      this.backendClient = backendClient;
    }

    public Task CreateTransaction(AddTransactionFormState request)
    {
      AddTransactionRequest apiRequest = new AddTransactionRequest(
        request.FundId,
        request.VendorName,
        request.Description,
        request.Amount,
        request.DateOfTransaction,
        request.CheckNumber);

      return this.backendClient.ExecuteCommand(AddTransactionEndpoint, apiRequest);
    }

    public Task DeleteTransaction(Guid transactionId)
    {
      DeleteTransactionRequest apiRequest = new DeleteTransactionRequest(transactionId);
      return this.backendClient.ExecuteCommand(DeleteTransactionEndpoint, apiRequest);
    }

    public async Task<BudgetTrackingContext> GetPageContext(int? timeboxId = null)
    {
      BudgetTrackingContextResponse contextResponse = await this.backendClient.Fetch<BudgetTrackingContextResponse>(ContextEndpoint);
      BudgetTrackingContext context = BudgetTrackingResponseResolvers.ToFrontendDto(contextResponse);
      return context;
    }
  }
}