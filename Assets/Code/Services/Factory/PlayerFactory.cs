using Zenject;
using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Services.AssetProvider;
using Codebase.Services.Initialize;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using IInitializable = Codebase.Services.Initialize.IInitializable;
using System;

namespace Codebase.Services.Factory
{
    public partial class PlayerFactory
    {
        private readonly DiContainer _container;
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetProviderService _assetProviderService;

        private Vector3 _initialPosition;
        private Player _playerPrefab;

        public PlayerFactory(
            DiContainer container,
            IStaticDataService staticDataService,
            IInitializationService initializationService,
            IAssetProviderService assetProviderService)
        {
            _container = container;
            _staticDataService = staticDataService;
            _assetProviderService = assetProviderService;

            initializationService.Register(this);
        }

        private void OnPlayerCreated(InjectContext context, Player player)
        {
            player.transform.position = _initialPosition;
        }
    }

    public partial class PlayerFactory : IPlayerFactory
    {
        public void CreatePlayer()
        {
            _container.Bind<Player>()
                .FromComponentInNewPrefab(_playerPrefab)
                .AsSingle()
                .OnInstantiated<Player>(OnPlayerCreated);
        }
    }

    public partial class PlayerFactory : IInitializable
    {
        public void Initialize()
        {
            _playerPrefab = _assetProviderService
                .GetPrefab<Player>(Constants.AssetPath.Player);
            _initialPosition = _staticDataService
                .ForPlayer().InitialPosition;
        }
    }
}