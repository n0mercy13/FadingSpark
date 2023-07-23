using System;
using UnityEngine;

namespace Codebase.Logic.PlayerComponents
{
    public class Energy : IDamageable
    {
        private readonly int _maxEnergy;

        private int _energy;

        public Energy(int maxEnergy)
        {
            _maxEnergy = maxEnergy;
            _energy = _maxEnergy;
        }

        public event Action Died = delegate { };
        public event Action<int, int> ValueChanged = delegate { };

        public void ApplyDamage(int value)
        {
            _energy -= value;
            _energy = Mathf.Clamp(_energy, 0, _maxEnergy);
            ValueChanged.Invoke(_energy, _maxEnergy);

            if (_energy == 0)
                Died.Invoke();
        }
    }
}