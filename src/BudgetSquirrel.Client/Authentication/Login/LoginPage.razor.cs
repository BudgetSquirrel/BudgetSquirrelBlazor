using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Client.Authentication.Login
{
  public partial class LoginPage : ComponentBase
  {
    private class FormModel
    {
      [Required]
      public string Email { get; set; }

      [Required]
      public string Password { get; set; }
    }

    [Inject]
    private ILoginService loginService { get; set; }

    private FormModel Model { get; set; } = new FormModel();

    private bool IsLoginIncorrect { get; set; }

    private bool IsPasswordPlainText { get; set; }


    private void TogglePasswordVisibility()
    {
      this.IsPasswordPlainText = !this.IsPasswordPlainText;
    }

    private Task OnLoginclicked()
    {
      this.IsLoginIncorrect = true;
      try
      {
        return this.loginService.Login(this.Model.Email, this.Model.Password);
      }
      catch (Exception)
      {
        this.IsLoginIncorrect = true;
        return Task.CompletedTask;
      }
    }
  }
}