using System;
using System.Collections.Generic;
using System.Linq;
using BudgetSquirrel.BudgetPlanning.Business.BudgetPlanning;
using BudgetSquirrel.Web.Common.Messages.BudgetPlanning;
using static BudgetSquirrel.Web.Common.Messages.BudgetPlanning.BudgetPlanningContextResponse;

namespace BudgetSquirrel.Backend.Resolvers
{
  public static class BudgetPlanningMessageResolvers
  {
    public static BudgetPlanningContextResponse ToApiMessage(GetBudgetPlanningContextQuery.BudgetPlanningContext context)
    {
      List<FundBudget> budgets = context.Budgets.Select(b => ToApiMessage(b)).ToList();
      FundSubFunds fundSubFundsTree = ToApiMessage(context.FundTree);
      BudgetPlanning.Domain.BudgetPlanning.FundBudget rootFundBudget = context.Budgets.Single(b => b.Fund.IsRoot);
      return new BudgetPlanningContextResponse(
        new TimeboxDetails(context.Timebox.Id, context.Timebox.StartDate, context.Timebox.EndDate),
        new UserProfile(context.Profile.ProfileId),
        fundSubFundsTree,
        budgets,
        rootFundBudget.Budget.IsFinalized);
    }

    private static FundSubFunds ToApiMessage(BudgetSquirrel.BudgetPlanning.Domain.Funds.FundSubFunds fundSubFunds)
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

    private static FundBudget ToApiMessage(BudgetSquirrel.BudgetPlanning.Domain.BudgetPlanning.FundBudget fundBudget)
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