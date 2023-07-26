using Codebase.Services.Factory;
using Codebase.Services.SceneLoader;
using Codebase.UI.Factory;

namespace Codebase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloaderState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISceneLoaderService _sceneLoader;
        private readonly IPlayerFactory _playerFactory;
        private readonly IUIFactory _uiFactory;
        private readonly GameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine,
            ISceneLoaderService sceneLoader,
            IPlayerFactory playerFactory,
            IUIFactory uiFactory,
            GameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _playerFactory = playerFactory;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {            
        }

        private void OnLoaded()
        {
            CreateUI();
            CreatePlayer();
            CreateEnemySpawner();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CreateEnemySpawner() => 
            _gameFactory.CreateEnemySpawner();

        private void CreatePlayer() => 
            _playerFactory.CreatePlayer();

        private void CreateUI()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateHUD();
        }
    }
}