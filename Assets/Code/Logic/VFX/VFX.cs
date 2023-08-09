using UnityEngine;

namespace Codebase.Logic.VFX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class VFX : MonoBehaviour
    {
        private void OnParticleSystemStopped() => 
            gameObject.SetActive(false);
    }
}