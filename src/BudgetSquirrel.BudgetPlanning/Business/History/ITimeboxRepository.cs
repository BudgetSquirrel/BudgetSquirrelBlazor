using System;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Domain.History;

namespace BudgetSquirrel.BudgetPlanning.Business.History
{
  public interface ITimeboxRepository
  {
    Task CreateTimebox(int profileId, DateTime startDate, DateTime endDate);
    Task<Timebox> GetTimebox(int timeboxId);

    /// <summary>
    /// Gets the timebox for the given profile where the given date is within
    /// the timebox's start and end date (inclusive).
    /// </summary>
    Task<Timebox> GetTimebox(int profileId, DateTime date);
  }
}