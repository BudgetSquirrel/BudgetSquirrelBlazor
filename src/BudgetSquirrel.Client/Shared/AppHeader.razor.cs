using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Client.Shared
{
  public partial class AppHeader : ComponentBase
  {
    public bool ShouldShowAuthenticatePrompts { get; set; } = true;
  }
}