using System.Collections.Generic;
using UnityEngine;
using Codebase.UI.Factory;
using Codebase.UI.Elements;
using Codebase.Extensions;

namespace Codebase.UI.Manager
{
    public partial class UIManager
    {
        private readonly IUIFactory _uiFactory;
        private readonly List<MonoBehaviour> _uiElements;

        private bool _isUIRootCreated;

        public UIManager(IUIFactory factory)
        {
            _uiFactory = factory;

            _uiElements = new List<MonoBehaviour>();
        }

        private void CreateUIRootIfNone()
        {
            if(_isUIRootCreated == false)
            {
                _uiFactory.CreateUIRoot();
                _isUIRootCreated = true;
            }
        }
    }

    public partial class UIManager : IUIManager
    {
        public UI_HUD GetHUD()
        {
            CreateUIRootIfNone();

            if (TryGetUIElement(out UI_HUD hud))
                return hud;

            UI_HUD newHUD = _uiFactory.CreateHUD();
            _uiElements.Add(newHUD);

            return hud;
        }

        public UI_GameOverScreen GetGameOverScreen()
        {
            if (TryGetUIElement(out UI_GameOverScreen gameOverScreen))
                return gameOverScreen;

            UI_GameOverScreen newGameOverScreen =
                _uiFactory.CreateGameOverScreen();
            _uiElements.Add(newGameOverScreen);

            return newGameOverScreen;
        }

        public UI_MainMenu GetMainMenu()
        {
            CreateUIRootIfNone();

            if (TryGetUIElement(out UI_MainMenu mainMenu))
                return mainMenu;

            UI_MainMenu newMainMenu = _uiFactory.CreateMainMenu();
            _uiElements.Add(newMainMenu);

            return newMainMenu;
        }

        public bool TryGetUIElement<TComponent>(out TComponent uiElement) where TComponent : MonoBehaviour
        {
            foreach (MonoBehaviour element in _uiElements)
            {
                if (element is TComponent component)
                {
                    uiElement = component;
                    return true;
                }
                else if (element.TryGetComponentInChildren(out TComponent childComponent))
                {
                    uiElement = childComponent;
                    return true;
                }
            }

            uiElement = null;
            return false;
        }
    }
}