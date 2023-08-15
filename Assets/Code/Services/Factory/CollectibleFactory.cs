using System;
using Zenject;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.Logic.PickUps;
using Codebase.Services.AssetProvider;
using Codebase.StaticData;

namespace Codebase.Services.Factory
{
    public partial class CollectibleFactory
    {
        private const string ParentName = "Collectibles";

        private readonly DiContainer _container;
        private readonly IAssetProviderService _assetProvider;
        private readonly Transform _parent;

        public CollectibleFactory(
            DiContainer container, 
            IAssetProviderService assetProvider,
            ICoroutineRunner runner)
        {
            _container = container;
            _assetProvider = assetProvider;

            if (runner is MonoBehaviour monoBehaviour)
            {
                _parent = new GameObject(ParentName).transform;
                _parent.SetParent(monoBehaviour.transform); 
            }
        }

        private TCollectable GetPrefab<TCollectable>() where TCollectable : Collectible
        {
            string assetPath;

            if (typeof(ExperienceCollectible).Equals(typeof(TCollectable)))
                assetPath = Constants.AssetPath.Collectables.Experience;
            else
                throw new InvalidOperationException(
                    $"Prefab with {nameof(TCollectable)} component was not found");

            return _assetProvider.GetPrefab<TCollectable>(assetPath);
        }
    }

    public partial class CollectibleFactory : ICollectibleFactory
    {
        public TCollectable Create<TCollectable>() where TCollectable: Collectible
        {
            TCollectable prefab = GetPrefab<TCollectable>();
            TCollectable collectable = _container
                .InstantiatePrefabForComponent<TCollectable>(prefab, _parent);

            return collectable;
        }
    }
}