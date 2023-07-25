using Zenject;
using UnityEngine;
using Codebase.Services.Input;
using Codebase.Services.Tick;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
	{
        private PlayerMover _playerMover;

        [Inject]
        private void Construct(
            IInputService inputService, 
            ITickProviderService tickProvider, 
            IStaticDataService staticData)
        {
            CharacterController characterController = GetComponent<CharacterController>();
            float movementSpeed = staticData.ForPlayer().Speed;

            _playerMover = new PlayerMover(characterController, inputService, tickProvider, movementSpeed);
        }
	} 
}