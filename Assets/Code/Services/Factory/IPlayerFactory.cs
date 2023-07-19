using Codebase.Logic.PlayerComponents;

namespace Codebase.Services.Factory
{
    public interface IPlayerFactory : IService
    {
        Player Player { get; }
        Player CreatePlayer();
    }
}