using System;
using Zenject;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.StaticData;
using System.Collections;

namespace Codebase.Logic.Weapon
{
    public class Projectile : MonoBehaviour, IDisposable
    {
        private const float SelfDestructDelay = 5.0f;

        private Coroutine _moveInDirectionCoroutine;
        private Coroutine _selfDestructAfterDelayCoroutine;
        private ICoroutineRunner _coroutineRunner;
        private int _damage;
        private float _speed;

        [Inject]
        private void Construct(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);

                this.Dispose();
            }
        }

        public void Initialize(WeaponStaticData weaponData, Vector2 direction)
        {
            _speed = weaponData.ProjectileSpeed;
            _damage = weaponData.ProjectileDamage;

            transform.forward = direction;

            _moveInDirectionCoroutine = _coroutineRunner
                .StartCoroutine(MoveInDirection(direction));
            _selfDestructAfterDelayCoroutine = _coroutineRunner
                .StartCoroutine(SelfDestructAfterDelay());
        }

        public void Dispose()
        {
            if (_moveInDirectionCoroutine != null)
                _coroutineRunner.StopCoroutine(_moveInDirectionCoroutine);

            if (_selfDestructAfterDelayCoroutine != null)
                _coroutineRunner.StopCoroutine(_selfDestructAfterDelayCoroutine);

            Destroy(gameObject);
        }

        private IEnumerator SelfDestructAfterDelay()
        {
            yield return new WaitForSeconds(SelfDestructDelay);

            this.Dispose();
        }

        private IEnumerator MoveInDirection(Vector3 direction)
        {
            transform.position += _speed * Time.deltaTime * direction;

            yield return null;
        }
    }
}