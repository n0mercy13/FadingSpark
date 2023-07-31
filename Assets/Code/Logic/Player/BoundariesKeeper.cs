using UnityEngine;
using Zenject;

namespace Codebase.Logic.PlayerComponents
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BoundariesKeeper : MonoBehaviour
    {
        private const float BottomContainerTopBoundary = 0.15f;
        private const float TopContainerBottomBoundary = 0.9f;

        private Player _player;
        private Camera _camera;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _position;
        private Vector2 _minBounds;
        private Vector2 _maxBounds;
        private float _spriteWidth;
        private float _spriteHeight;

        [Inject]
        private void Construct(Player player) =>
            _player = player;

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

        private Vector3 _playerPosition
        {
            get => _player.transform.position;
            set => _player.transform.position = value;
        }

        private void KeepInBounds()
        {
            _position.x = Mathf.Clamp(
                _playerPosition.x, 
                _minBounds.x + _spriteWidth, 
                _maxBounds.x - _spriteWidth);
            _position.y = Mathf.Clamp(
                _playerPosition.y, 
                _minBounds.y + _spriteHeight, 
                _maxBounds.y - _spriteHeight);

            _playerPosition = _position;
        }
    }    
}