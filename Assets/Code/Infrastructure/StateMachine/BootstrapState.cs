using Codebase.Services.Initialize;
using Codebase.Services.StaticData;
using Codebase.StaticData;

namespace Codebase.Infrastructure.StateMachine
{
    public partial class BootstrapState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly IInitializationService _initializationService;

        public BootstrapState(
            GameStateMachine gameStateMachine,
            IStaticDataService staticDataService,
            IInitializationService initializationService)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
            _initializationService = initializationService;
        }

        private void LoadStaticData() => 
            _staticDataService.Load();

        private void InitializeClasses() => 
            _initializationService.InitializeAll();
    }

    public partial class BootstrapState : IState
    {
        public void Enter()
        {
            LoadStaticData();
            InitializeClasses();

            _gameStateMachine.Enter<LoadLevelState, string>(Constants.Level.InitialLevelName);
        }

        public void Exit()
        {
        }
    }
}