using Zenject;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.Services.Input;
using Codebase.Services.AssetProvider;
using Codebase.Services.Factory;
using Codebase.UI.Factory;
using Codebase.Services.SceneLoader;

namespace Codebase.Infrastructure.Installer
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputs();
            BindServices();
            BindFactories();
        }

        private void BindFactories()
        {
            Container.BindInterfacesTo<PlayerFactory>().AsSingle();
            Container.BindInterfacesTo<UIFactory>().AsSingle();
        }

        private void BindInputs() => 
            Container.Bind<InputControls>().AsSingle().NonLazy();

        private void BindServices()
        {
            Container.BindInterfacesTo<StaticDataService>().AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle();
            Container.BindInterfacesTo<AssetProviderService>().AsSingle();
            Container.BindInterfacesTo<SceneLoaderService>().AsSingle();                    
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}