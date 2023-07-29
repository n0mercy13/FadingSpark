﻿using Codebase.Logic.EnemyComponents;
using Codebase.Logic.Weapon;
using Codebase.StaticData;

namespace Codebase.Services.StaticData
{
    public interface IStaticDataService
    {
        EnemyStaticData ForEnemy(EnemyTypes type);
        WeaponStaticData ForWeapon(WeaponTypes type);
        PlayerStaticData ForPlayer();
        void Load();
    }
}