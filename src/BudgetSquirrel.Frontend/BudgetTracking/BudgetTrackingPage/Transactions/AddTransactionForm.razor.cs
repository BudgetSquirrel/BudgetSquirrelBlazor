using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Transactions
{
  public class AddTransactionForm : ComponentBase
  {
    private string Description { get; set; } = string.Empty;

    private DateTime DateOfTransaction { get; set; } = DateTime.Now;
  }
}