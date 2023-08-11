using Zenject;
using UnityEngine;
using Codebase.Logic.EnemyComponents;
using Codebase.Services.AssetProvider;
using Codebase.Infrastructure;

namespace Codebase.Services.Factory
{
    public partial class EnemyFactory
    {
        private const string EnemiesFolderPath = "Enemies/";
        private const string ParentName = "Enemies";
        private readonly IAssetProviderService _assetProviderService;
        private readonly DiContainer _container;

        private Transform _parent;
        private string _assetPath;

        public EnemyFactory(
            DiContainer container,
            IAssetProviderService assetProviderService, 
            ICoroutineRunner coroutineRunner)
        {
            _container = container;
            _assetProviderService = assetProviderService;

            if (coroutineRunner is MonoBehaviour monoBehaviour)
            {
                _parent = new GameObject(ParentName).transform;
                _parent.SetParent(monoBehaviour.transform);
            }
        }

        private Enemy GetPrefab(EnemyTypes by)
        {
            _assetPath = EnemiesFolderPath + by.ToString();
            Enemy prefab = _assetProviderService.GetPrefab<Enemy>(_assetPath);

            return prefab;
        }
    }

    public partial class EnemyFactory : IEnemyFactory
    {
        public Enemy Create(EnemyTypes type, Vector3 at)
        {
            Enemy prefab = GetPrefab(by: type);
            return _container
                .InstantiatePrefabForComponent<Enemy>(
                prefab, at, Quaternion.identity, _parent);
        }
    }
}