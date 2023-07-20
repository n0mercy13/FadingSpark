using Codebase.Services.AssetProvider;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProviderService _assetProvider;

        private UIRoot _uiRoot;

        public UIFactory(IAssetProviderService assetProvider) => 
            _assetProvider = assetProvider;

        public void CreateHUD()
        {
            GameObject hud = _assetProvider.Instantiate<GameObject>(Constants.AssetPath.HUD, Vector3.zero);
            hud.transform.parent = _uiRoot.transform;
        }

        public void CreateMenu()
        {
            throw new System.NotImplementedException();
        }

        public void CreateUIRoot() => 
            _uiRoot = _assetProvider.Instantiate<UIRoot>(Constants.AssetPath.UIRoot, Vector3.zero);
    }
}