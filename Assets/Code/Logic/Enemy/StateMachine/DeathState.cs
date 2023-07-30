using System;
using Codebase.Infrastructure.StateMachine;
using Codebase.Logic.EnemyComponents;

namespace Codebase.Logic.Enemy.StateMachine
{
    public class DeathState : IState
    {
        private readonly EnemyMover _mover;
        private readonly Action _destruction;

        public DeathState(EnemyMover mover, Action destruction)
        {
            _mover = mover;
            _destruction = destruction;
        }

        public void Enter()
        {
            _mover.Dispose();

            _destruction.Invoke();
        }

        public void Exit()
        {
        }
    }
}