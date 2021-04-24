using System;
using System.Threading.Tasks;
using BudgetSquirrel.Core.BudgetPlanning;

namespace BudgetSquirrel.Backend.Biz.BudgetPlanning
{
  public interface IBudgetRepository
  {
    Task<int> CreateFundRootForUser(string userEmail);

    Task<int> CreateFund(int fundRootId, int? parentFundId, string name, bool isRoot);

    Task CreateBudgetForFund(int fundId, decimal plannedAmount);

    Task<Budget> GetBudget(int fundId);
  }
}