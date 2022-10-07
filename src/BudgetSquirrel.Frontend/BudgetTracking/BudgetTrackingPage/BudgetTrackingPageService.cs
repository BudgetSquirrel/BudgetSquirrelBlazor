using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.Frontend.BackendClient;
using BudgetSquirrel.Frontend.BudgetTracking.Domain;
using BudgetSquirrel.Web.Common.Messages.BudgetTracking;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage
{
  public class BudgetTrackingPageService : IBudgetTrackingPageService
  {
    private const string BudgetTrackingUri = "budget-tracking";
    private const string ContextEndpoint = BudgetTrackingUri + "/context";

    private IBackendClient backendClient;

    public BudgetTrackingPageService(IBackendClient backendClient)
    {
      this.backendClient = backendClient;
    }

    public async Task<BudgetTrackingContext> GetPageContext(int? timeboxId = null)
    {
      BudgetTrackingContextResponse contextResponse = await this.backendClient.Fetch<BudgetTrackingContextResponse>(
        ContextEndpoint,
        new Dictionary<string, object>() { { "profileId", 1 } });
      BudgetTrackingContext context = BudgetTrackingResponseResolvers.ToFrontendDto(contextResponse);
      return context;
    }
  }
}