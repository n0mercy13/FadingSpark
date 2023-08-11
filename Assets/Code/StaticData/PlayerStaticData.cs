using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "StaticData/Player")]
    public class PlayerStaticData : ScriptableObject
    {
        [field: SerializeField, Min(1), Header("Ship")] public int MaxEnergy { get; private set; }
        [field: SerializeField, Min(0.1f)] public float MoveSpeed { get; private set; }
        [field: SerializeField, Min(0.1f)] public float RotationSpeed { get; private set; }
        [field: SerializeField] public Vector3 InitialPosition { get; private set; }

        [field: SerializeField, Min(0f), Header("Shield")] public float ShieldActivationTime { get; private set; }
        [field: SerializeField, Min(0f)] public float ShieldAbsorptionTime { get; private set; }
        [field: SerializeField, Min(0f)] public float ShieldDeactivationTime { get; private set; }
        [field: SerializeField, Min(1f)] public float ShieldDamageAbsorptionCoefficient { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float ShieldDamageReductionCoefficient { get; private set; }
        [field: SerializeField, Min(0)] public float DeactivatedShieldRadius { get; private set; }
        [field: SerializeField, Min(0)] public float ActivatedShieldRadius { get; private set; }
        [field: SerializeField, Min(0)] public int ActivatedShieldContactDamage { get; private set; }
        [field: SerializeField] public Color ShieldInactiveColor { get; private set; }
        [field: SerializeField] public Color ShieldAbsorptionColor { get; private set; }
        [field: SerializeField] public Color ShieldActiveColor { get; private set; }
    }
}