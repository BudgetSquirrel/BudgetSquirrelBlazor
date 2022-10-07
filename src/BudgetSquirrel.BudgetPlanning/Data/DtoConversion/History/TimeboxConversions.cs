using BudgetSquirrel.BudgetPlanning.Domain.History;
using BudgetSquirrel.Common.Data.Schema.History;

namespace BudgetSquirrel.BudgetPlanning.Data.DtoConversions.History
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