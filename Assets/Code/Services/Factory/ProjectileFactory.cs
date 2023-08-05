using Zenject;
using UnityEngine;
using Codebase.Services.AssetProvider;
using Codebase.Infrastructure;
using Codebase.Logic.Weapons;
using Codebase.StaticData;
using Codebase.Services.StaticData;

namespace Codebase.Services.Factory
{
    public partial class ProjectileFactory
    {
        private const string WeaponsFolderPath = "Weapons/";

        private readonly IAssetProviderService _assetProviderService;
        private readonly IStaticDataService _staticDataService;
        private readonly DiContainer _container;
        private readonly Transform _parent;

        private string _assetPath;

        public ProjectileFactory(
            DiContainer container,
            IAssetProviderService assetProviderService,
            ICoroutineRunner runner,
            IStaticDataService staticDataService)
        {
            _container = container;
            _assetProviderService = assetProviderService;
            _staticDataService = staticDataService;

            if (runner is MonoBehaviour monoBehavior)
                _parent = monoBehavior.transform;
        }

        private Projectile GetPrefab(WeaponTypes type)
        {
            _assetPath = WeaponsFolderPath + type.ToString();

            return _assetProviderService.GetPrefab<Projectile>(_assetPath);
        }
    }

    public partial class ProjectileFactory : IProjectileFactory
    {
        public Projectile Create(
            WeaponTypes type, Vector3 spawnPosition, Vector3 direction)
        {
            Projectile prefab = GetPrefab(type);
            WeaponStaticData weaponData = _staticDataService.ForWeapon(type);

            Projectile projectile = _container
                .InstantiatePrefabForComponent<Projectile>(
                prefab, spawnPosition, Quaternion.identity, _parent);
            projectile.Initialize(weaponData, direction);
            projectile.gameObject.SetActive(true);

            return projectile;
        }
    }
}