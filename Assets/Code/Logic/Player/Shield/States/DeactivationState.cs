using System.Collections;
using Zenject;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.Infrastructure.Install;
using Codebase.Infrastructure.StateMachine;
using Codebase.Services.StaticData;
using Codebase.Services.Initialize;
using Codebase.StaticData;
using IInitializable = Codebase.Services.Initialize.IInitializable;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class DeactivationState
    {
        private readonly ShieldStateMachine _stateMachine;
        private readonly SpriteColorHandler _colorHandler;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticDataService _staticDataService;

        private Coroutine _switchToDeactivatedStateDelay;
        private Coroutine _colorChangeCoroutine;
        private YieldInstruction _deactivationDelay;
        private float _deactivationDuration;
        private Color _final;
        private Color _initial;

        public DeactivationState(
            ShieldStateMachine stateMachine,
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler,
            ICoroutineRunner coroutineRunner,
            IStaticDataService staticDataService,
            IInitializationService initializationService)
        {
            _stateMachine = stateMachine;
            _colorHandler = colorHandler;
            _coroutineRunner = coroutineRunner;
            _staticDataService = staticDataService;

            initializationService.Register(this);
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

    public partial class DeactivationState : IInitializable
    {
        public void Initialize()
        {
            PlayerStaticData playerData = _staticDataService.ForPlayer();

            _final = playerData.ShieldInactiveColor;
            _deactivationDuration = playerData.ShieldDeactivationTime;
            _deactivationDelay = new WaitForSeconds(_deactivationDuration);
        }
    }
}