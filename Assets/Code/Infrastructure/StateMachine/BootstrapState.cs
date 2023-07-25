using Codebase.Services.StaticData;
using Codebase.StaticData;

namespace Codebase.Infrastructure.StateMachine
{
    public class BootstrapState : IPayloaderState
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

        public void Enter()
        {
            LoadStaticData();

            _gameStateMachine.Enter<LoadLevelState, string>(Constants.Level.InitialLevelName);
        }

        public void Exit()
        {
        }

        private void LoadStaticData() => 
            _staticDataService.Load();
    }
}