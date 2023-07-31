using System;
using System.Collections.Generic;
using Codebase.Logic.Weapons;
using Codebase.Services.Factory;
using Codebase.Services.Input;
using Codebase.Services.StaticData;
using Codebase.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    public class PlayerWeaponHandler : IDisposable
    {
        private readonly Player _player;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly IProjectileFactory _projectileFactory;

        private WeaponMountPoint[] _mountPoints;
        private List<IWeapon> _weapons;

        public PlayerWeaponHandler(
            Player player,
            IStaticDataService staticDataService,
            IInputService inputService,
            IProjectileFactory projectileFactory)
        {
            _player = player;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _projectileFactory = projectileFactory;

            _weapons = new List<IWeapon>();

            _inputService.AttackButtonPressed += OnAttack;
        }

        public void Dispose() => 
            _inputService.AttackButtonPressed -= OnAttack;

        public void Initialize()
        {
            _mountPoints = GetMountPoints();
            GetWeapons(_mountPoints);
        }

        private void GetWeapons(WeaponMountPoint[] mountPoints)
        {
            foreach (WeaponMountPoint mountPoint in mountPoints)
            {
                if (mountPoint.Type != WeaponTypes.None)
                {
                    WeaponStaticData weaponData = _staticDataService
                        .ForWeapon(mountPoint.Type);
                    IWeapon weapon = new Weapon(
                        mountPoint, weaponData, _player.Energy, _projectileFactory);
                    _weapons.Add(weapon);
                }
            }
        }

        private WeaponMountPoint[] GetMountPoints() => 
            _player.GetComponentsInChildren<WeaponMountPoint>();

        private void OnAttack()
        {
            foreach (IWeapon weapon in _weapons)
                weapon.Shoot();
        }
    }
}