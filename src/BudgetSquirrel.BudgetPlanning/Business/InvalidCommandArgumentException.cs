using System;

namespace BudgetSquirrel.BudgetPlanning.Business
{
  public class InvalidCommandArgumentException : Exception
  {
    public InvalidCommandArgumentException(string message) : base(message)
    {
    }
  }
}