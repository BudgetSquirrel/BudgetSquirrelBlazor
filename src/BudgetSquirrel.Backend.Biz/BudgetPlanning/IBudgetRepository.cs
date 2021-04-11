using System;
using System.Threading.Tasks;

namespace BudgetSquirrel.Backend.Biz.BudgetPlanning
{
  public interface IBudgetRepository
  {
    Task<int> CreateFundRootForUser(string userEmail);

    Task<int> CreateFund(int fundRootId, int? parentFundId, string name, bool isRoot);

    Task CreateTimebox(int fundRootId, DateTime startDate, DateTime endDate);

    Task CreateBudgetForFund(int fundId, decimal plannedAmount);
  }
}