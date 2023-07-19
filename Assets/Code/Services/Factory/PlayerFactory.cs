using UnityEngine;
using Codebase.PlayerComponents;
using Codebase.Services.StaticData;
using Codebase.Services.PersistentProgress;

namespace Codebase.Services.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssetsProviderService _assetsProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly IPersistentProgressService _persistentProgressService;

        public Player CreatePlayer(Vector3 at)
        {
            throw new System.NotImplementedException();
        }
    }
}