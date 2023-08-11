using System;
using UnityEngine;
using Codebase.Logic.VFX;
using Codebase.Services.AssetProvider;
using Codebase.StaticData;
using Codebase.Infrastructure;

namespace Codebase.Services.Factory
{
    public partial class VFXFactory
    {
        private const string ParentName = "VFXs";

        private readonly IAssetProviderService _assetProviderService;
        private readonly Transform _parent;

        public VFXFactory(
            IAssetProviderService assetProviderService,
            ICoroutineRunner coroutineRunner)
        {
            _assetProviderService = assetProviderService;

            if (coroutineRunner is MonoBehaviour runner)
            {
                _parent = new GameObject(ParentName).transform;
                _parent.SetParent(runner.transform);
            }
        }

        private TEffect GetPrefab<TEffect>() where TEffect : VFX
        {
            string assetPath;

            if (typeof(VFX_OnPlayerLaserHit) == typeof(TEffect))
                assetPath = Constants.AssetPath.VFX.OnPlayerLaserHit;
            else
                throw new InvalidOperationException(
                    $"Prefab with {nameof(TEffect)} component was not found");

            return _assetProviderService.GetPrefab<TEffect>(assetPath);
        }
    }

    public partial class VFXFactory : IVFXFactory
    {
        public TEffect Create<TEffect>() where TEffect : VFX
        {
            TEffect prefab = GetPrefab<TEffect>();
            TEffect effect = UnityEngine.Object.Instantiate(prefab, _parent);

            return effect;
        }
    }
}