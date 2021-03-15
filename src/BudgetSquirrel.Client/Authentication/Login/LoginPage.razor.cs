using System;
using System.ComponentModel.DataAnnotations;
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

    private FormModel Model { get; set; } = new FormModel();

    private bool IsPasswordPlainText { get; set; }


    private void TogglePasswordVisibility()
    {
      this.IsPasswordPlainText = !this.IsPasswordPlainText;
    }

    private void OnLoginclicked()
    {
      Console.WriteLine(this.Model.Email + " - " + this.Model.Password);
    }
  }
}