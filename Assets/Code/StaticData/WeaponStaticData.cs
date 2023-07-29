using UnityEngine;
using Codebase.Logic.Weapon;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "WeaponStaticData", menuName = "StaticData/Weapon")]
    public class WeaponStaticData : ScriptableObject
    {
        [field: SerializeField] public WeaponTypes Type { get; private set; }
        [field: SerializeField, Min(0.1f)] public float ProjectileSpeed { get; private set; }
        [field: SerializeField, Min(0.1f)] public float RateOfFire { get; private set; }
        [field: SerializeField, Min(0)] public int ProjectileDamage { get; private set; }
        [field: SerializeField, Min(0)] public int EnergyConsumption { get; private set; }
    }
}