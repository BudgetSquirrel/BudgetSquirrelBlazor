using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.Frontend.Authentication.Login;
using BudgetSquirrel.Frontend.BackendClient;
using BudgetSquirrel.Web.Common.Messages.BudgetPlanning;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public class BudgetPlanningService : IBudgetPlanningService
  {
    private const string EndpointFormatFundId = "{fundId}";
    private const string EndpointFormatTimeboxId = "{timeboxId}";
    
    private const string BudgetPlanningUri = "budget-planning";
    private const string ContextEndpoint = BudgetPlanningUri + "/context";
    private const string EditPlannedIncomeEndpoint = BudgetPlanningUri + "/edit-planned-income";
    private const string DeleteBudgetEndpoint = BudgetPlanningUri + "/delete/" + EndpointFormatFundId + "/" + EndpointFormatTimeboxId;
    private const string EditFundNameEndpoint = BudgetPlanningUri + "/edit-fund-name";
    private const string CreateLevel1BudgetEndpoint = BudgetPlanningUri + "/create-level1-budget";
    private const string CreateSubBudgetEndpoint = BudgetPlanningUri + "/create-sub-budget";
    private const string FinalizeBudgetEndpoint = BudgetPlanningUri + "/finalize";

    private IBackendClient backendClient;

    public BudgetPlanningService(IBackendClient backendClient)
    {
      this.backendClient = backendClient;
    }

    public async Task<BudgetPlanningContext> GetBudgetTree(int? timeboxId = null)
    {
      BudgetPlanningContextResponse contextResponse = await this.backendClient.Fetch<BudgetPlanningContextResponse>(
        ContextEndpoint,
        new Dictionary<string, object>() { { "profileId", 1 } });
      BudgetPlanningContext context = BudgetPlanningResponseResolvers.ToFrontendDto(contextResponse);
      return context;
    }

    public Task EditPlannedAmount(int fundId, int timeboxId, decimal plannedIncome)
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

    public Task EditFundName(int fundId, string newName)
    {
      return this.backendClient.ExecuteCommand(
        EditFundNameEndpoint,
        new EditFundNameRequest()
        {
          FundId = fundId,
          NewName = newName
        });
    }

    public Task CreateLevel1Budget(int profileId, int timeboxId, string name, decimal plannedAmount)
    {
      return this.backendClient.ExecuteCommand(
        CreateLevel1BudgetEndpoint,
        new CreateLevel1BudgetRequest()
        {
          Name = name,
          PlannedAmount = plannedAmount,
          ProfileId = profileId,
          TimeboxId = timeboxId
        });
    }

    public Task CreateSubBudget(int parentFundId, int timeboxId, string name, decimal plannedAmount)
    {
      return this.backendClient.ExecuteCommand(
        CreateSubBudgetEndpoint,
        new CreateSubBudgetRequest()
        {
          Name = name,
          PlannedAmount = plannedAmount,
          ParentFundId = parentFundId,
          TimeboxId = timeboxId
        });
    }

    public Task DeleteBudget(int fundId, int timeboxId)
    {
      return this.backendClient.ExecuteCommand(
        DeleteBudgetEndpoint
          .Replace(EndpointFormatFundId, fundId.ToString())
          .Replace(EndpointFormatTimeboxId, timeboxId.ToString()));
    }

    public Task FinalizeBudget(int profileId, int timeboxId)
    {
      return this.backendClient.ExecuteCommand(
        FinalizeBudgetEndpoint,
        new FinalizeBudgetRequest()
        {
          ProfileId = profileId,
          TimeboxId = timeboxId
        });
    }
  }
}