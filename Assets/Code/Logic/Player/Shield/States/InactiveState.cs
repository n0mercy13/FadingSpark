using System;
using Zenject;
using UnityEngine;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Codebase.Infrastructure.Install;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public class InactiveState : IState
    {
        private readonly IShield _shield;
        private readonly SpriteColorHandler _colorHandler;
        private readonly Color _inactiveColor;

        public InactiveState(
            IShield shield,
            IStaticDataService staticDataService,
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler)
        {
            _shield = shield;
            _colorHandler = colorHandler;

            PlayerStaticData playerData = staticDataService.ForPlayer();
            _inactiveColor = playerData.ShieldInactiveColor;
        }

        public void Enter()
        {
            _colorHandler.CurrentColor = _inactiveColor;
            _shield.Disable();
        }

        public void Exit()
        {
        }
    }
}