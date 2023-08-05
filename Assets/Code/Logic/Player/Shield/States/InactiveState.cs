using Zenject;
using UnityEngine;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Codebase.Infrastructure.Install;
using Codebase.Services.Initialize;
using IInitializable = Codebase.Services.Initialize.IInitializable;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class InactiveState
    {
        private readonly IShield _shield;
        private readonly IPlayerWeaponActivatable _weapons;
        private readonly IStaticDataService _staticDataService;
        private readonly SpriteColorHandler _colorHandler;

        private Color _inactiveColor;

        public InactiveState(
            IShield shield,
            IStaticDataService staticDataService,
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler,
            IPlayerWeaponActivatable weaponHandler,
            IInitializationService initializationService)
        {
            _shield = shield;
            _colorHandler = colorHandler;
            _weapons = weaponHandler;
            _staticDataService = staticDataService;

            initializationService.Register(this);
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

    public partial class InactiveState : IInitializable
    {
        public void Initialize()
        {
            PlayerStaticData playerData = _staticDataService.ForPlayer();

            _inactiveColor = playerData.ShieldInactiveColor;
        }
    }
}