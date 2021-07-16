using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using static BudgetSquirrel.Frontend.BudgetPlanning.BudgetPlanningContext;

namespace BudgetSquirrel.Frontend.BudgetPlanning.Budgets
{
  public partial class Budget1 : ComponentBase
  {
    [Parameter]
    public FundRelationships Budget { get; set; }
    
    [Parameter]
    public EventCallback<IBudget2AddFormValues> OnCreateSubBudget2 { get; set; } = new EventCallback<IBudget2AddFormValues>();

    private bool IsAddingSubBudget { get; set; } = false;

    private bool ShouldShowSubBudgetArea => this.Budget.SubFunds.Any() || this.IsAddingSubBudget;

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

    public void OnAddSubBudgetClicked()
    {
      this.IsAddingSubBudget = true;
    }

    public async Task CreateSubBudget2(IBudget2AddFormValues values)
    {
      await this.OnCreateSubBudget2.InvokeAsync(values);
      this.IsAddingSubBudget = false;
    }

    public void CancelSubBudgetCreation()
    {
      this.IsAddingSubBudget = false;
    }
  }
}