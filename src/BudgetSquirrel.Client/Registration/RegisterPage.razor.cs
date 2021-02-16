using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Client.Registration
{
  public partial class RegisterPage : ComponentBase
  {
    public class Form
    {
      public string FullName { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
      public string ConfirmPassword { get; set; }
    }

    public Form FormValues = new Form();
  }
}