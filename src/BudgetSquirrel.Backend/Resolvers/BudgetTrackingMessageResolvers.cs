using System;
using System.Collections.Generic;
using System.Linq;
using BudgetSquirrel.BudgetTracking.Business.BudgetTrackingPage;
using BudgetSquirrel.Web.Common.Messages.BudgetTracking;
using static BudgetSquirrel.Web.Common.Messages.BudgetTracking.BudgetTrackingContextResponse;

namespace BudgetSquirrel.Backend.Resolvers
{
  public static class BudgetTrackingMessageResolvers
  {
    public static BudgetTrackingContextResponse ToApiMessage(BudgetTrackingPageContext context)
    {
      List<FundBudget> budgets = context.Funds.Select(b => ToApiMessage(b)).ToList();
      FundSubFunds fundSubFundsTree = ToApiMessage(context.FundTree);
      BudgetTracking.Domain.BudgetTracking.FundBudget rootFundBudget = context.Funds.Single(b => b.Fund.IsRoot);
      return new BudgetTrackingContextResponse(
        new TimeboxDetails(context.Timebox.Id, context.Timebox.StartDate, context.Timebox.EndDate),
        new UserProfile(context.Profile.ProfileId),
        fundSubFundsTree,
        budgets,
        rootFundBudget.Budget.IsFinalized);
    }

    private static FundSubFunds ToApiMessage(BudgetSquirrel.BudgetTracking.Domain.Funds.FundSubFunds fundSubFunds)
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

    private static FundBudget ToApiMessage(BudgetSquirrel.BudgetTracking.Domain.BudgetTracking.FundBudget fundBudget)
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