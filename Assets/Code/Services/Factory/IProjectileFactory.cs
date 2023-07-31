using Codebase.Logic.Weapons;
using UnityEngine;

namespace Codebase.Services.Factory
{
    public interface IProjectileFactory
    {
        Projectile Create(WeaponTypes type, Vector3 spawnPosition, Vector3 direction);
    }
}