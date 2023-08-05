using UnityEngine;

namespace Codebase.UI.Elements
{
    [RequireComponent(typeof(CanvasGroup))]
    public partial class HideableUI : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void Awake() => 
            _canvasGroup = GetComponent<CanvasGroup>();
    }

    public partial class HideableUI : IHideableUI
    {
        public void Hide()
        {
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void Open()
        {
            _canvasGroup.alpha = 1.0f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}