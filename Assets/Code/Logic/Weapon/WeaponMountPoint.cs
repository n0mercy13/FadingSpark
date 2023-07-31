using UnityEngine;

namespace Codebase.Logic.Weapons
{
    public class WeaponMountPoint : MonoBehaviour
    {
        [field: SerializeField] public WeaponTypes Type { get; private set; }
    }
}
