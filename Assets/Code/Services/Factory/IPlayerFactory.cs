using Codebase.Logic.PlayerComponents;

namespace Codebase.Services.Factory
{
    public interface IPlayerFactory : IService
    {
        void CreatePlayer();
    }
}