using Zenject;
using UnityEngine;
using Codebase.Services.StaticData;
using Codebase.Infrastructure.Install;
using Codebase.StaticData;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public class Shield : IShield
    {
        private readonly IEnergy _shipEnergy;
        private readonly SpriteColorHandler _colorHandler;
        private readonly int _activatedShieldContactDamage;
        private readonly float _damageReductionCoefficient;
        private readonly float _deactivatedShieldRadius;
        private readonly float _activatedShieldRadius;

        private CircleCollider2D _collider;
        private bool _canAbsorb;
        private bool _isActivated;

        public Shield(
            IEnergy energy,
            IStaticDataService staticDataService,
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler)
        {
            _colorHandler = colorHandler;
            _shipEnergy = energy;

            PlayerStaticData playerData = staticDataService.ForPlayer();
            _damageReductionCoefficient = playerData.ShieldDamageReductionCoefficient;
            _deactivatedShieldRadius = playerData.DeactivatedShieldRadius;
            _activatedShieldRadius = playerData.ActivatedShieldRadius;
            _activatedShieldContactDamage = playerData.ActivatedShieldContactDamage;
        }

        public void Initialize(CircleCollider2D collider, Material material)
        {
            _collider = collider;
            _colorHandler.Initialize(material);
        }

        public void Enable()
        {
            _isActivated = true;
            _collider.radius = _activatedShieldRadius;
        }

        public void Disable()
        {
            _isActivated = false;
            _collider.radius = _deactivatedShieldRadius;
        }

        public void EnableAbsorption() => 
            _canAbsorb = true;

        public void DisableAbsorption() =>
            _canAbsorb = false;

        public void OnHit(int value)
        {
            if (_canAbsorb)
                _shipEnergy.Absorb(value);
            else if (_isActivated)
                _shipEnergy.Reduce(
                    (int)(value * _damageReductionCoefficient));
            else
                _shipEnergy.Reduce(value);                
        }

        public void OnCollision(Collider2D with)
        {
            if (_isActivated
                && with.TryGetComponent<IDamageable>(out IDamageable damageable))
                    damageable.ApplyDamage(_activatedShieldContactDamage);
        }
    }
}