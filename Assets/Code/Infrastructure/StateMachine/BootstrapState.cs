using Codebase.Services.StaticData;
using Codebase.StaticData;

namespace Codebase.Infrastructure.StateMachine
{
    public partial class BootstrapState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(
            GameStateMachine gameStateMachine,
            IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
        }

        private void LoadStaticData() => 
            _staticDataService.Load();
    }

    public partial class BootstrapState : IState
    {
        public void Enter()
        {
            LoadStaticData();

            _gameStateMachine.Enter<LoadLevelState, string>(Constants.Level.InitialLevelName);
        }

        public void Exit()
        {
        }
    }
}