using System;
using System.Data;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetTracking.Business.Ports;
using BudgetSquirrel.Common.Data.Infrastructure;
using BudgetSquirrel.Common.Data.Schema;
using BudgetSquirrel.BudgetTracking.Domain.History;
using BudgetSquirrel.Common.Data.Schema.History;
using Dapper;
using BudgetSquirrel.BudgetTracking.Data.History;

namespace BudgetSquirrel.BudgetPlanning.Data.History
{
  public class TimeboxRepository : ITimeboxRepository
  {
    private DbConnectionProvider dbConnectionProvider;

    public TimeboxRepository(DbConnectionProvider dbConnectionProvider)
    {
      this.dbConnectionProvider = dbConnectionProvider;
    }

    public async Task<Timebox> GetTimebox(int timeboxId)
    {
      TimeboxDto timebox;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        timebox = await conn.QuerySingleAsync<TimeboxDto>(
          $"EXEC {StoredProcedures.History.GetTimebox} @TimeboxId",
          new
          {
            TimeboxId = timeboxId
          }
        );
      }
      return TimeboxConversions.ToDomain(timebox);
    }

    public async Task<Timebox> GetTimebox(int profileId, DateTime date)
    {
      TimeboxDto timebox;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        timebox = await conn.QuerySingleAsync<TimeboxDto>(
          $"EXEC {StoredProcedures.History.GetTimeboxByDate} @ProfileId, @Date",
          new
          {
            ProfileId = profileId,
            Date = date
          }
        );
      }
      return TimeboxConversions.ToDomain(timebox);
    }

    public async Task<Timebox> GetLastTimebox(int profileId)
    {
      TimeboxDto timebox;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        timebox = await conn.QuerySingleAsync<TimeboxDto>(
          $"EXEC {StoredProcedures.History.GetLastTimebox} @ProfileId",
          new
          {
            ProfileId = profileId,
          }
        );
      }
      return TimeboxConversions.ToDomain(timebox);
    }
  }
}