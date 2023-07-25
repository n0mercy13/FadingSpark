using Codebase.Logic.EnemyComponents;
using Zenject;

namespace Codebase.Services.Factory
{
    public class GameFactory
    {
        private readonly DiContainer _container;

        public GameFactory(DiContainer container) => 
            _container = container;

        public EnemySpawner CreateEnemySpawner() => 
            _container.Instantiate<EnemySpawner>();
    }
}
