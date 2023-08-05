using System;
using UnityEngine;

namespace Codebase.Services.AssetProvider
{
    public class AssetProviderService : IAssetProviderService
    {
        public TComponent GetPrefab<TComponent>(string path) where TComponent : MonoBehaviour
        {
            return Resources.Load<TComponent>(path) 
                ?? throw new InvalidOperationException(
                    $"Asset at path '{path}' is not existed"); ;
        }

        public TComponent Instantiate<TComponent>(string path, Vector3 at) where TComponent : MonoBehaviour
        {
            TComponent prefab = GetPrefab<TComponent>(path);               
            TComponent gameObject = UnityEngine.Object.Instantiate(prefab, at, Quaternion.identity);

            return gameObject;
        }
    }
}