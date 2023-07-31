using System;
using Codebase.UI.Elements;
using Codebase.UI.Factory;

namespace Codebase.Logic
{
    public class PlayerUIHandler : IDisposable
    {
        private readonly IUIFactory _uiFactory;
        private readonly IHealth _energy;
        private BarView _bar;

        public PlayerUIHandler(
            IUIFactory uiFactory, 
            IHealth energy)
        {
            _uiFactory = uiFactory;
            _energy = energy;

            _energy.Changed += OnHealthChanged;
        }

        public void Initialize()
        {
            _bar = _uiFactory.HUD.GetComponentInChildren<BarView>()
                ?? throw new InvalidOperationException(
                    $"{typeof(BarView)} not found on HUD");
        }

        public void Dispose() => 
            _energy.Changed -= OnHealthChanged;

        private void OnHealthChanged(int current, int max) => 
            _bar.SetValues(current, max);
    }
}