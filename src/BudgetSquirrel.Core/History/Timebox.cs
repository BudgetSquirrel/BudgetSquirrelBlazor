using System;

namespace BudgetSquirrel.Core.History
{
  /// <summary>
  /// Represents the amount of time in each period that the user budgets for.
  /// For example, if the use decides they want to budget on a monthly basis,
  /// this will contain the start date and end date of a specific month for
  /// which the user was actively using BudgetSquirrel. For another example,
  /// if the use wanted to budget on a yearly bases, this would contain the
  /// start and end date of a specific year in which they were using BudgetSquirrel.
  /// </summary>
  public class Timebox
  {
    public Timebox(int id, DateTime startDate, DateTime endDate)
    {
      this.Id = id;
      this.StartDate = startDate;
      this.EndDate = endDate;
    }

    public int Id { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }
  }
}