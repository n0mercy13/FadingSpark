using System;
using UnityEngine;

namespace Codebase.Services.Input
{
    public interface IInputService : IService
    {
        event Action AttackButtonPressed;
        event Action ShieldButtonPressed;
        event Action ShieldButtonReleased;
        event Action MainMenuOpenButtonPressed;
        Vector3 Movement { get; }
        Vector3 PointerPosition { get; }
    }
}