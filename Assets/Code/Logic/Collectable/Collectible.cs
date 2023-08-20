using System;
using Zenject;
using UnityEngine;
using Codebase.Services.Tick;

namespace Codebase.Logic.Collectables
{
    public partial class Collectible : MonoBehaviour
    {
        private ITickProviderService _tickProvider;
        private Transform _target;
        private float _speed;
        private int _value;
        private bool _isMovingTowardsTarget;

        [Inject]
        private void Construct(ITickProviderService tickProvider)
        {
            _tickProvider = tickProvider;

            _tickProvider.Ticked += OnTick;
        }

        public void Initialize(int value, Vector3 position)
        {
            _value = value;
            transform.position = position;

            gameObject.SetActive(true);
        }

        private void OnTick(int _) => 
            MoveTowardsTarget();

        private void MoveTowardsTarget()
        {
            if(_isMovingTowardsTarget.Equals(false)) 
                return;

            transform.position = Vector3.MoveTowards(
                transform.position, _target.position, _speed * _tickProvider.DeltaTime);
        }
    }

    public partial class Collectible : ICollectible
    {
        public int Collect()
        {
            _isMovingTowardsTarget = false;
            gameObject.SetActive(false);

            return _value;
        }

        public void MoveTowards(Transform target, float speed)
        {
            _target = target;
            _speed = speed;

            _isMovingTowardsTarget = true;
        }
    }

    public partial class Collectible : IDisposable
    {
        public void Dispose() => 
            _tickProvider.Ticked -= OnTick;
    }
}