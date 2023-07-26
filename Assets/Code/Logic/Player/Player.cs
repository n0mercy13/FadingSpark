using System;
using Zenject;
using UnityEngine;
using Codebase.Services.Input;
using Codebase.Services.Tick;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Codebase.UI.Elements;
using Codebase.UI.Factory;

namespace Codebase.Logic.PlayerComponents
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour, IDamageable
	{
        private PlayerMover _playerMover;
        private UIHandler _uiHandler;
        private IHealth _energy;

        [Inject]
        private void Construct(
            IInputService inputService, 
            ITickProviderService tickProvider, 
            IStaticDataService staticDataService,
            IUIFactory uiFactory)
        {
            CharacterController characterController = GetComponent<CharacterController>();

            PlayerStaticData staticData = staticDataService.ForPlayer();
            float movementSpeed = staticData.Speed;
            int maxEnergy = staticData.MaxHealth;
            BarView healthBar = uiFactory.HUD.GetComponentInChildren<BarView>();

            _playerMover = new PlayerMover(characterController, inputService, tickProvider, movementSpeed);
            _energy = new Energy(maxEnergy);
            _uiHandler = new UIHandler(_energy, healthBar);
        }

        public void ApplyDamage(int value) => 
            _energy.Reduce(by: value);
    }
}