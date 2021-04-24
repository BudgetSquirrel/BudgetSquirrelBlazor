using System.Threading.Tasks;
using BudgetSquirrel.Core.Funds;

namespace BudgetSquirrel.Backend.Biz.Funds
{
  public interface IFundRepository
  {
    Task<Profile> GetProfile(int profileId);
    
    Task<FundSubFunds> GetFundTree(int profileId);
  }
}