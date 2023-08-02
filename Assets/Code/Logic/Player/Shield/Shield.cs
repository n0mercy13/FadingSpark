using Zenject;
using UnityEngine;
using Codebase.Services.StaticData;
using Codebase.Infrastructure.Install;

namespace Codebase.Logic.PlayerComponents.Shield
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Shield : MonoBehaviour, IShield, IDamageable
    {
        [SerializeField, Range(0.1f, 0.5f)] private float _deactivatedShieldRadius;
        [SerializeField, Range(0.4f, 2.0f)] private float _activatedShieldRadius;

        private IEnergy _shipEnergy;
        private SpriteColorHandler _colorHandler;
        private CircleCollider2D _collider;
        private bool _canAbsorb;
        private bool _isActivated;
        private float _damageReductionCoefficient;

        [Inject]
        private void Construct(
            IEnergy energy,
            IStaticDataService staticDataService,
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler)
        {
            _colorHandler = colorHandler;
            _shipEnergy = energy;
            _damageReductionCoefficient = staticDataService
                .ForPlayer()
                .ShieldDamageReductionCoefficient;
        }

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            Material material = GetComponent<SpriteRenderer>().material;
            _colorHandler.Initialize(material);
        }

        public void Enable()
        {
            _isActivated = true;
            _collider.radius = _activatedShieldRadius;
        }

        public void Disable()
        {
            _collider.enabled = false;
            _collider.radius = _deactivatedShieldRadius;
        }

        public void EnableAbsorption() => 
            _canAbsorb = true;

        public void DisableAbsorption() =>
            _canAbsorb = false;

        public void ApplyDamage(int value)
        {
            if (_canAbsorb)
                _shipEnergy.Absorb(value);
            else if (_isActivated)
                _shipEnergy.Reduce(
                    (int)(value * _damageReductionCoefficient));
            else
                _shipEnergy.Reduce(value);                
        }
    }
}