using Zenject;
using UnityEngine;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        public void Start() => 
            _gameStateMachine.Enter<BootstrapState>();
    }
}