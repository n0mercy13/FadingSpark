using Codebase.Infrastructure.StateMachine;

namespace Codebase.Logic.Enemy.StateMachine
{
    internal class MoveState : IState
    {
        private readonly EnemyStateMachine _enemyStateMachine;

        public MoveState(EnemyStateMachine enemyStateMachine)
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