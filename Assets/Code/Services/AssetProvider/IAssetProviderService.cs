using UnityEngine;

namespace Codebase.Services.AssetProvider
{
    public interface IAssetProviderService : IService
    {
        TComponent GetPrefab<TComponent>(string path) where TComponent : MonoBehaviour;
        TComponent Instantiate<TComponent>(string path, Vector3 at) where TComponent : MonoBehaviour;
    }
}