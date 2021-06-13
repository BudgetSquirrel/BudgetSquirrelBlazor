using System.Collections.Generic;
using System.Linq;
using BudgetSquirrel.Web.Common.Messages.BudgetPlanning;
using static BudgetSquirrel.Frontend.BudgetPlanning.BudgetPlanningContext;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public static class BudgetPlanningResponseResolvers
  {
    public static BudgetPlanningContext ToFrontendDto(BudgetPlanningContextResponse contextResponse)
    {
      IEnumerable<FundBudget> fundBudgets = contextResponse.Budgets.Select(b => ToFrontendDto(b));
      return new BudgetPlanningContext(
        ToFrontendDto(contextResponse.FundTree, fundBudgets),
        new TimeboxDetails(contextResponse.Timebox.Id, contextResponse.Timebox.StartDate, contextResponse.Timebox.EndDate));
    }

    private static FundRelationships ToFrontendDto(
      BudgetPlanningContextResponse.FundSubFunds fundSubFunds,
      IEnumerable<FundBudget> fundBudgets)
    {
      BudgetPlanningContextResponse.Fund fund = fundSubFunds.Fund;
      Budget budget = fundBudgets.Single(fb => fb.FundId == fundSubFunds.Fund.Id).Budget;

      return new FundRelationships(
        new Fund(
          fund.Name,
          fund.Balance,
          fund.IsRoot,
          fund.ProfileId,
          fund.Id,
          fund.ParentFundId),
        budget,
        fundSubFunds.SubFunds.Select(fsf => ToFrontendDto(fsf, fundBudgets)));
    }

    private static FundBudget ToFrontendDto(BudgetPlanningContextResponse.FundBudget fundBudget)
    {
      return new FundBudget(
        new Budget(fundBudget.Budget.PlannedAmount),
        fundBudget.FundId);
    }
  }
}