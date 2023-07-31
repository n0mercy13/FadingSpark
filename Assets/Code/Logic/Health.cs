using System;
using UnityEngine;

namespace Codebase.Logic
{
    public class Health : IHealth
    {
        private readonly int _maxValue;
        private int _value;

        public Health(int maxHealth)
        {
            _maxValue = maxHealth;
            _value = maxHealth;
        }

        public int Current => _value;

        public event Action<int, int> Changed = delegate { };
        public event Action Died = delegate { };

        public void Reduce(int by)
        {
            _value -= by;
            _value = Math.Clamp(_value, 0, _maxValue);
            Changed.Invoke(_value, _maxValue);
            
            if (_value == 0)
                Died.Invoke();
        }
    }
}