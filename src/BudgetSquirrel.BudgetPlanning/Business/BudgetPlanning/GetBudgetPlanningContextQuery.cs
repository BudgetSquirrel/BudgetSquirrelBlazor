using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Business.Funds;
using BudgetSquirrel.BudgetPlanning.Business.History;
using BudgetSquirrel.BudgetPlanning.Domain;
using BudgetSquirrel.BudgetPlanning.Domain.BudgetPlanning;
using BudgetSquirrel.BudgetPlanning.Domain.Funds;
using BudgetSquirrel.BudgetPlanning.Domain.History;

namespace BudgetSquirrel.BudgetPlanning.Business.BudgetPlanning
{
  public class GetBudgetPlanningContextQuery : IQuery<GetBudgetPlanningContextQuery.BudgetPlanningContext>
  {
    /// <summary>
    /// Return type for the <see cref="GetBudgetPlanningContextQuery"/> query.
    /// </summary>
    public struct BudgetPlanningContext
    {
      public Timebox Timebox { get; private set; }

      public Profile Profile { get; private set; }
      
      public FundSubFunds FundTree { get; private set; }

      public IEnumerable<FundBudget> Budgets { get; private set; }

      public BudgetPlanningContext(Profile profile, FundSubFunds fundTree, IEnumerable<FundBudget> budgets, Timebox timebox)
      {
        this.Profile = profile;
        this.FundTree = fundTree;
        this.Budgets = budgets;
        this.Timebox = timebox;
      }
    }

    private IFundRepository fundRepository;
    private IBudgetRepository budgetRepository;
    private ITimeboxRepository timeboxRepository;

    private int? timeBoxId;
    private int profileId;

    public GetBudgetPlanningContextQuery(
      IFundRepository fundRepository,
      IBudgetRepository budgetRepository,
      ITimeboxRepository timeboxRepository,
      int? timeBoxId,
      int profileId)
    {
      this.budgetRepository = budgetRepository;
      this.timeboxRepository = timeboxRepository;
      this.timeBoxId = timeBoxId;
      this.profileId = profileId;
      this.fundRepository = fundRepository;
    }

    public async Task<BudgetPlanningContext> Query()
    {
      Timebox timebox;
      if (this.timeBoxId.HasValue)
      {
        timebox = await this.timeboxRepository.GetTimebox(this.timeBoxId.Value);
      }
      else
      {
        timebox = await this.timeboxRepository.GetTimebox(this.profileId, DateTime.Now);
      }

      Profile profile = await this.fundRepository.GetProfile(this.profileId);

      GetAllFundsAndBudgetsQuery fundsAndBudgetsQuery = new GetAllFundsAndBudgetsQuery(
        timebox.Id,
        this.profileId,
        this.fundRepository,
        this.budgetRepository);
      FundsAndBudgetsQueryResult queryResult = await fundsAndBudgetsQuery.Query();

      return new BudgetPlanningContext(profile, queryResult.FundTree, queryResult.FundBudgets, timebox);
    }

    private async Task<IEnumerable<FundBudget>> GetBudgetsForFundTree(FundSubFunds fundBranch, Timebox timebox)
    {
      List<FundBudget> budgetsInTree = new List<FundBudget>();

      // Load the budget for the current fund in fundBranch.
      Task<FundBudget> rootLoadTask = this.GetBudgetForFund(fundBranch.Fund, timebox);

      // Load budgets for each sub fund of the current fund branch.
      List<Task<IEnumerable<FundBudget>>> childLoadTasks = new List<Task<IEnumerable<FundBudget>>>();
      foreach (FundSubFunds fundSubBranch in fundBranch.SubFunds)
      {
        childLoadTasks.Add(this.GetBudgetsForFundTree(fundSubBranch, timebox));
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

    private async Task<FundBudget> GetBudgetForFund(Fund fund, Timebox timebox)
    {
      Budget budget = await this.budgetRepository.GetBudget(fund.Id, timebox.Id);
      FundBudget fundBudgetRelationship = new FundBudget(budget, fund);
      return fundBudgetRelationship;
    }
  }
}