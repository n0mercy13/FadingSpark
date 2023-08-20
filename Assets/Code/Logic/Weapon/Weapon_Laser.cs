using System;
using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Services.Tick;
using Codebase.StaticData;
using Codebase.Services.Pool;

namespace Codebase.Logic.Weapons
{
    public class Weapon_Laser : Weapon
    {
        public Weapon_Laser(
            WeaponMountPoint mountPoint, 
            WeaponStaticData weaponData, 
            IEnergy energy, 
            IProjectilePool projectilePool, 
            ITickProviderService tickProvider, 
            Func<Vector3> getShootDirection) 
            : base(mountPoint, weaponData, energy, projectilePool, tickProvider, getShootDirection)
        {
        }

        public override void Shoot()
        {
            Shoot<Projectile_Laser>();
        }

    }
}