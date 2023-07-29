using System;
using Codebase.UI.Elements;
using Codebase.UI.Factory;

namespace Codebase.Logic
{
    public class PlayerUIHandler : IDisposable
    {
        private readonly IHealth _energy;
        private readonly IUIFactory _uiFactory;

        private BarView _bar;

        public PlayerUIHandler(
            IHealth health, 
            IUIFactory uiFactory)
        {
            _energy = health;
            _uiFactory = uiFactory; 

            _energy.Changed += OnHealthChanged;
        }

        public void Initialize() => 
            _bar = _uiFactory.HUD.GetComponentInChildren<BarView>()
                ?? throw new InvalidOperationException(
                    $"{typeof(BarView)} not found on HUD");

        public void Dispose() => 
            _energy.Changed -= OnHealthChanged;

        private void OnHealthChanged(int currentHealth, int maxHealth) => 
            _bar.SetValues(currentHealth, maxHealth);
    }
}