using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Client.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BudgetSquirrel.Client.Authentication.Registration
{
  public partial class RegisterPage : ComponentBase
  {
    public class FormModel
    {
      [Required(ErrorMessage = "Name is required")]
      public string FullName { get; set; }

      [Required(ErrorMessage = "Email is required")]
      [EmailAddress]
      public string Email { get; set; }

      [Required]
      [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
      [MaxLength(32, ErrorMessage = "Password may not be greater than 32 characters")]
      [RegularExpression(
        @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=_-])(?=\S+$)([0-9]|[a-z]|[A-Z]|[!@#$%^&+=_-])+$",
        ErrorMessage = "Password must not contain spaces, must contain lowercase letters, uppercase letters, and at least one digit and special character (!@#$%^&+=_-)")]
      public string Password { get; set; }

      [Required]
      [Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
      public string ConfirmPassword { get; set; }
    }

    [Inject]
    public IRegistrationService RegistrationService { get; set; }

    public FormModel Model { get; } = new FormModel();

    public bool GotError { get; private set; }

    public bool IsSubmitting { get; private set; }

    public bool IsPasswordPlainText { get; private set; }
    public bool IsConfirmPasswordPlainText { get; private set; }

    public void TogglePasswordVisibility()
    {
      this.IsPasswordPlainText = !this.IsPasswordPlainText;
    }

    public void ToggleConfirmPasswordVisibility()
    {
      this.IsConfirmPasswordPlainText = !this.IsConfirmPasswordPlainText;
    }

    public async Task OnRegisterClicked(EditContext editContext)
    {
      this.IsSubmitting = true;
      this.GotError = false;
      if (this.Model.ConfirmPassword == this.Model.Password)
      {
        this.GotError = true;
      }

      string[] nameParts = this.Model.FullName.Split(' ');
      string firstName = nameParts[0];
      string lastName = nameParts.Length > 1 ? nameParts[1] : "";
      try
      {
        await this.RegistrationService.Register(
          firstName,
          lastName,
          this.Model.Email,
          this.Model.Password,
          this.Model.ConfirmPassword);
      }
      catch
      {
        this.GotError = true;
      }
      this.IsSubmitting = false;
    }
  }
}