using Codebase.Logic.PlayerComponents.Manager;
using Codebase.Services.Pause;
using Codebase.UI.Elements;
using Codebase.UI.Manager;

namespace Codebase.Infrastructure.StateMachine
{
    public partial class ResetState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPauseService _pauseService;
        private readonly IPlayerManager _playerManager;
        private readonly IUIManager _uIManager;

        public ResetState(
            GameStateMachine stateMachine,
            IPlayerManager playerManager,
            IUIManager uIManager,
            IPauseService pauseService)
        {
            _stateMachine = stateMachine;
            _playerManager = playerManager;
            _uIManager = uIManager;
            _pauseService = pauseService;
        }
    }

    public partial class ResetState : IState
    {
        public void Enter()
        {
            _uIManager.CloseAllUI();
            _uIManager.OpenUIElement<UI_HUD>();
            _playerManager.Reset();
            _pauseService.Resume();

            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}