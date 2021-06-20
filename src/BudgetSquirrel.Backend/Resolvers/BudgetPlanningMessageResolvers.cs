using System.Collections.Generic;
using System.Linq;
using BudgetSquirrel.Backend.Biz.BudgetPlanning;
using BudgetSquirrel.Web.Common.Messages.BudgetPlanning;
using static BudgetSquirrel.Web.Common.Messages.BudgetPlanning.BudgetPlanningContextResponse;

namespace BudgetSquirrel.Backend.Resolvers
{
  public static class BudgetPlanningMessageResolvers
  {
    public static BudgetPlanningContextResponse ToApiMessage(GetBudgetPlanningContextQuery.BudgetPlanningContext context)
    {
      return new BudgetPlanningContextResponse(
        new TimeboxDetails(context.Timebox.Id, context.Timebox.StartDate, context.Timebox.EndDate),
        new UserProfile(context.Profile.ProfileId),
        ToApiMessage(context.FundTree),
        context.Budgets.Select(b => ToApiMessage(b)));
    }

    private static FundSubFunds ToApiMessage(BudgetSquirrel.Core.Funds.FundSubFunds fundSubFunds)
    {
      return new FundSubFunds(
        new Fund(
          fundSubFunds.Fund.Id,
          fundSubFunds.Fund.Name,
          fundSubFunds.Fund.Balance,
          fundSubFunds.Fund.IsRoot,
          fundSubFunds.Fund.ProfileId,
          fundSubFunds.Fund.ParentFundId),
        fundSubFunds.SubFunds.Select(fsf => ToApiMessage(fsf)));
    }

    private static FundBudget ToApiMessage(BudgetSquirrel.Core.BudgetPlanning.FundBudget fundBudget)
    {
      return new FundBudget(
        new Budget(
          fundBudget.Budget.PlannedAmount),
        fundBudget.Fund.Id);
    }
  }
}