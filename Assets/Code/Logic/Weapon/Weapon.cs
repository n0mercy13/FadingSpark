using System;
using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Services.Tick;
using Codebase.StaticData;
using Codebase.Services.Pool;

namespace Codebase.Logic.Weapons
{
    public partial class Weapon
    {
        private readonly IEnergy _shipsEnergy;
        private readonly IProjectilePool _projectilePool;
        private readonly ITickProviderService _tickProvider;
        private readonly Func<Vector3> _getShootDirection;
        private readonly Transform _projectileSpawnPoint;
        private readonly int _energyConsumption;
        private readonly float _rateOfFire;

        private bool _isReadyToShoot;
        private float _time;

        public Weapon(
            WeaponMountPoint mountPoint,
            WeaponStaticData weaponData,
            IEnergy energy,
            IProjectilePool projectilePool,
            ITickProviderService tickProvider,
            Func<Vector3> getShootDirection)
        {
            _shipsEnergy = energy;
            _projectilePool = projectilePool;
            _tickProvider = tickProvider;
            _getShootDirection = getShootDirection;
            _projectileSpawnPoint = mountPoint.transform;
            Type = mountPoint.Type;

            _rateOfFire = weaponData.RateOfFire;
            _energyConsumption = weaponData.EnergyConsumption;

            _tickProvider.Ticked += OnTick;
        }

        public WeaponTypes Type { get; private set; }

        private bool _isEnoughEnergy => 
            _shipsEnergy.Current - _energyConsumption >= 0;

        private void OnTick(int _)
        {
            _time += _tickProvider.DeltaTime;

            if(_time > _rateOfFire)
                _isReadyToShoot = true;            
        }

        private bool CanShoot() =>
            _isReadyToShoot && _isEnoughEnergy;
    }

    public partial class Weapon : IWeapon
    {
        public virtual void Shoot()
        {
        }

        protected void Shoot<TProjectile>() where TProjectile : Projectile
        {
            if (CanShoot())
            {
                _time = 0;
                _isReadyToShoot = false;

                _projectilePool.Spawn<TProjectile>(
                    _projectileSpawnPoint.position, _getShootDirection.Invoke());
                _shipsEnergy.Reduce(_energyConsumption);
            }
        }
    }

    public partial class Weapon : IDisposable
    {
        public void Dispose() => 
            _tickProvider.Ticked -= OnTick;
    }
}