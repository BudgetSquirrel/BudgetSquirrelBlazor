using System;
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

    public BudgetPlanningContext Context;

    protected override async Task OnInitializedAsync()
    {
      await this.loginService.PromptLoginIfNecessary();
      this.Context = await this.budgetPlanningService.GetBudgetTree();
      Console.WriteLine(this.Context);
    }
  }
}