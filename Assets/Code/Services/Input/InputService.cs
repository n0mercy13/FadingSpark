using System;
using UnityEngine;

namespace Codebase.Services.Input
{
    public class InputService : IInputService
    {
        public Vector3 Axis => throw new NotImplementedException();

        public event Action AttackButtonPressed;
        public event Action ShieldButtonPressed;
        public event Action ShieldButtonReleased;
    }
}