using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Common.Data.Infrastructure;
using BudgetSquirrel.BudgetTracking.Domain.Funds;
using BudgetSquirrel.BudgetTracking.Business.Ports;
using Dapper;
using BudgetSquirrel.Common.Data.Schema;
using BudgetSquirrel.Common.Data.Schema.Funds;
using BudgetSquirrel.BudgetTracking.Data.Funds;

namespace BudgetSquirrel.BudgetPlanning.Data.Funds
{
  public class FundRepository : IFundRepository
  {
    private DbConnectionProvider dbConnectionProvider;

    public FundRepository(DbConnectionProvider dbConnectionProvider)
    {
      this.dbConnectionProvider = dbConnectionProvider;
    }

    public async Task<Profile> GetProfile(int profileId)
    {
      ProfileDto profile;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        profile = await conn.QuerySingleAsync<ProfileDto>(
          $"EXEC {StoredProcedures.Funds.GetProfile} @ProfileId",
          new
          {
            ProfileId = profileId
          }
        );
      }
      return ProfileConversions.ToDomain(profile);
    }

    public async Task<FundSubFunds> GetFundTree(int profileId, int timeboxId)
    {
      IEnumerable<FundDto> flatFundTree;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        flatFundTree = await conn.QueryAsync<FundDto>(
          $"EXEC {StoredProcedures.Funds.GetAllFundsInFundTree} @ProfileId, @TimeboxId",
          new
          {
            ProfileId = profileId,
            TimeboxId = timeboxId
          }
        );
      }

      Fund rootFund = FundConversions.ToDomain(flatFundTree.Single(f => f.IsRoot));
      FundSubFunds rootFundNode = this.BuildFundTree(rootFund, flatFundTree.Select(f => FundConversions.ToDomain(f)));

      return rootFundNode;
    }

    public async Task<Fund> GetFundById(int fundId)
    {
      FundDto fundDto;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        fundDto = await conn.QuerySingleAsync<FundDto>(
          $"EXEC {StoredProcedures.Funds.GetFundById} @FundId",
          new
          {
            FundId = fundId
          }
        );
      }
      return FundConversions.ToDomain(fundDto);
    }

    public async Task<Fund> GetRootFundForProfile(int profileId)
    {
      FundDto fundDto;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        fundDto = await conn.QuerySingleAsync<FundDto>(
          $"EXEC {StoredProcedures.Funds.GetRootFundForProfile} @ProfileId",
          new
          {
            ProfileId = profileId
          }
        );
      }
      return FundConversions.ToDomain(fundDto);
    }

    public async Task UpdateFund(int fundId, FundDetails fundDetails)
    {
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        await conn.ExecuteAsync(
          $"EXEC {StoredProcedures.Funds.UpdateFundDetails} @FundId, @Name",
          new
          {
            FundId = fundId,
            Name = fundDetails.Name
          }
        );
      }
    }

    private FundSubFunds BuildFundTree(Fund rootFund, IEnumerable<Fund> allFunds)
    {
      List<FundSubFunds> subFundNodes = new List<FundSubFunds>();
      IEnumerable<Fund> subFunds = allFunds.Where(f => f.ParentFundId == rootFund.Id);
      foreach (Fund subFund in subFunds)
      {
        FundSubFunds subFundNode = this.BuildFundTree(subFund, allFunds);
        subFundNodes.Add(subFundNode);
      }

      FundSubFunds fundSubFunds = new FundSubFunds(rootFund, subFundNodes);

      return fundSubFunds;
    }
  }
}