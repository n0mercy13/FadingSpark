namespace Codebase.Infrastructure.StateMachine
{
    public interface IPayloaderState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}