using System;
using UnityEngine;
using Codebase.Services.Tick;

namespace Codebase.Infrastructure
{
    public partial class Runner : MonoBehaviour, ICoroutineRunner
    {
        private void Awake() => 
            DontDestroyOnLoad(this);
    }

    public partial class Runner : IUpdateProvider
    {
        public event Action Updated = delegate { };

        private void Update() => 
            Updated.Invoke();
    }
}