using System;
using UnityEngine;
using Codebase.StaticData;
using System.Collections.Generic;
using Codebase.Logic.EnemyComponents;
using System.Linq;

namespace Codebase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypes, EnemyStaticData> _enemiesData;
        private PlayerStaticData _playerData;

        public void Load()
        {
            _enemiesData = Resources
                .LoadAll<EnemyStaticData>(Constants.StaticDataPaths.Enemies)
                .ToDictionary(enemyData => enemyData.Type, enemyData => enemyData);

            _playerData = Resources
                .Load<PlayerStaticData>(Constants.StaticDataPaths.Player);
        }

        public PlayerStaticData ForPlayer() =>
            _playerData
            ?? throw new ArgumentNullException(
                $"{typeof(PlayerStaticData)} was not loaded");


        public EnemyStaticData ForEnemy(EnemyTypes type) => 
            _enemiesData.TryGetValue(type, out EnemyStaticData enemyData)
            ? enemyData
            : throw new ArgumentNullException(
                $"{typeof(EnemyStaticData)} for type '{type}' was not loaded");
    }
}