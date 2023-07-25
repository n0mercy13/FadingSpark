using System;

namespace Codebase.Logic
{
    public class Health : IDamageable
    {
        private readonly int _maxHealth;

        private int _value;

        public Health(int maxHealth)
        {
            _maxHealth = maxHealth;
            _value = maxHealth;
        }

        public event Action<int, int> ValueChanged = delegate { };

        public void ApplyDamage(int damage)
        {
            _value -= damage;
            _value = Math.Clamp(_value, 0, _maxHealth);
            ValueChanged.Invoke(_value, _maxHealth);
        }
    }
}