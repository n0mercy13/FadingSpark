using Codebase.Logic.EnemyComponents;
using Codebase.StaticData;

namespace Codebase.Services.StaticData
{
    public interface IStaticDataService
    {
        EnemyStaticData ForEnemy(EnemyTypes type);
        PlayerStaticData ForPlayer();
        void Load();
    }
}
