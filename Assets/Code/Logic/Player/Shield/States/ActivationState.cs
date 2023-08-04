using UnityEngine;
using System.Collections;
using Codebase.Infrastructure;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Zenject;
using Codebase.Infrastructure.Install;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public class ActivationState : IState
    {
        private readonly ShieldStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SpriteColorHandler _colorHandler;
        private readonly Color _final;
        private readonly float _shieldActivationTime;

        private Color _initial;
        private Coroutine _switchToAbsorbStateCoroutine;
        private Coroutine _changeColorCoroutine;
        private YieldInstruction _activationDelay;
        private float _time;

        public ActivationState(
            ShieldStateMachine stateMachine,
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler,
            ICoroutineRunner coroutineRunner,
            IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            _colorHandler = colorHandler;

            PlayerStaticData playerData = staticDataService.ForPlayer();
            _shieldActivationTime = playerData.ShieldActivationTime;
            _activationDelay = new WaitForSeconds(_shieldActivationTime);
            _final = playerData.ShieldActiveColor;
        }

        public void Enter()
        {
            _initial = _colorHandler.CurrentColor;

            _changeColorCoroutine = _coroutineRunner.StartCoroutine(
                _colorHandler.ChangeColorOverTime(_initial, _final, _shieldActivationTime));

            _switchToAbsorbStateCoroutine = 
                _coroutineRunner.StartCoroutine(SwitchToAbsorbState());
        }

        public void Exit()
        {
            if(_changeColorCoroutine != null)
                _coroutineRunner.StopCoroutine(_changeColorCoroutine);

            if(_switchToAbsorbStateCoroutine != null)
                _coroutineRunner.StopCoroutine(_switchToAbsorbStateCoroutine);
        }

        private IEnumerator SwitchToAbsorbState()
        {           
            yield return _activationDelay;

            _stateMachine.Enter<AbsorptionState>();
        }
    }
}