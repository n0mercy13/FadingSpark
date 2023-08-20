using Zenject;
using UnityEngine;
using Codebase.Services.AssetProvider;
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

        private void SetParentFor<TUIElement>(TUIElement uIElement) 
            where TUIElement : MonoBehaviour, IHideableUI
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
            TUIElement prefab = _assetProvider.GetPrefab<TUIElement>();
            TUIElement uIElement = _container
                .InstantiatePrefabForComponent<TUIElement>(prefab);

            SetParentFor(uIElement);

            return uIElement;
        }
    }    
}