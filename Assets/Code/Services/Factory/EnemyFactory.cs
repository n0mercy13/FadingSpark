using Zenject;
using Codebase.Logic.EnemyComponents;
using Codebase.Services.AssetProvider;
using UnityEngine;

namespace Codebase.Services.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private const string EnemiesFolderPath = "Enemies/";

        private readonly IAssetProviderService _assetProviderService;
        private readonly DiContainer _container;

        private string _assetPath;

        public EnemyFactory(DiContainer container)
        {
            _container = container;

            _assetProviderService = _container
                .Resolve<IAssetProviderService>();
        }

        public Enemy Create(EnemyTypes type, Vector3 at)
        {
            Enemy enemy = Instantiate(type);
            Initialize(enemy, at);

            return enemy;
        }

        private void Initialize(Enemy enemy, Vector3 at)
        {
            enemy.transform.position = at;
            enemy.gameObject.SetActive(true);
        }

        private Enemy Instantiate(EnemyTypes type)
        {
            _assetPath = EnemiesFolderPath + type.ToString();
            GameObject prefab = _assetProviderService.Get<GameObject>(_assetPath);
            Enemy enemy = _container.InstantiatePrefabForComponent<Enemy>(prefab);
            enemy.gameObject.SetActive(false);

            return enemy;
        }
    }
}