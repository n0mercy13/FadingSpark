using UnityEngine;
using Codebase.Logic.Weapons;

namespace Codebase.Services.Pool
{
    public interface IProjectilePool
    {
        TProjectile Spawn<TProjectile>(Vector3 spawnPosition, Vector3 direction) 
            where TProjectile : Projectile;
    }
}
