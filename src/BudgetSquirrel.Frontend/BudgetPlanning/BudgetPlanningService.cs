using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.Frontend.Authentication.Login;
using BudgetSquirrel.Frontend.BackendClient;
using BudgetSquirrel.Web.Common.Messages.BudgetPlanning;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public class BudgetPlanningService : IBudgetPlanningService
  {
    private const string BudgetPlanningUri = "budget-planning";
    private const string ContextEndpoint = BudgetPlanningUri + "/context";
    private const string EditPlannedIncomeEndpoint = BudgetPlanningUri + "/edit-planned-income";
    
    private ILoginService loginService;
    private IBackendClient backendClient;

    public BudgetPlanningService(IBackendClient backendClient, ILoginService loginService)
    {
      this.backendClient = backendClient;
      this.loginService = loginService;
    }

    public async Task<BudgetPlanningContext> GetBudgetTree(int? timeboxId=null)
    {
      BudgetPlanningContextResponse contextResponse = await this.backendClient.Fetch<BudgetPlanningContextResponse>(
        ContextEndpoint,
        new Dictionary<string, object>() { { "profileId", 1 } });
      BudgetPlanningContext context = BudgetPlanningResponseResolvers.ToFrontendDto(contextResponse);
      return context;
    }

    public Task EditPlannedIncome(int fundId, int timeboxId, decimal plannedIncome)
    {
      return this.backendClient.ExecuteCommand(
        EditPlannedIncomeEndpoint,
        new EditPlannedIncomeRequest()
        {
          FundId = fundId,
          TimeboxId = timeboxId,
          PlannedIncome = plannedIncome
        });
    }
  }
}