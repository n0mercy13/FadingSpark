using System;
using UnityEngine;
using System.Collections.Generic;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Logic.Enemy.StateMachine
{
    public class EnemyStateMachine : ScriptableObject
    {
        protected readonly Dictionary<Type, IExitableState> States;

        protected IExitableState ActiveState;

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            ActiveState?.Exit();

            TState state = GetState<TState>();
            ActiveState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            States[typeof(TState)] as TState;
    }
}