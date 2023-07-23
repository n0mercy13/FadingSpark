using UnityEngine;

namespace Codebase.Services.AssetProvider
{
    public interface IAssetProviderService : IService
    {
        T Get<T>(string path) where T : Object;
        T Instantiate<T>(string path, Vector3 at) where T : Object;
    }
}