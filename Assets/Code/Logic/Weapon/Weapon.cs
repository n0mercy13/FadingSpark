using UnityEngine;
using Codebase.Services.Factory;
using Codebase.StaticData;
using Codebase.Logic.PlayerComponents;

namespace Codebase.Logic.Weapons
{
    public class Weapon : IWeapon
    {
        private readonly IEnergy _shipsEnergy;
        private readonly IProjectileFactory _projectileFactory;
        private readonly Transform _projectileSpawnPoint;
        private readonly int _energyConsumption;
        private readonly float _rateOfFire;

        private Vector3 _shootDirection;

        public Weapon(
            WeaponMountPoint mountPoint,
            WeaponStaticData weaponData,
            IEnergy energy,
            IProjectileFactory projectileFactory)
        {
            _projectileSpawnPoint = mountPoint.transform;
            Type = mountPoint.Type;

            _rateOfFire = weaponData.RateOfFire;
            _energyConsumption = weaponData.EnergyConsumption;

            _shipsEnergy = energy;
            _projectileFactory = projectileFactory;
        }

        public WeaponTypes Type { get; private set; }

        public void Shoot()
        {
            if (_shipsEnergy.Current - _energyConsumption >= 0)
            {
                
                _projectileFactory.Create(
                    Type, _projectileSpawnPoint.position, Vector3.up);
                _shipsEnergy.Reduce(_energyConsumption);
            }
        }
    }
}