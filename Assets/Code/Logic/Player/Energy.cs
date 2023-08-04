using System;
using UnityEngine;
using Codebase.Services.StaticData;
using Codebase.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    public class Energy : IEnergy
    {
        private readonly int _maxValue;
        private readonly float _absorptionCoefficient;
        private int _value;

        public Energy(IStaticDataService staticDataService)
        {
            PlayerStaticData playerData = staticDataService.ForPlayer();
            _maxValue = playerData.MaxEnergy;
            _absorptionCoefficient = playerData.ShieldDamageAbsorptionCoefficient;
            _value = _maxValue;
        }

        public int Current => _value;

        public event Action<int, int> Changed = delegate { };
        public event Action Died = delegate { };

        public void Absorb(int amount)
        {
            _value += (int)(amount * _absorptionCoefficient);
            _value = Mathf.Clamp(_value, 0, _maxValue);
            Changed.Invoke(_value, _maxValue);
        }

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