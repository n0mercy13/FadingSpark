using UnityEngine;
using Codebase.Logic.Enemy.StateMachine;

namespace Codebase.Logic.EnemyComponents.Behavior
{
    public partial class EnemyAsteroid : Enemy
    {
        private const float SelfDestructionTimer = 4.0f;

        private float _time;

        private void Start()
        {
            Vector3 direction = Target.transform.position - transform.position;
            StateMachine.Enter<MoveInDirectionState, Vector3>(direction);

            TickProvider.Ticked += OnTicked;
        }

        private void OnDestroy() => 
            TickProvider.Ticked -= OnTicked;

        private void OnTicked(int _)
        {
            _time += TickProvider.DeltaTime;

            if(_time > SelfDestructionTimer)            
                StateMachine.Enter<DeathState>();            
        }
    }

    public partial class EnemyAsteroid : IDamageable
    {
        public void ApplyDamage(int value) =>
            Health.Reduce(by: value);
    }
}