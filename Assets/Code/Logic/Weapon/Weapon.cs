using System;
using UnityEngine;

namespace Codebase.Logic.Weapons
{
    [Serializable]
    public class Weapon : IWeapon
    {
        [field: SerializeField] public RangeTypes Range { get; private set; }
        [field: SerializeField, Min(0)] public int Damage { get; private set; }
        [field: SerializeField, Min(0)] public float RateOfFire { get; private set; }

        public void Shoot(int damage)
        {
            throw new System.NotImplementedException();
        }
    }
}