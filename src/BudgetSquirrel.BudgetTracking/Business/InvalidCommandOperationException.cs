using System;

namespace BudgetSquirrel.BudgetTracking.Business
{
  public class InvalidCommandOperationException : Exception
  {
    public InvalidCommandOperationException(string message) : base(message)
    {
    }
  }
}