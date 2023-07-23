using System;
using Zenject;
using Codebase.Logic.EnemyComponents;
using Codebase.Services.AssetProvider;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Services.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private const string EnemiesFolderPath = "Enemies/";

        private readonly DiContainer _container;
        private readonly IAssetProviderService _assetProviderService;

        private string _assetPath;

        public EnemyFactory(DiContainer container)
        {
            _container = container;

            _assetProviderService = _container
                .Resolve<IAssetProviderService>();
        }

        public Enemy Create(EnemyTypes type)
        {
            Enemy enemy = Instantiate(type);
            Initialize(enemy);

            return enemy;
        }

        public void Initialize(Enemy enemy)
        {
            throw new System.NotImplementedException();
        }

        private Enemy Instantiate(EnemyTypes type)
        {
            _assetPath = EnemiesFolderPath + type.ToString();
            GameObject prefab = _assetProviderService.Get<GameObject>(_assetPath);

            return _container.InstantiatePrefabForComponent<Enemy>(prefab);
        }
    }
}