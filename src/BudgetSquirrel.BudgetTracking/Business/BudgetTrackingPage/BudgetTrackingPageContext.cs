using System.Collections.Generic;
using BudgetSquirrel.BudgetTracking.Domain.BudgetTracking;
using BudgetSquirrel.BudgetTracking.Domain.Funds;
using BudgetSquirrel.BudgetTracking.Domain.History;

namespace BudgetSquirrel.BudgetTracking.Business.BudgetTrackingPage
{
  /// <summary>
  /// Return type for the <see cref="GetBudgetPlanningContextQuery"/> query.
  /// </summary>
  public class BudgetTrackingPageContext
  {
    public Timebox Timebox { get; private set; }

    public Profile Profile { get; private set; }
    
    public FundSubFunds FundTree { get; private set; }

    public IEnumerable<FundRelationships> Funds { get; private set; }

    public BudgetTrackingPageContext(Profile profile, FundSubFunds fundTree, IEnumerable<FundRelationships> budgets, Timebox timebox)
    {
      this.Profile = profile;
      this.FundTree = fundTree;
      this.Funds = budgets;
      this.Timebox = timebox;
    }
  }
}