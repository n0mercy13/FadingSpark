using UnityEngine;

namespace Codebase.Services.Pool
{
    public interface IPool<TSpawnableBase> where TSpawnableBase : MonoBehaviour
    {
        TSpawnable Spawn<TSpawnable>(Vector3 position) where TSpawnable : TSpawnableBase;
    }
}