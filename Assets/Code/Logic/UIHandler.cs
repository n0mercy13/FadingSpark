using System;
using Codebase.UI.Elements;

namespace Codebase.Logic
{
    public class UIHandler : IDisposable
    {
        private readonly IHealth _health;
        private readonly BarView _bar;

        public UIHandler(IHealth health, BarView bar)
        {
            _health = health;
            _bar = bar;

            _health.Changed += OnHealthChanged;
        }

        public void Dispose() => 
            _health.Changed -= OnHealthChanged;

        private void OnHealthChanged(int currentHealth, int maxHealth) => 
            _bar.SetValues(currentHealth, maxHealth);
    }
}