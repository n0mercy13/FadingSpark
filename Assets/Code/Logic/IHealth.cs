using System;

namespace Codebase.Logic
{
    public interface IHealth
    {
        event Action<int, int> Changed;
        event Action Died;
        void Reduce(int by);
    }
}
