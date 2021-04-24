using System;
using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;

namespace BudgetSquirrel.Core.History
{
  /// <summary>
  /// Dto for <see cref="Timebox"/>
  /// </summary>
  public class TimeboxDto : IDto<Timebox>
  {
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Timebox ToDomain()
    {
      return new Timebox(this.StartDate, this.EndDate);
    }
  }
}