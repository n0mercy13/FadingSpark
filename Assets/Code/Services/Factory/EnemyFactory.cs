using System;
using Zenject;
using Codebase.Logic.Enemy;
using Codebase.Services.AssetProvider;

namespace Codebase.Services.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProviderService _assetProviderService;


        public EnemyFactory(DiContainer container)
        {
            _container = container;

            _assetProviderService = _container
                .Resolve<IAssetProviderService>();
        }

        public Enemy Create(EnemyTypes type)
        {
            switch (type)
            {
                case EnemyTypes.SmallAsteroid:
                    Instantiate(type);
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Unknown type of enemy: '{type}'");
            }
        }

        public void Initialize(Enemy enemy)
        {
            throw new System.NotImplementedException();
        }
        
        private void Instantiate(EnemyTypes type)
        {
            throw new NotImplementedException();
        }
    }
}