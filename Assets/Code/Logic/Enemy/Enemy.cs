using Zenject;
using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Logic.Enemy.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Codebase.Services.Tick;

namespace Codebase.Logic.EnemyComponents
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyTypes _type;

        protected EnemyStateMachine StateMachine;
        protected Player Target;

        private Health _health;
        private EnemyMover _mover;
        private EnemyCollisionHandler _collisionHandler;
        private EnemyWeaponHandler _weaponHandler;

        [Inject]
        private void Construct(
            Player target, 
            IStaticDataService staticDataService, 
            ITickProviderService tickProvider)
        {
            Target = target;

            EnemyStaticData enemyData = staticDataService.ForEnemy(_type);
            float speed = enemyData.Speed;
            int maxHealth = enemyData.MaxHealth;
            
            _mover = new EnemyMover(this, tickProvider, speed);
            _health = new Health(maxHealth);
            StateMachine = new EnemyStateMachine(_mover);

            Initialize();
        }

        protected virtual void Initialize()
        {
        }
    }
}