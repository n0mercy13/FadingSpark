using System;
using Codebase.Services.Input;
using Codebase.Services.Pause;
using Codebase.UI.Elements;
using Codebase.UI.Manager;

namespace Codebase.Infrastructure.StateMachine
{
    public partial class MainMenuState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ILockable _inputService;
        private readonly IPauseService _pauseService;
        private readonly IUIManager _uiManager;

        private UI_MainMenu_Button_Return _returnButton;

        public MainMenuState(
            GameStateMachine stateMachine,
            IPauseService pauseService,
            IUIManager uiManager,
            ILockable inputService)
        {
            _stateMachine = stateMachine;
            _pauseService = pauseService;
            _uiManager = uiManager;
            _inputService = inputService;
        }

        private void RegisterButtons()
        {
            if(_returnButton == null
                && _uiManager.TryGetUIComponent<UI_MainMenu, UI_MainMenu_Button_Return>(
                    out UI_MainMenu_Button_Return returnButton))
            {
                _returnButton = returnButton;
                _returnButton.onClick.AddListener(OnReturnButtonClicked);
            }
        }

        private void OnReturnButtonClicked() => 
            _stateMachine.Enter<GameLoopState>();
    }

    public partial class MainMenuState : IState
    {
        public void Enter()
        {
            _inputService.LockGameplayControls();
            _pauseService.Pause();
            _uiManager.CloseAllUI();
            _uiManager.OpenUIElement<UI_MainMenu>();

            RegisterButtons();
        }

        public void Exit()
        {
            _uiManager.CloseAllUI();
            _uiManager.OpenUIElement<UI_HUD>();
            _pauseService.Resume();
            _inputService.UnlockGameplayControls();
        }
    }

    public partial class MainMenuState : IDisposable
    {
        public void Dispose()
        {
            _returnButton.onClick.RemoveAllListeners();
        }
    }
}