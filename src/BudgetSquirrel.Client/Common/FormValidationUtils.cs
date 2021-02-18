namespace BudgetSquirrel.Client.Common
{
  public enum ValidState
  {
    Invalid,
    Valid,
    Empty
  }

  public static class FormValidationUtils
  {
    public static ValidState GetBasicValidationState(string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return ValidState.Empty;
      }
      else
      {
        return ValidState.Valid;
      }
    }
  }
}