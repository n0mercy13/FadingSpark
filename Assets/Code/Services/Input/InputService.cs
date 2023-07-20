using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Codebase.Services.Input
{
    public partial class InputService : IInputService
    {
        private readonly InputControls _controls;

        public InputService()
        {
            _controls = new InputControls();
            _controls.Enable();

            _controls.Movements.Attack.performed += OnAttackPressed;
            _controls.Movements.Shield.performed += OnShieldPressed;
            _controls.Movements.Shield.canceled += OnShieldCanceled;
        }

        public event Action AttackButtonPressed = delegate { };
        public event Action ShieldButtonPressed = delegate { };
        public event Action ShieldButtonReleased = delegate { };

        public Vector3 Axis => 
            _controls.Movements.Axis.ReadValue<Vector3>();

        private void OnAttackPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
                AttackButtonPressed.Invoke();
        }

        private void OnShieldPressed(InputAction.CallbackContext context)
        {
            if(context.performed)
                ShieldButtonPressed.Invoke();
        }

        private void OnShieldCanceled(InputAction.CallbackContext context)
        {
            if(context.canceled)
                ShieldButtonReleased.Invoke();
        }
    }

    public partial class InputService : IDisposable
    {
        public void Dispose()
        {
            _controls.Movements.Attack.performed -= OnAttackPressed;
            _controls.Movements.Shield.performed -= OnShieldPressed;
            _controls.Movements.Shield.canceled -= OnShieldCanceled;
        }
    }
}