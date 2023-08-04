using System;
using Zenject;
using UnityEngine;
using Codebase.Infrastructure.Install;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public class ActiveState : IState
    {
        private readonly SpriteColorHandler _colorHandler;
        private readonly IShield _shield;
        private readonly Color _activeColor;

        public ActiveState(
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler, 
            IShield shield, 
            IStaticDataService staticDataService)
        {
            _shield = shield;
            _colorHandler = colorHandler;

            PlayerStaticData playerData = staticDataService.ForPlayer();
            _activeColor = playerData.ShieldActiveColor;
        }

        public void Enter()
        {
            _colorHandler.CurrentColor = _activeColor;
        }

        public void Exit()
        {
        }
    }
}