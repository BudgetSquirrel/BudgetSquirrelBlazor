using System.Threading.Tasks;
using BudgetSquirrel.Core.Funds;

namespace BudgetSquirrel.Backend.Biz.Funds
{
  public interface IFundRepository
  {
    Task<FundRoot> GetFundRoot(int fundRootId);
    
    Task<FundSubFunds> GetFundTree(int fundRootId);
  }
}