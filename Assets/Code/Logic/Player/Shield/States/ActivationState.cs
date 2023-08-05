using System.Collections;
using Zenject;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.Infrastructure.StateMachine;
using Codebase.Infrastructure.Install;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Codebase.Services.Initialize;
using IInitializable = Codebase.Services.Initialize.IInitializable;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class ActivationState
    {
        private readonly ShieldStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticDataService _staticDataService;
        private readonly SpriteColorHandler _colorHandler;

        private Coroutine _switchToAbsorbStateCoroutine;
        private Coroutine _changeColorCoroutine;
        private YieldInstruction _activationDelay;
        private float _shieldActivationTime;
        private Color _final;
        private Color _initial;

        public ActivationState(
            ShieldStateMachine stateMachine,
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler,
            ICoroutineRunner coroutineRunner,
            IStaticDataService staticDataService,
            IInitializationService initializationService)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            _staticDataService = staticDataService;
            _colorHandler = colorHandler;

            initializationService.Register(this);
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

    public partial class ActivationState : IInitializable
    {
        public void Initialize()
        {
            PlayerStaticData playerData = _staticDataService.ForPlayer();

            _shieldActivationTime = playerData.ShieldActivationTime;
            _activationDelay = new WaitForSeconds(_shieldActivationTime);
            _final = playerData.ShieldActiveColor;
        }
    }
}