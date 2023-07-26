using Zenject;
using UnityEngine;
using Codebase.Services.Input;
using Codebase.Services.Tick;
using Codebase.Services.StaticData;
using System;
using Codebase.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour, IDamageable
	{
        private PlayerMover _playerMover;
        private IHealth _energy;

        [Inject]
        private void Construct(
            IInputService inputService, 
            ITickProviderService tickProvider, 
            IStaticDataService staticDataService)
        {
            CharacterController characterController = GetComponent<CharacterController>();

            PlayerStaticData staticData = staticDataService.ForPlayer();
            float movementSpeed = staticData.Speed;
            int maxEnergy = staticData.MaxHealth;

            _playerMover = new PlayerMover(characterController, inputService, tickProvider, movementSpeed);
            _energy = new Energy(maxEnergy);
        }

        public void ApplyDamage(int value) => 
            _energy.Reduce(by: value);
    }
}