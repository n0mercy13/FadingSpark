using Zenject;
using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Services.StaticData;
using Codebase.StaticData;

namespace Codebase.Services.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly DiContainer _container;

        public PlayerFactory(DiContainer container) => 
            _container = container;

        public void CreatePlayer()
        {
            InstantiateAndBindPlayer();
            Player player = GetPlayer();
            Initialize(player);            
        }

        private Player GetPlayer() => 
            _container.Resolve<Player>();

        private void Initialize(Player player) => 
            MovePlayerToInitialPosition(player);

        private void MovePlayerToInitialPosition(Player player)
        {
            Vector3 initialPosition = GetPlayerInitialPosition();
            player.gameObject.SetActive(false);
            player.transform.position = initialPosition;
            player.gameObject.SetActive(true);
        }

        private Vector3 GetPlayerInitialPosition() => 
            _container
            .Resolve<IStaticDataService>()
            .ForPlayer()
            .InitialPosition;

        private void InstantiateAndBindPlayer() =>
            _container
            .Bind<Player>()
            .FromNewComponentOnNewPrefabResource(Constants.AssetPath.Player)
            .AsSingle();
    }
}