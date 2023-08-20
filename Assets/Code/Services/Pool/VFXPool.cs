using Codebase.Logic.VisualEffects;
using Codebase.Services.Factory;
using UnityEngine;

namespace Codebase.Services.Pool
{
    public partial class VFXPool : IVFXPool
    {
        public TVFX Spawn<TVFX>(Vector3 spawnPosition) where TVFX : VFX
        {
            throw new System.NotImplementedException();
        }
    }
}