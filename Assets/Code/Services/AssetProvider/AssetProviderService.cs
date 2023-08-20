using System;
using System.Collections.Generic;
using UnityEngine;
using Codebase.Logic.VisualEffects;
using Codebase.StaticData;
using Codebase.UI.Elements;
using Codebase.Logic.PlayerComponents;
using Codebase.Logic.Collectables;

namespace Codebase.Services.AssetProvider
{
    public class AssetProviderService : IAssetProviderService
    {
        private readonly Dictionary<Type, string> _assetPaths;

        public AssetProviderService()
        {
            _assetPaths = new Dictionary<Type, string>
            {
                [typeof(Player)] = Constants.AssetPath.Player,

                [typeof(VFX_OnPlayerLaserHit)] = Constants.AssetPath.VFX.PlayerLaserHit,
                [typeof(VFX_OnPlayerActiveShieldHit)] = Constants.AssetPath.VFX.PlayerActiveShieldHit,
                [typeof(VFX_OnPlayerInactiveShieldHit)] = Constants.AssetPath.VFX.PlayerInactiveShieldHit,

                [typeof(UI_HUD)] = Constants.AssetPath.UI.HUD,
                [typeof(UI_Root)] = Constants.AssetPath.UI.Root,
                [typeof(UI_GameOverScreen)] = Constants.AssetPath.UI.GameOverScreen,
                [typeof(UI_MainMenu)] = Constants.AssetPath.UI.MainMenu,

                [typeof(Collectible_Experience)] = Constants.AssetPath.Collectables.Experience,
            };
        }

        public TComponent GetPrefab<TComponent>() where TComponent : MonoBehaviour
        {
            string assetPath = GetAssetPath<TComponent>();

            return Resources.Load<TComponent>(assetPath) 
            ?? throw new InvalidOperationException(
                $"Asset at path '{assetPath}' is not existed"); 
        }

        public TComponent Instantiate<TComponent>(string path, Vector3 at) where TComponent : MonoBehaviour
        {
            TComponent prefab = GetPrefab<TComponent>();               
            TComponent gameObject = UnityEngine.Object.Instantiate(prefab, at, Quaternion.identity);

            return gameObject;
        }

        private string GetAssetPath<TComponent>() where TComponent : MonoBehaviour
        {
            if (_assetPaths.TryGetValue(typeof(TComponent), out string path))
                return path;
            else
                throw new InvalidOperationException(
                    $"Prefab with {nameof(TComponent)} component was not found");
        }
    }
}