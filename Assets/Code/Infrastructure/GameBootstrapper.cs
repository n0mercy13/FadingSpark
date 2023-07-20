using Zenject;
using UnityEngine;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _gameStateMachine.Enter<BootstrapState>();
        }   
    }
}