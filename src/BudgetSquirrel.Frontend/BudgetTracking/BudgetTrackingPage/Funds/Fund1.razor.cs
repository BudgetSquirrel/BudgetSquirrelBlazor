using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using static BudgetSquirrel.Frontend.BudgetTracking.Domain.BudgetTrackingContext;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Funds
{
  public partial class Fund1 : ComponentBase
  {
    [Parameter]
    public FundRelationships Fund { get; set; } = null!;
    
    [Parameter]
    public EventCallback<IEditNameFormValues> OnNameChanged { get; set; } = new EventCallback<IEditNameFormValues>();
    
    [Parameter]
    public EventCallback<FundRelationships> OnView { get; set; } = new EventCallback<FundRelationships>();

    private FundComponentBase baseInstance = null!;

    private EditBudgetFormValues State => this.baseInstance.State;

    protected override Task OnInitializedAsync()
    {
      this.baseInstance = new FundComponentBase(
        this.Fund,
        this.OnNameChanged,
        this.OnView);
      return base.OnInitializedAsync();
    }

    private string Name => this.baseInstance.Name;

    private string AmountIn => this.baseInstance.AmountIn;

    private string Balance => this.baseInstance.Balance;

    private bool ShouldShowSubBudgetArea => this.baseInstance.ShouldShowSubBudgetArea;

    private string InputNameFundName => this.baseInstance.InputNameFundName;

    private Task ChangeName(string newName)
    {
      return this.baseInstance.ChangeName(newName);
    }

    private Task View()
    {
      return this.baseInstance.View();
    }
  }
}