using System;

namespace Codebase.Services.Tick
{
    public interface IUpdateProvider
    {
        event Action Updated;
    }
}
