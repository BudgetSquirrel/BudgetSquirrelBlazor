using System;
using BudgetSquirrel.Common.Data.Infrastructure;

namespace BudgetSquirrel.Common.Data.Schema.History
{
  /// <summary>
  /// Dto for <see cref="Timebox"/>
  /// </summary>
  public class TimeboxDto
  {
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
  }
}