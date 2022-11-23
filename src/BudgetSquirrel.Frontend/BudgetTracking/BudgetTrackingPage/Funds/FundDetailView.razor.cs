using Microsoft.AspNetCore.Components;
using static BudgetSquirrel.Frontend.BudgetTracking.Domain.BudgetTrackingContext;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Funds
{
  public partial class FundDetailView : ComponentBase
  {
    [Parameter]
    public FundRelationships Fund { get; set; } = null!;

    private string fundName => this.Fund.Fund.Name;
  }
}