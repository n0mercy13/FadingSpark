using Codebase.Logic.EnemyComponents;
using UnityEngine;

namespace Codebase.Services.Pool
{
    public interface IEnemyPool
    {
        TSpawnable Spawn<TSpawnable>(Vector3 spawnPosition, Vector3 targetPosition) 
            where TSpawnable : Enemy;
    }
}
