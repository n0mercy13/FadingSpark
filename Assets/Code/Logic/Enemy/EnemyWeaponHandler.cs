using Codebase.Logic.Weapons;
using System;
using UnityEngine;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemyWeaponHandler : MonoBehaviour
    {
        private Weapon[] _weapons;

        public void Initialize(Weapon[] weapons)
        {
            _weapons = weapons;
        }
    }
}
