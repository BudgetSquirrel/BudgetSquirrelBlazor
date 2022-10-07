using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Business.Funds;
using BudgetSquirrel.BudgetPlanning.Domain.BudgetPlanning;
using BudgetSquirrel.BudgetPlanning.Domain.Funds;
using BudgetSquirrel.BudgetPlanning.Domain.History;
using BudgetSquirrel.Common.AggregationUtils;

namespace BudgetSquirrel.BudgetPlanning.Business.BudgetPlanning
{
  public class GetAllFundsAndBudgetsQuery
  {
    private int timeBoxId;
    private int profileId;

    private IFundRepository fundRepository;
    private IBudgetRepository budgetRepository;

    public GetAllFundsAndBudgetsQuery(int timeBoxId, int profileId, IFundRepository fundRepository, IBudgetRepository budgetRepository)
    {
      this.timeBoxId = timeBoxId;
      this.profileId = profileId;
      this.fundRepository = fundRepository;
      this.budgetRepository = budgetRepository;
    }

    public async Task<FundsAndBudgetsQueryResult> Query()
    {
      FundSubFunds fundTree = await this.fundRepository.GetFundTree(this.profileId, this.timeBoxId);
      IEnumerable<FundBudget> fundBudgets = await TreeAggregationUtils.SelectAsync(
        fundTree,
        fund => fund.SubFunds,
        fund => FetchBudgetForFund(fund, this.timeBoxId));

      return new FundsAndBudgetsQueryResult(fundTree, fundBudgets);
    }

    private async Task<FundBudget> FetchBudgetForFund(FundSubFunds fund, int timeboxId)
    {
      Budget budget = await this.budgetRepository.GetBudget(fund.Fund.Id, this.timeBoxId);
      FundBudget fundBudgetRelationship = new FundBudget(budget, fund.Fund);
      return fundBudgetRelationship;
    }
  }

  public class FundsAndBudgetsQueryResult
  {
    public FundSubFunds FundTree;
    public IEnumerable<FundBudget> FundBudgets;

    public FundsAndBudgetsQueryResult(FundSubFunds fundTree, IEnumerable<FundBudget> fundBudgets)
    {
      this.FundTree = fundTree;
      this.FundBudgets = fundBudgets;
    }
  }
}