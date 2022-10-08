using System.Linq;
using Microsoft.AspNetCore.Components;
using static BudgetSquirrel.Frontend.BudgetTracking.Domain.BudgetTrackingContext;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Funds
{
  public class FundComponentBase
  {
    private FundRelationships Fund { get; set; } = null!;

    public FundComponentBase(FundRelationships fund)
    {
      Fund = fund;
      this.Initialize();
    }

    private void Initialize()
    {
      this.Name = this.Fund.Fund.Name;
      this._amountIn = this.Fund.Budget.PlannedAmount;
    }

    public string Name { get; private set; } = string.Empty;


    private decimal _amountIn;
    public string AmountIn => this._amountIn.ToString("C");

    public bool ShouldShowSubBudgetArea => this.Fund.SubFunds.Any();

    public string InputNameFundName => $"fundName{this.Fund.Fund.Id}";
  }
}