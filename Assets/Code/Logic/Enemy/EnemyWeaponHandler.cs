using System;
using Codebase.Logic.Weapons;
using UnityEngine;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemyWeaponHandler : MonoBehaviour
    {
        private IWeapon[] _weapons;

        public void Initialize(IWeapon[] weapons)
        {
            _weapons = weapons;
        }
    }
}
