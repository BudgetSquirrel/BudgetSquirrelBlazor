using System;

namespace BudgetSquirrel.Server.Biz
{
  public class InvalidCommandOperationException : Exception
  {
    public InvalidCommandOperationException(string message) : base(message)
    {
    }
  }
}