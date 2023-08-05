using System;
using System.Collections.Generic;
using UnityEngine;
using Codebase.Extensions;
using Codebase.UI.Elements;
using Codebase.UI.Factory;

namespace Codebase.UI.Manager
{
    public partial class UIManager
    {
        private readonly IUIFactory _uiFactory;
        private readonly Dictionary<Type, IHideableUI> _uiElements;

        public UIManager(IUIFactory factory)
        {
            _uiFactory = factory;

            _uiElements = new Dictionary<Type, IHideableUI>();
        }
    }

    public partial class UIManager : IUIManager
    {
        public IHideableUI OpenUIElement<TUIElement>() where TUIElement : MonoBehaviour, IHideableUI
        {
            if (_uiElements.TryGetValue(
                typeof(TUIElement), out IHideableUI uiElement))
            {
                uiElement.Open();

                return uiElement;
            }
            else
            {
                return _uiFactory.Create<TUIElement>();
            }
        }

        public void CloseUIElement<TUIElement>() where TUIElement : IHideableUI
        {
            if (_uiElements.TryGetValue(typeof(TUIElement), out IHideableUI uiElement))
                uiElement.Hide();
        }

        public bool TryGetUIComponent<TUIElement, TUIComponent>(out TUIComponent uiComponent)
            where TUIElement : MonoBehaviour, IHideableUI
            where TUIComponent : MonoBehaviour
        {
            if (_uiElements.TryGetValue(typeof(TUIElement), out IHideableUI uiElement)
                && uiElement is MonoBehaviour uiBehavior
                && uiBehavior.TryGetComponentInChildren(out uiComponent))
            {
                return true;
            }
            else
            {
                uiComponent = null;
                return false;
            }
        }
    }
}