using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BudgetSquirrel.Frontend.Authentication.Registration
{
  public partial class RegisterPage : ComponentBase
  {
    private class FormModel
    {
      [Required(ErrorMessage = "Name is required")]
      public string FullName { get; set; }

      [Required(ErrorMessage = "Email is required")]
      [EmailAddress]
      public string Email { get; set; }

      [Required(ErrorMessage = "Password is required")]
      [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
      [MaxLength(32, ErrorMessage = "Password may not be greater than 32 characters")]
      [RegularExpression(
        @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=_-])(?=\S+$)([0-9]|[a-z]|[A-Z]|[!@#$%^&+=_-])+$",
        ErrorMessage = "Password must not contain spaces, must contain lowercase letters, uppercase letters, and at least one digit and special character (!@#$%^&+=_-)")]
      public string Password { get; set; }

      [Required(ErrorMessage = "Confirm Password is required")]
      [Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
      public string ConfirmPassword { get; set; }
    }

    [Inject]
    private IRegistrationService RegistrationService { get; set; }

    private FormModel Model { get; } = new FormModel();

    private bool GotError { get; set; }

    private bool IsConfirmPasswordInvalid { get; set; }

    private bool IsSubmitting { get; set; }

    private bool IsPasswordPlainText { get; set; }
    private bool IsConfirmPasswordPlainText { get; set; }

    private void TogglePasswordVisibility()
    {
      this.IsPasswordPlainText = !this.IsPasswordPlainText;
    }

    private void ToggleConfirmPasswordVisibility()
    {
      this.IsConfirmPasswordPlainText = !this.IsConfirmPasswordPlainText;
    }

    /// <summary>
    /// We have to do this manually because the ASP.NET Compare validation is bugged
    /// when the form is submitted.
    /// https://github.com/dotnet/aspnetcore/issues/10643
    /// </summary>
    private bool ValidateConfirmPassword()
    {
      if (this.Model.ConfirmPassword != this.Model.Password)
      {
        this.IsConfirmPasswordInvalid = true;
        return false;
      }
      return true;
    }

    private async Task OnRegisterClicked(EditContext editContext)
    {
      this.IsSubmitting = true;
      this.GotError = false;
      this.IsConfirmPasswordInvalid = false;
      if (!this.ValidateConfirmPassword())
      {
        return;
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