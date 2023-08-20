using UnityEngine;
using Codebase.Logic.Weapons;

namespace Codebase.Services.Pool
{
    public partial class ProjectilePool
    {

    }

    public partial class ProjectilePool : IProjectilePool
    {
        public TProjectile Spawn<TProjectile>(Vector3 spawnPosition, Vector3 direction) 
            where TProjectile : Projectile
        {
            throw new System.NotImplementedException();
        }
    }
}
