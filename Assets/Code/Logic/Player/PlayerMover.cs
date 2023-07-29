using System;
using Codebase.Services.Input;
using Codebase.StaticData;
using Codebase.Services.Tick;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    public class PlayerMover : IDisposable
    {
        private readonly Player _player;
        private readonly ITickProviderService _tickProvider;
        private readonly IInputService _inputService;
        private readonly float _movementSpeed;

        public PlayerMover(
            Player player,
            IInputService inputService,
            ITickProviderService tickProvider,
            IStaticDataService staticDataService)
        {
            _player = player;
            _inputService = inputService;
            _tickProvider = tickProvider;

            _movementSpeed = staticDataService.ForPlayer().Speed;

            tickProvider.Ticked += OnTick;
        }

        public void Dispose() => 
            _tickProvider.Ticked -= OnTick;

        private void OnTick(int _) => 
            MovePlayer();

        private void MovePlayer()
        {
            if(_inputService.Axis.sqrMagnitude >= Constants.Game.Epsilon)
            {
                _player.transform.position +=
                    _movementSpeed * _tickProvider.DeltaTime * _inputService.Axis;                
            }
        }
    }    
}