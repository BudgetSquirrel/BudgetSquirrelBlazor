using System.Threading.Tasks;

namespace BudgetSquirrel.BudgetPlanning.Business
{
  public interface ICommand<TLoadedInputs>
  {
    Task Execute(TLoadedInputs loadedInputs);
    Task<TLoadedInputs> Load();
    Task<TLoadedInputs> Validate(TLoadedInputs loadedInputs);
  }
}