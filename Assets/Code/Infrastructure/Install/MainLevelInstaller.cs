using System;
using Zenject;
using UnityEngine;
using Codebase.Logic.Manager;

namespace Codebase.Infrastructure.Install
{
    public class MainLevelInstaller : MonoInstaller
    {
        [SerializeField] private EnemyManager _enemyManager;

        private void OnValidate()
        {
            if(_enemyManager == null)
                throw new ArgumentNullException(nameof(_enemyManager));
        }

        public override void InstallBindings()
        {
            BindManagers();
        }

        private void BindManagers()
        {
            Container.Bind<EnemyManager>().FromInstance(_enemyManager);
        }
    }
}
