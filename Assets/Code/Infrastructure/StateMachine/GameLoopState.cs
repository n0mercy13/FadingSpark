using System;
using Codebase.Logic.PlayerComponents;
using Codebase.UI.Elements;
using Codebase.UI.Manager;

namespace Codebase.Infrastructure.StateMachine
{
    public class GameLoopState : IState, IDisposable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUIManager _uiManager;
        private readonly IEnergy _playerEnergy;

        private UI_HUD_Button_OpenMainMenu _mainMenuButton;

        public GameLoopState(
            GameStateMachine gameStateMachine,
            IUIManager uiManager,
            IEnergy playerEnergy)
        {
            _gameStateMachine = gameStateMachine;
            _uiManager = uiManager;
            _playerEnergy = playerEnergy;

            _playerEnergy.Died += OnPlayerDied;
        }

        public void Dispose()
        {
            _playerEnergy.Died -= OnPlayerDied;
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _mainMenuButton = null;
        }

        public void Enter()
        {
            RegisterButtons();
        }

        public void Exit()
        {
        }

        private void OnPlayerDied() => 
            _gameStateMachine.Enter<GameOverState>();

        private void OnMainMenuButtonClicked() => 
            _gameStateMachine.Enter<MainMenuState>();

        private void RegisterButtons()
        {
            if (_mainMenuButton == null 
                && _uiManager.TryGetUIElement(
                    out UI_HUD_Button_OpenMainMenu mainMenuButton))
            {
                _mainMenuButton = mainMenuButton;
                _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            }
        }
    }
}