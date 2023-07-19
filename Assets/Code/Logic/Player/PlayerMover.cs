﻿using System;
using Zenject;
using UnityEngine;
using Codebase.Services.Input;
using Codebase.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        private CharacterController _characterController;
        private IInputService _inputService;
        private float _movementSpeed;

        [Inject]
        public void Construct(IInputService inputService) => 
            _inputService = inputService;

        public void Initialize(float movementSpeed) => 
            _movementSpeed = movementSpeed;

        private void Awake() => 
            _characterController = GetComponent<CharacterController>();

        private void Update() => 
            MovePlayer();

        private void MovePlayer()
        {
            if(_inputService.Axis.sqrMagnitude >= Constants.Game.Epsilon)
            {
                _characterController.Move(
                    _movementSpeed * Time.deltaTime * _inputService.Axis);
            }
        }
    }
}