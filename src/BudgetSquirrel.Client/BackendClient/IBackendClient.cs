using System.Threading.Tasks;

namespace BudgetSquirrel.Client.BackendClient
{
  public interface IBackendClient
  {
    Task ExecuteCommand(string endpoint, object request);
  }
}