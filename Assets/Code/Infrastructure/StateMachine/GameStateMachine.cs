using System;
using System.Collections.Generic;
using Codebase.Services.SceneLoader;
using Codebase.Services.Factory;
using Codebase.UI.Factory;
using Codebase.Services.StaticData;

namespace Codebase.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _currentState;

        public GameStateMachine(
            IStaticDataService staticDataService,
            ISceneLoaderService sceneLoader, 
            IPlayerFactory playerFactory, 
            IUIFactory uiFactory)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, staticDataService),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, playerFactory, uiFactory),
                [typeof(GameLoopState)] = new GameLoopState(this),
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

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}
