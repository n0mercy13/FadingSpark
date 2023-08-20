using System;
using Codebase.Services.Tick;
using Codebase.Services.RandomGenerator;
using Codebase.Services.Pool;
using Codebase.Logic.EnemyComponents.Behavior;
using UnityEngine;
using Codebase.Logic.PlayerComponents.Manager;

namespace Codebase.Logic.EnemyComponents
{
    public partial class EnemySpawner
    {
        private const float SpawnDelay = 1f;

        private readonly IEnemyPool _pool;
        private readonly ITickProviderService _tickProvider;
        private readonly IRandomGeneratorService _random;

        private Transform _playerTransform;
        private float _timer;

        public EnemySpawner(
            ITickProviderService tickProvider,
            IRandomGeneratorService random,
            IPlayerManager playerManager,
            IEnemyPool pool)
        {
            _tickProvider = tickProvider;
            _random = random;
            _pool = pool;
            _playerTransform = playerManager.Player;

            _tickProvider.Ticked += OnTick;
        }

        private float _deltaTime => 
            _tickProvider.DeltaTime;

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
            _pool.Spawn<Enemy_SmallAsteroid>(
                _random.GetPositionOutsideViewport(), _playerTransform.position);
    }

    public partial class EnemySpawner : IDisposable
    {
        public void Dispose() =>
            _tickProvider.Ticked -= OnTick;
    }
}