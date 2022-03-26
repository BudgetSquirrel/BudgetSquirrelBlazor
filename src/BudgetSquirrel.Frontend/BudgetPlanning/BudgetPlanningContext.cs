using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetSquirrel.Frontend.BudgetPlanning
{
  public class BudgetPlanningContext
  {
    public TimeboxDetails Timebox { get; private set; }
    
    public FundRelationships FundTree { get; private set; }

    public BudgetPlanningContext(FundRelationships fundTree, TimeboxDetails timebox)
    {
      this.FundTree = fundTree;
      this.Timebox = timebox;
    }
    
    public class TimeboxDetails
    {
      public int Id { get; private set; }
      
      public DateTime StartDate { get; private set; }

      public DateTime EndDate { get; private set; }

      public TimeboxDetails(int id, DateTime startDate, DateTime endDate)
      {
        this.Id = id;
        this.StartDate = startDate;
        this.EndDate = endDate;
      }
    }

    public class FundRelationships
    {
      public FundRelationships(Fund parentFund, Budget budget, IEnumerable<FundRelationships> subFunds)
      {
        this.Fund = parentFund;
        this.Budget = budget;
        this.SubFunds = subFunds;
      }

      public Fund Fund { get; private set; }

      public Budget Budget { get; private set; }

      public decimal SubBudgetsTotalPlannedAmount => this.SubFunds.Sum(f => f.Budget.PlannedAmount);

      /// <summary>
      /// Amount by which the total planned amount of the sub budgets differs from the
      /// planned amount of this budget.
      /// </summary>
      public decimal SubBudgetsTotalPlannedAmountDifference => this.Budget.PlannedAmount - this.SubBudgetsTotalPlannedAmount;

      public IEnumerable<FundRelationships> SubFunds { get; private set; }
    }

    public class Fund
    {
      public Fund(string name, decimal balance, bool isRoot, int profileId, int id, int parentFundId)
      {
        this.Name = name;
        this.Balance = balance;
        this.IsRoot = isRoot;
        this.ProfileId = profileId;
        this.Id = id;
        this.ParentFundId = parentFundId;
      }

      public int Id { get; private set; }

      public string Name { get; private set; }
      
      public decimal Balance { get; private set; }

      public bool IsRoot { get; private set; }

      public int ProfileId { get; private set; }

      public int ParentFundId { get; private set; }
    }

    public class FundBudget
    {
      public FundBudget(Budget budget, int fundId)
      {
        this.Budget = budget;
        this.FundId = fundId;
      }

      public Budget Budget { get; private set; }

      public int FundId { get; private set; }
    }

    public class Budget
    {
      public Budget(decimal plannedAmount)
      {
        this.PlannedAmount = plannedAmount;
      }

      public decimal PlannedAmount { get; private set; }
    }
  }
}