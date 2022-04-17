using System;
using System.Data;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Business.History;
using BudgetSquirrel.BudgetPlanning.Data.Infrastructure;
using BudgetSquirrel.BudgetPlanning.Data.Schema;
using BudgetSquirrel.BudgetPlanning.Domain.History;
using Dapper;

namespace BudgetSquirrel.BudgetPlanning.Data.History
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
      return timebox.ToDomain();
    }
  }
}