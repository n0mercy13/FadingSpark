using System;
using System.Collections.Generic;
using Codebase.Services.SceneLoader;
using Codebase.Services.Factory;
using Codebase.UI.Factory;
using Codebase.Services.StaticData;

namespace Codebase.Infrastructure.StateMachine
{
    public class GameStateMachine : StateMachineBase
    {
        public GameStateMachine(
            IStaticDataService staticDataService,
            ISceneLoaderService sceneLoader,
            IPlayerFactory playerFactory,
            IUIFactory uiFactory)
        {
            States = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, staticDataService),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, playerFactory, uiFactory),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloaderState<TPayload>
        {
            IPayloaderState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }
    }
}