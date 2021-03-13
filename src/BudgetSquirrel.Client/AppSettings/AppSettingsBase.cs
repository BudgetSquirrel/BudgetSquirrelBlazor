using BudgetSquirrel.Client.BackendClient;

namespace BudgetSquirrel.Client.AppSettings
{
  public abstract class AppSettingsBase
  {
    public virtual BackendConfiguration Backend { get; set; }
  }
}