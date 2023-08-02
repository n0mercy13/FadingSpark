namespace Codebase.Logic.PlayerComponents
{
    public interface IEnergy : IHealth
    {
        void Absorb(int amount);
    }
}