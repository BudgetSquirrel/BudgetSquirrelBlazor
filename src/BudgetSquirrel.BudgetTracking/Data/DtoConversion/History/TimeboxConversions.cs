using BudgetSquirrel.BudgetTracking.Domain.History;
using BudgetSquirrel.Common.Data.Schema.History;

namespace BudgetSquirrel.BudgetTracking.Data.History
{
  /// <summary>
  /// Conversion functions for timeboxes
  /// </summary>
  public static class TimeboxConversions
  {
    public static Timebox ToDomain(TimeboxDto timeboxDto)
    {
      return new Timebox(timeboxDto.Id, timeboxDto.StartDate, timeboxDto.EndDate);
    }
  }
}