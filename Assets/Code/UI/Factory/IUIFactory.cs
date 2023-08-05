using Codebase.Services;
using Codebase.UI.Elements;

namespace Codebase.UI.Factory
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();
        UI_HUD CreateHUD();
        UI_MainMenu CreateMainMenu();
        UI_GameOverScreen CreateGameOverScreen();
    }
}