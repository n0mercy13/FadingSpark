using System;
using UnityEngine;

namespace Codebase.Services.AssetProvider
{
    public class AssetProviderService : IAssetProviderService
    {
        public T Instantiate<T>(string path, Vector3 at) where T : UnityEngine.Object
        {
            T prefab = Resources.Load<T>(path);

            if (prefab == null)
                throw new InvalidOperationException(
                    $"Asset at path '{path}' is not existed");

            T gameObject = UnityEngine.Object.Instantiate(prefab, at, Quaternion.identity);

            return gameObject;
        }
    }
}