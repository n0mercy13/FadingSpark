namespace Codebase.Infrastructure.StateMachine
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}