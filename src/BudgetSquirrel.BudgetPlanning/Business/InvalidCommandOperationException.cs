using System;

namespace BudgetSquirrel.BudgetPlanning.Business
{
  public class InvalidCommandOperationException : Exception
  {
    public InvalidCommandOperationException(string message) : base(message)
    {
    }
  }
}