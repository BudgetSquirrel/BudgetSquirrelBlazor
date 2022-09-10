using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Business.Funds;
using BudgetSquirrel.BudgetPlanning.Domain.BudgetPlanning;
using BudgetSquirrel.BudgetPlanning.Domain.Funds;
using BudgetSquirrel.BudgetPlanning.Domain.History;

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
      IEnumerable<FundBudget> fundBudgets = await this.GetBudgetsForFundTree(fundTree, this.timeBoxId);

      return new FundsAndBudgetsQueryResult(fundTree, fundBudgets);
    }

    private async Task<IEnumerable<FundBudget>> GetBudgetsForFundTree(FundSubFunds fundBranch, int timeboxId)
    {
      List<FundBudget> budgetsInTree = new List<FundBudget>();

      // Load the budget for the current fund in fundBranch.
      Task<FundBudget> rootLoadTask = this.GetBudgetForFund(fundBranch.Fund, timeBoxId);

      // Load budgets for each sub fund of the current fund branch.
      List<Task<IEnumerable<FundBudget>>> childLoadTasks = new List<Task<IEnumerable<FundBudget>>>();
      foreach (FundSubFunds fundSubBranch in fundBranch.SubFunds)
      {
        childLoadTasks.Add(this.GetBudgetsForFundTree(fundSubBranch, timeboxId));
      }

      IEnumerable<Task> allLoads = childLoadTasks.Select(t => (Task)t)
        .Append(rootLoadTask)
        .ToList();
      await Task.WhenAll(allLoads);

      budgetsInTree.Add(await rootLoadTask);
      foreach (Task<IEnumerable<FundBudget>> childLoad in childLoadTasks)
      {
        budgetsInTree.AddRange(await childLoad);
      }

      return budgetsInTree;
    }

    private async Task<FundBudget> GetBudgetForFund(Fund fund, int timeboxId)
    {
      Budget budget = await this.budgetRepository.GetBudget(fund.Id, this.timeBoxId);
      FundBudget fundBudgetRelationship = new FundBudget(budget, fund);
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