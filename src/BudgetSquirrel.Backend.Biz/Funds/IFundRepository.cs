using System.Threading.Tasks;
using BudgetSquirrel.Core.Funds;

namespace BudgetSquirrel.Backend.Biz.Funds
{
  public interface IFundRepository
  {
    Task<Profile> GetProfile(int profileId);
    
    Task<FundSubFunds> GetFundTree(int profileId, int timeBoxId);
    
    Task<Fund> GetFundById(int fundId);
    
    Task<Fund> GetRootFundForProfile(int profileId);
    
    Task UpdateFund(int fundId, FundDetails fundDetails);
  }

  public class FundDetails
  {
    public string Name { get; private set; }

    public FundDetails(string name)
    {
      this.Name = name;
    }
  }
}