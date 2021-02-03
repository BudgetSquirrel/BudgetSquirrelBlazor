using System;

namespace BudgetSquirrel.Server.Biz
{
  public class InvalidCommandArgumentException : Exception
  {
    public InvalidCommandArgumentException(string message) : base(message)
    {
    }
  }
}