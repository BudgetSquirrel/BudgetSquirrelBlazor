using BudgetSquirrel.Frontend.BackendClient;

namespace BudgetSquirrel.Frontend.AppSettings
{
  public abstract class AppSettingsBase
  {
    public virtual BackendConfiguration Backend { get; set; }
  }
}