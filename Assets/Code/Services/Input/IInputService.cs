using System;
using UnityEngine;

namespace Codebase.Services.Input
{
    public interface IInputService : IService
    {
        event Action AttackButtonPressed;
        event Action ShieldButtonPressed;
        event Action ShieldButtonReleased;
        Vector3 Axis { get; }
    }
}