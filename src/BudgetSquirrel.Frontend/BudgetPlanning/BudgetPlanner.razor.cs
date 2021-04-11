using BudgetSquirrel.Frontend.Authentication.Login;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public partial class BudgetPlanner : ComponentBase
  {
    [Inject]
    private ILoginService loginService { get; set; }

    protected override void OnInitialized()
    {
      this.loginService.PromptLoginIfNecessary();
    }
  }
}