using Zenject;
using UnityEngine;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.Services.Input;
using Codebase.Services.AssetProvider;
using Codebase.Services.Factory;
using Codebase.Services.SceneLoader;
using Codebase.Services.Tick;
using Codebase.Services.RandomGenerator;
using Codebase.Services.Initialize;
using Codebase.Services.Pause;
using Codebase.UI.Factory;
using Codebase.Logic.PlayerComponents.Manager;
using Codebase.UI.Manager;
using Codebase.Services.Pool;
using Codebase.StaticData;
using Codebase.Logic.PlayerComponents;

namespace Codebase.Infrastructure.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Runner _coroutineRunner;

        public override void InstallBindings()
        {
            BindInfrastructure();
            BindInputs();
            BindServices();
            BindFactories();
            BindManagers();
            BindPools();
        }

        private void BindPools()
        {
            Container
                .BindInterfacesTo<VFXPool>()
                .AsSingle();
            Container
                .BindInterfacesTo<ProjectilePool>()
                .AsSingle();
        }

        private void BindManagers()
        {
            Container
                .BindInterfacesAndSelfTo<UIManager>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<PlayerManager>()
                .AsSingle()
                .NonLazy();
        }

        private void BindFactories()
        {
            Container
                .BindInterfacesTo<UIFactory>()
                .AsSingle();
            Container
                .Bind<GameFactory>()
                .AsSingle()
                .NonLazy();
            Container
                .BindFactory<PlayerStaticData, Player, PlayerFactory>()
                .FromFactory<CustomPlayerFactory>();
        }

        private void BindInfrastructure()
        {
            Container
                .BindInterfacesTo<Runner>()
                .FromComponentInNewPrefab(_coroutineRunner)
                .AsSingle();
            Container
                .Bind<GameStateMachine>()
                .AsSingle()
                .NonLazy();
        }

        private void BindInputs() => 
            Container
            .Bind<InputControls>()
            .AsSingle();

        private void BindServices()
        {
            Container
                .BindInterfacesTo<PauseService>()
                .AsSingle();
            Container
                .BindInterfacesTo<RandomGeneratorService>()
                .AsSingle();
            Container
                .BindInterfacesTo<InitializationService>()
                .AsSingle();
            Container
                .BindInterfacesTo<InputService>()
                .AsSingle();
            Container
                .BindInterfacesTo<SceneLoaderService>()
                .AsSingle();  
            Container
                .BindInterfacesTo<TickProviderService>()
                .AsSingle();
            Container
                .BindInterfacesTo<StaticDataService>()
                .AsSingle();
            Container
                .BindInterfacesTo<AssetProviderService>()
                .AsSingle();
        }
    }
}