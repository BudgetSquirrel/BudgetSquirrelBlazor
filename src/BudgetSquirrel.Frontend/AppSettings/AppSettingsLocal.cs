using BudgetSquirrel.Frontend.BackendClient;

namespace BudgetSquirrel.Frontend.AppSettings
{
  public class AppSettingsLocal : AppSettingsBase
  {
    public override BackendConfiguration Backend { get; set; } = new BackendConfiguration()
    {
      RootUrl = "https://localhost:5051"
    };
  }
}