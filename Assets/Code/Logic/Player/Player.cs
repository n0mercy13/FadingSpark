using Zenject;
using UnityEngine;
using System;

namespace Codebase.Logic.PlayerComponents
{
    public partial class Player : MonoBehaviour
	{
        private PlayerMoveHandler _moveHandler;
        private PlayerWeaponHandler _weaponHandler;
        private PlayerShieldHandler _shieldHandler;
        private IEnergy _energy;

        [Inject]
        private void Construct(
            IEnergy energy,
            PlayerWeaponHandler playerWeaponHandler,
            PlayerMoveHandler playerMoveHandler,
            PlayerShieldHandler playerShieldHandler)
        {
            _energy = energy;
            _weaponHandler = playerWeaponHandler;
            _moveHandler = playerMoveHandler;
            _shieldHandler = playerShieldHandler;
        }

        private void OnEnable() => 
            Reset();

        private void OnDestroy() => 
            Dispose();

        public IEnergy Energy => _energy;

        public void Reset()
        {
            _energy.Reset();
            _shieldHandler.Reset();
        }
    }

    public partial class Player : IDisposable
    {
        public void Dispose()
        {
            _weaponHandler.Dispose();
            _moveHandler.Dispose();
            _shieldHandler.Dispose();
        }
    }
}