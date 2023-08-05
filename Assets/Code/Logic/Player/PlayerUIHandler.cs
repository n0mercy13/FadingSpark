using System;
using Codebase.UI.Elements;
using Codebase.UI.Manager;

namespace Codebase.Logic.PlayerComponents
{
    public partial class PlayerUIHandler
    {
        private readonly IUIManager _uiManager;
        private readonly IEnergy _energy;
        private UI_Bar_PlayerEnergy _energyBar;

        public PlayerUIHandler(
            IUIManager uiManager, 
            IEnergy energy)
        {
            _uiManager = uiManager;
            _energy = energy;

            _energy.Changed += OnHealthChanged;
        }

        public void Initialize() => 
            SetEnergyBar();

        private void SetEnergyBar()
        {
            if (_uiManager.TryGetUIComponent<UI_HUD, UI_Bar_PlayerEnergy>
                            (out UI_Bar_PlayerEnergy energyBar))
                _energyBar = energyBar;
            else
                throw new InvalidOperationException(
                    $"{typeof(UI_Bar_PlayerEnergy)} not found on HUD");
        }

        private void OnHealthChanged(int current, int max) => 
            _energyBar.SetValues(current, max);
    }

    public partial class PlayerUIHandler : IDisposable
    {
        public void Dispose() =>
            _energy.Changed -= OnHealthChanged;
    }
}