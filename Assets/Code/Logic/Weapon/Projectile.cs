using System;
using Zenject;
using UnityEngine;
using Codebase.StaticData;
using Codebase.Logic.EnemyComponents;
using Codebase.Services.Tick;

namespace Codebase.Logic.Weapons
{
    public partial class Projectile : MonoBehaviour
    {
        private const float SelfDestructDelay = 3.0f;

        private ITickProviderService _tickProvider;
        private EnemyMover _mover;
        private Action<Vector3> _spawnVFX;
        private int _damage;
        private float _speed;
        private float _time;

        [Inject]
        private void Construct(ITickProviderService tickProvider)
        {
            _tickProvider = tickProvider;

            _tickProvider.Ticked += OnTicked;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);
                _spawnVFX.Invoke(transform.position);

                Destroy(gameObject);
            }
        }

        private void OnDestroy() => 
            Dispose();

        public void Initialize(
            WeaponStaticData weaponData, 
            Vector3 direction,
            Action<Vector3> spawnVFX)
        {
            _speed = weaponData.ProjectileSpeed;
            _damage = weaponData.ProjectileDamage;
            _spawnVFX = spawnVFX;

            _mover = new EnemyMover(this, _tickProvider, _speed);
            _mover.StartToMoveInDirection(direction);            
        }

        private void OnTicked(int _) => 
            SelfDestructionAfterDelay();

        private void SelfDestructionAfterDelay()
        {
            _time += _tickProvider.DeltaTime;

            if (_time > SelfDestructDelay)
                Destroy(gameObject);
        }
    }

    public partial class Projectile : IDisposable
    {
        public void Dispose()
        {
            _tickProvider.Ticked -= OnTicked;

            _mover.Dispose();
        }
    }
}