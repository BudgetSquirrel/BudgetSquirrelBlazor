using System;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetTracking.Domain.History;

namespace BudgetSquirrel.BudgetTracking.Business.Ports
{
  public interface ITimeboxRepository
  {
    Task<Timebox> GetTimebox(int timeboxId);

    /// <summary>
    /// Gets the timebox for the given profile where the given date is within
    /// the timebox's start and end date (inclusive).
    /// </summary>
    Task<Timebox> GetTimebox(int profileId, DateTime date);
  }
}