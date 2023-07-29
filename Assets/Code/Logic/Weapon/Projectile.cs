using System;
using Zenject;
using UnityEngine;
using Codebase.Services.Tick;
using Codebase.StaticData;

namespace Codebase.Logic.Weapon
{
    public class Projectile : MonoBehaviour, IDisposable
    {
        private ITickProviderService _tickProvider;
        private Vector2 _direction;
        private int _damage;
        private float _speed;

        [Inject]
        private void Construct(ITickProviderService tickProvider)
        {
            _tickProvider = tickProvider;

            _tickProvider.Ticked += OnTick;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);

                Destroy(gameObject);
            }
        }

        public void Initialize(WeaponStaticData weaponData, Vector2 direction)
        {
            _direction = direction;
            transform.forward = _direction;

            _speed = weaponData.ProjectileSpeed;
            _damage = weaponData.ProjectileDamage;
        }

        public void Dispose()
        {
            _tickProvider.Ticked -= OnTick;
        }

        private void OnTick(int _) => 
            transform.position = _speed * _tickProvider.DeltaTime * _direction;
    }
}