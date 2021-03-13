using BudgetSquirrel.Client.BackendClient;

namespace BudgetSquirrel.Client.AppSettings
{
  public class AppSettingsLocal : AppSettingsBase
  {
    public override BackendConfiguration Backend { get; set; } = new BackendConfiguration()
    {
      RootUrl = "https://localhost:5051"
    };
  }
}