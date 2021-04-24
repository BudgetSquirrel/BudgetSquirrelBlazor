using System;
using System.Threading.Tasks;
using BudgetSquirrel.Core.BudgetPlanning;

namespace BudgetSquirrel.Backend.Biz.BudgetPlanning
{
  public interface IBudgetRepository
  {
    Task<int> CreateProfileForUser(string userEmail);

    Task<int> CreateFund(int profileId, int? parentFundId, string name, bool isRoot);

    Task CreateBudgetForFund(int fundId, decimal plannedAmount, int timeboxId);

    Task<Budget> GetBudget(int fundId);
  }
}