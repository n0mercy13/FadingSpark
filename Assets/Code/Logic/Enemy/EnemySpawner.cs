using Zenject;
using Codebase.Services.Factory;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemySpawner
    {
        private readonly IEnemyFactory _enemyFactory;

        [Inject]
        public EnemySpawner(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }
    }
}
