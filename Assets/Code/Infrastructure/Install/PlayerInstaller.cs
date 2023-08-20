using Zenject;
using Codebase.Logic;
using Codebase.Logic.PlayerComponents;
using Codebase.Logic.PlayerComponents.Shield;

namespace Codebase.Infrastructure.Installer
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Player>()
                .FromComponentOnRoot()
                .AsSingle();
            Container
                .BindInterfacesTo<Energy>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<Shield>()
                .AsSingle();
            Container
                .Bind<PlayerWeaponHandler>()
                .AsSingle();
            Container
                .Bind<PlayerMoveHandler>()
                .AsSingle();
            Container
                .Bind<PlayerShieldHandler>()
                .AsSingle();
            Container
                .Bind<ShieldStateMachine>()
                .AsSingle();
            Container
                .Bind<SpriteColorHandler>()
                .AsSingle();            
            Container
                .Bind<BoundariesKeeper>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}