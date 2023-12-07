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

    [Parameter]
    public EventCallback OnTransactionDeleted { get; set; } = new EventCallback();

    [Inject]
    private IBudgetTrackingPageService pageService { get; set; } = null!;

    private string fundName => this.Fund.Fund.Name;

    private Transaction? transactionToDelete = null;

    private bool isDeletingTransaction => this.transactionToDelete != null;

    private IEnumerable<Transaction> transactions => this.Fund.Transactions;

    private Task Close()
    {
      return this.OnClose.InvokeAsync();
    }

    private void OnDeleteTransactionClicked(Transaction transaction)
    {
      this.transactionToDelete = transaction;
    }

    private void OnCancelDeleteTransactionClicked()
    {
      this.transactionToDelete = null;
    }

    private async Task OnConfirmDeleteTransactionClicked()
    {
      if (!this.isDeletingTransaction)
      {
        return;
      }

      await this.pageService.DeleteTransaction(this.transactionToDelete!.Id);
      this.transactionToDelete = null;
      await this.OnTransactionDeleted.InvokeAsync();
    }
  }
}