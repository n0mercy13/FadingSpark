using UnityEngine;

namespace Codebase.Logic.PlayerComponents
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BoundariesKeeper : MonoBehaviour
    {
        private const float BottomContainerTopBoundary = 0.15f;
        private const float TopContainerBottomBoundary = 0.9f;

        private Camera _camera;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _position;
        private Vector2 _minBounds;
        private Vector2 _maxBounds;
        private float _spriteWidth;
        private float _spriteHeight;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _camera = Camera.main;
        }

        private void Start()
        {
            _minBounds = _camera.ViewportToWorldPoint(new Vector2(0f, BottomContainerTopBoundary));
            _maxBounds = _camera.ViewportToWorldPoint(new Vector2(1f, TopContainerBottomBoundary));
            _spriteWidth = _spriteRenderer.bounds.size.x / 2;
            _spriteHeight = _spriteRenderer.bounds.size.y / 2;
        }

        private void LateUpdate() => 
            KeepInBounds();

        private void KeepInBounds()
        {
            _position.x = Mathf.Clamp(
                transform.position.x, 
                _minBounds.x + _spriteWidth, 
                _maxBounds.x - _spriteWidth);
            _position.y = Mathf.Clamp(
                transform.position.y, 
                _minBounds.y + _spriteHeight, 
                _maxBounds.y - _spriteHeight);

            transform.position = _position;
        }
    }    
}