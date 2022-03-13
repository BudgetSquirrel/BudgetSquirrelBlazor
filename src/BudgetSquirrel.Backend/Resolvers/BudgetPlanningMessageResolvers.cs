using System;
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
      List<FundBudget> budgets = context.Budgets.Select(b => ToApiMessage(b)).ToList();
      return new BudgetPlanningContextResponse(
        new TimeboxDetails(context.Timebox.Id, context.Timebox.StartDate, context.Timebox.EndDate),
        new UserProfile(context.Profile.ProfileId),
        ToApiMessage(context.FundTree),
        budgets);
    }

    private static FundSubFunds ToApiMessage(BudgetSquirrel.Core.Funds.FundSubFunds fundSubFunds)
    {
      List<FundSubFunds> subFunds = fundSubFunds.SubFunds.Select(fsf => ToApiMessage(fsf)).ToList();
      return new FundSubFunds(
        new Fund(
          fundSubFunds.Fund.Id,
          fundSubFunds.Fund.Name,
          fundSubFunds.Fund.Balance,
          fundSubFunds.Fund.IsRoot,
          fundSubFunds.Fund.ProfileId,
          fundSubFunds.Fund.ParentFundId),
        subFunds);
    }

    private static FundBudget ToApiMessage(BudgetSquirrel.Core.BudgetPlanning.FundBudget fundBudget)
    {
      decimal plannedAmount = fundBudget.Budget.PlannedAmount;
      int fundId = fundBudget.Fund.Id;
      return new FundBudget(
        new Budget(
          plannedAmount),
        fundId);
    }
  }
}