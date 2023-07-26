using Zenject;
using UnityEngine;
using Codebase.Logic.EnemyComponents;
using Codebase.Services.AssetProvider;
using Codebase.Infrastructure;

namespace Codebase.Services.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private const string EnemiesFolderPath = "Enemies/";

        private readonly IAssetProviderService _assetProviderService;
        private readonly DiContainer _container;
        private readonly MonoBehaviour _runner;

        private string _assetPath;

        public EnemyFactory(DiContainer container)
        {
            _container = container;

            _assetProviderService = _container
                .Resolve<IAssetProviderService>();
            _runner = _container
                .Resolve<ICoroutineRunner>() as MonoBehaviour;
        }

        public Enemy Create(EnemyTypes type, Vector3 at)
        {
            GameObject prefab = GetPrefab(type);
            return _container
                .InstantiatePrefabForComponent<Enemy>(
                prefab, at, Quaternion.identity, _runner.transform);
        }

        private GameObject GetPrefab(EnemyTypes type)
        {
            _assetPath = EnemiesFolderPath + type.ToString();
            GameObject prefab = _assetProviderService.Get<GameObject>(_assetPath);

            return prefab;
        }
    }
}