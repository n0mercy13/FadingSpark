using Codebase.Logic.Weapons;
using UnityEngine;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        [field: SerializeField] public Weapon[] Weapons { get; private set; }
        [field: SerializeField, Min(1)] public int MaxHealth { get; private set; }
        [field: SerializeField, Min(0.1f)] public float Speed { get; private set; }
    }
}