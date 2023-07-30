using System;

namespace Codebase.Logic
{
    public class Health : IHealth
    {
        private readonly int _maxHealth;
        private int _value;

        public Health(int maxHealth)
        {
            _maxHealth = maxHealth;
            _value = maxHealth;
        }

        public event Action<int, int> Changed = delegate { };
        public event Action Died = delegate { };

        public void Reduce(int by)
        {
            _value -= by;
            _value = Math.Clamp(_value, 0, _maxHealth);
            Changed.Invoke(_value, _maxHealth);

            if (_value == 0)
                Died.Invoke();
        }
    }
}