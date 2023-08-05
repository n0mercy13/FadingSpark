using Codebase.Services.Tick;

namespace Codebase.Services.Pause
{
    public partial class PauseService
    {
        private readonly ITickProviderService _tickProviderService;

        public PauseService(ITickProviderService tickProviderService) => 
            _tickProviderService = tickProviderService;
    }

    public partial class PauseService : IPauseService
    {
        public void Pause() => 
            _tickProviderService.Stop();

        public void Resume() => 
            _tickProviderService.Resume();
    }
}