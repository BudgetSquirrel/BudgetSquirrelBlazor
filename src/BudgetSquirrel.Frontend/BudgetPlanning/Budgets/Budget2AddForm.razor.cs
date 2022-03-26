using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using static BudgetSquirrel.Frontend.BudgetPlanning.BudgetPlanningContext;

namespace BudgetSquirrel.Frontend.BudgetPlanning.Budgets
{
  public interface IBudget2AddFormValues
  {
    int ParentFundId { get; }
    string Name { get; }
    decimal PlannedAmount { get; }
  }
  
  public partial class Budget2AddForm : ComponentBase
  {
    [Parameter]
    public EventCallback<IBudget2AddFormValues> OnSubmit { get; set; } = new EventCallback<IBudget2AddFormValues>();

    [Parameter]
    public EventCallback OnCancel { get; set; } = new EventCallback();

    [Parameter]
    public FundRelationships ParentBudget { get; set; } = null!;

    private class Budget2AddFormValues : IBudget2AddFormValues
    {
      public int ParentFundId { get; private set; } = -1;

      public string Name { get; set; } = string.Empty;

      public decimal PlannedAmount { get; set; } = -1;

      public Budget2AddFormValues(int parentBudgetId)
      {
        this.ParentFundId = parentBudgetId;
      }
    }

    protected override void OnInitialized()
    {
      this.values = new Budget2AddFormValues(this.ParentBudget.Fund.Id);
    }

    private Budget2AddFormValues values { get; set; } = null!;

    private string nameDisplay => this.values.Name;

    private string plannedAmountDisplay
    {
      get
      {
        if (this.values.PlannedAmount > -1)
        {
          return this.values.PlannedAmount.ToString();
        }
        else
        {
          return string.Empty;
        }
      }
    }

    private void OnNameChanged(string name)
    {
      this.values.Name = name;
    }

    private void OnPlannedAmountChanged(string plannedAmountStr)
    {
      decimal plannedAmount = decimal.Parse(plannedAmountStr);
      this.values.PlannedAmount = plannedAmount;
    }

    private Task OnSaveClicked()
    {
      return this.OnSubmit.InvokeAsync(this.values);
    }

    private Task OnCancelClicked()
    {
      return this.OnCancel.InvokeAsync();
    }
  }
}