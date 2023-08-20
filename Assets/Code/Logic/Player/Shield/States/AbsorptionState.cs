using System.Collections;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.Infrastructure.StateMachine;
using Codebase.StaticData;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class AbsorptionState
    {
        private readonly ShieldStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SpriteColorHandler _colorHandler;
        private readonly IShield _shield;

        private Color _shieldAbsorbColor;
        private float _absorbDuration;
        private Coroutine _switchToActiveStateCoroutine;
        private YieldInstruction _switchDelay;

        public AbsorptionState(
            ShieldStateMachine stateMachine,
            SpriteColorHandler colorHandler,
            IStaticDataService staticDataService,
            ICoroutineRunner coroutineRunner,
            IShield shield)
        {
            _stateMachine = stateMachine;
            _colorHandler = colorHandler;
            _coroutineRunner = coroutineRunner;
            _shield = shield;

            PlayerStaticData playerData = staticDataService.ForPlayer();

            _shieldAbsorbColor = playerData.ShieldAbsorptionColor;
            _absorbDuration = playerData.ShieldAbsorptionTime;
            _switchDelay = new WaitForSeconds(_absorbDuration);
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
}