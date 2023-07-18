using System;

namespace Codebase.Data
{
    [Serializable]
    public class PlayerState
    {
        public int MaxHealth;
        public int CurrentHealth;

        public void Reset()
        {
            CurrentHealth = MaxHealth;
        }
    }
}