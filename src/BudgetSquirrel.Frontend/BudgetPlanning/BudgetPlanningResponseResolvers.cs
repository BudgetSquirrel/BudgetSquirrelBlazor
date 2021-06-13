using System.Linq;
using BudgetSquirrel.Web.Common.Messages.BudgetPlanning;
using static BudgetSquirrel.Frontend.BudgetPlanning.BudgetPlanningContext;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public static class BudgetPlanningResponseResolvers
  {
    public static BudgetPlanningContext ToFrontendDto(BudgetPlanningContextResponse contextResponse)
    {
      return new BudgetPlanningContext(
        ToFrontendDto(contextResponse.FundTree),
        contextResponse.Budgets.Select(b => ToFrontendDto(b)),
        new TimeboxDetails(contextResponse.Timebox.Id, contextResponse.Timebox.StartDate, contextResponse.Timebox.EndDate));
    }

    private static FundSubFunds ToFrontendDto(BudgetPlanningContextResponse.FundSubFunds fundSubFunds)
    {
      return new FundSubFunds(
        new Fund(
          fundSubFunds.Fund.Name,
          fundSubFunds.Fund.Balance,
          fundSubFunds.Fund.IsRoot,
          fundSubFunds.Fund.ProfileId,
          fundSubFunds.Fund.Id,
          fundSubFunds.Fund.ParentFundId),
        fundSubFunds.SubFunds.Select(fsf => ToFrontendDto(fsf)));
    }

    private static FundBudget ToFrontendDto(BudgetPlanningContextResponse.FundBudget fundBudget)
    {
      return new FundBudget(
        new Budget(fundBudget.Budget.PlannedAmount),
        fundBudget.FundId);
    }
  }
}