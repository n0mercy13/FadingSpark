namespace Codebase.Infrastructure.StateMachine
{
    public interface IPayloaderState : IExitableState
    {
        void Enter();
    }
}