using Zenject;
using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Services.StaticData;
using Codebase.Services.AssetProvider;
using Codebase.StaticData;
using Codebase.Services.Input;

namespace Codebase.Services.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly DiContainer _container;

        private Player _player;

        public PlayerFactory(DiContainer container) => 
            _container = container;

        public void CreatePlayer()
        {
            Vector3 initialPosition = GetPlayerInitialPosition();
            _player = InstantiatePlayer(at: initialPosition);
            BindPlayer();
            Initialize();
        }

        private void Initialize()
        {
            InitializeMover();
        }

        private void InitializeMover()
        {
            PlayerMover mover = _player.GetComponent<PlayerMover>();
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
            _container
            .Resolve<IAssetProviderService>()
            .Instantiate<Player>(Constants.AssetPath.Player, at);

        private void BindPlayer() => 
            _container
            .Bind<Player>()
            .FromInstance(_player)
            .AsSingle();
    }
}