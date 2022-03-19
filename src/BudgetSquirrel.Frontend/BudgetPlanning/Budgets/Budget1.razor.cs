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
    
    [Parameter]
    public EventCallback<IEditPlannedAmountFormValues> OnPlannedAmountChanged { get; set; } = new EventCallback<IEditPlannedAmountFormValues>();
    
    [Parameter]
    public EventCallback<IDeleteBudgetFormValues> OnDeleteBudget { get; set; } = new EventCallback<IDeleteBudgetFormValues>();

    private bool IsAddingSubBudget { get; set; } = false;

    private bool IsDeletingBudget { get; set; } = false;

    private bool ShouldShowSubBudgetArea => this.Budget.SubFunds.Any() || this.IsAddingSubBudget;

    private bool IsSubBudgetPlannedAmountZeroedOut => this.Budget.SubBudgetsTotalPlannedAmount == this.Budget.Budget.PlannedAmount ||
                                                      this.Budget.SubFunds.Count() == 0;

    private EditBudgetFormValues State { get; set; }

    protected override void OnInitialized()
    {
      this.State = new EditBudgetFormValues(this.Budget.Fund.Id, this.Budget.Fund.Name, this.Budget.Budget.PlannedAmount);
    }

    private string Name => this.State.Name;

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

    public async Task CreateSubBudget2(IBudget2AddFormValues values)
    {
      await this.OnCreateSubBudget2.InvokeAsync(values);
      this.IsAddingSubBudget = false;
    }

    public void CancelSubBudgetCreation()
    {
      this.IsAddingSubBudget = false;
    }

    private void OnNameChanged(string newName)
    {
      this.State.Name = newName;
    }

    private async Task ChangePlannedAmount(string newPlannedAmountRaw)
    {
      bool isValidFormat = decimal.TryParse(newPlannedAmountRaw, out decimal newPlannedAmount);
      if (isValidFormat)
      {
        this.State.PlannedAmount = newPlannedAmount;
      }

      await this.OnPlannedAmountChanged.InvokeAsync(this.State);
    }

    private Task ChangeSubBudgetPlannedAmount(IEditPlannedAmountFormValues values)
    {
      return this.OnPlannedAmountChanged.InvokeAsync(values);
    }
  }
}