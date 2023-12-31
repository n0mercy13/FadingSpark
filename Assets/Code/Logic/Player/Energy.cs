﻿using System;
using UnityEngine;
using Codebase.StaticData;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    public partial class Energy
    {
        private float _absorptionCoefficient;
        private int _maxValue;
        private int _value;

        public Energy(IStaticDataService staticDataService)
        {
            PlayerStaticData playerData = staticDataService.ForPlayer();

            _maxValue = playerData.MaxEnergy;
            _absorptionCoefficient = playerData
                .ShieldDamageAbsorptionCoefficient;
        }
    }

    public partial class Energy : IEnergy
    {
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

        public void Reset()
        {
            _value = _maxValue;
            Changed.Invoke(_value, _maxValue);
        }
    }
}