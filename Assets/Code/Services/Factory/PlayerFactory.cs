using Zenject;
using Codebase.Logic.PlayerComponents;
using Codebase.Services.AssetProvider;
using Codebase.StaticData;

namespace Codebase.Services.Factory
{
    public partial class PlayerFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProviderService _assetProviderService;

        public PlayerFactory(
            DiContainer container,            
            IAssetProviderService assetProviderService)
        {
            _container = container;
            _assetProviderService = assetProviderService;
        }
    }

    public partial class PlayerFactory : IPlayerFactory
    {
        public Player Create()
        {
            Player playerPrefab = _assetProviderService
                .GetPrefab<Player>(Constants.AssetPath.Player);
            _container.Bind<Player>()
                .FromComponentInNewPrefab(playerPrefab)
                .AsSingle();

            return _container.Resolve<Player>();
        }
    }
}