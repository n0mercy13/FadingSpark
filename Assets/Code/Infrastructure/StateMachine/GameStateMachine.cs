using System;
using System.Collections.Generic;
using Codebase.Services.SceneLoader;
using Codebase.Services.Factory;
using Codebase.Services.StaticData;
using Codebase.Services.Pause;
using Codebase.UI.Manager;
using Codebase.Logic.PlayerComponents.Manager;
using Codebase.Services.Input;

namespace Codebase.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(
            IStaticDataService staticDataService,
            IInputService inputService,
            ILockable lockableInput,
            ISceneLoaderService sceneLoader,
            IPauseService pauseService,
            IPlayerManager playerManager,
            IUIManager uiManager,
            GameFactory gameFactory)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(
                    this, staticDataService),
                [typeof(LoadLevelState)] = new LoadLevelState(
                    this, sceneLoader,  uiManager, gameFactory, playerManager),
                [typeof(ResetState)] = new ResetState(
                    this, playerManager, uiManager, pauseService),
                [typeof(GameLoopState)] = new GameLoopState(
                    this, uiManager, inputService),
                [typeof(MainMenuState)] = new MainMenuState(
                    this, pauseService, uiManager, lockableInput),
                [typeof(GameOverState)] = new GameOverState(
                    this, uiManager, pauseService, lockableInput),
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