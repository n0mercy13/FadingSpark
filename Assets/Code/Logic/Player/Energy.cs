using System;
using UnityEngine;

namespace Codebase.Logic.PlayerComponents
{
    public class Energy
    {
        private readonly int _maxEnergy;

        private int _energy;

        public Energy(int maxEnergy)
        {
            _maxEnergy = maxEnergy;
            _energy = _maxEnergy;
        }

        public event Action<int, int> ValueChanged = delegate { };

        public void Reduce(int by)
        {
            _energy -= by;
            _energy = Mathf.Clamp(_energy, 0, _maxEnergy);
            ValueChanged.Invoke(_energy, _maxEnergy);
        }
    }
}