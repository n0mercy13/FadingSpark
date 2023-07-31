using System;
using System.Collections;
using Zenject;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.StaticData;
using Codebase.Logic.EnemyComponents;
using Codebase.Services.Tick;

namespace Codebase.Logic.Weapons
{
    public class Projectile : MonoBehaviour, IDisposable
    {
        private const float SelfDestructDelay = 5.0f;

        private EnemyMover _mover;
        private ICoroutineRunner _coroutineRunner;
        private ITickProviderService _tickProvider;
        private Coroutine _selfDestructAfterDelayCoroutine;
        private int _damage;
        private float _speed;

        [Inject]
        private void Construct(
            ICoroutineRunner coroutineRunner, 
            ITickProviderService tickProvider)
        {
            _coroutineRunner = coroutineRunner;
            _tickProvider = tickProvider;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);

                this.Dispose();
            }
        }

        public void Initialize(WeaponStaticData weaponData, Vector3 direction)
        {
            _speed = weaponData.ProjectileSpeed;
            _damage = weaponData.ProjectileDamage;

            _mover = new EnemyMover(this, _tickProvider, _speed);
            _mover.StartToMoveInDirection(direction);
            
            _selfDestructAfterDelayCoroutine = _coroutineRunner
                .StartCoroutine(SelfDestructAfterDelay());
        }

        public void Dispose()
        {
            if (_selfDestructAfterDelayCoroutine != null)
                _coroutineRunner.StopCoroutine(_selfDestructAfterDelayCoroutine);

            _mover.Dispose();

            Destroy(gameObject);
        }

        private IEnumerator SelfDestructAfterDelay()
        {
            yield return new WaitForSeconds(SelfDestructDelay);

            this.Dispose();
        }
    }
}