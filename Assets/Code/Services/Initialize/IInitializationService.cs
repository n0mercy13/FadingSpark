namespace Codebase.Services.Initialize
{
    public interface IInitializationService
    {
        void InitializeAll();
        void Register(IInitializable initializable);
    }
}