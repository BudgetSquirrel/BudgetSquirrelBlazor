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
using System;
using BudgetSquirrel.BudgetTracking.Domain.BudgetTracking;

namespace BudgetSquirrel.BudgetPlanning.Data.Funds
{
  public class FundRepository : IFundRepository
  {
    private DbConnectionProvider dbConnectionProvider;
    private ITransactionRepository transactionRepository;

    public FundRepository(DbConnectionProvider dbConnectionProvider, ITransactionRepository transactionRepository)
    {
      this.dbConnectionProvider = dbConnectionProvider;
      this.transactionRepository = transactionRepository;
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
          $"EXEC {StoredProcedures.Funds.GetAllFundsInFundTreeWithBudget} @ProfileId, @TimeboxId",
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

    private async Task<FundSubFunds> GetFundTreeFromFundId(int profileId)
    {
      IEnumerable<FundDto> flatFundTree;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        flatFundTree = await conn.QueryAsync<FundDto>(
          $"EXEC {StoredProcedures.Funds.GetAllFundsInFundTreeWithBudget} @ProfileId",
          new
          {
            ProfileId = profileId,
          }
        );
      }

      Fund rootFund = FundConversions.ToDomain(flatFundTree.Single(f => f.IsRoot));
      FundSubFunds rootFundNode = this.BuildFundTree(rootFund, flatFundTree.Select(f => FundConversions.ToDomain(f)));

      return rootFundNode;
    }

    private async Task<decimal> GetFundBalance(int profileId, DateTime startDate, DateTime endDate)
    {
      FundSubFunds rootFundNode = await this.GetFundTreeFromFundId(profileId);

      decimal balance = await this.GetFundBalance(rootFundNode, startDate, endDate);
      return balance;
    }

    private async Task<decimal> GetFundBalance(FundSubFunds fund, DateTime startDate, DateTime endDate)
    {
      decimal balance;
      if (fund.SubFunds.Any())
      {
        balance = await this.GetFundCategoryBalance(fund, startDate, endDate);
      }
      else
      {
        balance = await this.GetFundLeafBalance(fund, startDate, endDate);
      }

      return balance;
    }

    private async Task<decimal> GetFundLeafBalance(FundSubFunds fund, DateTime startDate, DateTime endDate)
    {
      IEnumerable<Transaction> transactions = await this.transactionRepository.GetTransactionsInDates(fund.Fund.Id, startDate, endDate);
      return transactions.Sum(t => t.Amount);
    }

    private async Task<decimal> GetFundCategoryBalance(FundSubFunds fund, DateTime startDate, DateTime endDate)
    {
      List<Task<decimal>> syncTasks = new List<Task<decimal>>();

      foreach (FundSubFunds subFund in fund.SubFunds)
      {
        Task<decimal> syncTask = this.GetFundBalance(subFund, startDate, endDate);
        syncTasks.Add(syncTask);
      }

      IEnumerable<decimal> balancesOfSubFunds = await Task.WhenAll(syncTasks);
      return balancesOfSubFunds.Sum();
    }
  }
}