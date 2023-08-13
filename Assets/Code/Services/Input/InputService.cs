using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Codebase.Services.Input
{
    public partial class InputService
    {
        private readonly InputControls _controls;

        private Vector3 _pointerScreenPosition;
        private Vector3 _pointerWorldPosition;

        public InputService(InputControls inputControls)
        {
            _controls = inputControls;
            _controls.Enable();

            _controls.Gameplay.Attack.performed += OnAttackPressed;
            _controls.Gameplay.Shield.performed += OnShieldPressed;
            _controls.Gameplay.Shield.canceled += OnShieldCanceled;
            _controls.Gameplay.Menu.performed += OnMenuPressed;
        }

        private void OnMenuPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
                MainMenuOpenButtonPressed.Invoke();
        }

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

        private Vector3 GetPointerPosition()
        {
            _pointerScreenPosition = Pointer.current.position.ReadValue();
            _pointerWorldPosition = Camera.main.ScreenToWorldPoint(_pointerScreenPosition);
            _pointerWorldPosition.z = 0;

            return _pointerWorldPosition;
        }
    }

    public partial class InputService : IInputService
    {
        public event Action AttackButtonPressed = delegate { };
        public event Action ShieldButtonPressed = delegate { };
        public event Action ShieldButtonReleased = delegate { };
        public event Action MainMenuOpenButtonPressed = delegate { };

        public Vector3 Movement =>
            _controls.Gameplay.Axis.ReadValue<Vector3>();

        public Vector3 PointerPosition => 
            GetPointerPosition();
    }

    public partial class InputService : ILockable
    {
        public void LockGameplayControls() => 
            _controls.Gameplay.Disable();

        public void UnlockGameplayControls() =>
            _controls.Gameplay.Enable();
    }

    public partial class InputService : IDisposable
    {
        public void Dispose()
        {
            _controls.Gameplay.Attack.performed -= OnAttackPressed;
            _controls.Gameplay.Shield.performed -= OnShieldPressed;
            _controls.Gameplay.Shield.canceled -= OnShieldCanceled;
            _controls.Gameplay.Menu.performed -= OnMenuPressed;
        }
    }
}