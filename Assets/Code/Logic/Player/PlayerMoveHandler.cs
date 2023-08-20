using System;
using UnityEngine;
using Codebase.Services.Input;
using Codebase.StaticData;
using Codebase.Services.Tick;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    public partial class PlayerMoveHandler
    {
        private readonly Transform _playerTransform;
        private readonly ITickProviderService _tickProvider;
        private readonly IInputService _input;
        private readonly float _movementSpeed;
        private readonly float _rotationSpeed;

        public PlayerMoveHandler(
            Player player,
            IInputService inputService,
            ITickProviderService tickProvider,
            IStaticDataService staticDataService)
        {
            _playerTransform = player.transform;
            _input = inputService;
            _tickProvider = tickProvider;

            PlayerStaticData playerData = staticDataService.ForPlayer();

            _movementSpeed = playerData.MoveSpeed;
            _rotationSpeed = playerData.RotationSpeed;

            _tickProvider.Ticked += OnTick;
        }

        private Vector3 _playerUp
        {
            get => _playerTransform.up;
            set => _playerTransform.up = value;
        }
        private Vector3 _pointerDirection =>
            (_input.PointerPosition - _playerTransform.position).normalized;
        private bool _isPlayerUpAlignsWithPointer =>
            Mathf.Approximately(_playerUp.x, _pointerDirection.x)
            && Mathf.Approximately(_playerUp.y, _pointerDirection.y);

        private void OnTick(int _)
        {
            MovePlayer();
            RotatePlayer();
        }

        private void RotatePlayer()
        {
            if (_isPlayerUpAlignsWithPointer)
                return;

            _playerUp = Vector3.MoveTowards(
                _playerUp, _pointerDirection, _tickProvider.DeltaTime * _rotationSpeed);
        }

        private void MovePlayer()
        {
            if(_input.Movement.sqrMagnitude >= Constants.Game.Epsilon)
            {
                _playerTransform.position +=
                    _movementSpeed * _tickProvider.DeltaTime * _input.Movement;                
            }
        }
    }  
    
    public partial class PlayerMoveHandler : IDisposable
    {
        public void Dispose() => 
            _tickProvider.Ticked -= OnTick;
    }
}