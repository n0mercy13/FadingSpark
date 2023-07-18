using System;

namespace Codebase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public PlayerState PlayerState;
        public WorldData WorldData;

        public PlayerProgress(string initialLevelName)
        {
            WorldData = new WorldData(initialLevelName);
            PlayerState = new PlayerState();
        }
    }
}
