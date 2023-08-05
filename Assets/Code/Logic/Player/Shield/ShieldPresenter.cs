using Zenject;
using UnityEngine;

namespace Codebase.Logic.PlayerComponents.Shield
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class ShieldPresenter : MonoBehaviour, IDamageable
    {
        private CircleCollider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private Shield _shield;

        [Inject]
        private void Construct(Shield shield) =>
            _shield = shield;

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            Initialize();
        }

        private void OnTriggerEnter2D(Collider2D collision) => 
            _shield.OnCollision(with: collision);

        public void ApplyDamage(int value) => 
            _shield.OnHit(value);

        private void Initialize()
        {
            Material material = _spriteRenderer.material;
            _shield.SetComponents(_collider, material);
        }
    }
}