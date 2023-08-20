using UnityEngine;

namespace Codebase.Logic.VisualEffects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class VFX : MonoBehaviour
    {
        private void OnParticleSystemStopped() => 
            gameObject.SetActive(false);
    }
}