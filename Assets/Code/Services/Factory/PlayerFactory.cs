using UnityEngine;
using Codebase.PlayerComponents;
using Codebase.Services.StaticData;
using Codebase.Services.PersistentProgress;
using Codebase.Services.AssetProvider;

namespace Codebase.Services.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssetProviderService _assetsProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly IPersistentProgressService _persistentProgressService;

        public Player CreatePlayer(Vector3 at)
        {
            throw new System.NotImplementedException();
        }
    }
}