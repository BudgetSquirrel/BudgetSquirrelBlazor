using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using static BudgetSquirrel.Frontend.BudgetPlanning.BudgetPlanningContext;

namespace BudgetSquirrel.Frontend.BudgetPlanning.Budgets
{
  public partial class Budget2 : ComponentBase
  {
    [Parameter]
    public FundRelationships Budget { get; set; } = null!;

    [Parameter]
    public EventCallback<IEditPlannedAmountFormValues> OnPlannedAmountChanged { get; set; } = new EventCallback<IEditPlannedAmountFormValues>();
    
    [Parameter]
    public EventCallback<IDeleteBudgetFormValues> OnDeleteBudget { get; set; } = new EventCallback<IDeleteBudgetFormValues>();

    private bool IsSubBudgetPlannedAmountZeroedOut => this.Budget.SubBudgetsTotalPlannedAmount == this.Budget.Budget.PlannedAmount ||
                                                      this.Budget.SubFunds.Count() == 0;

    private EditBudgetFormValues State { get; set; } = null!;

    protected override void OnInitialized()
    {
      this.State = new EditBudgetFormValues(this.Budget.Fund.Id, this.Budget.Fund.Name, this.Budget.Budget.PlannedAmount);
    }

    private string Name => this.Budget.Fund.Name;

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

    private bool IsDeletingBudget { get; set; } = false;

    private async Task ChangePlannedAmount(string newPlannedAmountRaw)
    {
      bool isValidFormat = decimal.TryParse(newPlannedAmountRaw, out decimal newPlannedAmount);
      if (isValidFormat)
      {
        this.State.PlannedAmount = newPlannedAmount;
      }

      await this.OnPlannedAmountChanged.InvokeAsync(this.State);
    }

    public void OnDeleteBudgetClicked()
    {
      this.IsDeletingBudget = true;
    }

    public Task OnDeleteBudgetConfirmed()
    {
      this.IsDeletingBudget = false;
      return this.OnDeleteBudget.InvokeAsync(new DeleteBudgetFormValues(this.Budget.Fund.Id));
    }

    public void OnDeleteBudgetCancelled()
    {
      this.IsDeletingBudget = false;
    }
  }
}