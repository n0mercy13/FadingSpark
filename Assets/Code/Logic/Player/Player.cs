using Zenject;
using UnityEngine;

namespace Codebase.Logic.PlayerComponents
{
    public class Player : MonoBehaviour
	{
        private IEnergy _energy;
        private PlayerMover _playerMover;
        private PlayerWeaponHandler _weaponHandler;
        private ShieldHandler _shieldHandler;

        [Inject]
        private void Construct(
            IEnergy energy,
            PlayerMover playerMover,
            PlayerWeaponHandler weaponHandler,
            ShieldHandler shieldHandler)
        {
            _energy = energy;
            _playerMover = playerMover;
            _weaponHandler = weaponHandler;
            _shieldHandler = shieldHandler;
        }

        private void Start() => 
            InitializeComponents();

        private void OnDestroy() => 
            DisposeComponents();

        public void Reset()
        {
            _energy.Reset();
        }

        private void InitializeComponents()
        {
            _weaponHandler.Initialize(this);
            _shieldHandler.Initialize();
        }

        private void DisposeComponents()
        {
            _playerMover.Dispose();
            _weaponHandler.Dispose();
            _shieldHandler.Dispose();
        }
    }
}