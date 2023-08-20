using Zenject;
using Codebase.Logic.PlayerComponents;
using Codebase.Services.AssetProvider;
using Codebase.Services.StaticData;
using Codebase.Infrastructure.Installer;
using Codebase.StaticData;

namespace Codebase.Services.Factory
{
    public class PlayerFactory : PlaceholderFactory<PlayerStaticData, Player>
    {
    }

    public partial class CustomPlayerFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProviderService _assetProviderService;

        public CustomPlayerFactory(
            DiContainer container, 
            IAssetProviderService assetProviderService)
        {
            _container = container;
            _assetProviderService = assetProviderService;
        }
    }

    public partial class CustomPlayerFactory : Zenject.IFactory<PlayerStaticData, Player>
    {
        public Player Create(PlayerStaticData playerData)
        {
            Player playerPrefab = 
                _assetProviderService.GetPrefab<Player>();
            _container
                .Bind<Player>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<PlayerInstaller>(playerPrefab)
                .AsSingle();

            return _container.Resolve<Player>();
        }
    }
}