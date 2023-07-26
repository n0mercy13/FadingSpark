using UnityEngine;
using Codebase.Infrastructure;
using System.Collections;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemyCollisionHandler
    {
        private const float CollisionDelay = 1.0f;

        private readonly ICoroutineRunner _coroutineRunner;
        private readonly YieldInstruction _waitForCollisionDelay;
        private readonly int _damageOnCollision;

        private bool _canCollide;

        public EnemyCollisionHandler(
            ICoroutineRunner coroutineRunner, 
            int damageOnCollision)
        {
            _damageOnCollision = damageOnCollision;
            _coroutineRunner = coroutineRunner;

            _waitForCollisionDelay = new WaitForSeconds(CollisionDelay);
            _canCollide = true;
        }

        public void OnCollision(Collider2D collision)
        {
            if (_canCollide == false)
                return;

            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damageOnCollision);

                _coroutineRunner.StartCoroutine(ResetAbilityToCollide());
            }
        }

        private IEnumerator ResetAbilityToCollide()
        {
            yield return _waitForCollisionDelay;

            _canCollide = true;
        }
    }
}