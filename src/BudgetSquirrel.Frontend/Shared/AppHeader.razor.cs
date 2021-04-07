using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Frontend.Shared
{
  public partial class AppHeader : ComponentBase
  {
    public bool ShouldShowAuthenticatePrompts { get; set; } = true;
  }
}