using Codebase.Services;

namespace Codebase.UI.Factory
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();
        void CreateHUD();
        void CreateMenu();
    }
}