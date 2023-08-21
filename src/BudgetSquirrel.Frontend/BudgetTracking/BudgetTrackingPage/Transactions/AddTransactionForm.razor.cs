using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using static BudgetSquirrel.Frontend.BudgetTracking.Domain.BudgetTrackingContext;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Transactions
{
  public partial class AddTransactionForm : ComponentBase
  {
    [Parameter]
    public EventCallback<AddTransactionFormState> OnTransactionAdded { get; set; }

    [Parameter]
    public EventCallback OnCancel { get; set; }

    [Parameter]
    public FundRelationships Fund { get; set; }

    protected override void OnInitialized()
    {
      Console.WriteLine("Initialized AddTransactionForm");
      Console.WriteLine(Fund?.Fund?.Name);
      State = new AddTransactionFormState(Fund.Fund.Id);
    }

    public AddTransactionFormState State { get; private set; }
    
    private string VendorName => State.VendorName;

    private string Description => State.Description;

    private decimal Amount => State.Amount;

    private DateTime DateOfTransaction => State.DateOfTransaction;

    private string CheckNumber => State.CheckNumber;

    private void ChangeVendorName(string vendorName)
    {
      State.VendorName = vendorName;
    }

    private void ChangeDescription(string description)
    {
      State.Description = description;
    }

    private void ChangeAmount(decimal amount)
    {
      State.Amount = amount;
    }

    private void ChangeDateOfTransaction(DateTime? dateOfTransaction)
    {
      if (dateOfTransaction.HasValue)
      {
        State.DateOfTransaction = dateOfTransaction.Value;
      }
    }

    private void ChangeCheckNumber(string checkNumber)
    {
      State.CheckNumber = checkNumber;
    }

    private async Task Cancel()
    {
      OnCancel.InvokeAsync();
    }

    private async Task Submit()
    {
      await OnTransactionAdded.InvokeAsync(State);
    }
  }
}