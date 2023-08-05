using UnityEngine;
using Codebase.UI.Elements;

namespace Codebase.UI.Manager
{
    public interface IUIManager
    {
        UI_GameOverScreen GetGameOverScreen();
        UI_HUD GetHUD();
        UI_MainMenu GetMainMenu();
        bool TryGetUIElement<TComponent>(out TComponent uiElement) where TComponent : MonoBehaviour;
    }
}