using System.Collections;
using UnityEngine;
using Codebase.Logic.Enemy.StateMachine;

namespace Codebase.Logic.EnemyComponents.Behavior
{
    public class EnemyAsteroid : Enemy
    {
        private const float SelfDestructionTimer = 4.0f;

        private Coroutine _selfDestructionCoroutine;

        private void OnEnable()
        {
            Vector3 direction = Target.transform.position - transform.position;
            StateMachine.Enter<MoveInDirectionState, Vector3>(direction);

            _selfDestructionCoroutine = StartCoroutine(SelfDestructAfterDelay());
        }

        private void OnDisable()
        {
            if(_selfDestructionCoroutine != null)
                StopCoroutine(_selfDestructionCoroutine);
        }

        private IEnumerator SelfDestructAfterDelay()
        {
            yield return new WaitForSeconds(SelfDestructionTimer);

            StateMachine.Enter<DeathState>();
        }
    }
}