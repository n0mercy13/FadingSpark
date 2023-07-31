using System;
using UnityEngine;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    public class Energy : IHealth
    {
        private readonly int _maxValue;
        private int _value;

        public Energy(IStaticDataService staticDataService)
        {
            _maxValue = staticDataService.ForPlayer().MaxEnergy;
            _value = _maxValue;
        }

        public int Current => _value;

        public event Action<int, int> Changed = delegate { };
        public event Action Died = delegate { };

        public void Reduce(int by)
        {
            _value -= by;
            _value = Mathf.Clamp(_value, 0, _maxValue);
            Changed.Invoke(_value, _maxValue);

            if (_value == 0)
                Died.Invoke();
        }
    }
}