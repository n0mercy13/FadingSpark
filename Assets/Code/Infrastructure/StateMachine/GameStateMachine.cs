using System;
using System.Collections.Generic;
using Codebase.Services.SceneLoader;
using Codebase.Services.Factory;
using Codebase.Services.StaticData;
using Codebase.Logic.PlayerComponents;
using Codebase.UI.Manager;
using Codebase.Services.Initialize;

namespace Codebase.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(
            IStaticDataService staticDataService,
            IInitializationService initializationService,
            ISceneLoaderService sceneLoader,
            IPlayerFactory playerFactory,
            IUIManager uiManager,
            IEnergy playerEnergy,
            GameFactory gameFactory)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(
                    this, staticDataService, initializationService),
                [typeof(LoadLevelState)] = new LoadLevelState(
                    this, sceneLoader, playerFactory, uiManager, gameFactory),
                [typeof(GameLoopState)] = new GameLoopState(
                    this, uiManager, playerEnergy),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloaderState<TPayload>
        {
            IPayloaderState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}