using System;
using System.Collections.Generic;

namespace BudgetSquirrel.Web.Common.Messages.BudgetPlanning
{
  public class BudgetPlanningContextResponse
  {
    public BudgetPlanningContextResponse()
    {
    }

    public BudgetPlanningContextResponse(TimeboxDetails timebox, UserProfile profile, FundSubFunds fundTree, IEnumerable<FundBudget> budgets)
    {
      Timebox = timebox;
      Profile = profile;
      FundTree = fundTree;
      Budgets = budgets;
    }

    public TimeboxDetails Timebox { get; set; }

    public UserProfile Profile { get; set; }
    
    public FundSubFunds FundTree { get; set; }

    public IEnumerable<FundBudget> Budgets { get; set; }

    public class TimeboxDetails
    {
      public TimeboxDetails()
      {
      }

      public TimeboxDetails(int id, DateTime startDate, DateTime endDate)
      {
        this.Id = id;
        this.StartDate = startDate;
        this.EndDate = endDate;
      }

      public int Id { get; set; }

      public DateTime StartDate { get; set; }

      public DateTime EndDate { get; set; }
    }

    public class FundSubFunds
    {
      public FundSubFunds()
      {
      }

      public FundSubFunds(Fund fund, IEnumerable<FundSubFunds> subFunds)
      {
        this.Fund = fund;
        this.SubFunds = subFunds;
      }

      public Fund Fund { get; set; }

      public IEnumerable<FundSubFunds> SubFunds { get; set; }
    }

    public class Fund
    {
      public Fund()
      {
      }

      public Fund(int id, string name, decimal balance, bool isRoot, int profileId, int parentFundId)
      {
        this.Id = id;
        this.Name = name;
        this.Balance = balance;
        this.IsRoot = isRoot;
        this.ProfileId = profileId;
        this.ParentFundId = parentFundId;
      }

      public int Id { get; set; }

      public string Name { get; set; }
      
      public decimal Balance { get; set; }

      public bool IsRoot { get; set; }

      public int ProfileId { get; set; }

      public int ParentFundId { get; set; }
    }

    public class FundBudget
    {
      public FundBudget()
      {
      }

      public FundBudget(Budget budget, int fundId)
      {
        this.Budget = budget;
        this.FundId = fundId;
      }

      public Budget Budget { get; set; }

      public int FundId { get; set; }
    }

    public class Budget
    {
      public decimal PlannedAmount { get; set; }

      public Budget(decimal plannedAmount)
      {
        this.PlannedAmount = plannedAmount;
      }

      public Budget()
      {
      }
    }

    public class UserProfile
    {
      public int ProfileId { get; set; }

      public UserProfile(int profileId)
      {
        this.ProfileId = profileId;
      }

      public UserProfile()
      {
      }
    }
  }
}