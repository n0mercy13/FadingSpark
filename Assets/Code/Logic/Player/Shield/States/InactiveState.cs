using UnityEngine;
using Codebase.Infrastructure.StateMachine;
using Codebase.StaticData;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class InactiveState
    {
        private readonly IShield _shield;
        private readonly IPlayerWeaponActivatable _weapons;
        private readonly SpriteColorHandler _colorHandler;

        private Color _inactiveColor;

        public InactiveState(
            IShield shield,
            IStaticDataService staticDataService,
            SpriteColorHandler colorHandler,
            IPlayerWeaponActivatable weaponHandler)
        {
            _shield = shield;
            _colorHandler = colorHandler;
            _weapons = weaponHandler;

            PlayerStaticData playerData = staticDataService.ForPlayer();

            _inactiveColor = playerData.ShieldInactiveColor;
        }
    }

    public partial class InactiveState : IState
    {
        public void Enter()
        {
            _colorHandler.CurrentColor = _inactiveColor;
            _shield.Disable();
            _weapons.Activate();
        }

        public void Exit()
        {
            _weapons.Deactivate();
            _shield.Enable();
        }
    }
}