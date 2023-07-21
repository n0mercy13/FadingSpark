using System;

namespace Codebase.Logic
{
    public interface IDamageable
    {
        event Action<int, int> ValueChanged;
        void ApplyDamage(int value);
    }
}