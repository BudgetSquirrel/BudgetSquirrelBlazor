using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using static BudgetSquirrel.Frontend.BudgetTracking.Domain.BudgetTrackingContext;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Funds
{
  public class FundComponentBase
  {
    private FundRelationships Fund { get; set; } = null!;
    private EventCallback<IEditNameFormValues> OnNameChanged { get; set; }
    private EventCallback<FundRelationships> OnView { get; set; }
    private EventCallback<FundRelationships> OnStartAddingTransactionClicked { get; set; }

    public FundComponentBase(
      FundRelationships fund,
      EventCallback<IEditNameFormValues> onNameChanged,
      EventCallback<FundRelationships> onView,
      EventCallback<FundRelationships> onStartAddingTransactionClicked,
      EditBudgetFormValues? state = null)
    {
      Fund = fund;
      this.OnNameChanged = onNameChanged;
      OnView = onView;
      OnStartAddingTransactionClicked = onStartAddingTransactionClicked;
      State = state ?? new EditBudgetFormValues(this.Fund.Fund.Id, this.Fund.Fund.Name);
      this.Initialize();
    }

    private void Initialize()
    {
      this.Name = this.Fund.Fund.Name;
    }

    public EditBudgetFormValues State { get; private set; } = null!;

    public string Name { get; private set; } = string.Empty;


    private decimal _amountIn => this.Fund.Budget.PlannedAmount;
    public string AmountIn => this._amountIn.ToString("C");
    
    private decimal _balance => this.Fund.Fund.Balance;
    public string Balance => this._balance.ToString("C");

    public bool ShouldShowSubBudgetArea => this.Fund.SubFunds.Any();

    public string InputNameFundName => $"fundName{this.Fund.Fund.Id}";

    public Task ChangeName(string newName)
    {
      this.State.Name = newName;
      return this.OnNameChanged.InvokeAsync(this.State);
    }

    public Task View()
    {
      return this.OnView.InvokeAsync(this.Fund);
    }

    public Task StartAddingTransaction()
    {
      return this.OnStartAddingTransactionClicked.InvokeAsync(this.Fund);
    }
  }
}