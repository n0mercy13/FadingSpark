using System;
using System.Collections.Generic;
using UnityEngine;
using Codebase.Logic.Weapons;
using Codebase.Services.Input;
using Codebase.Services.StaticData;
using Codebase.Services.Tick;
using Codebase.StaticData;
using Codebase.Services.Pool;

namespace Codebase.Logic.PlayerComponents
{
    public partial class PlayerWeaponHandler
    {
        private readonly IEnergy _energy;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly IProjectilePool _projectilePool;
        private readonly ITickProviderService _tickProvider;

        private WeaponMountPoint[] _mountPoints;
        private List<IWeapon> _weapons;
        private Transform _player;
        private WeaponStaticData _weaponData;
        private IWeapon _weapon;
        private bool _canShoot;

        public PlayerWeaponHandler(
            Player player,
            IEnergy energy,
            IStaticDataService staticDataService,
            IInputService inputService,
            IProjectilePool projectilePool,
            ITickProviderService tickProvider)
        {
            _energy = energy;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _projectilePool = projectilePool;
            _tickProvider = tickProvider;
            _player = player.transform;

            _canShoot = true;
            _weapons = new List<IWeapon>();

            _mountPoints = GetMountPoints(player);
            GetWeapons(_mountPoints);

            _inputService.AttackButtonPressed += OnAttack;
        }

        private Vector3 GetShootDirection() =>
            _player.up;

        private void OnAttack()
        {
            if (_canShoot)
                foreach (IWeapon weapon in _weapons)
                    weapon.Shoot();
        }

        private void GetWeapons(WeaponMountPoint[] mountPoints)
        {
            foreach (WeaponMountPoint mountPoint in mountPoints)
            {
                switch (mountPoint.Type)
                {
                    case WeaponTypes.None:
                        continue;

                    case WeaponTypes.Laser:
                        _weaponData = _staticDataService
                            .ForWeapon(mountPoint.Type);
                        _weapon = new Weapon_Laser(
                            mountPoint, _weaponData, _energy, _projectilePool, _tickProvider, GetShootDirection);
                        break;

                    default:
                        throw new InvalidOperationException(
                            $"Unknown type of weapon: '{mountPoint.Type}'.");
                }

                _weapons.Add(_weapon);

                if (mountPoint.Type != WeaponTypes.None)
                {
                    WeaponStaticData weaponData = _staticDataService
                        .ForWeapon(mountPoint.Type);
                    IWeapon weapon = new Weapon(
                        mountPoint, weaponData, _energy, _projectilePool, _tickProvider, GetShootDirection);
                    _weapons.Add(weapon);
                }
            }
        }

        private WeaponMountPoint[] GetMountPoints(Player player) => 
            player.GetComponentsInChildren<WeaponMountPoint>();
    }

    public partial class PlayerWeaponHandler : IPlayerWeaponActivatable
    {
        public void Activate() =>
            _canShoot = true;

        public void Deactivate() =>
            _canShoot = false;
    }

    public partial class PlayerWeaponHandler : IDisposable
    {
        public void Dispose() => 
            _inputService.AttackButtonPressed -= OnAttack;
    }
}