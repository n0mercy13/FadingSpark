using System;
using System.Collections;
using Zenject;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.Infrastructure.Install;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.StaticData;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public class AbsorptionState : IState
    {
        private readonly ShieldStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SpriteColorHandler _colorHandler;
        private readonly IShield _shield;
        private readonly Color _shieldAbsorbColor;
        private readonly float _absorbDuration;

        private Coroutine _switchToActiveStateCoroutine;
        private YieldInstruction _switchDelay;

        public AbsorptionState(
            ShieldStateMachine stateMachine,
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler,
            IStaticDataService staticDataService,
            IShield shield)
        {
            _stateMachine = stateMachine;
            _shield = shield;
            _colorHandler = colorHandler;

            PlayerStaticData staticData = staticDataService.ForPlayer();
            _shieldAbsorbColor = staticData.ShieldAbsorptionColor;
            _absorbDuration = staticData.ShieldAbsorptionTime;

            _switchDelay = new WaitForSeconds(_absorbDuration);
        }

        public void Enter()
        {
            _shield.EnableAbsorption();

            _switchToActiveStateCoroutine = 
                _coroutineRunner.StartCoroutine(SwitchToActiveState());
        }

        public void Exit()
        {
            _shield.DisableAbsorption();

            if(_switchToActiveStateCoroutine != null )
                _coroutineRunner.StopCoroutine(_switchToActiveStateCoroutine);
        }

        private IEnumerator SwitchToActiveState()
        {
            yield return _switchDelay;

            _stateMachine.Enter<ActiveState>();
        }
    }
}