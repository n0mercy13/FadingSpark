using UnityEngine;
using Codebase.Infrastructure.StateMachine;
using Codebase.StaticData;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class ActiveState
    {
        private readonly SpriteColorHandler _colorHandler;
        private Color _activeColor;

        public ActiveState(
            SpriteColorHandler colorHandler, 
            IStaticDataService staticDataService)
        {
            _colorHandler = colorHandler;

            PlayerStaticData playerData = staticDataService.ForPlayer();

            _activeColor = playerData.ShieldActiveColor;
        }
    }

    public partial class ActiveState : IState
    {
        public void Enter() => 
            _colorHandler.CurrentColor = _activeColor;

        public void Exit()
        {
        }
    }
}