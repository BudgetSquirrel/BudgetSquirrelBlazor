using System;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Domain.BudgetPlanning;

namespace BudgetSquirrel.BudgetPlanning.Business.BudgetPlanning
{
  public interface IBudgetRepository
  {
    Task<int> CreateProfileForUser(string userEmail);

    Task<int> CreateFund(int profileId, int? parentFundId, string name, bool isRoot);

    Task CreateBudgetForFund(int fundId, decimal plannedAmount, int timeboxId);

    Task<Budget> GetBudget(int fundId, int timeboxId);

    Task SaveBudget(int fundId, int timeboxId, Budget budget);

    Task DeleteBudget(int fundId, int timeboxId);
  }
}