using Zenject;
using UnityEngine;
using Codebase.Infrastructure.Install;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using IInitializable = Codebase.Services.Initialize.IInitializable;
using Codebase.Services.Initialize;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class ActiveState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly SpriteColorHandler _colorHandler;
        private Color _activeColor;

        public ActiveState(
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler, 
            IStaticDataService staticDataService,
            IInitializationService initializationService)
        {
            _colorHandler = colorHandler;
            _staticDataService = staticDataService;

            initializationService.Register(this);
        }
    }

    public partial class ActiveState : IState
    {
        public void Enter()
        {
            _colorHandler.CurrentColor = _activeColor;
        }

        public void Exit()
        {
        }
    }

    public partial class ActiveState : IInitializable
    {
        public void Initialize()
        {
            PlayerStaticData playerData = _staticDataService.ForPlayer();

            _activeColor = playerData.ShieldActiveColor;
        }
    }
}