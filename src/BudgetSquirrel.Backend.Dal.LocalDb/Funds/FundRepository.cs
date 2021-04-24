using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.Funds;
using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;
using BudgetSquirrel.Backend.Dal.LocalDb.Schema;
using BudgetSquirrel.Core.Funds;
using Dapper;

namespace BudgetSquirrel.Backend.Dal.LocalDb.Funds
{
  public class FundRepository : IFundRepository
  {
    private DbConnectionProvider dbConnectionProvider;

    public FundRepository(DbConnectionProvider dbConnectionProvider)
    {
      this.dbConnectionProvider = dbConnectionProvider;
    }

    public async Task<FundRoot> GetFundRoot(int fundRootId)
    {
      FundRootDto fundRoot;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        fundRoot = await conn.QuerySingleAsync<FundRootDto>(
          $"EXEC {StoredProcedures.Funds.GetFundRoot} @FundRootId",
          new
          {
            FundRootId = fundRootId
          }
        );
      }
      return fundRoot.ToDomain();
    }

    public async Task<FundSubFunds> GetFundTree(int fundRootId)
    {
      IEnumerable<FundDto> flatFundTree;
      using (IDbConnection conn = this.dbConnectionProvider.GetConnection())
      {
        flatFundTree = await conn.QueryAsync<FundDto>(
          $"EXEC {StoredProcedures.Funds.GetAllFundsInFundTree} @FundRootId",
          new
          {
            FundRootId = fundRootId
          }
        );
      }

      Fund rootFund = flatFundTree.Single(f => f.IsRoot).ToDomain();
      FundSubFunds rootFundNode = this.BuildFundTree(rootFund, flatFundTree.Select(f => f.ToDomain()));

      return rootFundNode;
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