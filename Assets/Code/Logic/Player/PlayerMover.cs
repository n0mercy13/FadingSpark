using System;
using UnityEngine;
using Codebase.Services.Input;
using Codebase.StaticData;
using Codebase.Services.Tick;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    public partial class PlayerMover
    {
        private readonly Player _player;
        private readonly ITickProviderService _tickProvider;
        private readonly IInputService _input;
        private readonly float _movementSpeed;
        private readonly float _rotationSpeed;

        public PlayerMover(
            Player player,
            IInputService inputService,
            ITickProviderService tickProvider,
            IStaticDataService staticDataService)
        {
            _player = player;
            _input = inputService;
            _tickProvider = tickProvider;

            _movementSpeed = staticDataService.ForPlayer().MoveSpeed;
            _rotationSpeed = staticDataService.ForPlayer().RotationSpeed;

            _tickProvider.Ticked += OnTick;
        }

        private Vector3 _up
        {
            get => _player.transform.up;
            set => _player.transform.up = value;
        }
        private Vector3 _pointerDirection =>
            (_input.PointerPosition - _player.transform.position).normalized;
        private bool _isPlayerUpAlignsWithPointer =>
            Mathf.Approximately(_up.x, _pointerDirection.x)
            && Mathf.Approximately(_up.y, _pointerDirection.y);

        private void OnTick(int _)
        {
            MovePlayer();
            RotatePlayer();
        }

        private void RotatePlayer()
        {
            if (_isPlayerUpAlignsWithPointer)
                return;

            _up = Vector3.MoveTowards(
                _up, _pointerDirection, _tickProvider.DeltaTime * _rotationSpeed);
        }

        private void MovePlayer()
        {
            if(_input.Movement.sqrMagnitude >= Constants.Game.Epsilon)
            {
                _player.transform.position +=
                    _movementSpeed * _tickProvider.DeltaTime * _input.Movement;                
            }
        }
    }  
    
    public partial class PlayerMover : IDisposable
    {
        public void Dispose() =>
            _tickProvider.Ticked -= OnTick;
    }
}