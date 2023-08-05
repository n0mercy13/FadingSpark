using Codebase.Services.Factory;
using Codebase.Services.SceneLoader;
using Codebase.UI.Elements;
using Codebase.UI.Manager;

namespace Codebase.Infrastructure.StateMachine
{
    public partial class LoadLevelState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISceneLoaderService _sceneLoader;
        private readonly IPlayerFactory _playerFactory;
        private readonly IUIManager _uiManager;
        private readonly GameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine,
            ISceneLoaderService sceneLoader,
            IPlayerFactory playerFactory,
            IUIManager uiManger,
            GameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _playerFactory = playerFactory;
            _uiManager = uiManger;
            _gameFactory = gameFactory;
        }

        private void OnLoaded()
        {
            CreateUI();
            CreatePlayer();
            CreateEnemySpawner();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CreateUI()
        {
            _uiManager.OpenUIElement<UI_Root>();
            _uiManager.OpenUIElement<UI_HUD>();
        }

        private void CreatePlayer() => 
            _playerFactory.CreatePlayer();

        private void CreateEnemySpawner() => 
            _gameFactory.CreateEnemySpawner();
    }

    public partial class LoadLevelState : IPayloaderState<string>
    {
        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit()
        {
        }
    }
}