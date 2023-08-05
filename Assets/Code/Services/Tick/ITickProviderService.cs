using System;

namespace Codebase.Services.Tick
{
    public interface ITickProviderService
    {
        event Action<int> Ticked;
        float DeltaTime { get; }
        void Stop();
        void Resume();
    }
}