using System;

namespace BudgetSquirrel.Backend.Biz
{
  public class InvalidCommandOperationException : Exception
  {
    public InvalidCommandOperationException(string message) : base(message)
    {
    }
  }
}