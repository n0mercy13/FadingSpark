using UnityEngine;

namespace Codebase.Services.AssetProvider
{
    public interface IAssetProviderService : IService
    {
        T Instantiate<T>(string path, Vector3 at) where T : UnityEngine.Object;
    }
}