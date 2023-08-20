using Codebase.Logic.Enemy;
using Codebase.Logic.Weapons;
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