using Codebase.Logic.EnemyComponents;
using UnityEngine;

namespace Codebase.Services.Factory
{
    public interface IEnemyFactory
    {
        Enemy Create(EnemyTypes type, Vector3 at );
    }
}
