namespace Codebase.Services.Input
{
    public interface ILockable
    {
        void LockGameplayControls();
        void UnlockGameplayControls();
    }
}