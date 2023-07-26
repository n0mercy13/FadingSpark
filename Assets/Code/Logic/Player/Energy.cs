using System;
using UnityEngine;

namespace Codebase.Logic.PlayerComponents
{
    public class Energy : IHealth
    {
        private readonly int _maxEnergy;

        private int _energy;

        public Energy(int maxEnergy)
        {
            _maxEnergy = maxEnergy;
            _energy = _maxEnergy;
        }

        public event Action<int, int> Changed = delegate { };
        public event Action Died = delegate { };

        public void Reduce(int by)
        {
            _energy -= by;
            _energy = Mathf.Clamp(_energy, 0, _maxEnergy);
            Changed.Invoke(_energy, _maxEnergy);

            if (_energy == 0)
                Died.Invoke();
        }
    }
}