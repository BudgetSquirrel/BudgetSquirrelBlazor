using System;

namespace BudgetSquirrel.BudgetTracking.Business
{
  public class InvalidCommandArgumentException : Exception
  {
    public InvalidCommandArgumentException(string message) : base(message)
    {
    }
  }
}