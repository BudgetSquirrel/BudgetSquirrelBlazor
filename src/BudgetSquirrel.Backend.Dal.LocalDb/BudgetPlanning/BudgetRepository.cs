using System;
using System.Data;
using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.BudgetPlanning;
using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;
using BudgetSquirrel.Core.BudgetPlanning;
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

    public async Task CreateBudgetForFund(int fundId, decimal plannedAmount, int timeboxId)
    {
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        await conn.ExecuteAsync(
          $"EXEC {BudgetPlanningProcedures.CreateBudgetForFund} @FundId, @PlannedAmount, @TimeboxId",
          new
          {
            FundId = fundId,
            PlannedAmount = plannedAmount,
            TimeboxId = timeboxId
          });
      }
    }

    public async Task<int> CreateFund(int profileId, int? parentFundId, string name, bool isRoot)
    {
      int fundId;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        fundId = await conn.ExecuteScalarAsync<int>(
          $"EXEC {BudgetPlanningProcedures.CreateFund} @ProfileId, @ParentFundId, @Name, @IsRoot",
          new
          {
            ProfileId = profileId,
            ParentFundId = parentFundId,
            Name = name,
            IsRoot = isRoot ? 1 : 0
          });
      }
      return fundId;
    }

    public async Task<int> CreateProfileForUser(string userEmail)
    {
      int profileId;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        profileId = await conn.ExecuteScalarAsync<int>(
          $"EXEC {BudgetPlanningProcedures.CreateProfileForUser} @Email",
          new
          {
            Email = userEmail
          });
      }
      return profileId;
    }

    public async Task<Budget> GetBudget(int fundId)
    {
      BudgetDto budget;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        budget = await conn.QuerySingleAsync<BudgetDto>(
          $"EXEC {BudgetPlanningProcedures.GetBudgetForFund} @FundId",
          new
          {
            FundId = fundId
          }
        );
      }
      return budget.ToDomain();
    }
  }
}