using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Frontend.BudgetPlanning.Budgets
{
  public interface IBudget2AddFormValues
  {
    string Name { get; }
    decimal PlannedAmount { get; }
  }
  
  public partial class Budget2AddForm : ComponentBase
  {
    [Parameter]
    public EventCallback<IBudget2AddFormValues> OnSubmit { get; set; } = new EventCallback<IBudget2AddFormValues>();

    [Parameter]
    public EventCallback OnCancel { get; set; } = new EventCallback();

    private class Budget2AddFormValues : IBudget2AddFormValues
    {
      public string Name { get; set; } = string.Empty;

      public decimal PlannedAmount { get; set; } = -1;
    }

    private Budget2AddFormValues values { get; set; } = new Budget2AddFormValues();

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