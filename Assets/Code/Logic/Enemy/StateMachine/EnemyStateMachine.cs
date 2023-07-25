﻿using System;
using UnityEngine;
using System.Collections.Generic;
using Codebase.Infrastructure.StateMachine;
using Codebase.Logic.EnemyComponents;

namespace Codebase.Logic.Enemy.StateMachine
{
    public class EnemyStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _activeState;

        public EnemyStateMachine(EnemyMover mover)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(MoveInDirectionState)] = new MoveInDirectionState(mover),
            };
        }

        public void Enter<TState>() where TState : class, IPayloaderState
        {
            IPayloaderState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloaderState<TPayload>
        {
            IPayloaderState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}