using System.Collections;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.Infrastructure.StateMachine;
using Codebase.StaticData;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class DeactivationState
    {
        private readonly ShieldStateMachine _stateMachine;
        private readonly SpriteColorHandler _colorHandler;
        private readonly ICoroutineRunner _coroutineRunner;

        private Coroutine _switchToDeactivatedStateDelay;
        private Coroutine _colorChangeCoroutine;
        private YieldInstruction _deactivationDelay;
        private float _deactivationDuration;
        private Color _final;
        private Color _initial;

        public DeactivationState(
            ShieldStateMachine stateMachine,
            SpriteColorHandler colorHandler,
            ICoroutineRunner coroutineRunner,
            IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _colorHandler = colorHandler;
            _coroutineRunner = coroutineRunner;

            PlayerStaticData playerData = staticDataService.ForPlayer();

            _final = playerData.ShieldInactiveColor;
            _deactivationDuration = playerData.ShieldDeactivationTime;
            _deactivationDelay = new WaitForSeconds(_deactivationDuration);
        }

        private IEnumerator SwitchingToDeactivationState()
        {
            yield return _deactivationDelay;

            _stateMachine.Enter<InactiveState>();
        }
    }

    public partial class DeactivationState : IState
    {
        public void Enter()
        {
            _initial = _colorHandler.CurrentColor;

            _switchToDeactivatedStateDelay = _coroutineRunner
                .StartCoroutine(SwitchingToDeactivationState());
            _colorChangeCoroutine = _coroutineRunner
                .StartCoroutine(_colorHandler
                .ChangeColorOverTime(_initial, _final, _deactivationDuration));
        }

        public void Exit()
        {
            if (_switchToDeactivatedStateDelay != null)
                _coroutineRunner.StopCoroutine(_switchToDeactivatedStateDelay);

            if (_colorChangeCoroutine != null)
                _coroutineRunner.StopCoroutine(_colorChangeCoroutine);
        }
    }
}