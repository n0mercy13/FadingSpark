using Zenject;
using UnityEngine;
using Codebase.Infrastructure.StateMachine;
using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        private void Awake() => 
            DontDestroyOnLoad(this);

        private void Start() => 
            _gameStateMachine.Enter<LoadLevelState, string>(
                Constants.Level.InitialLevelName);
    }
}