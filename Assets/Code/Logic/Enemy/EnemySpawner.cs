using System;
using UnityEngine;
using Codebase.Services.Factory;
using Codebase.Services.Tick;
using Codebase.Services.RandomGenerator;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemySpawner : IDisposable
    {
        private const float SpawnDelay = 2f;

        private readonly IEnemyFactory _enemyFactory;
        private readonly ITickProviderService _tickProvider;
        private readonly IRandomGeneratorService _random;

        private float _timer;

        public EnemySpawner(
            IEnemyFactory enemyFactory,
            ITickProviderService tickProvider,
            IRandomGeneratorService random)
        {
            _enemyFactory = enemyFactory;
            _tickProvider = tickProvider;
            _random = random;

            _tickProvider.Ticked += OnTick;
        }

        private float _deltaTime => 
            _tickProvider.DeltaTime;

        public void Dispose() => 
            _tickProvider.Ticked -= OnTick;

        private void OnTick(int tickCount)
        {
            _timer += _deltaTime;

            if(_timer >= SpawnDelay)
            {
                _timer -= SpawnDelay;
                CreateSmallAsteroid();
            }
        }

        private void CreateSmallAsteroid() => 
            _enemyFactory.Create(
                EnemyTypes.SmallAsteroid, 
                _random.GetPositionOutsideViewport());
    }
}
