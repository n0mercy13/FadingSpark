using Zenject;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;

namespace Codebase.Infrastructure.Installer
{
    public class Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            BindGameStateMachine();
        }

        private void BindServices()
        {
            Container
                .BindInterfacesTo<StaticDataService>()
                .AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container
                .Bind<LoadProgressState>()
                .AsSingle();
            Container
                .Bind<LoadLevelState>()
                .AsSingle();
            Container
                .Bind<GameLoopState>()
                .AsSingle();
            Container
                .Bind<GameStateMachine>()
                .AsSingle();
        }
    }
}