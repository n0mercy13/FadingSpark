using Codebase.Services;
using Codebase.UI.Elements;
using UnityEngine;

namespace Codebase.UI.Factory
{
    public interface IUIFactory : IService
    {
        TUIElement Create<TUIElement>() where TUIElement : MonoBehaviour, IHideableUI;
    }
}