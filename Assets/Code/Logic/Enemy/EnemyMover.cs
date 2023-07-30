﻿using UnityEngine;
using Codebase.Services.Tick;
using System;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemyMover : IDisposable
    {
        private readonly ITickProviderService _tickProvider;
        private readonly Enemy _enemy;
        private readonly float _speed;

        private Vector3 _direction;
        private Vector3 _destination;

        public EnemyMover(
            Enemy enemy,
            ITickProviderService tickProvider,
            float speed)
        {
            _tickProvider = tickProvider;
            _enemy = enemy;
            _speed = speed;

            _tickProvider.Ticked += OnTick;

            StopMoving();
        }

        private float _deltaDistance => _speed * _tickProvider.DeltaTime;
        private Vector3 _currentPosition
        {
            get => _enemy.transform.position;
            set => _enemy.transform.position = value;            
        }

        public void StartToMoveInDirection(Vector3 direction) => 
            _direction = direction.normalized;

        public void StartToMoveToDestination(Vector3 destination) => 
            _direction = destination;

        public void StopMoving()
        {
            _destination = Vector3.zero;
            _direction = Vector3.positiveInfinity;
        }

        public void Dispose()
        {
            StopMoving();

            _tickProvider.Ticked -= OnTick;
        }

        private void OnTick(int _)
        {           
            if (_direction != Vector3.zero)
                MoveInDirection();
            else if (_destination != Vector3.positiveInfinity)
                MoveToDestination();
        }

        private void MoveToDestination() => 
            _currentPosition = Vector3.MoveTowards(
                _currentPosition, _destination, _deltaDistance);

        private void MoveInDirection() => 
            _currentPosition += _deltaDistance * _direction;
    }
}
