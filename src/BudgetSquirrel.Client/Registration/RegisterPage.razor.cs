using System;
using BudgetSquirrel.Client.Common;
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
      }
    }

    public Form FormValues { get; } = new Form();
    
    public FormValidState FormValidStates => new FormValidState(this.FormValues);

    public bool CanSubmit => this.FormValidStates.IsCompleteAndValid;

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

    public void OnRegisterClicked()
    {
      Console.WriteLine($"FirstName: {this.FormValues.FullName}");
      Console.WriteLine($"Email: {this.FormValues.Email}");
      Console.WriteLine($"Password: {this.FormValues.Password}");
      Console.WriteLine($"ConfirmPassword: {this.FormValues.ConfirmPassword}");
    }
  }
}