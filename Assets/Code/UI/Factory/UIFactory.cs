using System;
using Zenject;
using UnityEngine;
using Codebase.Services.AssetProvider;
using Codebase.StaticData;
using Codebase.UI.Elements;

namespace Codebase.UI.Factory
{
    public partial class UIFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProviderService _assetProvider;

        private Transform _uiRootTransform;

        public UIFactory(
            DiContainer container,
            IAssetProviderService assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }

        private TUIElement GetPrefab<TUIElement>() where TUIElement : MonoBehaviour, IHideableUI
        {
            string assetPath;

            if (typeof(UI_HUD) == (typeof(TUIElement)))
                assetPath = Constants.AssetPath.UI.HUD;
            else if (typeof(UI_Root) == (typeof(TUIElement)))
                assetPath = Constants.AssetPath.UI.Root;
            else if (typeof(UI_GameOverScreen) == (typeof(TUIElement)))
                assetPath = Constants.AssetPath.UI.GameOverScreen;
            else if (typeof(UI_MainMenu) == (typeof(TUIElement)))
                assetPath = Constants.AssetPath.UI.MainMenu;
            else
                throw new InvalidOperationException($"Unknown type of UI: {nameof(TUIElement)}");

            return _assetProvider.GetPrefab<TUIElement>(assetPath);
        }

        private void SetParentFor<TUIElement>(TUIElement uIElement) where TUIElement : MonoBehaviour, IHideableUI
        {
            if (typeof(UI_Root).IsAssignableFrom(typeof(TUIElement)))
                _uiRootTransform = uIElement.transform;
            else
                uIElement.transform.SetParent(_uiRootTransform, false);
        }
    }

    public partial class UIFactory : IUIFactory
    {
        public TUIElement Create<TUIElement>() where TUIElement : MonoBehaviour, IHideableUI
        {
            TUIElement prefab = GetPrefab<TUIElement>();
            TUIElement uIElement = _container
                .InstantiatePrefabForComponent<TUIElement>(prefab);

            SetParentFor(uIElement);

            return uIElement;
        }
    }    
}