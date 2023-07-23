using System;

namespace Codebase.Logic
{
    public class Health : IDamageable
    {
        public event Action<int, int> ValueChanged;

        public void ApplyDamage(int value)
        {
            throw new NotImplementedException();
        }
    }
}