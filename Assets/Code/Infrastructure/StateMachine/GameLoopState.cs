namespace Codebase.Infrastructure.StateMachine
{
    public class GameLoopState : IPayloaderState
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}