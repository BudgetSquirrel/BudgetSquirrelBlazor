using System;
using System.Threading.Tasks;

namespace BudgetSquirrel.Core.History
{
  public interface ITimeboxRepository
  {
    Task CreateTimebox(int fundRootId, DateTime startDate, DateTime endDate);
    Task<Timebox> GetTimebox(int timeboxId);
  }
}