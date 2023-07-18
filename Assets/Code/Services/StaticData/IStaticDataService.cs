using Codebase.StaticData;

namespace Codebase.Services.StaticData
{
    public interface IStaticDataService
    {
        PlayerStaticData ForPlayer();
        void Load();
    }
}
