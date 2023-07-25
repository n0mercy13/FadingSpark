using Zenject;
using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Logic.Enemy.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Codebase.Services.Tick;

namespace Codebase.Logic.EnemyComponents
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyTypes _type;

        private Health _health;
        private EnemyStateMachine _enemyStateMachine;
        private EnemyMover _mover;
        private EnemyCollisionHandler _collisionHandler;
        private EnemyWeaponHandler _weaponHandler;

        [Inject]
        public void Construct(Player target, IStaticDataService staticDataService, ITickProviderService tickProvider)
        {
            EnemyStaticData enemyData = staticDataService.ForEnemy(_type);
            float speed = enemyData.Speed;
            int maxHealth = enemyData.MaxHealth;

            _mover = new EnemyMover(this, tickProvider, speed);
            _enemyStateMachine = new EnemyStateMachine(_mover);
            _health = new Health(maxHealth);
        }
    }
}