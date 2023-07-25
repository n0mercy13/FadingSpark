using System;
using UnityEngine;
using Codebase.Services.Input;
using Codebase.StaticData;
using Codebase.Services.Tick;

namespace Codebase.Logic.PlayerComponents
{
    public class PlayerMover : IDisposable
    {
        private readonly CharacterController _characterController;
        private readonly ITickProviderService _tickProvider;
        private readonly IInputService _inputService;
        private readonly float _movementSpeed;

        public PlayerMover(
            CharacterController characterController,
            IInputService inputService,
            ITickProviderService tickProvider,
            float movementSpeed)
        {
            _characterController = characterController;
            _inputService = inputService;
            _tickProvider = tickProvider;
            _movementSpeed = movementSpeed;

            tickProvider.Ticked += OnTick;
        }

        public void Dispose() => 
            _tickProvider.Ticked -= OnTick;

        private void OnTick(int _)
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            if(_inputService.Axis.sqrMagnitude >= Constants.Game.Epsilon)
            {
                _characterController.Move(
                    _movementSpeed * _tickProvider.DeltaTime * _inputService.Axis);                
            }
        }
    }    
}