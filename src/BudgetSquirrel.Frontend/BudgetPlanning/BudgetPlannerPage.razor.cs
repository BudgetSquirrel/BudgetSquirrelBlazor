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

    protected override async Task OnInitializedAsync()
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

    private string PlannedIncome
    {
      get
      {
        if (this.context == null)
        {
          return "";
        }
        return $"{this.rootBudget.Budget.PlannedAmount.ToString("C")}";
      }
    }

    private void ChangePlannedIncome(string amount)
    {
      Console.WriteLine("Change income: " + amount);
    }
  }
}