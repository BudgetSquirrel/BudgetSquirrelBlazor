using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Frontend.Authentication.Login;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public partial class BudgetPlannerPage : ComponentBase
  {
    [Inject]
    private ILoginService loginService { get; set; }

    [Inject]
    private IBudgetPlanningService budgetPlanningService { get; set; }

    private BudgetPlanningContext context;

    private bool isLoading = true;

    protected override Task OnInitializedAsync()
    {
      return this.ReloadContext();
    }

    private async Task ReloadContext()
    {
      this.isLoading = true;
      await this.loginService.PromptLoginIfNecessary();
      this.context = await this.budgetPlanningService.GetBudgetTree();
      this.isLoading = false;
    }

    private BudgetPlanningContext.FundBudget rootBudget => this.context.Budgets.Single(b => b.FundId == this.context.FundTree.Fund.Id);

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

    private decimal PlannedIncome => this.rootBudget.Budget.PlannedAmount;

    private string RootFundName => this.context.FundTree.Fund.Name;

    private async Task ChangePlannedIncome(string amount)
    {
      decimal newPlannedIncome = decimal.Parse(amount);
      await this.budgetPlanningService.EditPlannedIncome(this.context.FundTree.Fund.Id, this.context.Timebox.Id, newPlannedIncome);
      await this.ReloadContext();
    }

    private async Task ChangeRootFundName(string newName)
    {
      await this.budgetPlanningService.EditFundName(this.context.FundTree.Fund.Id, newName);
      await this.ReloadContext();
    }
  }
}