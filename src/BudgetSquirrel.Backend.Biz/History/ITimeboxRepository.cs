using System;
using System.Threading.Tasks;
using BudgetSquirrel.Core.History;

namespace BudgetSquirrel.Backend.Biz.History
{
  public interface ITimeboxRepository
  {
    Task CreateTimebox(int profileId, DateTime startDate, DateTime endDate);
    Task<Timebox> GetTimebox(int timeboxId);
    Task<Timebox> GetTimebox(int profileId, DateTime startDate);
  }
}