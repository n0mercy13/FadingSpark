using Codebase.Logic.PlayerComponents.Manager;
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
        private readonly IPlayerManager _playerManager;
        private readonly IUIManager _uiManager;
        private readonly GameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine,
            ISceneLoaderService sceneLoader,
            IUIManager uiManger,
            GameFactory gameFactory,
            IPlayerManager playerManager)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiManager = uiManger;
            _gameFactory = gameFactory;
            _playerManager = playerManager;
        }

        private void OnLoaded()
        {
            InitializeLevel();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitializeLevel()
        {
            CreateUI();
            CreatePlayer();
            //CreateEnemySpawner();
        }

        private void CreateUI()
        {
            _uiManager.OpenUIElement<UI_Root>();
            _uiManager.OpenUIElement<UI_HUD>();
        }

        private void CreatePlayer() =>
            _playerManager.Spawn();

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