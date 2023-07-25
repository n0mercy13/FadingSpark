using System;

namespace Codebase.Services.Tick
{
    public interface ITickProviderService
    {
        event Action<int> Ticked;
        public float DeltaTime { get; }
    }
}
