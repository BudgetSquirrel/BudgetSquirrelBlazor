using System.Threading.Tasks;

namespace BudgetSquirrel.Backend.Biz
{
  public interface ICommand<TLoadedInputs>
  {
    Task Execute(TLoadedInputs loadedInputs);
    Task<TLoadedInputs> Load();
    Task<TLoadedInputs> Validate(TLoadedInputs loadedInputs);
  }
}