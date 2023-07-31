using System;
using Zenject;
using UnityEngine;

namespace Codebase.Logic.PlayerComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Player : MonoBehaviour, IDamageable
	{
        private PlayerMover _playerMover;
        private PlayerUIHandler _uiHandler;
        private PlayerWeaponHandler _weaponHandler;
        private IHealth _energy;

        [Inject]
        private void Construct(
            PlayerMover playerMover,
            PlayerUIHandler uiHandler,
            PlayerWeaponHandler weaponHandler,
            IHealth energy)
        {
            _playerMover = playerMover;
            _uiHandler = uiHandler;
            _weaponHandler = weaponHandler;
            _energy = energy;
        }

        private void Start() => 
            InitializeComponents();

        private void OnDestroy() => 
            DisposeComponents();

        public IHealth Energy => _energy;

        public void ApplyDamage(int value) => 
            _energy.Reduce(by: value);

        private void InitializeComponents()
        {
            _uiHandler.Initialize();
            _weaponHandler.Initialize();
        }

        private void DisposeComponents()
        {
            _playerMover.Dispose();
            _uiHandler.Dispose();
            _weaponHandler.Dispose();
        }
    }
}