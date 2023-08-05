using System.Collections;
using Zenject;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.Infrastructure.Install;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Codebase.Services.Initialize;
using IInitializable = Codebase.Services.Initialize.IInitializable;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class AbsorptionState
    {
        private readonly ShieldStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticDataService _staticDataService;
        private readonly SpriteColorHandler _colorHandler;
        private readonly IShield _shield;

        private Color _shieldAbsorbColor;
        private float _absorbDuration;
        private Coroutine _switchToActiveStateCoroutine;
        private YieldInstruction _switchDelay;

        public AbsorptionState(
            ShieldStateMachine stateMachine,
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler,
            IStaticDataService staticDataService,
            ICoroutineRunner coroutineRunner,
            IInitializationService initializationService,
            IShield shield)
        {
            _stateMachine = stateMachine;
            _colorHandler = colorHandler;
            _coroutineRunner = coroutineRunner;
            _staticDataService = staticDataService;
            _shield = shield;

            initializationService.Register(this);
        }

        private IEnumerator SwitchingToActiveState()
        {
            yield return _switchDelay;

            _stateMachine.Enter<ActiveState>();
        }
    }

    public partial class AbsorptionState : IState
    {
        public void Enter()
        {
            _colorHandler.CurrentColor = _shieldAbsorbColor;

            _shield.EnableAbsorption();

            _switchToActiveStateCoroutine =
                _coroutineRunner.StartCoroutine(SwitchingToActiveState());
        }

        public void Exit()
        {
            _shield.DisableAbsorption();

            if (_switchToActiveStateCoroutine != null)
                _coroutineRunner.StopCoroutine(_switchToActiveStateCoroutine);
        }
    }

    public partial class AbsorptionState : IInitializable
    {
        public void Initialize()
        {
            PlayerStaticData staticData = _staticDataService.ForPlayer();

            _shieldAbsorbColor = staticData.ShieldAbsorptionColor;
            _absorbDuration = staticData.ShieldAbsorptionTime;
            _switchDelay = new WaitForSeconds(_absorbDuration);
        }
    }
}