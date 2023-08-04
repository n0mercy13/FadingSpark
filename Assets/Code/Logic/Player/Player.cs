using Zenject;
using UnityEngine;

namespace Codebase.Logic.PlayerComponents
{
    public class Player : MonoBehaviour
	{
        private IEnergy _energy;
        private PlayerMover _playerMover;
        private PlayerUIHandler _uiHandler;
        private PlayerWeaponHandler _weaponHandler;
        private ShieldHandler _shieldHandler;

        [Inject]
        private void Construct(
            IEnergy energy,
            PlayerMover playerMover,
            PlayerUIHandler uiHandler,
            PlayerWeaponHandler weaponHandler,
            ShieldHandler shieldHandler)
        {
            _energy = energy;
            _playerMover = playerMover;
            _uiHandler = uiHandler;
            _weaponHandler = weaponHandler;
            _shieldHandler = shieldHandler;
        }

        private void Start() => 
            InitializeComponents();

        private void OnDestroy() => 
            DisposeComponents();

        public IEnergy Energy => _energy;

        private void InitializeComponents()
        {
            _uiHandler.Initialize();
            _weaponHandler.Initialize(this);
            _shieldHandler.Initialize();
        }

        private void DisposeComponents()
        {
            _playerMover.Dispose();
            _uiHandler.Dispose();
            _weaponHandler.Dispose();
            _shieldHandler.Dispose();
        }
    }
}