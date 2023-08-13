using Zenject;
using UnityEngine;
using Codebase.Infrastructure.Install;
using Codebase.Logic.VFX;
using Codebase.Services.StaticData;
using Codebase.Services.Initialize;
using Codebase.Services.Pool;
using Codebase.StaticData;
using IInitializable = Codebase.Services.Initialize.IInitializable;

namespace Codebase.Logic.PlayerComponents.Shield
{
    public partial class Shield
    {
        private readonly IEnergy _shipEnergy;
        private readonly IVFXPool _vfxPool;
        private readonly IStaticDataService _staticDataService;
        private readonly SpriteColorHandler _colorHandler;

        private CircleCollider2D _collider;
        private Vector3 _vfxSpawnPosition;
        private int _activatedShieldContactDamage;
        private float _damageReductionCoefficient;
        private float _deactivatedShieldRadius;
        private float _activatedShieldRadius;
        private bool _canAbsorb;
        private bool _isActivated;

        public Shield(
            IEnergy energy,
            IStaticDataService staticDataService,
            IInitializationService initiationService,
            [Inject(Id = InjectionIDs.Shield)]
            SpriteColorHandler colorHandler,
            IVFXPool vfxPool)
        {
            _staticDataService = staticDataService;
            _colorHandler = colorHandler;
            _shipEnergy = energy;
            _vfxPool = vfxPool;

            initiationService.Register(this);
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

    public partial class Shield : IInitializable
    {
        public void Initialize()
        {
            PlayerStaticData playerData = _staticDataService.ForPlayer();

            _damageReductionCoefficient = playerData.ShieldDamageReductionCoefficient;
            _deactivatedShieldRadius = playerData.DeactivatedShieldRadius;
            _activatedShieldRadius = playerData.ActivatedShieldRadius;
            _activatedShieldContactDamage = playerData.ActivatedShieldContactDamage;
        }
    }
}