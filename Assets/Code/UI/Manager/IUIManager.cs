using UnityEngine;
using Codebase.UI.Elements;

namespace Codebase.UI.Manager
{
    public interface IUIManager
    {
        IHideableUI OpenUIElement<TUIElement>() where TUIElement : MonoBehaviour, IHideableUI;
        void CloseUIElement<TUIElement>() where TUIElement : IHideableUI;
        bool TryGetUIComponent<TUIElement, TUIComponent>(out TUIComponent uiComponent) 
            where TUIElement : MonoBehaviour, IHideableUI
            where TUIComponent : MonoBehaviour;
    }
}