using System;

namespace Codebase.Logic
{
    public class Health : IDamageable
    {
        private int maxHealth;

        public Health(int maxHealth)
        {
            this.maxHealth = maxHealth;
        }

        public event Action<int, int> ValueChanged = delegate { };

        public void ApplyDamage(int value)
        {
            throw new NotImplementedException();
        }
    }
}