using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "StaticData/Player")]
    public class PlayerStaticData : ScriptableObject
    {
        [field: SerializeField] public Vector3 InitialPosition { get; private set; }
        [field: SerializeField, Range(1, 200)] public int MaxHealth { get; private set; } 
    }
}