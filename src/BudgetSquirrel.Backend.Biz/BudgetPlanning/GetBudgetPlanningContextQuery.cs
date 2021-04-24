using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.Funds;
using BudgetSquirrel.Backend.Biz.History;
using BudgetSquirrel.Core;
using BudgetSquirrel.Core.BudgetPlanning;
using BudgetSquirrel.Core.Funds;
using BudgetSquirrel.Core.History;

namespace BudgetSquirrel.Backend.Biz.BudgetPlanning
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

    private int timeBoxId;
    private int profileId;

    public GetBudgetPlanningContextQuery(
      IFundRepository fundRepository,
      IBudgetRepository budgetRepository,
      ITimeboxRepository timeboxRepository,
      int timeBoxId,
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
      Timebox timebox = await this.timeboxRepository.GetTimebox(this.timeBoxId);
      Profile profile = await this.fundRepository.GetProfile(this.profileId);
      FundSubFunds fundTree = await this.fundRepository.GetFundTree(this.profileId);
      IEnumerable<FundBudget> fundBudgets = await this.GetBudgetsForFundTree(fundTree);

      return new BudgetPlanningContext(profile, fundTree, fundBudgets, timebox);
    }

    private async Task<IEnumerable<FundBudget>> GetBudgetsForFundTree(FundSubFunds fundBranch)
    {
      IEnumerable<FundBudget> budgetsInTree = new List<FundBudget>();

      List<Task> budgetLoadTasks = new List<Task>();

      // Load the budget for the current fund in fundBranch.
      budgetLoadTasks.Add(Task.Run(async () =>
      {
        budgetsInTree = budgetsInTree.Append(await this.GetBudgetForFund(fundBranch.Fund));
      }));

      // Load budgets for each sub fund of the current fund branch.
      foreach (FundSubFunds fundSubBranch in fundBranch.SubFunds)
      {
        budgetLoadTasks.Add(Task.Run(async () =>
        {
          budgetsInTree = budgetsInTree.Concat(await this.GetBudgetsForFundTree(fundSubBranch));
        }));
      }

      await Task.WhenAll(budgetLoadTasks);

      return budgetsInTree;
    }

    private async Task<FundBudget> GetBudgetForFund(Fund fund)
    {
      Budget budget = await this.budgetRepository.GetBudget(fund.Id);
      FundBudget fundBudgetRelationship = new FundBudget(budget, fund);
      return fundBudgetRelationship;
    }
  }
}