using UnityEngine;
using Codebase.Logic.Enemy.StateMachine;

namespace Codebase.Logic.EnemyComponents.Behavior
{
    public class EnemyAsteroid : Enemy
    {
        protected override void Initialize()
        {
            Vector3 direction = Target.transform.position - transform.position;

            StateMachine.Enter<MoveInDirectionState, Vector3>(direction);
        }
    }
}