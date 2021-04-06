using System;

namespace BudgetSquirrel.Backend.Biz
{
  public class InvalidCommandArgumentException : Exception
  {
    public InvalidCommandArgumentException(string message) : base(message)
    {
    }
  }
}