using System;
using Zenject;
using UnityEngine;
using Codebase.Services.AssetProvider;
using Codebase.Infrastructure;
using Codebase.Logic.Weapons;
using Codebase.StaticData;
using Codebase.Services.StaticData;
using Codebase.Services.Pool;
using Codebase.Logic.VFX;

namespace Codebase.Services.Factory
{
    public partial class ProjectileFactory
    {
        private const string WeaponsFolderPath = "Weapons/";
        private const string ParentName = "Projectiles";

        private readonly IAssetProviderService _assetProviderService;
        private readonly IVFXPool _vfxPool;
        private readonly IStaticDataService _staticDataService;
        private readonly DiContainer _container;
        
        private Transform _parent;
        private Action<Vector3> _spawnVFX;
        private Projectile _projectile;
        private string _assetPath;

        public ProjectileFactory(
            DiContainer container,
            IAssetProviderService assetProviderService,
            ICoroutineRunner runner,
            IStaticDataService staticDataService,
            IVFXPool vfxPool)
        {
            _container = container;
            _assetProviderService = assetProviderService;
            _staticDataService = staticDataService;
            _vfxPool = vfxPool;

            if (runner is MonoBehaviour monoBehavior)
            {
                _parent = new GameObject(ParentName).transform;
                _parent.SetParent(monoBehavior.transform);
            }
        }

        private Projectile GetPrefab(WeaponTypes type)
        {
            _assetPath = WeaponsFolderPath + type.ToString();

            return _assetProviderService.GetPrefab<Projectile>(_assetPath);
        }

        private Action<Vector3> GetSpawnVFXAction(WeaponTypes type)
        {
            switch (type)
            {
                case WeaponTypes.None:
                    return null;

                case WeaponTypes.Laser:
                    return _vfxPool.Spawn<VFX_OnPlayerLaserHit>;

                default:
                    throw new InvalidOperationException(
                        $"VFX spawn action for weapon type '{nameof(type)}' was not found!");
            }
        }
    }

    public partial class ProjectileFactory : IProjectileFactory
    {
        public Projectile Create(
            WeaponTypes type, Vector3 spawnPosition, Vector3 direction)
        {
            Projectile prefab = GetPrefab(type);
            WeaponStaticData weaponData = _staticDataService.ForWeapon(type);
            _spawnVFX = GetSpawnVFXAction(type);

            _projectile = _container
                .InstantiatePrefabForComponent<Projectile>(
                prefab, spawnPosition, Quaternion.identity, _parent);
            _projectile.Initialize(weaponData, direction, _spawnVFX);
            _projectile.gameObject.SetActive(true);

            return _projectile;
        }
    }
}