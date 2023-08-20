using UnityEngine;
using Codebase.Logic.EnemyComponents;

namespace Codebase.Services.Pool
{
    public partial class EnemyPool : IEnemyPool
    {
        public TSpawnable Spawn<TSpawnable>(Vector3 spawnPosition, Vector3 targetPosition) where TSpawnable : Enemy
        {
            throw new System.NotImplementedException();
        }
    }
}