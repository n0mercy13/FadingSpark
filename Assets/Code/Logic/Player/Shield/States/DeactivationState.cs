using Codebase.Infrastructure;
using Codebase.Infrastructure.Install;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public class DeactivationState : IState
    {
        private readonly ShieldStateMachine _stateMachine;
        private readonly SpriteColorHandler _colorHandler;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly Color _final;
        private readonly float _deactivationDuration;
        private readonly YieldInstruction _deactivationDelay;

        private Color _initial;
        private Coroutine _switchToDeactivatedStateDelay;
        private Coroutine _colorChangeCoroutine;

        public DeactivationState(
            ShieldStateMachine stateMachine,
            [Inject(Id = InjectionIDs.Shield)]
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

        public void Enter()
        {
            _initial = _colorHandler.CurrentColor;

            _switchToDeactivatedStateDelay = _coroutineRunner
                .StartCoroutine(SwitchToDeactivationState());
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

        private IEnumerator SwitchToDeactivationState()
        {
            yield return _deactivationDelay;

            _stateMachine.Enter<InactiveState>();
        }
    }}