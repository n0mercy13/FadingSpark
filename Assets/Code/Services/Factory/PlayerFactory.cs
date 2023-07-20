using Zenject;
using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Services.StaticData;
using Codebase.Services.AssetProvider;
using Codebase.StaticData;
using System;
using Codebase.Services.Input;

namespace Codebase.Services.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly DiContainer _container;

        public PlayerFactory(DiContainer container)
        {
            _container = container;
        }

        public void CreatePlayer()
        {
            Vector3 initialPosition = GetPlayerInitialPosition();
            Player player = InstantiatePlayer(at: initialPosition);
            BindPlayer(player);
            Initialize(player);
        }

        private void Initialize(Player player)
        {
            InitializeMover(player);
        }

        private void InitializeMover(Player player)
        {
            PlayerMover mover = player.GetComponent<PlayerMover>();
            mover.Initialize(
                _container.Resolve<IInputService>(), 
                _container.Resolve<IStaticDataService>().ForPlayer().Speed);
        }

        private Vector3 GetPlayerInitialPosition() => 
            _container
            .Resolve<IStaticDataService>()
            .ForPlayer()
            .InitialPosition;

        private Player InstantiatePlayer(Vector3 at) =>
            _container.Resolve<IAssetProviderService>()
            .Instantiate<Player>(Constants.AssetPath.Player, at);

        private void BindPlayer(Player player) => 
            _container
            .Bind<Player>()
            .FromInstance(player)
            .AsSingle();
    }
}