using System.Collections.Generic;
using System.Linq;
using BudgetSquirrel.Frontend.BudgetTracking.Domain;
using BudgetSquirrel.Web.Common.Messages.BudgetTracking;
using static BudgetSquirrel.Frontend.BudgetTracking.Domain.BudgetTrackingContext;

namespace BudgetSquirrel.Frontend.BudgetTracking
{
  public static class BudgetTrackingResponseResolvers
  {
    public static BudgetTrackingContext ToFrontendDto(BudgetTrackingContextResponse contextResponse)
    {
      IEnumerable<FundBudget> allAvailableFundBudgets = contextResponse.FundRelationships.Select(b => ToFrontendDto(b));
      return new BudgetTrackingContext(
        ToFrontendDto(contextResponse.FundTree, allAvailableFundBudgets),
        new TimeboxDetails(contextResponse.Timebox.Id, contextResponse.Timebox.StartDate, contextResponse.Timebox.EndDate),
        contextResponse.IsFinalized);
    }

    private static FundRelationships ToFrontendDto(
      BudgetTrackingContextResponse.FundSubFunds fundSubFunds,
      IEnumerable<FundBudget> allAvailableFundBudgets)
    {
      BudgetTrackingContextResponse.Fund fund = fundSubFunds.Fund;
      Budget budget = allAvailableFundBudgets.Single(fb => fb.FundId == fundSubFunds.Fund.Id).Budget;

      return new FundRelationships(
        new Fund(
          fund.Name,
          fund.Balance,
          fund.IsRoot,
          fund.ProfileId,
          fund.Id,
          fund.ParentFundId),
        budget,
        fundSubFunds.SubFunds.Select(fsf => ToFrontendDto(fsf, allAvailableFundBudgets)));
    }

    private static FundBudget ToFrontendDto(BudgetTrackingContextResponse.FundRelationshipDtos fundBudget)
    {
      return new FundBudget(
        new Budget(fundBudget.Budget.PlannedAmount),
        fundBudget.FundId);
    }
  }
}