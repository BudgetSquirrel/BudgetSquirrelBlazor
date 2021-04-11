using System;
using System.Data;
using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.BudgetPlanning;
using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;
using Dapper;
using BudgetPlanningProcedures = BudgetSquirrel.Backend.Dal.LocalDb.Schema.StoredProcedures.BudgetPlanning;

namespace BudgetSquirrel.Backend.Dal.LocalDb
{
  public class BudgetRepository : IBudgetRepository
  {
    private DbConnectionProvider dbConnectionProvider;

    public BudgetRepository(DbConnectionProvider dbConnectionProvider)
    {
      this.dbConnectionProvider = dbConnectionProvider;
    }

    public async Task CreateBudgetForFund(int fundId, decimal plannedAmount)
    {
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        await conn.ExecuteAsync(
          $"EXEC {BudgetPlanningProcedures.CreateBudgetForFund} @FundId, @PlannedAmount",
          new
          {
            FundId = fundId,
            PlannedAmount = plannedAmount
          });
      }
    }

    public async Task<int> CreateFund(int fundRootId, int? parentFundId, string name, bool isRoot)
    {
      int fundId;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        fundId = await conn.ExecuteScalarAsync<int>(
          $"EXEC {BudgetPlanningProcedures.CreateFund} @FundRootId, @ParentFundId, @Name, @IsRoot",
          new
          {
            FundRootId = fundRootId,
            ParentFundId = parentFundId,
            Name = name,
            IsRoot = isRoot
          });
      }
      return fundId;
    }

    public async Task<int> CreateFundRootForUser(string userEmail)
    {
      int fundRootId;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        fundRootId = await conn.ExecuteScalarAsync<int>(
          $"EXEC {BudgetPlanningProcedures.CreateFundRootForUser} @Email",
          new
          {
            Email = userEmail
          });
      }
      return fundRootId;
    }

    public async Task CreateTimebox(int fundRootId, DateTime startDate, DateTime endDate)
    {
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        await conn.ExecuteAsync(
          $"EXEC {BudgetPlanningProcedures.CreateTimebox} @FundRootId, @StartDate, @EndDate",
          new
          {
            FundRootId = fundRootId,
            StartDate = startDate,
            EndDate = endDate
          });
      }
    }
  }
}