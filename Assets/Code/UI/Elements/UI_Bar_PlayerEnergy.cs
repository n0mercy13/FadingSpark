using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Elements
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_Bar_PlayerEnergy : MonoBehaviour
    {
        [SerializeField] private Image _energyBarView;
        [SerializeField] private TMP_Text _energyValueLabel;

        private CanvasGroup _canvasGroup;

        private void OnValidate()
        {
            if (_energyBarView == null)
                throw new ArgumentNullException(nameof(_energyBarView));

            if (_energyValueLabel == null)
                throw new ArgumentNullException(nameof(_energyValueLabel));
        }

        private void Awake() =>
            _canvasGroup = GetComponent<CanvasGroup>();

        public void Hide() =>
            _canvasGroup.alpha = 0;

        public void SetValues(float currentValue, float maxValue)
        {
            SetBarView(currentValue, maxValue);
            SetEnergyValueText(currentValue, maxValue);
        }

        private void SetEnergyValueText(float currentValue, float maxValue) =>
            _energyValueLabel.text = $"{currentValue} / {maxValue}";

        private void SetBarView(float currentValue, float maxValue) =>
            _energyBarView.fillAmount = currentValue / maxValue;
    }
}