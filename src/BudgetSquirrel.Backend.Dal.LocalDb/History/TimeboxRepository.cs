using System;
using System.Data;
using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.History;
using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;
using BudgetSquirrel.Backend.Dal.LocalDb.Schema;
using BudgetSquirrel.Core.History;
using Dapper;

namespace BudgetSquirrel.Backend.Dal.LocalDb.History
{
  public class TimeboxRepository : ITimeboxRepository
  {
    private DbConnectionProvider dbConnectionProvider;

    public TimeboxRepository(DbConnectionProvider dbConnectionProvider)
    {
      this.dbConnectionProvider = dbConnectionProvider;
    }

    public async Task CreateTimebox(int profileId, DateTime startDate, DateTime endDate)
    {
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        await conn.ExecuteAsync(
          $"EXEC {StoredProcedures.History.CreateTimebox} @ProfileId, @StartDate, @EndDate",
          new
          {
            ProfileId = profileId,
            StartDate = startDate,
            EndDate = endDate
          });
      }
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
      return timebox.ToDomain();
    }

    public async Task<Timebox> GetTimebox(int profileId, DateTime startDate)
    {
      TimeboxDto timebox;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        timebox = await conn.QuerySingleAsync<TimeboxDto>(
          $"EXEC {StoredProcedures.History.GetTimeboxByStartDate} @ProfileId, @StartDate",
          new
          {
            ProfileId = profileId,
            StartDate = startDate
          }
        );
      }
      return timebox.ToDomain();
    }
  }
}