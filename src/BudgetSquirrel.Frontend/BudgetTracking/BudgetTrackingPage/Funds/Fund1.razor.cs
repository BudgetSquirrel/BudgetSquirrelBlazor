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

    private FundComponentBase baseInstance = null!;

    private EditBudgetFormValues State { get; set; } = null!;

    protected override Task OnInitializedAsync()
    {
      this.baseInstance = new FundComponentBase(this.Fund);
      this.State = new EditBudgetFormValues(this.Fund.Fund.Id, this.Fund.Fund.Name);
      return base.OnInitializedAsync();
    }

    private string Name => this.baseInstance.Name;

    private string AmountIn => this.baseInstance.AmountIn;

    private string Balance => this.baseInstance.Balance;

    private bool ShouldShowSubBudgetArea => this.baseInstance.ShouldShowSubBudgetArea;

    private string InputNameFundName => this.baseInstance.InputNameFundName;

    private void ChangeName(string newName)
    {
      this.State.Name = newName;
      this.OnNameChanged.InvokeAsync(this.State);
    }
  }
}