using System;
using Codebase.Logic.PlayerComponents.Shield;
using Codebase.Services.Input;

namespace Codebase.Logic.PlayerComponents
{
    public class ShieldHandler : IDisposable
    {
        private readonly ShieldStateMachine _stateMachine;
        private readonly IInputService _input;

        public ShieldHandler(
            ShieldStateMachine stateMachine, 
            IInputService input)
        {
            _stateMachine = stateMachine;
            _input = input;

            _input.ShieldButtonPressed += OnShieldButtonPressed;
            _input.ShieldButtonReleased += OnShieldButtonReleased;
        }

        public void Initialize() => 
            _stateMachine.Enter<InactiveState>();

        public void Dispose()
        {
            _input.ShieldButtonPressed -= OnShieldButtonPressed;
            _input.ShieldButtonReleased -= OnShieldButtonReleased;
        }

        private void OnShieldButtonPressed() => 
            _stateMachine.Enter<ActivationState>();

        private void OnShieldButtonReleased() => 
            _stateMachine.Enter<DeactivationState>();
    }
}