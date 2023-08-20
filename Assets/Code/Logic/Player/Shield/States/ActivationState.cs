using System.Collections;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.Infrastructure.StateMachine;
using Codebase.StaticData;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class ActivationState
    {
        private readonly ShieldStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SpriteColorHandler _colorHandler;

        private Coroutine _switchToAbsorbStateCoroutine;
        private Coroutine _changeColorCoroutine;
        private YieldInstruction _activationDelay;
        private float _shieldActivationTime;
        private Color _final;
        private Color _initial;

        public ActivationState(
            ShieldStateMachine stateMachine,
            SpriteColorHandler colorHandler,
            ICoroutineRunner coroutineRunner,
            IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            _colorHandler = colorHandler;

            PlayerStaticData playerData = staticDataService.ForPlayer();

            _final = playerData.ShieldActiveColor;
            _shieldActivationTime = playerData.ShieldActivationTime;
            _activationDelay = new WaitForSeconds(_shieldActivationTime);
        }

        private IEnumerator SwitchingToAbsorbState()
        {           
            yield return _activationDelay;

            _stateMachine.Enter<AbsorptionState>();
        }
    }

    public partial class ActivationState : IState
    {
        public void Enter()
        {
            _initial = _colorHandler.CurrentColor;

            _changeColorCoroutine = _coroutineRunner.StartCoroutine(
                _colorHandler.ChangeColorOverTime(_initial, _final, _shieldActivationTime));

            _switchToAbsorbStateCoroutine =
                _coroutineRunner.StartCoroutine(SwitchingToAbsorbState());
        }

        public void Exit()
        {
            if (_changeColorCoroutine != null)
                _coroutineRunner.StopCoroutine(_changeColorCoroutine);

            if (_switchToAbsorbStateCoroutine != null)
                _coroutineRunner.StopCoroutine(_switchToAbsorbStateCoroutine);
        }
    }
}