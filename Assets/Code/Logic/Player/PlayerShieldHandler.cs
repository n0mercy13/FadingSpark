using System;
using Codebase.Logic.PlayerComponents.Shield;
using Codebase.Services.Input;

namespace Codebase.Logic.PlayerComponents
{
    public partial class PlayerShieldHandler
    {
        private readonly ShieldStateMachine _stateMachine;
        private readonly IInputService _input;

        public PlayerShieldHandler(
            ShieldStateMachine stateMachine, 
            IInputService input)
        {
            _stateMachine = stateMachine;
            _input = input;

            _input.ShieldButtonPressed += OnShieldButtonPressed;
            _input.ShieldButtonReleased += OnShieldButtonReleased;
        }

        public void Reset() => 
            _stateMachine.Enter<InactiveState>();

        private void OnShieldButtonPressed() => 
            _stateMachine.Enter<ActivationState>();

        private void OnShieldButtonReleased() => 
            _stateMachine.Enter<DeactivationState>();
    }

    public partial class PlayerShieldHandler : IDisposable
    {
        public void Dispose()
        {
            _input.ShieldButtonPressed -= OnShieldButtonPressed;
            _input.ShieldButtonReleased -= OnShieldButtonReleased;
        }
    }
}