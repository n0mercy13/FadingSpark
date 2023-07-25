using Zenject;
using UnityEngine;
using Codebase.Logic.EnemyComponents;
using Codebase.Services.Factory;

namespace Codebase.Logic.Manager
{
    public class EnemyManager : MonoBehaviour
    {
        private EnemySpawner _spawner;

        [Inject]
        private void Construct(GameFactory gameFactory)
        {
            _spawner = gameFactory.CreateEnemySpawner();
        }
    }
}