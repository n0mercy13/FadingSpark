using System;
using Zenject;
using UnityEngine;
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
        [SerializeField] private GameBootstrapper _coroutineRunner;

        private void OnValidate()
        {
            if (_coroutineRunner == null)
                throw new ArgumentNullException(nameof(_coroutineRunner));

            if(_coroutineRunner is ICoroutineRunner runner == false)
                throw new InvalidOperationException($"{_coroutineRunner} is not implementing {typeof(ICoroutineRunner)}");
        }

        public override void InstallBindings()
        {
            BindInfrastructure();
            BindInputs();
            BindServices();
            BindFactories();
        }

        private void BindInfrastructure() => 
            Container.BindInterfacesAndSelfTo<ICoroutineRunner>().FromInstance(_coroutineRunner);

        private void BindFactories()
        {
            Container.BindInterfacesTo<PlayerFactory>().AsSingle();
            Container.BindInterfacesTo<UIFactory>().AsSingle();
        }

        private void BindInputs() => 
            Container.Bind<InputControls>().AsSingle();

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