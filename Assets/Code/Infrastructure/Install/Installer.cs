using System;
using Zenject;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.Services.Input;

namespace Codebase.Infrastructure.Installer
{
    public class Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputs();
            BindServices();
            BindGameStateMachine();
        }

        private void BindInputs() => 
            Container.Bind<InputControls>().AsSingle();

        private void BindServices()
        {
            Container.BindInterfacesTo<StaticDataService>().AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}