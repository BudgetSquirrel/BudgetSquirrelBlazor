using System.Threading.Tasks;
using BudgetSquirrel.Frontend.Authentication.Login;
using BudgetSquirrel.Frontend.BudgetTracking.Domain;
using Microsoft.AspNetCore.Components;
using static BudgetSquirrel.Frontend.BudgetTracking.Domain.BudgetTrackingContext;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage
{
  public partial class BudgetTrackingPage : ComponentBase
  {
    [Inject]
    private ILoginService loginService { get; set; } = null!;

    [Inject]
    private IBudgetTrackingPageService pageService { get; set; } = null!;

    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

#region initial state
    
    private bool isLoading = true;

    private BudgetTrackingContext? context;

    protected override Task OnInitializedAsync()
    {
      return this.ReloadContext();
    }

    private async Task ReloadContext()
    {
      this.isLoading = true;
      await this.loginService.PromptLoginIfNecessary();
      this.context = await this.pageService.GetPageContext();
      
      if (!this.context.isFinalized)
      {
        this.navigationManager.NavigateTo("/budget-planner");
      }
      this.isLoading = false;
    }

#endregion initial state

#region convenience accessors

    private FundRelationships rootBudget => this.context?.FundTree ?? null!;

#endregion convenience accessors

#region template accessors

    private string timeboxDisplay
    {
      get
      {
        if (this.context == null)
        {
          return "";
        }
        return $"({this.context.Timebox.StartDate.ToString("%M/%d")} - {this.context.Timebox.EndDate.ToString("%M/%d")})";
      }
    }

    private string RootFundName => this.context?.FundTree.Fund.Name ?? string.Empty;

#endregion template accessors
  }
}