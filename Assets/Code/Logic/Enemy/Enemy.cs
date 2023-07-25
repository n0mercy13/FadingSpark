using Zenject;
using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Logic.Enemy.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;

namespace Codebase.Logic.EnemyComponents
{
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(EnemyCollisionHandler))]
    [RequireComponent(typeof(EnemyWeaponHandler))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyTypes _type;

        private Player _target;
        private Health _health;
        private EnemyStateMachine _enemyStateMachine;

        private EnemyMover _mover;
        private EnemyCollisionHandler _collisionHandler;
        private EnemyWeaponHandler _weaponHandler;

        [Inject]
        public void Construct(Player target, IStaticDataService staticDataService)
        {
            _target = target;            

            GetComponents();
            Initialize(staticDataService);
        }

        private void GetComponents()
        {
            _mover = GetComponent<EnemyMover>();
            _collisionHandler = GetComponent<EnemyCollisionHandler>();
            _weaponHandler = GetComponent<EnemyWeaponHandler>();
        }

        public void Initialize(IStaticDataService staticDataService)
        {
            EnemyStaticData enemyData = staticDataService.ForEnemy(_type);

            _enemyStateMachine = enemyData.AI;
            _health = new Health(enemyData.MaxHealth);
            _mover.Initialize(enemyData.Speed);
            _weaponHandler.Initialize(enemyData.Weapons);
        }
    }
}