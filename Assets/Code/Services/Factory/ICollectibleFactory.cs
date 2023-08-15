using Codebase.Logic.PickUps;

namespace Codebase.Services.Factory
{
    public interface ICollectibleFactory
    {
        TCollectable Create<TCollectable>() where TCollectable : Collectible;
    }
}