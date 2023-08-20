using System;
using System.Linq;
using UnityEngine;
using Codebase.StaticData;
using System.Collections.Generic;
using Codebase.Logic.Weapons;
using Codebase.Logic.Enemy;

namespace Codebase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypes, EnemyStaticData> _enemiesData;
        private Dictionary<WeaponTypes, WeaponStaticData> _weaponsData;
        private PlayerStaticData _playerData;

        public void Load()
        {
            _enemiesData = Resources
                .LoadAll<EnemyStaticData>(Constants.StaticDataPaths.Enemies)
                .ToDictionary(enemyData => enemyData.Type, enemyData => enemyData);

            _weaponsData = Resources
                .LoadAll<WeaponStaticData>(Constants.StaticDataPaths.Weapons)
                .ToDictionary(weaponData => weaponData.Type, weaponData => weaponData);

            _playerData = Resources
                .Load<PlayerStaticData>(Constants.StaticDataPaths.Player);
        }

        public PlayerStaticData ForPlayer() =>
            _playerData
            ?? throw new ArgumentNullException(
                $"{typeof(PlayerStaticData)} was not loaded");


        public EnemyStaticData ForEnemy(EnemyTypes type)
        {
            if (_enemiesData.TryGetValue(type, out EnemyStaticData enemyData))
                return enemyData;
            else
                throw new ArgumentNullException(
                $"{typeof(EnemyStaticData)} for type '{type}' was not loaded");
        }

        public WeaponStaticData ForWeapon(WeaponTypes type) =>
            _weaponsData.TryGetValue(type, out WeaponStaticData weaponData)
            ? weaponData
            : throw new ArgumentNullException(
                $"{typeof(WeaponStaticData)} for type '{type}' was not loaded");
    }
}