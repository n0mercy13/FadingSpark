﻿using Zenject;
using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Logic.Enemy.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Codebase.Services.Tick;
using Codebase.Infrastructure;

namespace Codebase.Logic.EnemyComponents
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyTypes _type;

        protected EnemyStateMachine StateMachine;
        protected Player Target;

        private Health _health;
        private EnemyMover _mover;
        private EnemyCollisionHandler _collisionHandler;
        private EnemyWeaponHandler _weaponHandler;

        private void OnTriggerEnter2D(Collider2D collision) => 
            _collisionHandler.OnCollision(collision);

        [Inject]
        private void Construct(
            Player target, 
            IStaticDataService staticDataService, 
            ITickProviderService tickProvider,
            ICoroutineRunner coroutineRunner)
        {
            Target = target;

            EnemyStaticData enemyData = staticDataService.ForEnemy(_type);
            float speed = enemyData.Speed;
            int maxHealth = enemyData.MaxHealth;
            int damageOnCollision = enemyData.DamageOnCollision;
            
            _mover = new EnemyMover(this, tickProvider, speed);
            _health = new Health(maxHealth);
            _collisionHandler = new EnemyCollisionHandler(coroutineRunner, damageOnCollision);
            StateMachine = new EnemyStateMachine(_mover);

            Initialize();
        }

        protected virtual void Initialize()
        {
        }
    }
}