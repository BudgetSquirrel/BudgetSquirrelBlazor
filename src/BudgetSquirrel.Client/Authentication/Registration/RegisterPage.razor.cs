using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Client.Common;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Client.Authentication.Registration
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

    public class FormValidState
    {
      public ValidState IsFullNameValid { get; set; } = ValidState.Empty;
      public ValidState IsEmailValid { get; set; } = ValidState.Empty;
      public ValidState IsPasswordValid { get; set; } = ValidState.Empty;
      public ValidState IsConfirmPasswordValid { get; set; } = ValidState.Empty;

      public bool IsCompleteAndValid => this.IsFullNameValid == ValidState.Valid &&
                                        this.IsEmailValid == ValidState.Valid &&
                                        this.IsPasswordValid == ValidState.Valid &&
                                        this.IsConfirmPasswordValid == ValidState.Valid;

      public FormValidState(Form form)
      {
        this.IsFullNameValid = FormValidationUtils.GetBasicValidationState(form.FullName);
        this.IsEmailValid = FormValidationUtils.GetBasicValidationState(form.Email);
        this.IsPasswordValid = FormValidationUtils.GetBasicValidationState(form.Password);
        this.IsConfirmPasswordValid = FormValidationUtils.GetBasicValidationState(form.ConfirmPassword);

        if (form.Password != null &&
            form.ConfirmPassword != null &&
            form.ConfirmPassword != form.Password)
        {
          this.IsConfirmPasswordValid = ValidState.Invalid;
        }

        if (this.IsEmailValid != ValidState.Empty)
        {
          // Must contain one and only one '@' and it must not be the first or last character.
          bool isInvalidEmailFormat = !form.Email.Contains("@") ||
                                      form.Email.Where(c => c == '@').Count() != 1 ||
                                      form.Email.EndsWith("@") ||
                                      form.Email.StartsWith("@");
          if (isInvalidEmailFormat)
          {
            this.IsEmailValid = ValidState.Invalid;
          }
        }
      }
    }

    [Inject]
    public IRegistrationService RegistrationService { get; set; }

    public Form FormValues { get; } = new Form();
    
    public FormValidState FormValidStates => new FormValidState(this.FormValues);

    public bool CanSubmit => this.FormValidStates.IsCompleteAndValid;

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

    public async Task OnRegisterClicked()
    {
      if (!this.FormValidStates.IsCompleteAndValid)
      {
        return;
      }
     
      this.GotError = false;
      this.IsSubmitting = true; 

      string[] nameParts = this.FormValues.FullName.Split(' ');
      string firstName = nameParts[0];
      string lastName = nameParts.Length > 1 ? nameParts[1] : "";
      try
      {
        await this.RegistrationService.Register(
          firstName,
          lastName,
          this.FormValues.Email,
          this.FormValues.Password,
          this.FormValues.ConfirmPassword);
      }
      catch
      {
        this.GotError = true;
      }
      this.IsSubmitting = false;
    }
  }
}