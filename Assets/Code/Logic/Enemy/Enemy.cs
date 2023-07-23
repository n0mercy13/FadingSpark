using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Logic.Weapons;
using Codebase.Logic.Enemy.StateMachine;
using Zenject;
using Codebase.Services.StaticData;

namespace Codebase.Logic.EnemyComponents
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyTypes _type;

        private Player _target;
        private Health _health;
        private EnemyStateMachine _stateMachine;
        private IStaticDataService _staticDataService;

        [Inject]
        public void Construct(Player target, IStaticDataService staticDataService)
        {
            _target = target;    
            _staticDataService = staticDataService;
        }

        public void Initialize(Weapon weapon)
        {

        }
    }
}
