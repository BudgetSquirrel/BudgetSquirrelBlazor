using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Transactions
{
  public partial class AddTransactionForm : ComponentBase
  {
    private string VendorName { get; set; } = string.Empty;

    private string Description { get; set; } = string.Empty;

    private decimal Amount { get; set; } = 0;

    private DateTime DateOfTransaction { get; set; } = DateTime.Now;

    private string CheckNumber { get; set; } = string.Empty;

    private void ChangeVendorName(string vendorName)
    {
      VendorName = vendorName;
    }

    private void ChangeDescription(string description)
    {
      Description = description;
    }

    private void ChangeAmount(decimal amount)
    {
      Amount = amount;
    }

    private void ChangeDateOfTransaction(DateTime? dateOfTransaction)
    {
      if (dateOfTransaction.HasValue)
      {
        DateOfTransaction = dateOfTransaction.Value;
      }
    }

    private void ChangeCheckNumber(string checkNumber)
    {
      CheckNumber = checkNumber;
    }
  }
}