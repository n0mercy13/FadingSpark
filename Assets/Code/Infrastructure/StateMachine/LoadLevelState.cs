using Codebase.Services.Factory;
using Codebase.Services.SceneLoader;
using Codebase.UI.Factory;
using System;

namespace Codebase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloaderState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISceneLoaderService _sceneLoader;
        private readonly IPlayerFactory _playerFactory;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, 
            ISceneLoaderService sceneLoader, 
            IPlayerFactory playerFactory, 
            IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _playerFactory = playerFactory;
            _uiFactory = uiFactory;
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
            CreatePlayer();
            CreateUI();
            //CreateEnemySpawner();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CreateEnemySpawner()
        {
            throw new NotImplementedException();
        }

        private void CreatePlayer() => 
            _playerFactory.CreatePlayer();

        private void CreateUI()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateHUD();
        }
    }
}