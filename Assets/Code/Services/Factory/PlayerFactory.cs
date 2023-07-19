using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Services.StaticData;
using Codebase.Services.AssetProvider;
using Codebase.Services.Input;
using Codebase.StaticData;

namespace Codebase.Services.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssetProviderService _assetsProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;

        public PlayerFactory(
            IAssetProviderService assetsProvider, 
            IStaticDataService staticDataService, 
            IInputService inputService)
        {
            _assetsProvider = assetsProvider;
            _staticDataService = staticDataService;
            _inputService = inputService;
        }

        public Player Player { get; private set; }

        public Player CreatePlayer()
        {
            Vector3 initialPosition = _staticDataService.ForPlayer().InitialPosition;
            Player = _assetsProvider.Instantiate<Player>(Constants.AssetPath.Player, initialPosition);            

            return Player;
        }
    }
}