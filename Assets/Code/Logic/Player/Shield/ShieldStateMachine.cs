using System;
using System.Collections.Generic;
using Zenject;
using Codebase.Infrastructure;
using Codebase.Infrastructure.Install;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.Services.Initialize;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public class ShieldStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public ShieldStateMachine(
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler,
            IShield shield,
            IStaticDataService staticDataService,
            ICoroutineRunner coroutineRunner,
            PlayerWeaponHandler weaponHandler,
            IInitializationService initializationService)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(InactiveState)] = new InactiveState(
                    shield, staticDataService, colorHandler, weaponHandler, initializationService),
                [typeof(ActivationState)] = new ActivationState(
                    this, colorHandler,coroutineRunner, staticDataService, initializationService),
                [typeof(AbsorptionState)] = new AbsorptionState(
                    this, colorHandler, staticDataService, coroutineRunner, initializationService,shield),
                [typeof(ActiveState)] = new ActiveState(
                    colorHandler, staticDataService, initializationService),
                [typeof(DeactivationState)] = new DeactivationState(
                    this, colorHandler, coroutineRunner, staticDataService, initializationService),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
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