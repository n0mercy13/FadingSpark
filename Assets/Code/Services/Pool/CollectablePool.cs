using Codebase.Logic.Collectables;
using Codebase.Services.Factory;

namespace Codebase.Services.Pool
{
    public class CollectablePool : Pool<IFactory<Collectible>, Collectible>, ICollectablePool
    {
        public CollectablePool(IFactory<Collectible> factory) : base(factory)
        {
        }
    }
}