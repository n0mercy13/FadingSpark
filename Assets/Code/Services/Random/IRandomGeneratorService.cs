using UnityEngine;

namespace Codebase.Services.RandomGenerator
{
    public interface IRandomGeneratorService
    {
        Vector2 GetPositionOutsideViewport();
    }
}
