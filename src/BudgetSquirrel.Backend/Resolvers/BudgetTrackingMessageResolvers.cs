using System;
using System.Collections.Generic;
using System.Linq;
using BudgetSquirrel.BudgetTracking.Business.BudgetTrackingPage;
using BudgetSquirrel.Web.Common.Messages.BudgetTracking;
using BudgetSquirrel.Web.Common.Messages.BudgetTracking.Transactions;
using static BudgetSquirrel.Web.Common.Messages.BudgetTracking.BudgetTrackingContextResponse;

namespace BudgetSquirrel.Backend.Resolvers
{
  public static class BudgetTrackingMessageResolvers
  {
    public static BudgetTrackingContextResponse ToApiMessage(BudgetTrackingPageContext context)
    {
      List<FundRelationshipDtos> fundRelationships = context.Funds.Select(b => ToApiMessage(b)).ToList();
      FundSubFunds fundSubFundsTree = ToApiMessage(context.FundTree);
      BudgetTracking.Domain.BudgetTracking.FundRelationships rootFundBudget = context.Funds.Single(b => b.Fund.IsRoot);
      return new BudgetTrackingContextResponse(
        new TimeboxDetails(context.Timebox.Id, context.Timebox.StartDate, context.Timebox.EndDate),
        new UserProfile(context.Profile.ProfileId),
        fundSubFundsTree,
        fundRelationships,
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

    private static FundRelationshipDtos ToApiMessage(BudgetSquirrel.BudgetTracking.Domain.BudgetTracking.FundRelationships fundBudget)
    {
      decimal plannedAmount = fundBudget.Budget.PlannedAmount;
      int fundId = fundBudget.Fund.Id;
      TransactionResponse[] transactions = fundBudget.Transactions.Select(t => ToApiMessage(t)).ToArray();

      return new FundRelationshipDtos(
        new Budget(
          plannedAmount),
        transactions,
        fundId);
    }

    private static TransactionResponse ToApiMessage(BudgetTracking.Domain.BudgetTracking.Transaction t)
    {
      return new TransactionResponse(
        t.Id,
        t.VendorName,
        t.Description,
        t.Amount,
        t.DateOfTransaction,
        t.CheckNumber);
    }
  }
}