using System;
using Codebase.Data;
using Codebase.Services.PersistentProgress;
using Codebase.Services.SaveLoad;
using Codebase.StaticData;

namespace Codebase.Infrastructure.StateMachine
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(
            GameStateMachine stateMachine, 
            IPersistentProgressService progressService, 
            ISaveLoadService saveLoadService) 
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress =
            _saveLoadService.LoadProgress()
            ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(Constants.Level.InitialLevelName);
        }
    }
}