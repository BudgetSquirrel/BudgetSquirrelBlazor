using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Frontend.Authentication.Login
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

    [Inject]
    NavigationManager navigationManager { get; set; }

    private FormModel Model { get; set; } = new FormModel();

    private bool IsLoginIncorrect { get; set; }

    private bool IsPasswordPlainText { get; set; }


    private void TogglePasswordVisibility()
    {
      this.IsPasswordPlainText = !this.IsPasswordPlainText;
    }

    private async Task OnLoginclicked()
    {
      this.IsLoginIncorrect = false;
      try
      {
        await this.loginService.Login(this.Model.Email, this.Model.Password);
        this.navigationManager.NavigateTo("/budget-planner");
      }
      catch (Exception)
      {
        this.IsLoginIncorrect = true;
      }
    }
  }
}