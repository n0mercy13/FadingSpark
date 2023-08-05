using System;
using UnityEngine;
using Codebase.Services.AssetProvider;
using Codebase.StaticData;
using Codebase.UI.Elements;

namespace Codebase.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProviderService _assetProvider;
        private UI_Root _uiRoot;

        public UIFactory(IAssetProviderService assetProvider) => 
            _assetProvider = assetProvider;

        public UI_GameOverScreen CreateGameOverScreen() =>
            CreateUIElement<UI_GameOverScreen>(Constants.AssetPath.GameOverScreen);

        public UI_HUD CreateHUD() => 
            CreateUIElement<UI_HUD>(Constants.AssetPath.HUD);

        public UI_MainMenu CreateMainMenu() =>
            CreateUIElement<UI_MainMenu>(Constants.AssetPath.MainMenu);

        public void CreateUIRoot() => 
            _uiRoot = _assetProvider.Instantiate<UI_Root>(Constants.AssetPath.UIRoot, Vector3.zero);

        private TComponent CreateUIElement<TComponent>(string assetsPath) where TComponent : MonoBehaviour
        {
            TComponent element = _assetProvider
                .Instantiate<TComponent>(assetsPath, Vector3.zero)
                ?? throw new InvalidOperationException(
                    $"UI prefab was not found at path: {assetsPath}");

            element.transform.SetParent(_uiRoot.transform, false);

            return element;
        }
    }
}