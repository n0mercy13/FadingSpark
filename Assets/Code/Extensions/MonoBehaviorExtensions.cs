using UnityEngine;

namespace Codebase.Extensions
{
    public static class MonoBehaviorExtensions
    {
        public static bool TryGetComponentInChildren<TComponent>(
            this MonoBehaviour gameObject, out TComponent component) where TComponent : MonoBehaviour
        {
            component = gameObject.GetComponentInChildren<TComponent>();

            return component != null;
        }
    }
}
