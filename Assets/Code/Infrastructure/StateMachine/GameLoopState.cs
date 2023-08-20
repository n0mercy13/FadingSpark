using System;
using Codebase.Services.Input;
using Codebase.UI.Elements;
using Codebase.UI.Manager;

namespace Codebase.Infrastructure.StateMachine
{
    public partial class GameLoopState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly ILockable _lockableInput;
        private readonly IUIManager _uiManager;

        private UI_HUD_Button_OpenMainMenu _openMainMenuButton;

        public GameLoopState(
            GameStateMachine gameStateMachine,
            IUIManager uiManager,
            IInputService inputService)
        {
            _stateMachine = gameStateMachine;
            _inputService = inputService;
            _uiManager = uiManager;

            if(_inputService is ILockable lockableInput)
                _lockableInput = lockableInput;

            _inputService.MainMenuOpenButtonPressed += OnMainMenuButtonClicked;
        }
        
        private void OnMainMenuButtonClicked() =>
            _stateMachine.Enter<MainMenuState>();

        private void RegisterButtons()
        {
            if (_openMainMenuButton == null
                && _uiManager.TryGetUIComponent<UI_HUD, UI_HUD_Button_OpenMainMenu>(
                    out UI_HUD_Button_OpenMainMenu openMainMenuButton))
            {
                _openMainMenuButton = openMainMenuButton;
                _openMainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            }
        }
    }

    public partial class GameLoopState : IState
    {
        public void Enter()
        {
            RegisterButtons();
            _lockableInput.UnlockGameplayControls();
        }

        public void Exit()
        {
        }
    }

    public partial class GameLoopState : IDisposable
    {
        public void Dispose()
        {
            _inputService.MainMenuOpenButtonPressed -= OnMainMenuButtonClicked;
            _openMainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _openMainMenuButton = null;
        }
    }
}