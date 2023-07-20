using System;
using Zenject;
using UnityEngine;

namespace Codebase.Infrastructure.Installer
{
    public class CoroutineRunnerInstaller : MonoInstaller
    {
        [SerializeField] private GameBootstrapper _coroutineRunner;

        private void OnValidate()
        {
            if (_coroutineRunner == null)
                throw new ArgumentNullException(nameof(_coroutineRunner));

            if (_coroutineRunner is ICoroutineRunner runner == false)
                throw new InvalidOperationException($"{_coroutineRunner} is not implementing {typeof(ICoroutineRunner)}");
        }

        public override void InstallBindings()
        {
            BindInfrastructure();
        }

        private void BindInfrastructure() =>
            Container.BindInterfacesAndSelfTo<ICoroutineRunner>().FromInstance(_coroutineRunner);
    }
}