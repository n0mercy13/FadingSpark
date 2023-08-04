﻿using System;
using System.Collections.Generic;
using Codebase.Logic.Weapons;
using Codebase.Services.Factory;
using Codebase.Services.Input;
using Codebase.Services.StaticData;
using Codebase.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    public partial class PlayerWeaponHandler : IDisposable, IPlayerWeaponActivatable
    {
        private readonly IEnergy _energy;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly IProjectileFactory _projectileFactory;

        private WeaponMountPoint[] _mountPoints;
        private List<IWeapon> _weapons;
        private bool _canShoot;

        public PlayerWeaponHandler(
            IEnergy energy,
            IStaticDataService staticDataService,
            IInputService inputService,
            IProjectileFactory projectileFactory)
        {
            _energy = energy;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _projectileFactory = projectileFactory;

            _canShoot = true;
            _weapons = new List<IWeapon>();

            _inputService.AttackButtonPressed += OnAttack;
        }
        public void Activate() =>
            _canShoot = true;

        public void Deactivate() =>
            _canShoot = false;

        public void Initialize(Player player)
        {
            _mountPoints = GetMountPoints(player);
            GetWeapons(_mountPoints);
        }

        public void Dispose() => 
            _inputService.AttackButtonPressed -= OnAttack;

        private void GetWeapons(WeaponMountPoint[] mountPoints)
        {
            foreach (WeaponMountPoint mountPoint in mountPoints)
            {
                if (mountPoint.Type != WeaponTypes.None)
                {
                    WeaponStaticData weaponData = _staticDataService
                        .ForWeapon(mountPoint.Type);
                    IWeapon weapon = new Weapon(
                        mountPoint, weaponData, _energy, _projectileFactory);
                    _weapons.Add(weapon);
                }
            }
        }

        private WeaponMountPoint[] GetMountPoints(Player player) => 
            player.GetComponentsInChildren<WeaponMountPoint>();

        private void OnAttack()
        {
            if(_canShoot)           
                foreach (IWeapon weapon in _weapons)
                    weapon.Shoot();
        }
    }
}