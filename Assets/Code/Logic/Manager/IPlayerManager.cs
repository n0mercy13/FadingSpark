using UnityEngine;

namespace Codebase.Logic.PlayerComponents.Manager
{
    public interface IPlayerManager
    {
        Transform Player { get; }
        void Spawn();
    }
}