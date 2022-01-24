using System.Linq;
using Microsoft.AspNetCore.Components;
using static BudgetSquirrel.Frontend.BudgetPlanning.BudgetPlanningContext;

namespace BudgetSquirrel.Frontend.BudgetPlanning.Budgets
{
  public partial class Budget2 : ComponentBase
  {
    [Parameter]
    public FundRelationships Budget { get; set; }

    private bool IsSubBudgetPlannedAmountZeroedOut => this.Budget.SubBudgetsTotalPlannedAmount == this.Budget.Budget.PlannedAmount ||
                                                      this.Budget.SubFunds.Count() == 0;

    private string Name => this.Budget.Fund.Name;

    private string AmountInDisplay => this.Budget.Budget.PlannedAmount.ToString("C");

    private string AmountInStatValueCssClass
    {
      get
      {
        string cssClass = "stat__value";
        if (!this.IsSubBudgetPlannedAmountZeroedOut)
        {
          cssClass += " stat__value--bad";
        }
        else
        {
          cssClass += " stat__value--good";
        }
        return cssClass;
      }
    }

    public string BalanceDisplay => this.Budget.Fund.Balance.ToString("C");
  }
}