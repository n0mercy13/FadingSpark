using Zenject;
using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Services.StaticData;
using Codebase.Services.AssetProvider;
using Codebase.StaticData;

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