using UnityEngine;
using Codebase.Services.AssetProvider;
using Codebase.StaticData;

namespace Codebase.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProviderService _assetProvider;

        private UIRoot _uiRoot;
        private GameObject _hud;

        public UIFactory(IAssetProviderService assetProvider) => 
            _assetProvider = assetProvider;

        public GameObject HUD => _hud;

        public void CreateHUD()
        {
            _hud = _assetProvider.Instantiate<GameObject>(Constants.AssetPath.HUD, Vector3.zero);
            _hud.transform.SetParent(_uiRoot.transform, false);
        }

        public void CreateMenu()
        {
            throw new System.NotImplementedException();
        }

        public void CreateUIRoot() => 
            _uiRoot = _assetProvider.Instantiate<UIRoot>(Constants.AssetPath.UIRoot, Vector3.zero);
    }
}