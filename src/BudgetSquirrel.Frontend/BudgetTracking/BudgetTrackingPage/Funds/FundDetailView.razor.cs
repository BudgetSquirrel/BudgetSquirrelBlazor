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

    private IEnumerable<Transaction> transactions => this.Fund.Transactions
      .Append(new Transaction(Guid.NewGuid(), "Meijer", "Grocieries", (decimal) 105.24, DateTime.Now.AddDays(-7), string.Empty))
      .Append(new Transaction(Guid.NewGuid(), "Walmart", "Quick food", (decimal) 24.46, DateTime.Now.AddDays(-1), string.Empty));

    private Task Close()
    {
      return this.OnClose.InvokeAsync();
    }
  }
}