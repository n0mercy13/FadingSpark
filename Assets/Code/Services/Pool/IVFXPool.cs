using UnityEngine;
using Codebase.Logic.VFX;

namespace Codebase.Services.Pool
{
    public interface IVFXPool
    {
        void Spawn<TEffect>(Vector3 position) where TEffect : VFX;
    }
}