using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.Frontend.BudgetTracking.Domain;
using BudgetSquirrel.Frontend.Shared.Controls;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using static BudgetSquirrel.Frontend.BudgetTracking.Domain.BudgetTrackingContext;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Transactions
{
  public partial class EditTransactionForm : ComponentBase
  {
    [Parameter]
    public EventCallback<EditTransactionFormState> OnTransactionEdited { get; set; }

    [Parameter]
    public EventCallback OnCancel { get; set; }

    [Parameter]
    public Transaction Original { get; set; } = default!;

    protected override void OnInitialized()
    {
      State = new EditTransactionFormState(this.Original);
    }

    public EditTransactionFormState State { get; private set; } = default!;

    private Switch.SwitchOption amountDirectionOptionExpense = new Switch.SwitchOption("Expense", "expense");
    private Switch.SwitchOption amountDirectionOptionIncome = new Switch.SwitchOption("Income", "income");

    /// <summary>
    /// The options to specify whether this transaction is an expense or an income.
    /// </summary>
    private IEnumerable<Switch.SwitchOption> AmountDirectionOptions =>
    [
      amountDirectionOptionExpense,
      amountDirectionOptionIncome
    ];

    /// <summary>
    /// The option to specify whether this transaction is an expense or an income.
    /// </summary>
    private string ActiveAmountDirectionOptionId => State.IsExpense ? amountDirectionOptionExpense.Id : amountDirectionOptionIncome.Id;
    
    private string VendorName => State.VendorName;

    private string Description => State.Description;

    private decimal Amount => State.Amount;

    private DateTime DateOfTransaction => State.DateOfTransaction;

    private string CheckNumber => State.CheckNumber;

    private void ChangeAmountDirection(Switch.SwitchOption option)
    {
      State.IsExpense = option.Id == amountDirectionOptionExpense.Id;
    }

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

    private Task Cancel()
    {
      return OnCancel.InvokeAsync();
    }

    private async Task Submit()
    {
      Console.WriteLine(JsonConvert.SerializeObject(State));
      await OnTransactionEdited.InvokeAsync(State);
    }
  }
}