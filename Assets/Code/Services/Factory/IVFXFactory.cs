using Codebase.Logic.VFX;

namespace Codebase.Services.Factory
{
    public interface IVFXFactory
    {
        TEffect Create<TEffect>() where TEffect : VFX;
    }
}