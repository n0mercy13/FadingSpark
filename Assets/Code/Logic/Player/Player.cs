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
        private IHealth _energy;

        [Inject]
        private void Construct(
            PlayerMover playerMover,
            PlayerUIHandler uiHandler,
            IHealth energy)
        {
            _playerMover = playerMover;
            _uiHandler = uiHandler;
            _energy = energy;
        }

        private void Start() => 
            _uiHandler.Initialize();

        public void ApplyDamage(int value) => 
            _energy.Reduce(by: value);
    }
}