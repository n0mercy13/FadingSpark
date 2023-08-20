using Codebase.Logic.VisualEffects;
using UnityEngine;

namespace Codebase.Services.Pool
{
    public interface IVFXPool
    {
        TVFX Spawn<TVFX>(Vector3 spawnPosition) where TVFX : VFX;
    }
}