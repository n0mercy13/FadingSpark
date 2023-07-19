using Codebase.Services.PersistentProgress;
using Codebase.Services.StaticData;
using Codebase.UI.Factory;

namespace Codebase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloaderState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISceneLoadService _sceneLoader;
        private readonly IPlayerFactory _playerFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;

        public void Enter(string payload)
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}