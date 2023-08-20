using Zenject;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.Services.AssetProvider;

namespace Codebase.Services.Factory
{
    public partial class MonoBehaviorFactory<TInstanceBase, TInstaller> 
        where TInstanceBase : MonoBehaviour
        where TInstaller : Installer
    {
        private readonly IAssetProviderService _assetProvider;
        private readonly DiContainer _container;
        private readonly Transform _parent;

        public MonoBehaviorFactory(
            DiContainer container,
            IAssetProviderService assetProvider,
            ICoroutineRunner runner,
            string parentName)
        {
            _container = container;
            _assetProvider = assetProvider;

            if (runner is MonoBehaviour monoBehaviour)
            {
                _parent = new GameObject(parentName).transform;
                _parent.SetParent(monoBehaviour.transform);
            }
        }
    }

    public partial class MonoBehaviorFactory<TInstanceBase, TInstaller> : IFactory<TInstanceBase>
    {
        public TInstance Create<TInstance>() where TInstance : TInstanceBase
        {
            TInstance prefab = _assetProvider.GetPrefab<TInstance>();
            _container
                .Bind<TInstance>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<TInstaller>(prefab)
                .AsSingle();

            return _container.Resolve<TInstance>();
        }
    }
}