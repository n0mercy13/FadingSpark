using System;
using Zenject;
using UnityEngine;
using Codebase.Infrastructure.StateMachine;
using Codebase.Infrastructure.Install;
using Codebase.Logic;
using Codebase.Logic.PlayerComponents;
using Codebase.Logic.PlayerComponents.Shield;
using Codebase.Logic.PlayerComponents.Manager;
using Codebase.Services.StaticData;
using Codebase.Services.Input;
using Codebase.Services.AssetProvider;
using Codebase.Services.Factory;
using Codebase.Services.SceneLoader;
using Codebase.Services.Tick;
using Codebase.Services.RandomGenerator;
using Codebase.Services.Pool;
using Codebase.Services.Initialize;
using Codebase.Services.Pause;
using Codebase.UI.Factory;
using Codebase.UI.Manager;

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
            BindPools();
            BindManagers();
            BindPlayerComponents();
        }

        private void BindPools()
        {
            Container
                .BindInterfacesTo<VFXPool>()
                .AsSingle()
                .NonLazy();
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

        private void BindPlayerComponents()
        {
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
                .Bind<PlayerMover>()
                .AsSingle();
            Container
                .Bind<ShieldHandler>()
                .AsSingle();
            Container
                .Bind<ShieldStateMachine>()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<SpriteColorHandler>()
                .WithId(InjectionIDs.Shield)
                .AsSingle();
        }

        private void BindFactories()
        {
            Container
                .BindInterfacesTo<PlayerFactory>()
                .AsSingle();
            Container
                .BindInterfacesTo<EnemyFactory>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesTo<UIFactory>()
                .AsSingle();
            Container
                .BindInterfacesTo<ProjectileFactory>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesTo<VFXFactory>()
                .AsSingle();
            Container
                .BindInterfacesTo<CollectibleFactory>()
                .AsSingle();
            Container
                .Bind<GameFactory>()
                .AsSingle()
                .NonLazy();
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
                .BindInterfacesTo<StaticDataService>()
                .AsSingle();
            Container
                .BindInterfacesTo<InputService>()
                .AsSingle();
            Container
                .BindInterfacesTo<AssetProviderService>()
                .AsSingle();
            Container
                .BindInterfacesTo<SceneLoaderService>()
                .AsSingle();  
            Container
                .BindInterfacesTo<TickProviderService>()
                .AsSingle();
        }
    }
}