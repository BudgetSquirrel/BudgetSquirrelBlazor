using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.Frontend.Authentication.Login;
using BudgetSquirrel.Frontend.BudgetPlanning;
using BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Funds;
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
    private IBudgetPlanningService budgetPlanningService { get; set; } = null!;

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

#region actions state

    private FundRelationships? viewingFund { get; set; }


#endregion actions state

#region convenience accessors

    private FundRelationships rootBudget => this.context?.FundTree ?? null!;

    private bool isViewingFund => this.viewingFund != null;

    private string rootBalanceDisplay => this.rootBudget.Fund.Balance.ToString("C");

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

    private string plannedIncomeDisplay => this.context?.FundTree.Budget.PlannedAmount.ToString("C") ?? string.Empty;

    public IEnumerable<FundRelationships> Level1Budgets
    {
      get
      {
        if (this.isLoading)
        {
          return Array.Empty<FundRelationships>();
        }
        return this.rootBudget.SubFunds;
      }
    }

#endregion template accessors

#region event handlers

    private async Task ChangeSubFundName(IEditNameFormValues values)
    {
      await this.budgetPlanningService.EditFundName(values.FundId, values.Name);
      await this.ReloadContext();
    }

    private void ViewFund(FundRelationships fund)
    {
      this.viewingFund = fund;
    }

    private void CloseFundView()
    {
      this.viewingFund = null;
    }

#endregion event handlers
  }
}