using System;
using System.Collections.Generic;

namespace Codebase.Infrastructure.StateMachine
{
    public class StateMachineBase
    {
        protected Dictionary<Type, IExitableState> States;

        protected IExitableState CurrentState;

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        protected TState ChangeState<TState>() where TState : class, IExitableState
        {
            CurrentState?.Exit();

            TState state = GetState<TState>();
            CurrentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return States[typeof(TState)] as TState;
        }
    }
}