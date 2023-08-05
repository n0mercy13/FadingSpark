using System;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Elements
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_Bar_PlayerEnergy : MonoBehaviour
    {
        [SerializeField] private Image _barView;

        private CanvasGroup _canvasGroup;

        private void OnValidate()
        {
            if(_barView == null)
                throw new ArgumentNullException(nameof(_barView));
        }

        private void Awake() => 
            _canvasGroup = GetComponent<CanvasGroup>();

        public void Hide() =>
            _canvasGroup.alpha = 0;

        public void SetValues(float currentValue, float maxValue) =>
            _barView.fillAmount = currentValue / maxValue;
    }
}