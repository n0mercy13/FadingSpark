using Codebase.Infrastructure.StateMachine;

namespace Codebase.Logic.Enemy.StateMachine
{
    internal class IdleState : IState
    {
        private readonly EnemyStateMachine enemyStateMachine;

        public IdleState(EnemyStateMachine enemyStateMachine)
        {
            this.enemyStateMachine = enemyStateMachine;
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