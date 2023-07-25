using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;

namespace Codebase.Logic.Enemy.StateMachine
{
    public class InitializeState : IPayloaderState
    {
        private EnemyStateMachine enemyStateMachine;
        private IStaticDataService staticDataService;

        public InitializeState(EnemyStateMachine enemyStateMachine, IStaticDataService staticDataService)
        {
            this.enemyStateMachine = enemyStateMachine;
            this.staticDataService = staticDataService;
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