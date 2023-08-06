using System;
using UnityEngine;
using Codebase.Services.Pause;
using Codebase.UI.Elements;
using Codebase.UI.Manager;

namespace Codebase.Infrastructure.StateMachine
{
    public partial class GameOverState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IUIManager _uiManager;
        private readonly IPauseService _pauseService;

        private UI_GameOverScreen_Button_Restart _restartButton;

        public GameOverState(
            GameStateMachine stateMachine,
            IUIManager uiManager,
            IPauseService pauseService)
        {
            _stateMachine = stateMachine;
            _uiManager = uiManager;
            _pauseService = pauseService;
        }

        private void SetUI()
        {
            _uiManager.CloseAllUI();
            _uiManager.OpenUIElement<UI_GameOverScreen>();

            RegisterButtons();
        }

        private void RegisterButtons()
        {
            if (_restartButton == null
                && _uiManager.TryGetUIComponent<UI_GameOverScreen, UI_GameOverScreen_Button_Restart>(
                out UI_GameOverScreen_Button_Restart restartButton))
            {
                _restartButton = restartButton;
                _restartButton.onClick.AddListener(OnGameOverScreenRestartButtonPressed);
            }
        }

        private void OnGameOverScreenRestartButtonPressed() => 
            _stateMachine.Enter<ResetState>();
    }

    public partial class GameOverState : IState
    {
        public void Enter()
        {
            _pauseService.Pause();
            SetUI();
        }

        public void Exit()
        {
        }
    }

    public partial class GameOverState : IDisposable
    {
        public void Dispose()
        {
            _restartButton.onClick.RemoveAllListeners();
            _restartButton = null;
        }
    }
}