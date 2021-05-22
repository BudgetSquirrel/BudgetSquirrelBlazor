using System.Threading.Tasks;
using BudgetSquirrel.Frontend.Authentication.Login;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public partial class BudgetPlanner : ComponentBase
  {
    [Inject]
    private ILoginService loginService { get; set; }

    protected override async Task OnInitializedAsync()
    {
      await this.loginService.PromptLoginIfNecessary();
    }
  }
}