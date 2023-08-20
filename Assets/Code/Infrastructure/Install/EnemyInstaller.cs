using Zenject;
using Codebase.Logic.EnemyComponents;
using Codebase.Logic.Enemy.StateMachine;
using UnityEngine;
using Codebase.StaticData;

namespace Codebase.Infrastructure.Installer
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        private readonly EnemyStaticData _enemyData;

        public EnemyInstaller(
            [InjectOptional]
            EnemyStaticData enemyData)
        {
            _enemyData = enemyData;
        }

        public override void InstallBindings()
        {
            Container
                .Bind<Enemy>()
                .AsSingle();
            Container
                .Bind<Transform>()
                .FromComponentOnRoot();
            Container
                .BindInstance(_enemyData);
            Container
                .Bind<EnemyStateMachine>()
                .AsSingle();
            Container
                .Bind<EnemyMover>()
                .AsSingle();
            Container
                .Bind<EnemyCollisionHandler>()
                .AsSingle();
            Container
                .Bind<EnemyWeaponHandler>()
                .AsSingle();
        }
    }
}