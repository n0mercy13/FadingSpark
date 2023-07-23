using Codebase.Logic.Enemy;

namespace Codebase.Services.Factory
{
    public interface IEnemyFactory
    {
        Enemy Create(EnemyTypes type);
        void Initialize(Enemy enemy);
    }
}
