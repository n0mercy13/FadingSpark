using System;
using UnityEngine;

namespace Codebase.Services.AssetProvider
{
    public class AssetProviderService : IAssetProviderService
    {
        public T Get<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path) 
                ?? throw new InvalidOperationException(
                    $"Asset at path '{path}' is not existed"); ;
        }

        public T Instantiate<T>(string path, Vector3 at) where T : UnityEngine.Object
        {
            T prefab = Get<T>(path);               
            T gameObject = UnityEngine.Object.Instantiate(prefab, at, Quaternion.identity);

            return gameObject;
        }
    }
}