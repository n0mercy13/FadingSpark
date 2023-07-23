using System;
using System.Collections.Generic;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;

namespace Codebase.Logic.Enemy.StateMachine
{
    public class EnemyStateMachine : StateMachineBase
    {
        public EnemyStateMachine(IStaticDataService staticDataService)
        {
            States = new Dictionary<Type, IExitableState>
            {
                [typeof(InitializeState)] = new InitializeState(this, staticDataService),
                [typeof(IdleState)] = new IdleState(this),
                [typeof(MoveState)] = new MoveState(this),
                [typeof(AttackState)] = new AttackState(this),
                [typeof(DeathState)] = new DeathState(this),
            };
        }
    }
}