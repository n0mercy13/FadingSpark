using System;
using UnityEngine;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemyMover : MonoBehaviour
    {
        private float _speed;

        public void Initialize(float speed)
        {
            _speed = speed;
        }
    }
}
