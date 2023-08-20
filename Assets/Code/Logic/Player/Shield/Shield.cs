using UnityEngine;
using Codebase.Logic.VisualEffects;
using Codebase.Services.Pool;
using Codebase.StaticData;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class Shield
    {
        private readonly IEnergy _shipEnergy;
        private readonly IVFXPool _vfxPool;
        private readonly SpriteColorHandler _colorHandler;
        private readonly int _activatedShieldContactDamage;
        private readonly float _damageReductionCoefficient;
        private readonly float _deactivatedShieldRadius;
        private readonly float _activatedShieldRadius;

        private CircleCollider2D _collider;
        private Vector3 _vfxSpawnPosition;
        private bool _canAbsorb;
        private bool _isActivated;

        public Shield(
            IEnergy energy,
            IStaticDataService staticDataService,
            SpriteColorHandler colorHandler,
            IVFXPool vfxPool)
        {
            _colorHandler = colorHandler;
            _shipEnergy = energy;
            _vfxPool = vfxPool;

            PlayerStaticData playerData = staticDataService.ForPlayer();

            _damageReductionCoefficient = playerData.ShieldDamageReductionCoefficient;
            _deactivatedShieldRadius = playerData.DeactivatedShieldRadius;
            _activatedShieldRadius = playerData.ActivatedShieldRadius;
            _activatedShieldContactDamage = playerData.ActivatedShieldContactDamage;
        }

        public void SetComponents(CircleCollider2D collider, Material material)
        {
            _collider = collider;
            _colorHandler.Initialize(material);
        }

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

        public void OnCollision(Collider2D collider)
        {
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                _vfxSpawnPosition = _collider
                    .ClosestPoint(collider.transform.position);

                ApplyDamageOnContact(damageable);
            }
        }

        private void ApplyDamageOnContact(IDamageable damageable)
        {
            if (_isActivated)
            {
                _vfxPool.Spawn<VFX_OnPlayerActiveShieldHit>(_vfxSpawnPosition);

                damageable.ApplyDamage(_activatedShieldContactDamage);
            }
            else
            {
                _vfxPool.Spawn<VFX_OnPlayerInactiveShieldHit>(_vfxSpawnPosition);
            }
        }
    }

    public partial class Shield : IShield
    {
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
    }
}