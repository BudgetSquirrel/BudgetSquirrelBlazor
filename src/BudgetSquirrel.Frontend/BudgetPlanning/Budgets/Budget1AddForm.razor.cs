using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Frontend.BudgetPlanning.Budgets
{
  public interface IBudget1AddFormValues
  {
    string Name { get; }
    decimal PlannedAmount { get; }
  }
  
  public partial class Budget1AddForm : ComponentBase
  {
    [Parameter]
    public EventCallback<IBudget1AddFormValues> OnSubmit { get; set; } = new EventCallback<IBudget1AddFormValues>();

    private class Budget1AddFormValues : IBudget1AddFormValues
    {
      public string Name { get; set; } = string.Empty;

      public decimal PlannedAmount { get; set; } = -1;
    }

    private Budget1AddFormValues values { get; set; } = new Budget1AddFormValues();

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
  }
}