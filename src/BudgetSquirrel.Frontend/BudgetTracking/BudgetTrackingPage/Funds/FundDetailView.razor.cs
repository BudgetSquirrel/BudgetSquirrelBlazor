using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Frontend.BudgetTracking.Domain;
using Microsoft.AspNetCore.Components;
using static BudgetSquirrel.Frontend.BudgetTracking.Domain.BudgetTrackingContext;

namespace BudgetSquirrel.Frontend.BudgetTracking.BudgetTrackingPage.Funds
{
  public partial class FundDetailView : ComponentBase
  {
    [Parameter]
    public FundRelationships Fund { get; set; } = null!;
    
    [Parameter]
    public EventCallback OnClose { get; set; } = new EventCallback();

    private string fundName => this.Fund.Fund.Name;

    private IEnumerable<Transaction> transactions => this.Fund.Transactions;

    private Task Close()
    {
      return this.OnClose.InvokeAsync();
    }
  }
}