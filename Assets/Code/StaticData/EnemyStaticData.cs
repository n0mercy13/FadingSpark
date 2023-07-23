using System;
using UnityEngine;
using Codebase.Logic.Weapons;
using Codebase.Logic.EnemyComponents;
using Codebase.Logic.Enemy.StateMachine;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        [field: SerializeField] public EnemyTypes Type { get; private set; }
        [field: SerializeField] public EnemyStateMachine AI { get; private set; }
        [field: SerializeField, Min(1)] public int MaxHealth { get; private set; }
        [field: SerializeField, Min(0.1f)] public float Speed { get; private set; }
        [field: SerializeField] public Weapon[] Weapons { get; private set; }

        private void OnValidate()
        {
            if(Weapons == null)
                throw new ArgumentNullException(nameof(Weapons));
        }
    }
}