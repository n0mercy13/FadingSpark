using System;
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
using Codebase.UI.Factory;
using Codebase.Logic;
using Codebase.Logic.PlayerComponents;
using Codebase.Logic.PlayerComponents.Shield;
using Codebase.Infrastructure.Install;

namespace Codebase.Infrastructure.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Runner _coroutineRunner;

        public override void InstallBindings()
        {
            BindInfrastructure();
            BindServices();
            BindInputs();
            BindFactories();
            BindPlayerComponents();
        }

        private void BindPlayerComponents()
        {
            Container.BindInterfacesTo<Energy>().AsSingle();
            Container.BindInterfacesAndSelfTo<Shield>().AsSingle();
            Container.Bind<PlayerWeaponHandler>().AsSingle();
            Container.Bind<PlayerUIHandler>().AsSingle();
            Container.Bind<PlayerMover>().AsSingle();  
            Container.Bind<ShieldHandler>().AsSingle();
            Container.Bind<ShieldStateMachine>().AsSingle();
            Container.Bind<SpriteColorHandler>().WithId(InjectionIDs.Shield).AsSingle();
        }

        private void BindFactories()
        {
            Container.BindInterfacesTo<PlayerFactory>().AsSingle();
            Container.BindInterfacesTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesTo<UIFactory>().AsSingle();
            Container.BindInterfacesTo<ProjectileFactory>().AsSingle();
            Container.Bind<GameFactory>().AsSingle();
        }

        private void BindInfrastructure()
        {
            Container
                .BindInterfacesTo<Runner>()
                .FromComponentInNewPrefab(_coroutineRunner)
                .AsSingle();
        }

        private void BindInputs() => 
            Container.Bind<InputControls>().AsSingle();

        private void BindServices()
        {
            Container.BindInterfacesTo<RandomGeneratorService>().AsSingle();
            Container.BindInterfacesTo<StaticDataService>().AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle();
            Container.BindInterfacesTo<AssetProviderService>().AsSingle();
            Container.BindInterfacesTo<SceneLoaderService>().AsSingle();  
            Container.BindInterfacesTo<TickProviderService>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}