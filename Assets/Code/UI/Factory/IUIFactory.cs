using Codebase.Services;
using UnityEngine;

namespace Codebase.UI.Factory
{
    public interface IUIFactory : IService
    {
        GameObject HUD { get; }

        void CreateUIRoot();
        void CreateHUD();
        void CreateMenu();
    }
}