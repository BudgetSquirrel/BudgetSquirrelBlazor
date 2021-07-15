using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Frontend.Authentication.Login;
using BudgetSquirrel.Frontend.BudgetPlanning.Budgets;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using static BudgetSquirrel.Frontend.BudgetPlanning.BudgetPlanningContext;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public partial class BudgetPlannerPage : ComponentBase
  {
    #region component arguments

    [Inject]
    private ILoginService loginService { get; set; }

    [Inject]
    private IBudgetPlanningService budgetPlanningService { get; set; }

    #endregion

    #region component state

    private BudgetPlanningContext context;

    private bool isLoading = true;

    private bool isCreatingBudget = false;

    protected override Task OnInitializedAsync()
    {
      return this.ReloadContext();
    }

    private async Task ReloadContext()
    {
      this.isLoading = true;
      await this.loginService.PromptLoginIfNecessary();
      this.context = await this.budgetPlanningService.GetBudgetTree();
      this.isLoading = false;
    }

    #endregion

    #region convenience accessors

    private FundRelationships rootBudget => this.context.FundTree;

    private string timeboxDisplay
    {
      get
      {
        if (this.context == null)
        {
          return "";
        }
        return $"({this.context.Timebox.StartDate.ToString("%M/%d")} - {this.context.Timebox.EndDate.ToString("%M/%d")})";
      }
    }

    private decimal amountLeftToBudget
    {
      get
      {
        decimal amountBudgeted = this.context.FundTree.SubFunds.Sum(fundRelationships => fundRelationships.Budget.PlannedAmount);
        return this.PlannedIncome - amountBudgeted;
      }
    }

    #endregion

    #region template accessors

    private string RootFundName => this.context.FundTree.Fund.Name;

    private decimal PlannedIncome => this.rootBudget.Budget.PlannedAmount;

    public bool ShouldShowAmountLeftToBudget => this.amountLeftToBudget != 0;

    public string AmountLeftToBudgetLabel
    {
      get
      {
        if (this.amountLeftToBudget > 0)
        {
          return "Left to Budget";
        }
        else if (this.amountLeftToBudget < 0)
        {
          return "Over Budget";
        }
        else
        {
          return "";
        }
      }
    }
    
    private string AmountLeftToBudgetDisplay => Math.Abs(this.amountLeftToBudget).ToString("C");

    private string PlannedIncomeCssClass
    {
      get
      {
        string cssClass = "stat__value ";
        if (this.amountLeftToBudget == 0)
        {
          cssClass += "stat__value--good";
        }
        return cssClass;
      }
    }

    private string AmountLeftToBudgetCssClass
    {
      get
      {
        string cssClass = "stat__value ";
        if (this.amountLeftToBudget == 0)
        {
          cssClass += "stat__value--good";
        }
        else
        {
          cssClass += "stat__value--bad";
        }
        return cssClass;
      }
    }

    public IEnumerable<FundRelationships> Level1Budgets
    {
      get
      {
        if (this.isLoading)
        {
          return Array.Empty<FundRelationships>();
        }
        return this.rootBudget.SubFunds;
      }
    }

    #endregion

    #region event handlers

    private async Task ChangePlannedIncome(string amount)
    {
      decimal newPlannedIncome = decimal.Parse(amount);
      await this.budgetPlanningService.EditPlannedIncome(this.context.FundTree.Fund.Id, this.context.Timebox.Id, newPlannedIncome);
      await this.ReloadContext();
    }

    private async Task ChangeRootFundName(string newName)
    {
      await this.budgetPlanningService.EditFundName(this.context.FundTree.Fund.Id, newName);
      await this.ReloadContext();
    }

    /// <summary>
    /// "Add Budget" button was clicked. Budget detail form
    /// will pop up.
    /// </summary>
    private void StartCreateBudgetClicked()
    {
      this.isCreatingBudget = true;
    }

    /// <summary>
    /// "Stop Adding Budget" button was clicked. Budget detail form
    /// will close.
    /// </summary>
    private void CancelCreateBudgetClicked()
    {
      this.isCreatingBudget = false;
    }

    private async Task SubmitNewBudget1(IBudget1AddFormValues values)
    {
      await this.budgetPlanningService.CreateBudget(
        this.rootBudget.Fund.ProfileId,
        this.context.Timebox.Id,
        values.Name,
        values.PlannedAmount);
        
      this.isCreatingBudget = false;
      await this.ReloadContext();
    }

    private async Task SubmitNewBudget2(IBudget2AddFormValues values)
    {
      Console.WriteLine(JsonConvert.SerializeObject(values));
      await this.ReloadContext();
    }

    #endregion
  }
}