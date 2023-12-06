using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetTracking.Business.Ports;
using BudgetSquirrel.BudgetTracking.Domain.BudgetPlanning;
using BudgetSquirrel.BudgetTracking.Domain.BudgetTracking;
using BudgetSquirrel.BudgetTracking.Domain.Funds;
using BudgetSquirrel.BudgetTracking.Domain.History;
using BudgetSquirrel.Common.AggregationUtils;

namespace BudgetSquirrel.BudgetTracking.Business.Queries
{
  public class GetAllFundsAndBudgetsQuery
  {
    private Timebox timebox;
    private int profileId;

    private IFundRepository fundRepository;
    private IBudgetRepository budgetRepository;
    private ITransactionRepository transactionRepository;

    public GetAllFundsAndBudgetsQuery(
      Timebox timebox,
      int profileId,
      IFundRepository fundRepository,
      IBudgetRepository budgetRepository,
      ITransactionRepository transactionRepository)
    {
      this.timebox = timebox;
      this.profileId = profileId;
      this.fundRepository = fundRepository;
      this.budgetRepository = budgetRepository;
      this.transactionRepository = transactionRepository;
    }

    public async Task<FundsAndBudgetsQueryResult> Query()
    {
      FundSubFunds fundTree = await this.fundRepository.GetFundTree(this.profileId, this.timebox.Id);
      IEnumerable<FundRelationships> fundRelationships = await TreeAggregationUtils.SelectAsync(
        fundTree,
        fund => fund.SubFunds,
        fund => FetchRelatedEntitiesForFund(fund, this.timebox));

      return new FundsAndBudgetsQueryResult(fundTree, fundRelationships);
    }

    private async Task<FundRelationships> FetchRelatedEntitiesForFund(FundSubFunds fund, Timebox timebox)
    {
      Budget budget = await this.budgetRepository.GetBudget(fund.Fund.Id, this.timebox.Id);
      IEnumerable<Transaction> transactions = await this.transactionRepository.GetTransactionsInDates(fund.Fund.Id, timebox.StartDate, timebox.EndDate);
      FundRelationships fundBudgetRelationship = new FundRelationships(budget, fund.Fund, transactions);
      return fundBudgetRelationship;
    }
  }

  public class FundsAndBudgetsQueryResult
  {
    public FundSubFunds FundTree;
    public IEnumerable<FundRelationships> FundRelationships;

    public FundsAndBudgetsQueryResult(FundSubFunds fundTree, IEnumerable<FundRelationships> fundRelationships)
    {
      this.FundTree = fundTree;
      this.FundRelationships = fundRelationships;
    }
  }
}