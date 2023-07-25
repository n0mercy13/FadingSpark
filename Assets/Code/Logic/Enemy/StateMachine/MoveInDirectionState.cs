using Codebase.Infrastructure.StateMachine;
using UnityEngine;
using Codebase.Logic.EnemyComponents;

namespace Codebase.Logic.Enemy.StateMachine
{
    public class MoveInDirectionState : IPayloaderState<Vector3>
    {
        private readonly EnemyMover _mover;

        public MoveInDirectionState(EnemyMover mover) => 
            _mover = mover;

        public void Enter(Vector3 direction) => 
            _mover.StartToMoveInDirection(direction);

        public void Exit() => 
            _mover.StopMoving();
    }
}