using Codebase.Infrastructure.StateMachine;

namespace Codebase.Logic.Enemy.StateMachine
{
    internal class AttackState : IState
    {
        private EnemyStateMachine _enemyStateMachine;

        public AttackState(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}