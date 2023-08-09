using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Codebase.Logic.VFX;
using Codebase.Services.Factory;

namespace Codebase.Services.Pool
{
    public partial class VFXPool
    {
        private const int InitialCount = 10;

        private readonly IVFXFactory _factory;
        private readonly Dictionary<Type, List<VFX>> _effects = new();

        private VFX _effect;

        public VFXPool(IVFXFactory factory) => 
            _factory = factory;

        private void SetActive(VFX effect, Vector3 position)
        {
            effect.transform.position = position;
            effect.gameObject.SetActive(true);
        }

        private void AddNewVFX<TEffect>() where TEffect : VFX
        {
            List<VFX> newEffectsList = new();

            for (int i = 0; i < InitialCount; i++)
            {
                _effect = _factory.Create<TEffect>();
                _effect.gameObject.SetActive(false);
                newEffectsList.Add(_effect);
            }

            _effects.Add(typeof(TEffect), newEffectsList);
        }

        private void DoubleVFXCapacity<TEffect>() where TEffect : VFX
        {
            const int two = 2;

            List<VFX> _effectList = _effects[typeof(TEffect)];
            List<VFX> _newEffectList = new();

            for (int i = 0; i < _effectList.Count * two; i++)
            {
                if (i < _effectList.Count)
                {
                    _newEffectList.Add(_effectList[i]);
                }
                else
                {
                    _effect = _factory.Create<TEffect>();
                    _effect.gameObject.SetActive(false);
                    _newEffectList.Add(_effect);
                }
            }

            _effects[typeof(TEffect)] = _newEffectList;
        }
    }

    public partial class VFXPool : IVFXPool
    {
        public void Spawn<TEffect>(Vector3 position) where TEffect : VFX
        {
            if(_effects.TryGetValue(typeof(TEffect), out List<VFX> effectList))
            {
                _effect = effectList
                    .FirstOrDefault(effect => effect.isActiveAndEnabled == false);

                if (_effect == null)
                {
                    DoubleVFXCapacity<TEffect>();
                    Spawn<TEffect>(position);
                }

                SetActive(_effect, position);
            }
            else
            {
                AddNewVFX<TEffect>();

                Spawn<TEffect>(position);
            }
        }
    }
}