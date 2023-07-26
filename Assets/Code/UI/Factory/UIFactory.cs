using UnityEngine;
using Codebase.Services.AssetProvider;
using Codebase.StaticData;

namespace Codebase.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProviderService _assetProvider;

        private UIRoot _uiRoot;

        public UIFactory(IAssetProviderService assetProvider) => 
            _assetProvider = assetProvider;

        public GameObject HUD { get; private set; }

        public void CreateHUD()
        {
            GameObject hud = _assetProvider.Instantiate<GameObject>(Constants.AssetPath.HUD, Vector3.zero);
            hud.transform.SetParent(_uiRoot.transform, false);
            HUD = hud;
        }

        public void CreateMenu()
        {
            throw new System.NotImplementedException();
        }

        public void CreateUIRoot() => 
            _uiRoot = _assetProvider.Instantiate<UIRoot>(Constants.AssetPath.UIRoot, Vector3.zero);
    }
}